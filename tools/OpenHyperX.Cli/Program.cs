using OpenHyperX.Core;
using OpenHyperX.Devices.CloudAlphaWireless;
using OpenHyperX.Devices.QuadCast;
using OpenHyperX.Hid;

var enumerator = new HidSharpDeviceEnumerator();
var devices = DiscoverSupportedDevices(enumerator);

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
        enumerator
            .ListDevices(QuadCastDeviceIds.Filter)
            .Where(QuadCastDeviceIds.IsLikelyCommandInterface)
            .Select(device => new DetectedDevice(device, DeviceKind.QuadCast)));

    return devices
        .GroupBy(device => device.Info.Path, StringComparer.OrdinalIgnoreCase)
        .Select(group => group.First())
        .OrderBy(device => device.Kind)
        .ThenBy(device => device.Info.DisplayName, StringComparer.OrdinalIgnoreCase)
        .ToArray();
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

internal enum DeviceKind
{
    CloudAlphaWireless,
    QuadCast
}
