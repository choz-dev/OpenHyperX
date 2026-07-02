using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using OpenHyperX.App.Services;
using OpenHyperX.App.ViewModels;
using OpenHyperX.Hid;

namespace OpenHyperX.App;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var startHidden = Environment.GetCommandLineArgs()
                .Skip(1)
                .Any(arg => string.Equals(arg, "--tray", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(arg, "--minimized", StringComparison.OrdinalIgnoreCase));

            desktop.MainWindow = new MainWindow
            {
                StartHidden = startHidden,
                DataContext = new MainWindowViewModel(
                    new HidSharpDeviceEnumerator(),
                    new DeviceSettingsStore(),
                    new StartupRegistrationService(),
                    new DtsSpatialAudioService())
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
