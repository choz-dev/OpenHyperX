using Microsoft.Win32;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;

namespace OpenHyperX.App.Services;

public sealed class DtsSpatialAudioService
{
    private const string DriverStorePath = @"C:\Windows\System32\DriverStore\FileRepository";
    private const string WindowsAppsPath = @"C:\Program Files\WindowsApps";
    private const string RenderEndpointsKeyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\MMDevices\Audio\Render";
    private const string ServicesKeyPath = @"SYSTEM\CurrentControlSet\Services";
    private const string DtsServiceName = "DtsHPXV2Apo4Service";
    private const string EffectsPropertyName = "{d04e05a6-594b-4fb6-a80d-01af5eed7d1d},13";
    private const string ProcessingModesPropertyName = "{d3993a3f-99c2-4402-b5ec-a92a0367664b},5";
    private const string DtsEffectClassId = "{8B778F49-83A0-4EE9-896A-ED52903EDF1F}";
    private const string RenderDeviceIdPrefix = "{0.0.0.00000000}.";

    private static readonly Guid DtsApoSystemClassId = new("7766EC50-C2D3-466F-BC7A-9C339C5908A2");

    private static readonly string[] DriverPackageNames =
    [
        "dtshpxv2_hyperx_ext.inf",
        "dtshpxv2apo4xservice.inf",
        "dtsapo4xhpxv2x64.inf"
    ];

    private static readonly string[] RequiredFxPropertySubkeys =
    [
        "{0CC8F6D7-FA5E-448B-AA61-01227B03E43F}",
        "{13C6D2E0-9A3D-4808-BD0B-1F50AAD83D99}",
        "{13CBF472-DD1D-4055-A74D-6055827B3D91}",
        "{20AF49E8-939F-4843-9C6B-1DCCD83B11DC}",
        "{B970A8EF-5733-434D-B624-770402E2500B}"
    ];

    public DtsSpatialAudioStatus GetStatus()
    {
        if (!OperatingSystem.IsWindows())
        {
            return DtsSpatialAudioStatus.Unsupported(DriverPackageNames.Length);
        }

        return GetWindowsStatus();
    }

    public async Task<DtsSpatialAudioApplyResult> SetSpatialAudioEnabledAsync(bool enabled)
    {
        if (!OperatingSystem.IsWindows())
        {
            var unsupportedStatus = DtsSpatialAudioStatus.Unsupported(DriverPackageNames.Length);
            return new DtsSpatialAudioApplyResult(
                Success: false,
                RestartRequired: false,
                Message: "DTS spatial audio is only supported on Windows.",
                Status: unsupportedStatus);
        }

        return await SetSpatialAudioEnabledWindowsAsync(enabled).ConfigureAwait(false);
    }

    [SupportedOSPlatform("windows")]
    private static DtsSpatialAudioStatus GetWindowsStatus()
    {
        var driverPackageCount = CountDetectedDriverPackages();
        var apoComRegistered = IsDtsApoComRegistered();
        var serviceRegistered = IsDtsServiceRegistered();
        var serviceRunning = IsDtsServiceRunning();
        var renderEndpointStatus = CountRenderEndpoints();
        var driverSourceAvailable = FindDriverSource() is not null;

        return new DtsSpatialAudioStatus(
            IsSupported: true,
            DetectedDriverPackageCount: driverPackageCount,
            RequiredDriverPackageCount: DriverPackageNames.Length,
            ApoComRegistered: apoComRegistered,
            ServiceRegistered: serviceRegistered,
            ServiceRunning: serviceRunning,
            DetectedRenderEndpointCount: renderEndpointStatus.Detected,
            ConfiguredRenderEndpointCount: renderEndpointStatus.Configured,
            DriverSourceAvailable: driverSourceAvailable);
    }

