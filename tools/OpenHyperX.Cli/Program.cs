using OpenHyperX.Devices.CloudAlphaWireless;
using OpenHyperX.Hid;

var devices = new HidSharpDeviceEnumerator()
    .ListDevices(CloudAlphaWirelessDeviceIds.Filter)
    .Where(CloudAlphaWirelessDeviceIds.IsLikelyCommandInterface)
    .ToArray();

Console.WriteLine($"Found {devices.Length} Cloud Alpha Wireless HID interface(s).");
Console.WriteLine();

for (var index = 0; index < devices.Length; index++)
{
    var device = devices[index];
    Console.WriteLine($"[{index}] {device.DisplayName}");
    Console.WriteLine($"    VID/PID: {device.VendorIdHex}:{device.ProductIdHex}");
    Console.WriteLine($"    Reports: input {device.InputReportLength}, output {device.OutputReportLength}");
    Console.WriteLine($"    Path: {device.Path}");
    Console.WriteLine();
}

if (!args.Contains("--status", StringComparer.OrdinalIgnoreCase) || devices.Length == 0)
{
    Console.WriteLine("Use --status to read the first matching device.");
    return;
}

var selected = devices[0];
using var client = new CloudAlphaWirelessClient(new HidSharpDeviceEnumerator().Open(selected));
var status = await client.GetStatusAsync();

Console.WriteLine("Status");
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
