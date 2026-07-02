using System.Collections.ObjectModel;
using System.ComponentModel;
using Avalonia;
using Avalonia.Styling;
using OpenHyperX.App.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OpenHyperX.Core;
using OpenHyperX.Devices.CloudAlphaWireless;
using OpenHyperX.Devices.QuadCast;

namespace OpenHyperX.App.ViewModels;

public sealed class MainWindowViewModel : ObservableObject, IDisposable
{
    private readonly IHidDeviceEnumerator _deviceEnumerator;
    private readonly DeviceSettingsStore _settingsStore;
    private readonly StartupRegistrationService _startupRegistrationService;
    private readonly DtsSpatialAudioService _dtsSpatialAudioService;
    private bool _disposed;
    private bool _applyingDtsSpatialAudioSetting;
    private bool _dtsSpatialAudioEnabled;
    private bool _isDtsBusy;
    private bool _isBusy;
    private bool _isDarkMode;
    private bool _startInTrayOnStartup;
    private bool _startWithWindows;
    private string _dtsApoText = "--";
    private string _dtsDriverText = "--";
    private string _dtsEndpointText = "--";
    private string _dtsServiceText = "--";
    private string _dtsSummaryText = "Not checked";
    private string _statusMessage = "Supported devices connect automatically when scanned.";
    private IConnectedDeviceViewModel? _selectedDevice;
    private CloseBehaviorOption? _selectedCloseBehaviorOption;

    public MainWindowViewModel(
        IHidDeviceEnumerator deviceEnumerator,
        DeviceSettingsStore settingsStore,
        StartupRegistrationService startupRegistrationService,
        DtsSpatialAudioService dtsSpatialAudioService)
    {
        _deviceEnumerator = deviceEnumerator;
        _settingsStore = settingsStore;
        _startupRegistrationService = startupRegistrationService;
        _dtsSpatialAudioService = dtsSpatialAudioService;
        _isDarkMode = settingsStore.IsDarkMode;
        _dtsSpatialAudioEnabled = settingsStore.DtsSpatialAudioEnabled;
        _startInTrayOnStartup = settingsStore.StartInTrayOnStartup;
        _startWithWindows = startupRegistrationService.IsRegistered;
        ApplyTheme(_isDarkMode);

        RefreshDevicesCommand = new AsyncRelayCommand(RefreshDevicesAsync, CanScan);
        RefreshStatusCommand = new AsyncRelayCommand(RefreshSelectedStatusAsync, CanRefreshSelectedStatus);
        RefreshDtsStatusCommand = new AsyncRelayCommand(RefreshDtsStatusAsync, CanRunDtsAction);

        AutoShutdownOptions =
        [
            new AutoShutdownOption(0, "Never"),
            new AutoShutdownOption(5, "5 minutes"),
            new AutoShutdownOption(10, "10 minutes"),
            new AutoShutdownOption(20, "20 minutes"),
            new AutoShutdownOption(30, "30 minutes"),
            new AutoShutdownOption(60, "60 minutes")
        ];

        CloseBehaviorOptions =
        [
            new CloseBehaviorOption(AppCloseBehavior.Ask, "Ask when closing"),
            new CloseBehaviorOption(AppCloseBehavior.Exit, "Exit application"),
            new CloseBehaviorOption(AppCloseBehavior.CloseToTray, "Close to system tray")
        ];
        _selectedCloseBehaviorOption = FindCloseBehaviorOption(settingsStore.CloseBehavior);
        RefreshDtsStatus(announce: false);
        if (_dtsSpatialAudioEnabled)
        {
            _ = ApplyDtsSpatialAudioPreferenceAsync(enabled: true);
        }
    }

    public ObservableCollection<IConnectedDeviceViewModel> ConnectedDevices { get; } = [];

    public ObservableCollection<AutoShutdownOption> AutoShutdownOptions { get; }

    public ObservableCollection<CloseBehaviorOption> CloseBehaviorOptions { get; }

    public AsyncRelayCommand RefreshDevicesCommand { get; }

    public AsyncRelayCommand RefreshStatusCommand { get; }

