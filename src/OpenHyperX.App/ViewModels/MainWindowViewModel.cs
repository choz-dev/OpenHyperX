using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OpenHyperX.Core;
using OpenHyperX.Devices.CloudAlphaWireless;

namespace OpenHyperX.App.ViewModels;

public sealed class MainWindowViewModel : ObservableObject, IDisposable
{
    private readonly IHidDeviceEnumerator _deviceEnumerator;
    private CloudAlphaWirelessClient? _client;
    private bool _applyingDeviceState;
    private bool _disposed;
    private bool _isBusy;
    private bool _isConnected;
    private bool _isDarkMode;
    private bool _micMuted;
    private bool _sidetoneEnabled;
    private bool _voicePromptEnabled;
    private string _activeDeviceText = "Cloud Alpha Wireless";
    private string _statusMessage = "Ready";
    private string _batteryText = "--";
    private string _chargingText = "--";
    private string _pairIdText = "--";
    private string _autoShutdownText = "--";
    private DeviceListItemViewModel? _selectedDevice;
    private AutoShutdownOption? _selectedAutoShutdownOption;

    public MainWindowViewModel(IHidDeviceEnumerator deviceEnumerator)
    {
        _deviceEnumerator = deviceEnumerator;

        RefreshDevicesCommand = new AsyncRelayCommand(RefreshDevicesAsync, CanRun);
        ConnectCommand = new AsyncRelayCommand(ConnectAsync, CanConnect);
        RefreshStatusCommand = new AsyncRelayCommand(RefreshStatusAsync, CanRefreshStatus);

        AutoShutdownOptions =
        [
            new AutoShutdownOption(0, "Never"),
            new AutoShutdownOption(5, "5 minutes"),
            new AutoShutdownOption(10, "10 minutes"),
            new AutoShutdownOption(20, "20 minutes"),
            new AutoShutdownOption(30, "30 minutes"),
            new AutoShutdownOption(60, "60 minutes")
        ];
    }

    public ObservableCollection<DeviceListItemViewModel> Devices { get; } = [];

    public ObservableCollection<AutoShutdownOption> AutoShutdownOptions { get; }

    public AsyncRelayCommand RefreshDevicesCommand { get; }

    public AsyncRelayCommand ConnectCommand { get; }

    public AsyncRelayCommand RefreshStatusCommand { get; }

    public string ConnectionText => IsConnected ? "Connected" : "Not connected";

    public string ProtocolSummary =>
        "Requests use report ID 0x00 followed by 0x21 0xBB and the command byte. Status reads poll the headset and ignore unrelated notification packets.";

    public bool IsDarkMode
    {
        get => _isDarkMode;
        set
        {
            if (SetProperty(ref _isDarkMode, value))
            {
                ApplyTheme(value);
            }
        }
    }

    public bool IsBusy
    {
        get => _isBusy;
        private set
        {
            if (SetProperty(ref _isBusy, value))
            {
                RefreshDevicesCommand.NotifyCanExecuteChanged();
                ConnectCommand.NotifyCanExecuteChanged();
                RefreshStatusCommand.NotifyCanExecuteChanged();
            }
        }
    }

    public bool IsConnected
    {
        get => _isConnected;
        private set
        {
            if (SetProperty(ref _isConnected, value))
            {
                OnPropertyChanged(nameof(ConnectionText));
                ConnectCommand.NotifyCanExecuteChanged();
                RefreshStatusCommand.NotifyCanExecuteChanged();
            }
        }
    }

    public string ActiveDeviceText
    {
        get => _activeDeviceText;
        private set => SetProperty(ref _activeDeviceText, value);
    }

    public string StatusMessage
    {
        get => _statusMessage;
        private set => SetProperty(ref _statusMessage, value);
    }

    public string BatteryText
    {
        get => _batteryText;
        private set => SetProperty(ref _batteryText, value);
    }

    public string ChargingText
    {
        get => _chargingText;
        private set => SetProperty(ref _chargingText, value);
    }

    public string PairIdText
    {
        get => _pairIdText;
        private set => SetProperty(ref _pairIdText, value);
    }

    public string AutoShutdownText
    {
        get => _autoShutdownText;
        private set => SetProperty(ref _autoShutdownText, value);
    }

    public DeviceListItemViewModel? SelectedDevice
    {
        get => _selectedDevice;
        set
        {
            if (SetProperty(ref _selectedDevice, value))
            {
                ConnectCommand.NotifyCanExecuteChanged();
            }
        }
    }

    public bool MicMuted
    {
        get => _micMuted;
        set
        {
            if (SetProperty(ref _micMuted, value))
            {
                _ = ApplyToggleAsync(() => _client?.SetMicMuteAsync(value) ?? Task.CompletedTask, "Microphone updated");
            }
        }
    }

    public bool SidetoneEnabled
    {
        get => _sidetoneEnabled;
        set
        {
            if (SetProperty(ref _sidetoneEnabled, value))
            {
                _ = ApplyToggleAsync(() => _client?.SetSidetoneEnabledAsync(value) ?? Task.CompletedTask, "Sidetone updated");
            }
        }
    }

