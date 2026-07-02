using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using OpenHyperX.Core;
using OpenHyperX.Devices.QuadCast;

namespace OpenHyperX.App.ViewModels;

public sealed class QuadCastDeviceViewModel : ObservableObject, IConnectedDeviceViewModel
{
    private static readonly TimeSpan AutoRefreshInterval = TimeSpan.FromSeconds(5);
    private readonly QuadCastClient _client;
    private readonly SemaphoreSlim _deviceOperationLock = new(1, 1);
    private readonly DispatcherTimer _autoRefreshTimer;
    private bool _applyingDeviceState;
    private bool _disposed;
    private bool _isBusy;
    private bool _isConnected;
    private bool _muted;
    private bool _highPassEnabled;
    private string _statusMessage = "Connected to HID interface.";
    private string _muteText = "--";
    private string _polarPatternText = "--";
    private string _highPassText = "--";
    private string _microphoneGainText = "--";
    private string _mixBalanceText = "--";
    private string _brightnessText = "--";
    private string _reverseLightsText = "--";
    private QuadCastPolarPatternOption? _selectedPolarPatternOption;

    public QuadCastDeviceViewModel(HidDeviceInfo deviceInfo, QuadCastClient client)
    {
        DeviceInfo = deviceInfo;
        _client = client;
        PatternOptions =
        [
            new QuadCastPolarPatternOption(QuadCastPolarPattern.Cardioid, "Cardioid"),
            new QuadCastPolarPatternOption(QuadCastPolarPattern.Omnidirectional, "Omnidirectional"),
            new QuadCastPolarPatternOption(QuadCastPolarPattern.Stereo, "Stereo"),
            new QuadCastPolarPatternOption(QuadCastPolarPattern.Bidirectional, "Bidirectional")
        ];
        _autoRefreshTimer = new DispatcherTimer
        {
            Interval = AutoRefreshInterval
        };
        _autoRefreshTimer.Tick += AutoRefreshTimer_Tick;
    }

    public HidDeviceInfo DeviceInfo { get; }

    public QuadCastModel Model => _client.Model;

    public IReadOnlyList<QuadCastPolarPatternOption> PatternOptions { get; }

    public string DevicePath => DeviceInfo.Path;

    public string DisplayName => DeviceInfo.DisplayName;

    public string DeviceType => Model switch
    {
        QuadCastModel.QuadCastS => "QuadCast S",
        QuadCastModel.QuadCast2 => "QuadCast 2",
        QuadCastModel.QuadCast2S => "QuadCast 2 S",
        _ => "QuadCast"
    };

    public string DetailText =>
        $"{DeviceInfo.VendorIdHex}:{DeviceInfo.ProductIdHex}  In {DeviceInfo.InputReportLength} / Out {DeviceInfo.OutputReportLength} / Feature {DeviceInfo.FeatureReportLength}";

    public bool SupportsReportControls => Model is QuadCastModel.QuadCast2 or QuadCastModel.QuadCast2S;

    public bool SupportsHighPass => SupportsReportControls;

    public bool SupportsGainAndBalance => Model == QuadCastModel.QuadCast2S;

    public bool SupportsLightingState => Model == QuadCastModel.QuadCastS;

    public bool CanChangeReportSettings => ControlsEnabled && SupportsReportControls;

    public bool IsBusy
    {
        get => _isBusy;
        private set
        {
            if (SetProperty(ref _isBusy, value))
            {
                OnPropertyChanged(nameof(ControlsEnabled));
                OnPropertyChanged(nameof(CanChangeReportSettings));
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
                OnPropertyChanged(nameof(CanChangeReportSettings));
            }
        }
    }

    public bool ControlsEnabled => IsConnected && !IsBusy;

    public string ConnectionText => IsConnected ? "Microphone connected" : "Microphone unavailable";

    public string StatusMessage
    {
        get => _statusMessage;
        private set => SetProperty(ref _statusMessage, value);
    }

    public string MuteText
    {
        get => _muteText;
        private set => SetProperty(ref _muteText, value);
    }

    public string PolarPatternText
    {
        get => _polarPatternText;
        private set => SetProperty(ref _polarPatternText, value);
    }

    public string HighPassText
    {
        get => _highPassText;
        private set => SetProperty(ref _highPassText, value);
    }

    public string MicrophoneGainText
    {
        get => _microphoneGainText;
        private set => SetProperty(ref _microphoneGainText, value);
    }

    public string MixBalanceText
    {
        get => _mixBalanceText;
        private set => SetProperty(ref _mixBalanceText, value);
    }

