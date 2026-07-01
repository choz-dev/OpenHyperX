using OpenHyperX.Core;

namespace OpenHyperX.Devices.CloudAlphaWireless;

public static class CloudAlphaWirelessDeviceIds
{
    public const int VendorId = 0x03F0;
    public const int ProductIdUsbDongle = 0x098D;
    public const int ProductIdDongleAlt1 = 0x1743;
    public const int ProductIdDongleAlt2 = 0x1765;

    public static readonly IReadOnlyCollection<int> ProductIds =
    [
        ProductIdUsbDongle,
        ProductIdDongleAlt1,
        ProductIdDongleAlt2
    ];

    public static HidDeviceFilter Filter { get; } = new(VendorId, ProductIds);

    public static bool IsDongle(int productId)
    {
        return ProductIds.Contains(productId);
    }

    public static bool IsLikelyCommandInterface(HidDeviceInfo deviceInfo)
    {
        return IsDongle(deviceInfo.ProductId)
            && deviceInfo.InputReportLength >= 31
            && deviceInfo.OutputReportLength >= 31;
    }
}
