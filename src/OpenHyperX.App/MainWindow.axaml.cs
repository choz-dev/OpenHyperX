using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using System.ComponentModel;
using OpenHyperX.App.Services;
using OpenHyperX.App.ViewModels;

namespace OpenHyperX.App;

public partial class MainWindow : Window
{
    private readonly TrayIcon? _trayIcon;
    private readonly NativeMenuItem _trayDeviceItem = new("No device connected");
    private readonly NativeMenuItem _trayRefreshStatusItem = new("Refresh status");
    private readonly NativeMenuItem _trayMuteItem = new("Mute microphone");
    private readonly NativeMenuItem _trayMicMonitoringItem = new("Microphone monitoring");
    private readonly NativeMenuItem _trayHighPassItem = new("High-pass filter");
    private MainWindowViewModel? _trayViewModel;
    private IConnectedDeviceViewModel? _trayObservedDevice;
    private bool _allowClose;
    private bool _handlingClose;
    private bool _startHiddenApplied;

    public bool StartHidden { get; init; }

    public MainWindow()
    {
        InitializeComponent();
        Icon = CreateGeneratedIcon();
        _trayIcon = CreateTrayIcon();
    }

    protected override void OnClosing(WindowClosingEventArgs e)
    {
        if (_allowClose || e.CloseReason != WindowCloseReason.WindowClosing)
        {
            base.OnClosing(e);
            return;
        }

        e.Cancel = true;
        _ = HandleCloseRequestAsync();
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);
        DetachTrayStateObservers();

        if (DataContext is MainWindowViewModel viewModel)
        {
            _trayViewModel = viewModel;
            _trayViewModel.PropertyChanged += TrayViewModel_PropertyChanged;
            ObserveTrayDevice(viewModel.SelectedDevice);
        }

