using CommunityToolkit.Mvvm.ComponentModel;
using Avalonia.Threading;
using OpenHyperX.App.Services;
using OpenHyperX.Core;
using OpenHyperX.Devices.CloudAlphaWireless;

namespace OpenHyperX.App.ViewModels;

public sealed class CloudAlphaWirelessDeviceViewModel : ObservableObject, IDisposable
{
    private static readonly TimeSpan AutoRefreshInterval = TimeSpan.FromSeconds(5);
    private readonly CloudAlphaWirelessClient _client;
    private readonly IReadOnlyList<AutoShutdownOption> _autoShutdownOptions;
    private readonly DeviceSettingsStore _settingsStore;
    private readonly SemaphoreSlim _deviceOperationLock = new(1, 1);
    private readonly DispatcherTimer _autoRefreshTimer;
    private bool _applyingDeviceState;
    private bool _disposed;
    private bool _hasRefreshedStatus;
    private bool _isBusy;
    private bool _isConnected;
    private bool _micMuted;
    private bool _microphoneMonitoringEnabled;
    private bool _voicePromptEnabled;
    private string? _settingsKey;
    private string _statusMessage = "Connected to HID interface.";
    private string _batteryText = "--";
    private string _chargingText = "--";
    private string _pairIdText = "--";
    private string _autoShutdownText = "--";
    private string _microphoneBoomText = "--";
    private string _productColorText = "--";
    private AutoShutdownOption? _selectedAutoShutdownOption;

    public CloudAlphaWirelessDeviceViewModel(
        HidDeviceInfo deviceInfo,
        CloudAlphaWirelessClient client,
        IReadOnlyList<AutoShutdownOption> autoShutdownOptions,
        DeviceSettingsStore settingsStore)
    {
        DeviceInfo = deviceInfo;
        _client = client;
        _autoShutdownOptions = autoShutdownOptions;
        _settingsStore = settingsStore;
        _autoRefreshTimer = new DispatcherTimer
        {
            Interval = AutoRefreshInterval
        };
        _autoRefreshTimer.Tick += AutoRefreshTimer_Tick;
    }

    public HidDeviceInfo DeviceInfo { get; }

    public string DevicePath => DeviceInfo.Path;

    public string DisplayName => DeviceInfo.DisplayName;

    public string DeviceType => "Cloud Alpha Wireless";

    public string DetailText =>
        $"{DeviceInfo.VendorIdHex}:{DeviceInfo.ProductIdHex}  In {DeviceInfo.InputReportLength} / Out {DeviceInfo.OutputReportLength}";

    public bool IsBusy
    {
        get => _isBusy;
        private set
        {
            if (SetProperty(ref _isBusy, value))
            {
                OnPropertyChanged(nameof(ControlsEnabled));
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
                OnPropertyChanged(nameof(ControlsEnabled));
            }
        }
    }

    public bool ControlsEnabled => IsConnected && !IsBusy;

    public string ConnectionText => IsConnected ? "Headset connected" : "Dongle connected";

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

    public string MicrophoneBoomText
    {
        get => _microphoneBoomText;
        private set => SetProperty(ref _microphoneBoomText, value);
    }

    public string ProductColorText
    {
        get => _productColorText;
        private set => SetProperty(ref _productColorText, value);
    }

    public bool MicMuted
    {
        get => _micMuted;
        set
        {
            if (SetProperty(ref _micMuted, value))
            {
                _ = ApplyDeviceChangeAsync(() => _client.SetMicMuteAsync(value), "Microphone updated");
            }
        }
    }

    public bool MicrophoneMonitoringEnabled
    {
        get => _microphoneMonitoringEnabled;
        set
        {
            if (SetProperty(ref _microphoneMonitoringEnabled, value))
            {
                _ = ApplyDeviceChangeAsync(
                    () => _client.SetMicrophoneMonitoringEnabledAsync(value),
                    "Microphone monitoring updated");
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
                _ = ApplyDeviceChangeAsync(() => _client.SetVoicePromptEnabledAsync(value), "Voice prompts updated");
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
                _ = ApplyDeviceChangeAsync(() => _client.SetAutoShutdownAsync(value.Minutes), "Auto shutdown updated");
            }
        }
    }

    public async Task InitializeAsync()
    {
        await RefreshStatusAsync();
        if (await ApplySavedSettingsAsync())
        {
            await RefreshStatusAsync();
        }

        StartAutoRefresh();
    }

    public Task RefreshStatusAsync()
    {
        return RefreshStatusAsync(updateStatusMessage: true, showBusy: true);
    }