    public string BrightnessText
    {
        get => _brightnessText;
        private set => SetProperty(ref _brightnessText, value);
    }

    public string ReverseLightsText
    {
        get => _reverseLightsText;
        private set => SetProperty(ref _reverseLightsText, value);
    }

    public bool Muted
    {
        get => _muted;
        set
        {
            if (SetProperty(ref _muted, value))
            {
                _ = ApplyDeviceChangeAsync(() => _client.SetMicrophoneMutedAsync(value), "Microphone updated");
            }
        }
    }

    public bool HighPassEnabled
    {
        get => _highPassEnabled;
        set
        {
            if (SetProperty(ref _highPassEnabled, value))
            {
                _ = ApplyDeviceChangeAsync(() => _client.SetHighPassEnabledAsync(value), "High-pass filter updated");
            }
        }
    }

    public QuadCastPolarPatternOption? SelectedPolarPatternOption
    {
        get => _selectedPolarPatternOption;
        set
        {
            if (SetProperty(ref _selectedPolarPatternOption, value) && value is not null)
            {
                _ = ApplyDeviceChangeAsync(
                    () => _client.SetPolarPatternAsync(value.Pattern),
                    "Polar pattern updated");
            }
        }
    }

    public async Task InitializeAsync()
    {
        await RefreshStatusAsync();
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
        var status = await GetStatusWithLockAsync().ConfigureAwait(true);
        ApplyStatus(status);

        if (updateStatusMessage)
        {
            StatusMessage = "Status refreshed. Auto-updating.";
        }
    }

    private async Task RefreshStatusQuietlyAsync(bool updateStatusMessage)
    {
        if (IsBusy || _disposed)
        {
            return;
        }

        try
        {
            var status = await TryGetStatusWithLockAsync().ConfigureAwait(true);
            if (status is null)
            {
                return;
            }

            ApplyStatus(status);
            if (updateStatusMessage)
            {
                StatusMessage = "Status refreshed. Auto-updating.";
            }
        }
        catch (Exception ex)
        {
            StatusMessage = ex.Message;
            IsConnected = false;
        }
    }

    private async Task<QuadCastStatus> GetStatusWithLockAsync()
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

    private async Task<QuadCastStatus?> TryGetStatusWithLockAsync()
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

    private void ApplyStatus(QuadCastStatus status)
    {
        _applyingDeviceState = true;
        try
        {
            IsConnected = true;
            MuteText = status.Muted is null ? "--" : status.Muted.Value ? "Muted" : "Live";
            PolarPatternText = status.PolarPattern is null ? "--" : FormatPattern(status.PolarPattern.Value);
            HighPassText = status.HighPassEnabled is null ? "--" : status.HighPassEnabled.Value ? "On" : "Off";
            MicrophoneGainText = status.MicrophoneGain is null ? "--" : status.MicrophoneGain.Value.ToString();
            MixBalanceText = status.MixBalance is null ? "--" : status.MixBalance.Value.ToString();
            BrightnessText = status.BrightnessPercent is null ? "--" : $"{status.BrightnessPercent}%";
            ReverseLightsText = status.ReverseLights is null ? "--" : status.ReverseLights.Value ? "On" : "Off";

            if (status.Muted is not null)
            {
                Muted = status.Muted.Value;
            }

            if (status.HighPassEnabled is not null)
            {
                HighPassEnabled = status.HighPassEnabled.Value;
            }

            if (status.PolarPattern is not null)
            {
                SelectedPolarPatternOption = PatternOptions.FirstOrDefault(option => option.Pattern == status.PolarPattern.Value);
            }
        }
        finally
        {
            _applyingDeviceState = false;
        }
    }

    private async Task ApplyDeviceChangeAsync(Func<Task> apply, string successMessage)
    {
        if (_applyingDeviceState || !CanChangeReportSettings)
        {
            return;
        }

        await RunAsync(
            async () =>
            {
                await RunDeviceOperationAsync(apply).ConfigureAwait(true);
                StatusMessage = successMessage;
            }).ConfigureAwait(false);
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

    private static string FormatPattern(QuadCastPolarPattern pattern)
    {
        return pattern switch
        {
            QuadCastPolarPattern.Bidirectional => "Bidirectional",
            QuadCastPolarPattern.Cardioid => "Cardioid",
            QuadCastPolarPattern.Omnidirectional => "Omnidirectional",
            QuadCastPolarPattern.Stereo => "Stereo",
            _ => pattern.ToString()
        };
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