    public AsyncRelayCommand RefreshDtsStatusCommand { get; }

    public string ActiveDeviceText => SelectedDevice?.DisplayName ?? "No supported device selected";

    public string DeviceCountText => ConnectedDevices.Count switch
    {
        0 => "No devices connected",
        1 => "1 device connected",
        _ => $"{ConnectedDevices.Count} devices connected"
    };

    public string ProtocolSummary =>
        "Cloud Alpha Wireless uses 0x21 0xBB reports. QuadCast 2 devices use 0x77 reports; QuadCast S uses HID feature reports.";

    public string DtsSummaryText
    {
        get => _dtsSummaryText;
        private set => SetProperty(ref _dtsSummaryText, value);
    }

    public string DtsDriverText
    {
        get => _dtsDriverText;
        private set => SetProperty(ref _dtsDriverText, value);
    }

    public string DtsApoText
    {
        get => _dtsApoText;
        private set => SetProperty(ref _dtsApoText, value);
    }

    public string DtsEndpointText
    {
        get => _dtsEndpointText;
        private set => SetProperty(ref _dtsEndpointText, value);
    }

    public string DtsServiceText
    {
        get => _dtsServiceText;
        private set => SetProperty(ref _dtsServiceText, value);
    }

    public bool DtsSpatialAudioEnabled
    {
        get => _dtsSpatialAudioEnabled;
        set
        {
            if (!SetProperty(ref _dtsSpatialAudioEnabled, value))
            {
                return;
            }

            _settingsStore.SaveDtsSpatialAudioEnabled(value);
            if (!_applyingDtsSpatialAudioSetting)
            {
                _ = ApplyDtsSpatialAudioPreferenceAsync(value);
            }
        }
    }

    public bool IsDtsBusy
    {
        get => _isDtsBusy;
        private set
        {
            if (SetProperty(ref _isDtsBusy, value))
            {
                OnPropertyChanged(nameof(DtsControlsEnabled));
                RefreshDtsStatusCommand.NotifyCanExecuteChanged();
            }
        }
    }

    public bool DtsControlsEnabled => !IsDtsBusy;

    public string StatusMessage
    {
        get => _statusMessage;
        private set => SetProperty(ref _statusMessage, value);
    }

    public bool IsDarkMode
    {
        get => _isDarkMode;
        set
        {
            if (SetProperty(ref _isDarkMode, value))
            {
                ApplyTheme(value);
                _settingsStore.SaveTheme(value);
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
                RefreshStatusCommand.NotifyCanExecuteChanged();
            }
        }
    }

    public CloseBehaviorOption? SelectedCloseBehaviorOption
    {
        get => _selectedCloseBehaviorOption;
        set
        {
            if (SetProperty(ref _selectedCloseBehaviorOption, value) && value is not null)
            {
                _settingsStore.SaveCloseBehavior(value.Behavior);
                OnPropertyChanged(nameof(CloseBehavior));
            }
        }
    }

    public AppCloseBehavior CloseBehavior => SelectedCloseBehaviorOption?.Behavior ?? AppCloseBehavior.Ask;

    public bool IsStartupRegistrationSupported => _startupRegistrationService.IsSupported;

    public bool StartWithWindows
    {
        get => _startWithWindows;
        set
        {
            if (_startWithWindows == value)
            {
                return;
            }

            var previousValue = _startWithWindows;
            if (!SetProperty(ref _startWithWindows, value))
            {
                return;
            }

            OnPropertyChanged(nameof(CanStartInTrayOnStartup));

            try
            {
                _startupRegistrationService.SetRegistered(value, StartInTrayOnStartup);
                StatusMessage = value
                    ? "OpenHyperX will start when you sign in."
                    : "OpenHyperX startup registration removed.";
            }
            catch (Exception ex)
            {
                SetProperty(ref _startWithWindows, previousValue, nameof(StartWithWindows));
                OnPropertyChanged(nameof(CanStartInTrayOnStartup));
                StatusMessage = ex.Message;
            }
        }
    }