    private Task RefreshStatusAsync(bool updateStatusMessage, bool showBusy)
    {
        return showBusy
            ? RunAsync(() => RefreshStatusCoreAsync(updateStatusMessage))
            : RefreshStatusQuietlyAsync(updateStatusMessage);
    }

    private async Task RefreshStatusCoreAsync(bool updateStatusMessage)
    {
        var wasConnected = IsConnected;
        var status = await GetStatusWithLockAsync().ConfigureAwait(true);
        await ApplyRefreshedStatusAsync(status, wasConnected, updateStatusMessage).ConfigureAwait(true);
    }

    private async Task RefreshStatusQuietlyAsync(bool updateStatusMessage)
    {
        if (IsBusy || _disposed)
        {
            return;
        }

        try
        {
            var wasConnected = IsConnected;
            var status = await TryGetStatusWithLockAsync().ConfigureAwait(true);
            if (status is null)
            {
                return;
            }

            await ApplyRefreshedStatusAsync(status, wasConnected, updateStatusMessage).ConfigureAwait(true);
        }
        catch (Exception ex)
        {
            StatusMessage = ex.Message;
            IsConnected = false;
        }
    }

    private async Task ApplyRefreshedStatusAsync(
        CloudAlphaWirelessStatus status,
        bool wasConnected,
        bool updateStatusMessage)
    {
        var reconnected = _hasRefreshedStatus && !wasConnected && status.Connected;
        var savedSettingsApplied = false;
        ApplyStatus(status);
        _hasRefreshedStatus = true;

        if (reconnected)
        {
            savedSettingsApplied = await ApplySavedSettingsCoreAsync().ConfigureAwait(true);
            if (savedSettingsApplied)
            {
                var refreshedStatus = await GetStatusWithLockAsync().ConfigureAwait(true);
                ApplyStatus(refreshedStatus);
            }
        }

        if (updateStatusMessage)
        {
            StatusMessage = status.Connected
                ? savedSettingsApplied
                    ? "Status refreshed. Saved settings applied."
                    : "Status refreshed. Auto-updating."
                : "Dongle is present; headset is not connected.";
        }
        else if (reconnected)
        {
            StatusMessage = savedSettingsApplied
                ? "Headset connected. Saved settings applied."
                : "Headset connected.";
        }
        else if (wasConnected != status.Connected)
        {
            StatusMessage = "Dongle is present; headset is not connected.";
        }
    }

    private async Task<CloudAlphaWirelessStatus> GetStatusWithLockAsync()
    {
        await _deviceOperationLock.WaitAsync().ConfigureAwait(true);

        try
        {
            return await _client.GetStatusAsync().ConfigureAwait(true);
        }
        finally
        {
            _deviceOperationLock.Release();
        }
    }

    private async Task<CloudAlphaWirelessStatus?> TryGetStatusWithLockAsync()
    {
        if (!await _deviceOperationLock.WaitAsync(0).ConfigureAwait(true))
        {
            return null;
        }

        try
        {
            return await _client.GetStatusAsync().ConfigureAwait(true);
        }
        finally
        {
            _deviceOperationLock.Release();
        }
    }

    private void StartAutoRefresh()
    {
        if (!_disposed && !_autoRefreshTimer.IsEnabled)
        {
            _autoRefreshTimer.Start();
        }
    }

    private void AutoRefreshTimer_Tick(object? sender, EventArgs e)
    {
        _ = RefreshStatusAsync(updateStatusMessage: false, showBusy: false);
    }

    private void ApplyStatus(CloudAlphaWirelessStatus status)
    {
        _applyingDeviceState = true;
        try
        {
            IsConnected = status.Connected;
            _settingsKey = DeviceSettingsStore.CreateCloudAlphaWirelessKey(status.PairId, DeviceInfo);
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
            MicrophoneBoomText = status.MicrophoneBoomAttached is null
                ? "--"
                : status.MicrophoneBoomAttached.Value
                    ? "Attached"
                    : "Detached";
            ProductColorText = status.ProductColor is null ? "--" : $"0x{status.ProductColor.Value:X2}";

            if (status.MicMuted is not null)
            {
                MicMuted = status.MicMuted.Value;
            }

            if (status.MicrophoneMonitoringEnabled is not null)
            {
                MicrophoneMonitoringEnabled = status.MicrophoneMonitoringEnabled.Value;
            }

            if (status.VoicePromptEnabled is not null)
            {
                VoicePromptEnabled = status.VoicePromptEnabled.Value;
            }

            if (status.AutoShutdownMinutes is not null)
            {
                SelectedAutoShutdownOption = _autoShutdownOptions.FirstOrDefault(
                    option => option.Minutes == status.AutoShutdownMinutes.Value);
            }
        }
        finally
        {
            _applyingDeviceState = false;
        }
    }

