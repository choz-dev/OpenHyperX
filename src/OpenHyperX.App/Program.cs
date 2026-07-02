using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;

namespace OpenHyperX.App;

internal static class Program
{
    private const string SingleInstanceMutexName = "OpenHyperX.App.SingleInstance";
    private const string ActivationEventName = "OpenHyperX.App.Activate";

    [STAThread]
    public static void Main(string[] args)
    {
        using var activationEvent = new EventWaitHandle(
            initialState: false,
            mode: EventResetMode.AutoReset,
            name: ActivationEventName);
        using var singleInstanceMutex = new Mutex(
            initiallyOwned: true,
            name: SingleInstanceMutexName,
            createdNew: out var ownsInstance);

        if (!ownsInstance)
        {
            activationEvent.Set();
            return;
        }

        using var activationListenerCancellation = new CancellationTokenSource();
        var activationListener = Task.Run(
            () => ListenForActivationRequests(activationEvent, activationListenerCancellation.Token));

        try
        {
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args, ShutdownMode.OnExplicitShutdown);
        }
        finally
        {
            activationListenerCancellation.Cancel();
            activationEvent.Set();
            activationListener.GetAwaiter().GetResult();
            singleInstanceMutex.ReleaseMutex();
        }
    }

    public static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder
            .Configure<App>()
            .UsePlatformDetect()
            .LogToTrace();
    }

    private static void ListenForActivationRequests(EventWaitHandle activationEvent, CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            activationEvent.WaitOne();
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            Dispatcher.UIThread.Post(ShowMainWindow);
        }
    }

    private static void ShowMainWindow()
    {
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop
            || desktop.MainWindow is not { } mainWindow)
        {
            return;
        }

        mainWindow.Show();

        if (mainWindow.WindowState == WindowState.Minimized)
        {
            mainWindow.WindowState = WindowState.Normal;
        }

        mainWindow.Activate();
    }
}