    public bool StartInTrayOnStartup
    {
        get => _startInTrayOnStartup;
        set
        {
            if (!SetProperty(ref _startInTrayOnStartup, value))
            {
                return;
            }

            _settingsStore.SaveStartInTrayOnStartup(value);

            if (!StartWithWindows)
            {
                return;
            }

            try
            {
                _startupRegistrationService.SetRegistered(registered: true, value);
                StatusMessage = value
                    ? "OpenHyperX will start in the system tray."
                    : "OpenHyperX will open normally when you sign in.";
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
            }
        }
    }

    public bool CanStartInTrayOnStartup => IsStartupRegistrationSupported && StartWithWindows;

    public IConnectedDeviceViewModel? SelectedDevice
    {
        get => _selectedDevice;
        set
        {
            if (_selectedDevice is not null)
            {
                _selectedDevice.PropertyChanged -= SelectedDevice_PropertyChanged;
            }

            if (SetProperty(ref _selectedDevice, value))
            {
                OnPropertyChanged(nameof(ActiveDeviceText));
                OnPropertyChanged(nameof(SelectedCloudAlphaWirelessDevice));
                OnPropertyChanged(nameof(SelectedQuadCastDevice));
                OnPropertyChanged(nameof(HasCloudAlphaWirelessDevice));
                OnPropertyChanged(nameof(HasQuadCastDevice));
                RefreshStatusCommand.NotifyCanExecuteChanged();
            }

            if (_selectedDevice is not null)
            {
                _selectedDevice.PropertyChanged += SelectedDevice_PropertyChanged;
            }
        }
    }

    public CloudAlphaWirelessDeviceViewModel? SelectedCloudAlphaWirelessDevice =>
        SelectedDevice as CloudAlphaWirelessDeviceViewModel;

    public QuadCastDeviceViewModel? SelectedQuadCastDevice =>
        SelectedDevice as QuadCastDeviceViewModel;

    public bool HasCloudAlphaWirelessDevice => SelectedCloudAlphaWirelessDevice is not null;

    public bool HasQuadCastDevice => SelectedQuadCastDevice is not null;

    public async Task RefreshDevicesAsync()
    {
        await RunAsync(
            async () =>
            {
                var discoveredDevices = await Task.Run(DiscoverSupportedDevices);

                var failures = new List<string>();
                RemoveMissingDevices(discoveredDevices);

                foreach (var deviceInfo in discoveredDevices)
                {
                    if (ConnectedDevices.Any(device => SamePath(device.DevicePath, deviceInfo.Path)))
                    {
                        continue;
                    }

                    try
                    {
                        ConnectedDevices.Add(CreateDeviceViewModel(deviceInfo));
                    }
                    catch (Exception ex)
                    {
                        failures.Add($"{deviceInfo.DisplayName}: {ex.Message}");
                    }
                }

                SelectedDevice ??= ConnectedDevices.FirstOrDefault();
                await InitializeAllConnectedDevicesAsync();
                UpdateDeviceCollectionProperties();

                StatusMessage = BuildScanStatusMessage(discoveredDevices.Length, failures);
            });
    }

    private async Task RefreshSelectedStatusAsync()
    {
        if (SelectedDevice is null)
        {
            StatusMessage = "No supported device is selected.";
            return;
        }

        await SelectedDevice.RefreshStatusAsync();
    }

    private void RefreshDtsStatus(bool announce)
    {
        try
        {
            var status = _dtsSpatialAudioService.GetStatus();
            ApplyDtsStatus(status);

            if (announce)
            {
                StatusMessage = "DTS spatial audio status refreshed.";
            }
        }
        catch (Exception ex)
        {
            DtsSummaryText = "Unavailable";
            DtsDriverText = "--";
            DtsApoText = "--";
            DtsServiceText = "--";
            DtsEndpointText = "--";
            StatusMessage = ex.Message;
        }
    }

    private Task RefreshDtsStatusAsync()
    {
        return RunDtsActionAsync(
            () =>
            {
                RefreshDtsStatus(announce: true);
                return Task.CompletedTask;
            });
    }