    private async Task ApplyDeviceChangeAsync(Func<Task> apply, string successMessage)
    {
        if (_applyingDeviceState || !ControlsEnabled)
        {
            return;
        }

        await RunAsync(
            async () =>
            {
                await RunDeviceOperationAsync(apply).ConfigureAwait(true);
                SaveCurrentSettings();
                StatusMessage = successMessage;
            }).ConfigureAwait(false);
    }

    private async Task<bool> ApplySavedSettingsAsync()
    {
        var savedSettingsApplied = false;

        await RunAsync(
            async () =>
            {
                savedSettingsApplied = await ApplySavedSettingsCoreAsync().ConfigureAwait(true);
                if (savedSettingsApplied)
                {
                    StatusMessage = "Saved settings applied.";
                }
            }).ConfigureAwait(false);

        return savedSettingsApplied;
    }

    private async Task<bool> ApplySavedSettingsCoreAsync()
    {
        var settingsKey = _settingsKey ?? DeviceSettingsStore.CreateCloudAlphaWirelessKey(null, DeviceInfo);
        var savedSettings = _settingsStore.GetCloudAlphaWirelessSettings(settingsKey);
        if (savedSettings is null || !IsConnected)
        {
            return false;
        }

        if (savedSettings.MicMuted is not null)
        {
            await RunDeviceOperationAsync(() => _client.SetMicMuteAsync(savedSettings.MicMuted.Value))
                .ConfigureAwait(true);
        }

        if (savedSettings.MicrophoneMonitoringEnabled is not null)
        {
            await RunDeviceOperationAsync(
                    () => _client.SetMicrophoneMonitoringEnabledAsync(
                        savedSettings.MicrophoneMonitoringEnabled.Value))
                .ConfigureAwait(true);
        }

        if (savedSettings.VoicePromptEnabled is not null)
        {
            await RunDeviceOperationAsync(() => _client.SetVoicePromptEnabledAsync(savedSettings.VoicePromptEnabled.Value))
                .ConfigureAwait(true);
        }

        if (savedSettings.AutoShutdownMinutes is not null)
        {
            await RunDeviceOperationAsync(() => _client.SetAutoShutdownAsync(savedSettings.AutoShutdownMinutes.Value))
                .ConfigureAwait(true);
        }

        ApplySavedSettingsToView(savedSettings);
        return true;
    }

    private void ApplySavedSettingsToView(CloudAlphaWirelessSavedSettings savedSettings)
    {
        _applyingDeviceState = true;
        try
        {
            if (savedSettings.MicMuted is not null)
            {
                MicMuted = savedSettings.MicMuted.Value;
            }

            if (savedSettings.MicrophoneMonitoringEnabled is not null)
            {
                MicrophoneMonitoringEnabled = savedSettings.MicrophoneMonitoringEnabled.Value;
            }

            if (savedSettings.VoicePromptEnabled is not null)
            {
                VoicePromptEnabled = savedSettings.VoicePromptEnabled.Value;
            }

            if (savedSettings.AutoShutdownMinutes is not null)
            {
                SelectedAutoShutdownOption = _autoShutdownOptions.FirstOrDefault(
                    option => option.Minutes == savedSettings.AutoShutdownMinutes.Value);
            }
        }
        finally
        {
            _applyingDeviceState = false;
        }
    }

    private void SaveCurrentSettings()
    {
        var settingsKey = _settingsKey ?? DeviceSettingsStore.CreateCloudAlphaWirelessKey(null, DeviceInfo);
        _settingsStore.SaveCloudAlphaWirelessSettings(
            settingsKey,
            new CloudAlphaWirelessSavedSettings
            {
                MicMuted = MicMuted,
                MicrophoneMonitoringEnabled = MicrophoneMonitoringEnabled,
                VoicePromptEnabled = VoicePromptEnabled,
                AutoShutdownMinutes = SelectedAutoShutdownOption?.Minutes
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
            await action().ConfigureAwait(true);
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

    private async Task RunDeviceOperationAsync(Func<Task> action)
    {
        await _deviceOperationLock.WaitAsync().ConfigureAwait(true);

        try
        {
            await action().ConfigureAwait(true);
        }
        finally
        {
            _deviceOperationLock.Release();
        }
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _autoRefreshTimer.Stop();
        _autoRefreshTimer.Tick -= AutoRefreshTimer_Tick;
        _client.Dispose();
        _deviceOperationLock.Dispose();
        _disposed = true;
    }
}