        UpdateTrayMenuItems();
    }

    protected override void OnClosed(EventArgs e)
    {
        DetachTrayStateObservers();
        _trayIcon?.Dispose();

        if (DataContext is IDisposable disposable)
        {
            disposable.Dispose();
        }

        base.OnClosed(e);
    }

    protected override async void OnOpened(EventArgs e)
    {
        base.OnOpened(e);

        if (StartHidden && !_startHiddenApplied)
        {
            _startHiddenApplied = true;
            HideToTray();
        }

        if (DataContext is MainWindowViewModel viewModel)
        {
            await viewModel.RefreshDevicesAsync();
        }
    }

    private async Task HandleCloseRequestAsync()
    {
        if (_handlingClose)
        {
            return;
        }

        _handlingClose = true;

        try
        {
            var behavior = DataContext is MainWindowViewModel viewModel
                ? viewModel.CloseBehavior
                : AppCloseBehavior.Ask;

            if (behavior == AppCloseBehavior.Ask)
            {
                var dialog = new CloseChoiceDialog();
                if (Classes.Contains("dark"))
                {
                    dialog.Classes.Add("dark");
                }

                var result = await dialog.ShowDialog<CloseChoiceDialogResult?>(this);
                if (result is null)
                {
                    return;
                }

                behavior = result.Behavior;
                if (result.RememberChoice && DataContext is MainWindowViewModel resultViewModel)
                {
                    resultViewModel.SetCloseBehavior(result.Behavior);
                }
            }

            if (behavior == AppCloseBehavior.CloseToTray)
            {
                HideToTray();
                return;
            }

            ExitApplication();
        }
        finally
        {
            _handlingClose = false;
        }
    }

    private TrayIcon CreateTrayIcon()
    {
        var openItem = new NativeMenuItem("Open");
        openItem.Click += OpenFromTray_Click;

        var exitItem = new NativeMenuItem("Exit");
        exitItem.Click += ExitFromTray_Click;

        _trayDeviceItem.IsEnabled = false;
        _trayRefreshStatusItem.Click += RefreshStatusFromTray_Click;
        _trayMuteItem.Click += ToggleMuteFromTray_Click;
        _trayMicMonitoringItem.Click += ToggleMicMonitoringFromTray_Click;
        _trayHighPassItem.Click += ToggleHighPassFromTray_Click;

        var menu = new NativeMenu();
        menu.NeedsUpdate += TrayMenu_NeedsUpdate;
        menu.Items.Add(_trayDeviceItem);
        menu.Items.Add(_trayRefreshStatusItem);
        menu.Items.Add(new NativeMenuItemSeparator());
        menu.Items.Add(_trayMuteItem);
        menu.Items.Add(_trayMicMonitoringItem);
        menu.Items.Add(_trayHighPassItem);
        menu.Items.Add(new NativeMenuItemSeparator());
        menu.Items.Add(openItem);
        menu.Items.Add(new NativeMenuItemSeparator());
        menu.Items.Add(exitItem);
        UpdateTrayMenuItems();

        var trayIcon = new TrayIcon
        {
            ToolTipText = "OpenHyperX",
            Icon = CreateGeneratedIcon(),
            Menu = menu,
            IsVisible = true
        };
        trayIcon.Clicked += OpenFromTray_Click;

        return trayIcon;
    }

    private void TrayMenu_NeedsUpdate(object? sender, EventArgs e)
    {
        UpdateTrayMenuItems();
    }

    private void TrayViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(MainWindowViewModel.SelectedDevice)
            or nameof(MainWindowViewModel.SelectedCloudAlphaWirelessDevice)
            or nameof(MainWindowViewModel.SelectedQuadCastDevice)
            or nameof(MainWindowViewModel.HasCloudAlphaWirelessDevice)
            or nameof(MainWindowViewModel.HasQuadCastDevice))
        {
            ObserveTrayDevice(_trayViewModel?.SelectedDevice);
        }

        UpdateTrayMenuItems();
    }

    private void TrayDevice_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        UpdateTrayMenuItems();
    }

    private void OpenFromTray_Click(object? sender, EventArgs e)
    {
        ShowFromTray();
    }

    private void RefreshStatusFromTray_Click(object? sender, EventArgs e)
    {
        _ = RefreshStatusFromTrayAsync();
    }

    private async Task RefreshStatusFromTrayAsync()
    {
        if (DataContext is not MainWindowViewModel { SelectedDevice: { } selectedDevice })
        {
            return;
        }

        await selectedDevice.RefreshStatusAsync();
        UpdateTrayMenuItems();
    }

    private async void ToggleMuteFromTray_Click(object? sender, EventArgs e)
    {
        switch (DataContext as MainWindowViewModel)
        {
            case { SelectedCloudAlphaWirelessDevice: { ControlsEnabled: true } headset }:
                await headset.SetMicMutedAsync(!headset.MicMuted);
                break;
            case { SelectedQuadCastDevice: { CanChangeReportSettings: true } microphone }:
                microphone.Muted = !microphone.Muted;
                break;
        }

        UpdateTrayMenuItems();
    }

    private async void ToggleMicMonitoringFromTray_Click(object? sender, EventArgs e)
    {
        if (DataContext is MainWindowViewModel
            {
                SelectedCloudAlphaWirelessDevice: { ControlsEnabled: true } headset
            })
        {
            await headset.SetMicrophoneMonitoringEnabledAsync(!headset.MicrophoneMonitoringEnabled);
        }

        UpdateTrayMenuItems();
    }

    private void ToggleHighPassFromTray_Click(object? sender, EventArgs e)
    {
        if (DataContext is MainWindowViewModel
            {
                SelectedQuadCastDevice: { CanChangeReportSettings: true } microphone
            })
        {
            microphone.HighPassEnabled = !microphone.HighPassEnabled;
        }

        UpdateTrayMenuItems();
    }

    private void ExitFromTray_Click(object? sender, EventArgs e)
    {
        ExitApplication();
    }

    private void HideToTray()
    {
        Hide();
    }

    private void ShowFromTray()
    {
        Show();

        if (WindowState == WindowState.Minimized)
        {
            WindowState = WindowState.Normal;
        }

        Activate();
    }

    private void ExitApplication()
    {
        _allowClose = true;

        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Shutdown();
            return;
        }

        Close();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private static WindowIcon CreateGeneratedIcon()
    {
        return new WindowIcon(AssetLoader.Open(new Uri("avares://OpenHyperX.App/Assets/OpenHyperXLogo.png")));
    }

    private void ObserveTrayDevice(IConnectedDeviceViewModel? device)
    {
        if (ReferenceEquals(_trayObservedDevice, device))
        {
            return;
        }

        if (_trayObservedDevice is not null)
        {
            _trayObservedDevice.PropertyChanged -= TrayDevice_PropertyChanged;
        }

        _trayObservedDevice = device;

        if (_trayObservedDevice is not null)
        {
            _trayObservedDevice.PropertyChanged += TrayDevice_PropertyChanged;
        }
    }

    private void DetachTrayStateObservers()
    {
        if (_trayViewModel is not null)
        {
            _trayViewModel.PropertyChanged -= TrayViewModel_PropertyChanged;
            _trayViewModel = null;
        }

        if (_trayObservedDevice is not null)
        {
            _trayObservedDevice.PropertyChanged -= TrayDevice_PropertyChanged;
            _trayObservedDevice = null;
        }
    }

    private void UpdateTrayMenuItems()
    {
        if (DataContext is not MainWindowViewModel viewModel)
        {
            ApplyNoDeviceTrayState();
            return;
        }

        _trayDeviceItem.Header = viewModel.SelectedDevice?.DisplayName ?? "No device connected";
        _trayRefreshStatusItem.IsEnabled = viewModel.SelectedDevice is { IsBusy: false };

        if (viewModel.SelectedCloudAlphaWirelessDevice is { } headset)
        {
            _trayMuteItem.IsVisible = true;
            _trayMuteItem.Header = headset.MicMuted ? "Unmute microphone" : "Mute microphone";
            _trayMuteItem.IsEnabled = headset.ControlsEnabled;

            _trayMicMonitoringItem.IsVisible = true;
            _trayMicMonitoringItem.Header = headset.MicrophoneMonitoringEnabled
                ? "Turn mic monitoring off"
                : "Turn mic monitoring on";
            _trayMicMonitoringItem.IsEnabled = headset.ControlsEnabled;

            _trayHighPassItem.IsVisible = false;
            _trayHighPassItem.Header = "High-pass filter";
            _trayHighPassItem.IsEnabled = false;
            return;
        }

        if (viewModel.SelectedQuadCastDevice is { } microphone)
        {
            _trayMuteItem.IsVisible = true;
            _trayMuteItem.Header = microphone.Muted ? "Unmute microphone" : "Mute microphone";
            _trayMuteItem.IsEnabled = microphone.CanChangeReportSettings;

            _trayMicMonitoringItem.IsVisible = false;
            _trayMicMonitoringItem.Header = "Microphone monitoring";
            _trayMicMonitoringItem.IsEnabled = false;

            _trayHighPassItem.IsVisible = microphone.SupportsHighPass;
            _trayHighPassItem.Header = microphone.HighPassEnabled
                ? "Turn high-pass off"
                : "Turn high-pass on";
            _trayHighPassItem.IsEnabled = microphone.CanChangeReportSettings;
            return;
        }

        ApplyNoDeviceTrayState();
    }

    private void ApplyNoDeviceTrayState()
    {
        _trayDeviceItem.Header = "No device connected";
        _trayRefreshStatusItem.IsEnabled = false;

        _trayMuteItem.IsVisible = false;
        _trayMuteItem.Header = "Mute microphone";
        _trayMuteItem.IsEnabled = false;

        _trayMicMonitoringItem.IsVisible = false;
        _trayMicMonitoringItem.Header = "Microphone monitoring";
        _trayMicMonitoringItem.IsEnabled = false;

        _trayHighPassItem.IsVisible = false;
        _trayHighPassItem.Header = "High-pass filter";
        _trayHighPassItem.IsEnabled = false;
    }
}
