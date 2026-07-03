using OpenHyperX.Core;
using OpenHyperX.Devices.CloudAlphaWireless;
using OpenHyperX.Devices.QuadCast;
using OpenHyperX.Hid;

var enumerator = new HidSharpDeviceEnumerator();
var devices = DiscoverSupportedDevices(enumerator);

if (args.Contains("--quadcast-probe", StringComparer.OrdinalIgnoreCase))
{
    await RunQuadCastProbeAsync(enumerator);
    return;
}

Console.WriteLine($"Found {devices.Length} supported HID interface(s).");
Console.WriteLine();

for (var index = 0; index < devices.Length; index++)
{
    var device = devices[index];
    Console.WriteLine($"[{index}] {device.Kind}");
    Console.WriteLine($"    Name: {device.Info.DisplayName}");
    Console.WriteLine($"    VID/PID: {device.Info.VendorIdHex}:{device.Info.ProductIdHex}");
    Console.WriteLine($"    Reports: input {device.Info.InputReportLength}, output {device.Info.OutputReportLength}, feature {device.Info.FeatureReportLength}");
    Console.WriteLine($"    Path: {device.Info.Path}");
    Console.WriteLine();
}

if (!args.Contains("--status", StringComparer.OrdinalIgnoreCase) || devices.Length == 0)
{
    Console.WriteLine("Use --status to read the first matching device.");
    return;
}

var selected = devices[0];
if (selected.Kind == DeviceKind.CloudAlphaWireless)
{
    using var client = new CloudAlphaWirelessClient(enumerator.Open(selected.Info));
    var status = await client.GetStatusAsync();

    Console.WriteLine("Cloud Alpha Wireless status");
    Console.WriteLine($"    Connected: {status.Connected}");
    Console.WriteLine($"    Battery: {Format(status.BatteryPercent, "%")}");
    Console.WriteLine($"    Charging: {status.ChargingState}");
    Console.WriteLine($"    Mic muted: {Format(status.MicMuted)}");
    Console.WriteLine($"    Mic boom: {FormatAttached(status.MicrophoneBoomAttached)}");
    Console.WriteLine($"    Microphone monitoring: {Format(status.MicrophoneMonitoringEnabled)}");
    Console.WriteLine($"    Voice prompts: {Format(status.VoicePromptEnabled)}");
    Console.WriteLine($"    Auto shutdown: {Format(status.AutoShutdownMinutes, " minute(s)")}");
    Console.WriteLine($"    Pair ID: {FormatPairId(status.PairId)}");
    Console.WriteLine($"    Product color: {FormatHex(status.ProductColor)}");
}
else
{
    using var client = new QuadCastClient(enumerator.Open(selected.Info));
    var status = await client.GetStatusAsync();

    Console.WriteLine($"{FormatModel(status.Model)} status");
    Console.WriteLine($"    Mic muted: {Format(status.Muted)}");
    Console.WriteLine($"    Polar pattern: {FormatPattern(status.PolarPattern)}");
    Console.WriteLine($"    High-pass: {Format(status.HighPassEnabled)}");
    Console.WriteLine($"    Gain: {Format(status.MicrophoneGain)}");
    Console.WriteLine($"    Mix balance: {Format(status.MixBalance)}");
    Console.WriteLine($"    Brightness: {Format(status.BrightnessPercent, "%")}");
    Console.WriteLine($"    Reverse lights: {Format(status.ReverseLights)}");
}

static DetectedDevice[] DiscoverSupportedDevices(HidSharpDeviceEnumerator enumerator)
{
    var devices = new List<DetectedDevice>();
    devices.AddRange(
        enumerator
            .ListDevices(CloudAlphaWirelessDeviceIds.Filter)
            .Where(CloudAlphaWirelessDeviceIds.IsLikelyCommandInterface)
            .Select(device => new DetectedDevice(device, DeviceKind.CloudAlphaWireless)));
    devices.AddRange(
        QuadCastDeviceIds.SelectPreferredCommandInterfaces(
                enumerator
                    .ListDevices(QuadCastDeviceIds.Filter)
                    .Where(QuadCastDeviceIds.IsLikelyCommandInterface))
            .Select(device => new DetectedDevice(device, DeviceKind.QuadCast)));

    return devices
        .GroupBy(device => device.Info.Path, StringComparer.OrdinalIgnoreCase)
        .Select(group => group.First())
        .OrderBy(device => device.Kind)
        .ThenBy(device => device.Info.DisplayName, StringComparer.OrdinalIgnoreCase)
        .ToArray();
}

static async Task RunQuadCastProbeAsync(HidSharpDeviceEnumerator enumerator)
{
    var allDevices = enumerator.ListDevices(QuadCastDeviceIds.Filter).ToArray();
    var commandDevices = allDevices
        .Where(QuadCastDeviceIds.IsLikelyCommandInterface)
        .ToArray();
    var selectedDevices = QuadCastDeviceIds
        .SelectPreferredCommandInterfaces(commandDevices)
        .Select(device => device.Path)
        .ToHashSet(StringComparer.OrdinalIgnoreCase);

    Console.WriteLine($"Found {allDevices.Length} raw QuadCast HID interface(s).");
    Console.WriteLine($"Found {commandDevices.Length} likely command interface(s).");
    Console.WriteLine();

    foreach (var device in allDevices.OrderBy(device => device.ProductId).ThenBy(device => device.Path, StringComparer.OrdinalIgnoreCase))
    {
        var known = QuadCastDeviceIds.TryGetModel(device, out var model);
        var likelyCommand = QuadCastDeviceIds.IsLikelyCommandInterface(device);
        var selected = selectedDevices.Contains(device.Path);

        Console.WriteLine($"{FormatModel(model)} interface");
        Console.WriteLine($"    Name: {device.DisplayName}");
        Console.WriteLine($"    VID/PID: {device.VendorIdHex}:{device.ProductIdHex}");
        Console.WriteLine($"    Reports: input {device.InputReportLength}, output {device.OutputReportLength}, feature {device.FeatureReportLength}");
        Console.WriteLine($"    Known model: {known}");
        Console.WriteLine($"    Likely command interface: {likelyCommand}");
        Console.WriteLine($"    Selected by app: {selected}");
        Console.WriteLine($"    Path: {device.Path}");

        if (!likelyCommand)
        {
            Console.WriteLine();
            continue;
        }

        if (QuadCastDeviceIds.UsesFeatureReports(device))
        {
            await ProbeQuadCastFeatureStatusAsync(enumerator, device);
        }
        else
        {
            await ProbeQuadCastReportStatusAsync(enumerator, device);
        }

        Console.WriteLine();
    }
}