    [SupportedOSPlatform("windows")]
    private static async Task<DtsSpatialAudioApplyResult> SetSpatialAudioEnabledWindowsAsync(bool enabled)
    {
        var status = GetWindowsStatus();
        if (!enabled)
        {
            if (status.ApoComRegistered && status.DetectedRenderEndpointCount > 0)
            {
                var disableResult = TrySetApoEnabled(enabled: false);
                status = GetWindowsStatus();
                return new DtsSpatialAudioApplyResult(
                    disableResult.Success,
                    RestartRequired: false,
                    disableResult.Success ? "DTS spatial audio disabled." : disableResult.Message,
                    status);
            }

            return new DtsSpatialAudioApplyResult(
                Success: true,
                RestartRequired: false,
                Message: "DTS spatial audio disabled.",
                Status: status);
        }

        if (!status.DriverPackagesPresent || !status.ApoComRegistered || !status.ServiceRegistered)
        {
            var source = FindDriverSource();
            if (source is null)
            {
                return new DtsSpatialAudioApplyResult(
                    Success: false,
                    RestartRequired: false,
                    Message: "DTS driver source was not found. Install HyperX NGenuity or place the AudioDTS driver folder beside OpenHyperX.",
                    Status: status);
            }

            var installResult = await InstallDriverPackagesElevatedAsync(source).ConfigureAwait(false);
            var refreshedStatus = GetWindowsStatus();
            if (!installResult.Success)
            {
                return new DtsSpatialAudioApplyResult(
                    Success: false,
                    RestartRequired: false,
                    Message: $"DTS driver installation failed with exit code {installResult.ExitCode}.",
                    Status: refreshedStatus);
            }

            return new DtsSpatialAudioApplyResult(
                Success: true,
                RestartRequired: true,
                Message: "DTS driver installed. Restart your PC before DTS spatial audio can be enabled.",
                Status: refreshedStatus);
        }

        if (!status.ServiceRunning)
        {
            var serviceResult = await EnsureServiceReadyElevatedAsync().ConfigureAwait(false);
            status = GetWindowsStatus();
            if (!serviceResult.Success)
            {
                return new DtsSpatialAudioApplyResult(
                    Success: false,
                    RestartRequired: false,
                    Message: $"DTS service could not be started. Exit code {serviceResult.ExitCode}.",
                    Status: status);
            }
        }

        var apoResult = TrySetApoEnabled(enabled: true);
        status = GetWindowsStatus();
        return new DtsSpatialAudioApplyResult(
            Success: apoResult.Success,
            RestartRequired: false,
            Message: apoResult.Success ? "DTS spatial audio enabled." : apoResult.Message,
            Status: status);
    }

    private static int CountDetectedDriverPackages()
    {
        if (!Directory.Exists(DriverStorePath))
        {
            return 0;
        }

        var detected = 0;
        foreach (var packageName in DriverPackageNames)
        {
            if (DriverPackageExists(packageName))
            {
                detected++;
            }
        }

        return detected;
    }

    private static bool DriverPackageExists(string packageName)
    {
        try
        {
            return Directory.EnumerateDirectories(DriverStorePath, $"{packageName}_*").Any();
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or System.Security.SecurityException)
        {
            return false;
        }
    }

