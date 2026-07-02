using HidSharp;
using OpenHyperX.Core;

namespace OpenHyperX.Hid;

public sealed class HidSharpDeviceEnumerator : IHidDeviceEnumerator
{
    public IReadOnlyList<HidDeviceInfo> ListDevices(HidDeviceFilter filter)
    {
        var vendorId = filter.VendorId
            ?? (filter.VendorIds is { Count: 1 } vendorIds ? vendorIds.First() : null);

        var devices = DeviceList.Local
            .GetHidDevices(vendorId, null, null, null)
            .Where(device => filter.Matches(device.VendorID, device.ProductID))
            .OrderBy(device => device.ProductID)
            .ThenBy(device => device.DevicePath, StringComparer.OrdinalIgnoreCase)
            .Select(CreateInfo)
            .ToArray();

        return devices;
    }

    public IHyperXTransport Open(HidDeviceInfo deviceInfo)
    {
        var device = DeviceList.Local
            .GetHidDevices(deviceInfo.VendorId, deviceInfo.ProductId, null, null)
            .FirstOrDefault(candidate => string.Equals(candidate.DevicePath, deviceInfo.Path, StringComparison.OrdinalIgnoreCase));

        if (device is null)
        {
            throw new InvalidOperationException("The selected HID device is no longer available.");
        }

        if (!device.TryOpen(out var stream))
        {
            throw new InvalidOperationException("The selected HID device could not be opened.");
        }

        stream.ReadTimeout = 1000;
        stream.WriteTimeout = 1000;

        return new HidSharpTransport(CreateInfo(device), stream);
    }

    private static HidDeviceInfo CreateInfo(HidDevice device)
    {
        return new HidDeviceInfo(
            device.DevicePath,
            device.VendorID,
            device.ProductID,
            GetFriendlyName(device),
            device.GetMaxInputReportLength(),
            device.GetMaxOutputReportLength(),
            device.GetMaxFeatureReportLength());
    }

    private static string GetFriendlyName(HidDevice device)
    {
        try
        {
            var name = device.GetFriendlyName();
            return string.IsNullOrWhiteSpace(name) ? "HyperX HID Device" : name;
        }
        catch
        {
            return "HyperX HID Device";
        }
    }
}