    private Task ApplyDtsSpatialAudioPreferenceAsync(bool enabled)
    {
        return RunDtsActionAsync(
            async () =>
            {
                var result = await _dtsSpatialAudioService.SetSpatialAudioEnabledAsync(enabled).ConfigureAwait(true);
                ApplyDtsStatus(result.Status, result.RestartRequired ? "Restart required" : null);
                StatusMessage = result.Message;

                if (!result.Success)
                {
                    SetDtsSpatialAudioEnabledSilently(!enabled);
                }
            });
    }

    private async Task RunDtsActionAsync(Func<Task> action)
    {
        if (IsDtsBusy)
        {
            return;
        }

        try
        {
            IsDtsBusy = true;
            await action().ConfigureAwait(true);
        }
        catch (Exception ex)
        {
            StatusMessage = ex.Message;
        }
        finally
        {
            IsDtsBusy = false;
        }
    }

    private bool CanRunDtsAction()
    {
        return !IsDtsBusy;
    }

    private void ApplyDtsStatus(DtsSpatialAudioStatus status, string? summaryOverride = null)
    {
        DtsSummaryText = summaryOverride ?? status.Summary;
        DtsDriverText = BuildDtsDriverText(status);
        DtsApoText = BuildDtsApoText(status);
        DtsServiceText = BuildDtsServiceText(status);
        DtsEndpointText = BuildDtsEndpointText(status);
    }

    private void SetDtsSpatialAudioEnabledSilently(bool enabled)
    {
        _applyingDtsSpatialAudioSetting = true;
        try
        {
            DtsSpatialAudioEnabled = enabled;
        }
        finally
        {
            _applyingDtsSpatialAudioSetting = false;
        }
    }

    private async Task InitializeAllConnectedDevicesAsync()
    {
        foreach (var device in ConnectedDevices)
        {
            await device.InitializeAsync();
        }
    }