    [SupportedOSPlatform("windows")]
    private static bool IsDtsApoComRegistered()
    {
        try
        {
            using var classesRoot = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry64);
            using var clsidKey = classesRoot.OpenSubKey($@"CLSID\{DtsApoSystemClassId:B}", writable: false);
            return clsidKey is not null;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or System.Security.SecurityException)
        {
            return false;
        }
    }

    [SupportedOSPlatform("windows")]
    private static bool IsDtsServiceRegistered()
    {
        try
        {
            using var localMachine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            using var serviceKey = localMachine.OpenSubKey($@"{ServicesKeyPath}\{DtsServiceName}", writable: false);
            return serviceKey is not null;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or System.Security.SecurityException)
        {
            return false;
        }
    }

    private static bool IsDtsServiceRunning()
    {
        var result = RunProcess("sc.exe", $"query {DtsServiceName}", timeout: TimeSpan.FromSeconds(2));
        return result.Success
            && result.Output.Contains("RUNNING", StringComparison.OrdinalIgnoreCase);
    }

    private static DtsDriverSource? FindDriverSource()
    {
        foreach (var candidate in GetDriverSourceCandidates())
        {
            var infPaths = DriverPackageNames
                .Select(packageName => Path.Combine(candidate, packageName))
                .ToArray();

            if (infPaths.All(File.Exists))
            {
                return new DtsDriverSource(candidate, infPaths);
            }
        }

        return null;
    }

    private static IEnumerable<string> GetDriverSourceCandidates()
    {
        var localAudioDtsPath = Path.Combine(AppContext.BaseDirectory, "AudioDTS");
        if (Directory.Exists(localAudioDtsPath))
        {
            yield return localAudioDtsPath;
        }

        if (!Directory.Exists(WindowsAppsPath))
        {
            yield break;
        }

        IEnumerable<DirectoryInfo> packageDirectories;
        try
        {
            packageDirectories = new DirectoryInfo(WindowsAppsPath)
                .EnumerateDirectories("33C30B79.HyperXNGenuity_*_x64__0a78dr3hq0pvt")
                .OrderByDescending(directory => directory.LastWriteTimeUtc)
                .ToArray();
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or System.Security.SecurityException)
        {
            yield break;
        }

        foreach (var packageDirectory in packageDirectories)
        {
            var audioDtsPath = Path.Combine(packageDirectory.FullName, "Assets", "Native", "AudioDTS");
            if (Directory.Exists(audioDtsPath))
            {
                yield return audioDtsPath;
            }
        }
    }

    private static Task<ProcessResult> InstallDriverPackagesElevatedAsync(DtsDriverSource source)
    {
        var commands = source.InfPaths.Select(infPath => $"""pnputil.exe /add-driver "{infPath}" /install""");
        return RunElevatedCommandScriptAsync(commands);
    }

    private static Task<ProcessResult> EnsureServiceReadyElevatedAsync()
    {
        var commands = new[]
        {
            $"sc.exe config {DtsServiceName} start= auto",
            "if %ERRORLEVEL% NEQ 0 exit /b %ERRORLEVEL%",
            $"sc.exe start {DtsServiceName}",
            "if %ERRORLEVEL% EQU 1056 exit /b 0",
            "if %ERRORLEVEL% NEQ 0 exit /b %ERRORLEVEL%"
        };

        return RunElevatedCommandScriptAsync(commands, checkAfterEachCommand: false);
    }

    private static async Task<ProcessResult> RunElevatedCommandScriptAsync(
        IEnumerable<string> commands,
        bool checkAfterEachCommand = true)
    {
        var scriptPath = Path.Combine(Path.GetTempPath(), $"OpenHyperX-DTS-{Guid.NewGuid():N}.cmd");
        var script = new StringBuilder()
            .AppendLine("@echo off")
            .AppendLine("setlocal");

        foreach (var command in commands)
        {
            script.AppendLine(command);
            if (checkAfterEachCommand)
            {
                script.AppendLine("if %ERRORLEVEL% NEQ 0 exit /b %ERRORLEVEL%");
            }
        }

        script.AppendLine("exit /b 0");
        await File.WriteAllTextAsync(scriptPath, script.ToString(), Encoding.ASCII).ConfigureAwait(false);

        try
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c \"{scriptPath}\"",
                UseShellExecute = true,
                Verb = "runas",
                WindowStyle = ProcessWindowStyle.Hidden
            };

            using var process = Process.Start(processStartInfo);
            if (process is null)
            {
                return new ProcessResult(Success: false, ExitCode: -1, Output: string.Empty);
            }

            await process.WaitForExitAsync().ConfigureAwait(false);
            return new ProcessResult(
                Success: process.ExitCode == 0,
                ExitCode: process.ExitCode,
                Output: string.Empty);
        }
        finally
        {
            try
            {
                File.Delete(scriptPath);
            }
            catch
            {
            }
        }
    }

    private static ProcessResult RunProcess(string fileName, string arguments, TimeSpan timeout)
    {
        try
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            using var process = Process.Start(processStartInfo);
            if (process is null)
            {
                return new ProcessResult(Success: false, ExitCode: -1, Output: string.Empty);
            }

            if (!process.WaitForExit((int)timeout.TotalMilliseconds))
            {
                process.Kill(entireProcessTree: true);
                return new ProcessResult(Success: false, ExitCode: -1, Output: string.Empty);
            }

            var output = process.StandardOutput.ReadToEnd() + process.StandardError.ReadToEnd();
            return new ProcessResult(
                Success: process.ExitCode == 0,
                ExitCode: process.ExitCode,
                Output: output);
        }
        catch
        {
            return new ProcessResult(Success: false, ExitCode: -1, Output: string.Empty);
        }
    }

    [SupportedOSPlatform("windows")]
    private static DtsRenderEndpointStatus CountRenderEndpoints()
    {
        try
        {
            using var localMachine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            using var renderEndpointsKey = localMachine.OpenSubKey(RenderEndpointsKeyPath, writable: false);
            if (renderEndpointsKey is null)
            {
                return new DtsRenderEndpointStatus(Detected: 0, Configured: 0);
            }

            var detected = 0;
            var configured = 0;
            foreach (var endpointKeyName in renderEndpointsKey.GetSubKeyNames())
            {
                var endpointStatus = GetRenderEndpointStatus(renderEndpointsKey, endpointKeyName);
                if (endpointStatus.Detected > 0)
                {
                    detected++;
                }

                if (endpointStatus.Configured > 0)
                {
                    configured++;
                }
            }

            return new DtsRenderEndpointStatus(detected, configured);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or System.Security.SecurityException)
        {
            return new DtsRenderEndpointStatus(Detected: 0, Configured: 0);
        }
    }

    [SupportedOSPlatform("windows")]
    private static DtsRenderEndpointStatus GetRenderEndpointStatus(RegistryKey renderEndpointsKey, string endpointKeyName)
    {
        try
        {
            using var fxPropertiesKey = renderEndpointsKey.OpenSubKey($@"{endpointKeyName}\FxProperties", writable: false);
            if (fxPropertiesKey is null)
            {
                return new DtsRenderEndpointStatus(Detected: 0, Configured: 0);
            }

            var hasDtsEffect = ContainsDtsEffect(fxPropertiesKey);
            var isConfigured = hasDtsEffect
                && (HasEndpointRegistryConfiguration(fxPropertiesKey)
                    || IsEndpointDspConfigured(endpointKeyName));

            return new DtsRenderEndpointStatus(
                Detected: hasDtsEffect ? 1 : 0,
                Configured: isConfigured ? 1 : 0);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or System.Security.SecurityException)
        {
            return new DtsRenderEndpointStatus(Detected: 0, Configured: 0);
        }
    }

    [SupportedOSPlatform("windows")]
    private static bool ContainsDtsEffect(RegistryKey fxPropertiesKey)
    {
        return fxPropertiesKey.GetValue(EffectsPropertyName) is string[] effects
            && effects.Any(effect => string.Equals(effect, DtsEffectClassId, StringComparison.OrdinalIgnoreCase));
    }

    [SupportedOSPlatform("windows")]
    private static bool HasExpectedProcessingModes(RegistryKey fxPropertiesKey)
    {
        return fxPropertiesKey.GetValue(ProcessingModesPropertyName) is string[] processingModes
            && processingModes.Length == 6;
    }

    [SupportedOSPlatform("windows")]
    private static bool HasRequiredFxPropertySubkeys(RegistryKey fxPropertiesKey)
    {
        var subKeyNames = fxPropertiesKey.GetSubKeyNames();
        return RequiredFxPropertySubkeys.All(requiredSubKey =>
            subKeyNames.Any(subKey => string.Equals(subKey, requiredSubKey, StringComparison.OrdinalIgnoreCase)));
    }

    [SupportedOSPlatform("windows")]
    private static bool HasEndpointRegistryConfiguration(RegistryKey fxPropertiesKey)
    {
        return HasExpectedProcessingModes(fxPropertiesKey)
            && HasRequiredFxPropertySubkeys(fxPropertiesKey);
    }

    [SupportedOSPlatform("windows")]
    private static DtsApoApplyResult TrySetApoEnabled(bool enabled)
    {
        var endpointKeys = GetDtsMarkedRenderEndpointKeys();
        if (endpointKeys.Count == 0)
        {
            return new DtsApoApplyResult(
                Success: false,
                Message: "No DTS render endpoint was found.");
        }

        object? systemObject = null;
        IDtsEndpointEnumerator? endpointEnumerator = null;
        try
        {
            var systemType = Type.GetTypeFromCLSID(DtsApoSystemClassId, throwOnError: false);
            if (systemType is null)
            {
                return new DtsApoApplyResult(
                    Success: false,
                    Message: "DTS APO controller is not registered.");
            }

            systemObject = Activator.CreateInstance(systemType);
            if (systemObject is not IDtsApoSystem dtsSystem)
            {
                return new DtsApoApplyResult(
                    Success: false,
                    Message: "DTS APO controller could not be opened.");
            }

            dtsSystem.InitializeSystem(null);
            endpointEnumerator = dtsSystem.EnumerateDtsEndpoints();

            var changedEndpoints = 0;
            foreach (var endpointKey in endpointKeys)
            {
                if (TrySetEndpointApoState(endpointEnumerator, BuildRenderDeviceId(endpointKey), enabled))
                {
                    changedEndpoints++;
                }
            }

            if (changedEndpoints == 0)
            {
                return new DtsApoApplyResult(
                    Success: false,
                    Message: "DTS APO endpoint could not be activated.");
            }

            return new DtsApoApplyResult(
                Success: true,
                Message: enabled
                    ? "DTS spatial audio enabled."
                    : "DTS spatial audio disabled.");
        }
        catch (Exception ex)
        {
            return new DtsApoApplyResult(
                Success: false,
                Message: $"DTS APO update failed: {ex.Message}");
        }
        finally
        {
            ReleaseComObject(endpointEnumerator);
            ReleaseComObject(systemObject);
        }
    }

    [SupportedOSPlatform("windows")]
    private static bool TrySetEndpointApoState(IDtsEndpointEnumerator endpointEnumerator, string renderDeviceId, bool enabled)
    {
        IDtsEndpointInfo? endpoint = null;
        try
        {
            endpoint = endpointEnumerator.GetEndpointFromDevice(renderDeviceId);
            if (endpoint is null)
            {
                return false;
            }

            endpoint.SetConfig(enabled ? DtsApoEndpointConfig.Dsp : DtsApoEndpointConfig.None);
            // Clearing SpatialEnable can crash inside the DTS APO COM server; SetConfig(None) is the disable path.
            if (enabled)
            {
                TrySetCurrentOperationMode(endpoint);
            }

            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            ReleaseComObject(endpoint);
        }
    }

    [SupportedOSPlatform("windows")]
    private static bool TrySetCurrentOperationMode(IDtsEndpointInfo endpoint)
    {
        IDtsOperatingMode? mode = null;
        try
        {
            mode = endpoint.GetOpMode();
            var changed = false;

            if (mode is null)
            {
                return false;
            }

            changed |= TrySetOperationModeValue(() => mode.AutoContentModeEnable = 0);
            changed |= TrySetOperationModeValue(() => mode.ApoEnable = 1);
            _ = TrySetOperationModeValue(() => mode.SpatialEnable = 1);

            return changed;
        }
        catch
        {
            return false;
        }
        finally
        {
            ReleaseComObject(mode);
        }
    }

    private static bool TrySetOperationModeValue(Action setter)
    {
        try
        {
            setter();
            return true;
        }
        catch
        {
            return false;
        }
    }

    [SupportedOSPlatform("windows")]
    private static bool IsEndpointDspConfigured(string endpointKey)
    {
        object? systemObject = null;
        IDtsEndpointEnumerator? endpointEnumerator = null;
        IDtsEndpointInfo? endpoint = null;

        try
        {
            var systemType = Type.GetTypeFromCLSID(DtsApoSystemClassId, throwOnError: false);
            if (systemType is null)
            {
                return false;
            }

            systemObject = Activator.CreateInstance(systemType);
            if (systemObject is not IDtsApoSystem dtsSystem)
            {
                return false;
            }

            dtsSystem.InitializeSystem(null);
            endpointEnumerator = dtsSystem.EnumerateDtsEndpoints();
            endpoint = endpointEnumerator.GetEndpointFromDevice(BuildRenderDeviceId(endpointKey));
            return endpoint?.GetConfig() == DtsApoEndpointConfig.Dsp;
        }
        catch
        {
            return false;
        }
        finally
        {
            ReleaseComObject(endpoint);
            ReleaseComObject(endpointEnumerator);
            ReleaseComObject(systemObject);
        }
    }

    [SupportedOSPlatform("windows")]
    private static IReadOnlyList<string> GetDtsMarkedRenderEndpointKeys()
    {
        try
        {
            using var localMachine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            using var renderEndpointsKey = localMachine.OpenSubKey(RenderEndpointsKeyPath, writable: false);
            if (renderEndpointsKey is null)
            {
                return [];
            }

            var endpointKeys = new List<string>();
            foreach (var endpointKeyName in renderEndpointsKey.GetSubKeyNames())
            {
                using var fxPropertiesKey = renderEndpointsKey.OpenSubKey($@"{endpointKeyName}\FxProperties", writable: false);
                if (fxPropertiesKey is not null && ContainsDtsEffect(fxPropertiesKey))
                {
                    endpointKeys.Add(endpointKeyName);
                }
            }

            return endpointKeys;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or System.Security.SecurityException)
        {
            return [];
        }
    }

    private static string BuildRenderDeviceId(string endpointKey)
    {
        return endpointKey.StartsWith(RenderDeviceIdPrefix, StringComparison.OrdinalIgnoreCase)
            ? endpointKey
            : $"{RenderDeviceIdPrefix}{endpointKey}";
    }

    [SupportedOSPlatform("windows")]
    private static void ReleaseComObject(object? value)
    {
        if (value is not null && Marshal.IsComObject(value))
        {
            Marshal.ReleaseComObject(value);
        }
    }
}