static async Task ProbeQuadCastFeatureStatusAsync(HidSharpDeviceEnumerator enumerator, HidDeviceInfo device)
{
    Console.WriteLine("    Feature status probe:");

    try
    {
        using var client = new QuadCastClient(enumerator.Open(device));
        var status = await client.GetStatusAsync();
        Console.WriteLine($"        OK: mute {Format(status.Muted)}, pattern {FormatPattern(status.PolarPattern)}, brightness {Format(status.BrightnessPercent, "%")}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"        FAILED: {ex.GetType().Name}: {ex.Message}");
    }
}

static async Task ProbeQuadCastReportStatusAsync(HidSharpDeviceEnumerator enumerator, HidDeviceInfo device)
{
    Console.WriteLine("    Report status probes using read-only GetMicrophoneMute (0x86):");

    var variants = CreateReportProbeVariants(QuadCastCommandIds.GetMicrophoneMute, device.OutputReportLength);
    foreach (var variant in variants)
    {
        Console.WriteLine($"        {variant.Name}: write {variant.Report.Length} byte(s), bytes {FormatBytes(variant.Report, 8)}");

        try
        {
            using var transport = enumerator.Open(device);
            await transport.WriteAsync(variant.Report);

            var response = await transport.ReadAsync(TimeSpan.FromMilliseconds(750));
            if (response is null)
            {
                Console.WriteLine("            write OK, no response before timeout");
                continue;
            }

            Console.WriteLine($"            response {response.Length} byte(s): {FormatBytes(response, 16)}");
            if (QuadCastProtocol.TryGetReportValue(response, QuadCastCommandIds.GetMicrophoneMute, out var value))
            {
                Console.WriteLine($"            parsed mute value: 0x{value:X2} ({(value == 0x01 ? "muted" : "live")})");
            }
            else if (QuadCastProtocol.TryGetReportCommand(response, out var command))
            {
                Console.WriteLine($"            parsed different command: 0x{command:X2}");
            }
            else
            {
                Console.WriteLine("            response did not contain a 0x77 report marker");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"            FAILED: {ex.GetType().Name}: {ex.Message}");
        }
    }
}

static IReadOnlyList<ReportProbeVariant> CreateReportProbeVariants(byte command, int outputReportLength)
{
    var reportedLength = Math.Max(outputReportLength, 3);
    var idLength = Math.Max(outputReportLength + (outputReportLength <= 64 ? 1 : 0), 4);
    var raw64Length = Math.Max(64, 3);

    return
    [
        new ReportProbeVariant("raw reported length", CreateProbeReport(command, reportedLength, includeReportId: false)),
        new ReportProbeVariant("report-id prefixed length", CreateProbeReport(command, idLength, includeReportId: true)),
        new ReportProbeVariant("raw 64-byte fallback", CreateProbeReport(command, raw64Length, includeReportId: false))
    ];
}

static byte[] CreateProbeReport(byte command, int length, bool includeReportId)
{
    var report = new byte[length];
    var offset = includeReportId ? 1 : 0;
    report[offset] = QuadCastCommandIds.ReportMarker;
    report[offset + 1] = command;
    return report;
}

static string FormatBytes(IReadOnlyList<byte> bytes, int maxLength)
{
    return string.Join(" ", bytes.Take(maxLength).Select(value => value.ToString("X2")))
        + (bytes.Count > maxLength ? " ..." : string.Empty);
}

static string Format<T>(T? value, string suffix = "")
    where T : struct
{
    return value is null ? "--" : $"{value}{suffix}";
}

static string FormatPairId(uint? value)
{
    return value is null ? "--" : $"0x{value.Value:X8}";
}

static string FormatAttached(bool? value)
{
    return value is null ? "--" : value.Value ? "Attached" : "Detached";
}

static string FormatHex(byte? value)
{
    return value is null ? "--" : $"0x{value.Value:X2}";
}

static string FormatModel(QuadCastModel model)
{
    return model switch
    {
        QuadCastModel.QuadCastS => "QuadCast S",
        QuadCastModel.QuadCast2 => "QuadCast 2",
        QuadCastModel.QuadCast2S => "QuadCast 2 S",
        _ => "QuadCast"
    };
}

static string FormatPattern(QuadCastPolarPattern? pattern)
{
    return pattern switch
    {
        QuadCastPolarPattern.Bidirectional => "Bidirectional",
        QuadCastPolarPattern.Cardioid => "Cardioid",
        QuadCastPolarPattern.Omnidirectional => "Omnidirectional",
        QuadCastPolarPattern.Stereo => "Stereo",
        _ => "--"
    };
}

internal readonly record struct DetectedDevice(HidDeviceInfo Info, DeviceKind Kind);

internal readonly record struct ReportProbeVariant(string Name, byte[] Report);

internal enum DeviceKind
{
    CloudAlphaWireless,
    QuadCast
}