    private void RemoveMissingDevices(IReadOnlyCollection<HidDeviceInfo> discoveredDevices)
    {
        foreach (var device in ConnectedDevices.ToArray())
        {
            if (discoveredDevices.Any(discovered => SamePath(discovered.Path, device.DevicePath)))
            {
                continue;
            }

            if (ReferenceEquals(SelectedDevice, device))
            {
                SelectedDevice = null;
            }

            ConnectedDevices.Remove(device);
            device.Dispose();
        }
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
        }
        finally
        {
            IsBusy = false;
        }
    }

    private bool CanScan()
    {
        return !IsBusy;
    }

    private bool CanRefreshSelectedStatus()
    {
        return !IsBusy && SelectedDevice is not null && !SelectedDevice.IsBusy;
    }

    private void SelectedDevice_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(IConnectedDeviceViewModel.IsBusy)
            or nameof(IConnectedDeviceViewModel.IsConnected))
        {
            RefreshStatusCommand.NotifyCanExecuteChanged();
        }
    }

    private HidDeviceInfo[] DiscoverSupportedDevices()
    {
        var devices = new List<HidDeviceInfo>();
        devices.AddRange(
            _deviceEnumerator
                .ListDevices(CloudAlphaWirelessDeviceIds.Filter)
                .Where(CloudAlphaWirelessDeviceIds.IsLikelyCommandInterface));
        devices.AddRange(
            _deviceEnumerator
                .ListDevices(QuadCastDeviceIds.Filter)
                .Where(QuadCastDeviceIds.IsLikelyCommandInterface));

        return devices
            .GroupBy(device => device.Path, StringComparer.OrdinalIgnoreCase)
            .Select(group => group.First())
            .OrderBy(device => device.DisplayName, StringComparer.OrdinalIgnoreCase)
            .ThenBy(device => device.Path, StringComparer.OrdinalIgnoreCase)
            .ToArray();
    }

    private IConnectedDeviceViewModel CreateDeviceViewModel(HidDeviceInfo deviceInfo)
    {
        var transport = _deviceEnumerator.Open(deviceInfo);

        if (CloudAlphaWirelessDeviceIds.IsLikelyCommandInterface(deviceInfo))
        {
            return new CloudAlphaWirelessDeviceViewModel(
                deviceInfo,
                new CloudAlphaWirelessClient(transport),
                AutoShutdownOptions,
                _settingsStore);
        }

        if (QuadCastDeviceIds.IsLikelyCommandInterface(deviceInfo))
        {
            return new QuadCastDeviceViewModel(deviceInfo, new QuadCastClient(transport));
        }

        transport.Dispose();
        throw new InvalidOperationException("The selected HID interface is not supported.");
    }

    private void UpdateDeviceCollectionProperties()
    {
        OnPropertyChanged(nameof(DeviceCountText));
        OnPropertyChanged(nameof(ActiveDeviceText));
        RefreshStatusCommand.NotifyCanExecuteChanged();
    }

    private string BuildScanStatusMessage(int discoveredCount, IReadOnlyCollection<string> failures)
    {
        if (failures.Count > 0)
        {
            return $"Found {discoveredCount} supported interface(s), but {failures.Count} failed to connect.";
        }

        return ConnectedDevices.Count switch
        {
            0 => "No supported devices found.",
            1 => "Auto-connected 1 supported device.",
            _ => $"Auto-connected {ConnectedDevices.Count} supported devices."
        };
    }

    private static bool SamePath(string left, string right)
    {
        return string.Equals(left, right, StringComparison.OrdinalIgnoreCase);
    }

    private static string BuildDtsDriverText(DtsSpatialAudioStatus status)
    {
        if (!status.IsSupported)
        {
            return "Unavailable";
        }

        if (status.DetectedDriverPackageCount == 0)
        {
            return "Not detected";
        }

        return $"{status.DetectedDriverPackageCount} of {status.RequiredDriverPackageCount} packages";
    }

    private static string BuildDtsApoText(DtsSpatialAudioStatus status)
    {
        if (!status.IsSupported)
        {
            return "Unavailable";
        }

        return status.ApoComRegistered ? "Registered" : "Not registered";
    }

    private static string BuildDtsServiceText(DtsSpatialAudioStatus status)
    {
        if (!status.IsSupported)
        {
            return "Unavailable";
        }

        if (!status.ServiceRegistered)
        {
            return "Not installed";
        }

        return status.ServiceRunning ? "Running" : "Stopped";
    }

    private static string BuildDtsEndpointText(DtsSpatialAudioStatus status)
    {
        if (!status.IsSupported)
        {
            return "Unavailable";
        }

        if (status.DetectedRenderEndpointCount == 0)
        {
            return "None detected";
        }

        if (status.ConfiguredRenderEndpointCount == 0)
        {
            return status.DetectedRenderEndpointCount == 1
                ? "1 partial endpoint"
                : $"{status.DetectedRenderEndpointCount} partial endpoints";
        }

        if (status.ConfiguredRenderEndpointCount == status.DetectedRenderEndpointCount)
        {
            return status.ConfiguredRenderEndpointCount == 1
                ? "1 configured endpoint"
                : $"{status.ConfiguredRenderEndpointCount} configured endpoints";
        }

        var partialEndpointCount = status.DetectedRenderEndpointCount - status.ConfiguredRenderEndpointCount;
        return partialEndpointCount switch
        {
            1 => $"{status.ConfiguredRenderEndpointCount} configured, 1 partial",
            _ => $"{status.ConfiguredRenderEndpointCount} configured, {partialEndpointCount} partial"
        };
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

    public void SetCloseBehavior(AppCloseBehavior behavior)
    {
        SelectedCloseBehaviorOption = FindCloseBehaviorOption(behavior);
    }

    private CloseBehaviorOption FindCloseBehaviorOption(AppCloseBehavior behavior)
    {
        return CloseBehaviorOptions.FirstOrDefault(option => option.Behavior == behavior)
            ?? CloseBehaviorOptions[0];
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        foreach (var device in ConnectedDevices.ToArray())
        {
            device.Dispose();
        }

        ConnectedDevices.Clear();
        _disposed = true;
    }
}