public sealed record DtsSpatialAudioStatus(
    bool IsSupported,
    int DetectedDriverPackageCount,
    int RequiredDriverPackageCount,
    bool ApoComRegistered,
    bool ServiceRegistered,
    bool ServiceRunning,
    int DetectedRenderEndpointCount,
    int ConfiguredRenderEndpointCount,
    bool DriverSourceAvailable)
{
    public bool DriverPackagesPresent =>
        DetectedDriverPackageCount >= RequiredDriverPackageCount && RequiredDriverPackageCount > 0;

    public bool AnySignalDetected =>
        DetectedDriverPackageCount > 0
        || ApoComRegistered
        || DetectedRenderEndpointCount > 0
        || ConfiguredRenderEndpointCount > 0;

    public string Summary
    {
        get
        {
            if (!IsSupported)
            {
                return "Windows only";
            }

            if (DriverPackagesPresent && ApoComRegistered && ConfiguredRenderEndpointCount > 0)
            {
                return ServiceRunning ? "Ready" : "Service stopped";
            }

            if (DriverPackagesPresent && ApoComRegistered && ServiceRegistered && DetectedRenderEndpointCount > 0)
            {
                return ServiceRunning ? "Available" : "Service stopped";
            }

            return AnySignalDetected ? "Partial install" : "Not detected";
        }
    }

    public static DtsSpatialAudioStatus Unsupported(int requiredDriverPackageCount)
    {
        return new DtsSpatialAudioStatus(
            IsSupported: false,
            DetectedDriverPackageCount: 0,
            RequiredDriverPackageCount: requiredDriverPackageCount,
            ApoComRegistered: false,
            ServiceRegistered: false,
            ServiceRunning: false,
            DetectedRenderEndpointCount: 0,
            ConfiguredRenderEndpointCount: 0,
            DriverSourceAvailable: false);
    }
}

