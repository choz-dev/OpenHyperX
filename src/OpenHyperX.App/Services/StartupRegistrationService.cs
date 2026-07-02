using Microsoft.Win32;
using System.Runtime.Versioning;

namespace OpenHyperX.App.Services;

public sealed class StartupRegistrationService
{
    private const string RunKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Run";
    private const string ValueName = "OpenHyperX";
    private const string TrayArgument = "--tray";

    public bool IsSupported => OperatingSystem.IsWindows();

    public bool IsRegistered
    {
        get
        {
            if (!OperatingSystem.IsWindows())
            {
                return false;
            }

            return IsRegisteredWindows();
        }
    }

    public void SetRegistered(bool registered, bool startInTray)
    {
        if (!OperatingSystem.IsWindows())
        {
            throw new PlatformNotSupportedException("Startup registration is only supported on Windows.");
        }

        SetRegisteredWindows(registered, startInTray);
    }

    [SupportedOSPlatform("windows")]
    private static bool IsRegisteredWindows()
    {
        using var key = Registry.CurrentUser.OpenSubKey(RunKeyPath, writable: false);
        return key?.GetValue(ValueName) is string value && !string.IsNullOrWhiteSpace(value);
    }

    [SupportedOSPlatform("windows")]
    private static void SetRegisteredWindows(bool registered, bool startInTray)
    {
        using var key = Registry.CurrentUser.CreateSubKey(RunKeyPath, writable: true);
        if (registered)
        {
            key.SetValue(ValueName, BuildStartupCommand(startInTray), RegistryValueKind.String);
            return;
        }

        key.DeleteValue(ValueName, throwOnMissingValue: false);
    }

    private static string BuildStartupCommand(bool startInTray)
    {
        var command = Quote(GetExecutablePath());
        return startInTray ? $"{command} {TrayArgument}" : command;
    }

    private static string GetExecutablePath()
    {
        var appExe = Path.Combine(AppContext.BaseDirectory, "OpenHyperX.App.exe");
        if (File.Exists(appExe))
        {
            return appExe;
        }

        if (Environment.ProcessPath is { Length: > 0 } processPath
            && Path.GetExtension(processPath).Equals(".exe", StringComparison.OrdinalIgnoreCase)
            && File.Exists(processPath))
        {
            return processPath;
        }

        throw new InvalidOperationException("The app executable path could not be resolved.");
    }

    private static string Quote(string value)
    {
        return $"\"{value}\"";
    }
}