    public bool VoicePromptEnabled
    {
        get => _voicePromptEnabled;
        set
        {
            if (SetProperty(ref _voicePromptEnabled, value))
            {
                _ = ApplyToggleAsync(() => _client?.SetVoicePromptEnabledAsync(value) ?? Task.CompletedTask, "Voice prompts updated");
            }
        }
    }

    public AutoShutdownOption? SelectedAutoShutdownOption
    {
        get => _selectedAutoShutdownOption;
        set
        {
            if (SetProperty(ref _selectedAutoShutdownOption, value) && value is not null)
            {
                _ = ApplyToggleAsync(() => _client?.SetAutoShutdownAsync(value.Minutes) ?? Task.CompletedTask, "Auto shutdown updated");
            }
        }
    }

    public async Task RefreshDevicesAsync()
    {
        await RunAsync(
            async () =>
            {
                var devices = await Task.Run(
                    () => _deviceEnumerator
                        .ListDevices(CloudAlphaWirelessDeviceIds.Filter)
                        .Where(CloudAlphaWirelessDeviceIds.IsLikelyCommandInterface)
                        .ToArray());

                Devices.Clear();
                foreach (var device in devices)
                {
                    Devices.Add(new DeviceListItemViewModel(device));
                }

                SelectedDevice ??= Devices.FirstOrDefault();
                StatusMessage = Devices.Count == 0
                    ? "No Cloud Alpha Wireless HID device found."
                    : $"{Devices.Count} matching HID interface(s) found.";
            });
    }

    private async Task ConnectAsync()
    {
        if (SelectedDevice is null)
        {
            return;
        }

        await RunAsync(
            async () =>
            {
                await DisposeClientAsync();
                var transport = _deviceEnumerator.Open(SelectedDevice.DeviceInfo);
                _client = new CloudAlphaWirelessClient(transport);
                ActiveDeviceText = SelectedDevice.DisplayName;
                StatusMessage = "Connected to HID interface.";
                await RefreshStatusCoreAsync();
            });
    }

    private Task RefreshStatusAsync()
    {
        return RunAsync(RefreshStatusCoreAsync);
    }

    private async Task RefreshStatusCoreAsync()
    {
        if (_client is null)
        {
            StatusMessage = "Choose a device and connect first.";
            return;
        }

        var status = await _client.GetStatusAsync();
        ApplyStatus(status);
        StatusMessage = status.Connected ? "Status refreshed." : "Dongle is present; headset is not connected.";
    }

    private void ApplyStatus(CloudAlphaWirelessStatus status)
    {
        _applyingDeviceState = true;
        try
        {
            IsConnected = status.Connected;
            BatteryText = status.BatteryPercent is null ? "--" : $"{status.BatteryPercent}%";
            ChargingText = status.ChargingState switch
            {
                ChargingState.NotCharging => "No",
                ChargingState.WiredCharging => "Wired",
                _ => "--"
            };
            PairIdText = status.PairId is null ? "--" : $"0x{status.PairId.Value:X8}";
            AutoShutdownText = status.AutoShutdownMinutes is null
                ? "--"
                : status.AutoShutdownMinutes.Value == 0
                    ? "Never"
                    : $"{status.AutoShutdownMinutes} min";

            if (status.MicMuted is not null)
            {
                MicMuted = status.MicMuted.Value;
            }

            if (status.SidetoneEnabled is not null)
            {
                SidetoneEnabled = status.SidetoneEnabled.Value;
            }

            if (status.VoicePromptEnabled is not null)
            {
                VoicePromptEnabled = status.VoicePromptEnabled.Value;
            }

            if (status.AutoShutdownMinutes is not null)
            {
                SelectedAutoShutdownOption = AutoShutdownOptions.FirstOrDefault(option => option.Minutes == status.AutoShutdownMinutes.Value);
            }
        }
        finally
        {
            _applyingDeviceState = false;
        }
    }

    private async Task ApplyToggleAsync(Func<Task> apply, string successMessage)
    {
        if (_applyingDeviceState || _client is null || !IsConnected)
        {
            return;
        }

        await RunAsync(
            async () =>
            {
                await apply();
                StatusMessage = successMessage;
            });
    }

    private async Task RunAsync(Func<Task> action)
    {
        if (IsBusy)
        {
            return;
        }

        try
        {
            IsBusy = true;
            await action();
        }
        catch (Exception ex)
        {
            StatusMessage = ex.Message;
            IsConnected = false;
        }
        finally
        {
            IsBusy = false;
        }
    }

    private bool CanRun()
    {
        return !IsBusy;
    }

    private bool CanConnect()
    {
        return !IsBusy && SelectedDevice is not null;
    }

    private bool CanRefreshStatus()
    {
        return !IsBusy && _client is not null;
    }

    private static void ApplyTheme(bool isDarkMode)
    {
        if (Application.Current is null)
        {
            return;
        }

        Application.Current.RequestedThemeVariant = isDarkMode
            ? ThemeVariant.Dark
            : ThemeVariant.Light;
    }

    private async Task DisposeClientAsync()
    {
        if (_client is null)
        {
            return;
        }

        await _client.DisposeAsync();
        _client = null;
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        DisposeClientAsync().GetAwaiter().GetResult();
        _disposed = true;
    }
}
