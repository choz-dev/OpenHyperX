using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using OpenHyperX.App.Services;
using OpenHyperX.App.ViewModels;

namespace OpenHyperX.App;

public partial class MainWindow : Window
{
    private readonly TrayIcon? _trayIcon;
    private bool _allowClose;
    private bool _handlingClose;

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

    protected override void OnClosed(EventArgs e)
    {
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

        if (StartHidden)
        {
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

        var menu = new NativeMenu();
        menu.Items.Add(openItem);
        menu.Items.Add(new NativeMenuItemSeparator());
        menu.Items.Add(exitItem);

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

    private void OpenFromTray_Click(object? sender, EventArgs e)
    {
        ShowFromTray();
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
}