internal readonly record struct DtsRenderEndpointStatus(int Detected, int Configured);

public sealed record DtsSpatialAudioApplyResult(
    bool Success,
    bool RestartRequired,
    string Message,
    DtsSpatialAudioStatus Status);

internal sealed record DtsDriverSource(string DirectoryPath, IReadOnlyList<string> InfPaths);

internal sealed record ProcessResult(bool Success, int ExitCode, string Output);

internal sealed record DtsApoApplyResult(bool Success, string Message);

internal enum DtsApoEndpointConfig
{
    None = 0,
    Dsp = 1
}

[ComImport]
[Guid("7A00E0CA-5B10-4F6D-8C20-ED4A1A288F32")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface IDtsApoSystem
{
    [return: MarshalAs(UnmanagedType.Interface)]
    IDtsEndpointEnumerator EnumerateDtsEndpoints();

    void InitializeSystem([MarshalAs(UnmanagedType.BStr)] string? apoPath);
}

[ComImport]
[Guid("AF721805-05A3-4D9C-9946-06999051168F")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface IDtsEndpointEnumerator
{
    void _VtblGap1_2();

    [return: MarshalAs(UnmanagedType.Interface)]
    IDtsEndpointInfo? GetEndpointFromDevice([MarshalAs(UnmanagedType.BStr)] string deviceId);
}

[ComImport]
[Guid("6F079AA4-8604-4260-9F29-525B3124350B")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface IDtsEndpointInfo
{
    string FriendlyName { get; }

    void _VtblGap1_2();

    [return: MarshalAs(UnmanagedType.Interface)]
    IDtsApoOpModeEnumerator GetOpModeEnumerator();

    void _VtblGap2_1();

    void SetOpMode([MarshalAs(UnmanagedType.Interface)] IDtsOperatingMode mode);

    [return: MarshalAs(UnmanagedType.Interface)]
    IDtsOperatingMode? GetOpMode();

    DtsApoEndpointConfig GetConfig();

    void SetConfig(DtsApoEndpointConfig config);
}

[ComImport]
[Guid("37EC876D-B530-496E-8940-44053EA7D3EF")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface IDtsApoOpModeEnumerator
{
    [return: MarshalAs(UnmanagedType.Interface)]
    IDtsOperatingMode? Next();
}

[ComImport]
[Guid("93D5CED3-DB72-475E-9EA7-AEAED0BC2AA2")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface IDtsOperatingMode
{
    void _VtblGap1_3();

    int ApoEnable { get; set; }

    int AutoContentModeEnable { get; set; }

    void _VtblGap2_4();

    int SpatialEnable { get; set; }
}
