using OpenHyperX.Core;

namespace OpenHyperX.Devices.QuadCast;

public static class QuadCastDeviceIds
{
    public const int HyperXVendorId = 0x03F0;

    public const int ProductIdQuadCastSController = 0x028C;
    public const int ProductIdQuadCastSControllerAlt = 0x068C;
    public const int ProductIdQuadCast2Command = 0x09AF;
    public const int ProductIdQuadCast2SCommand = 0x02B5;
    public const int ProductIdQuadCast2SCommandAlt = 0x03B5;

    public static readonly IReadOnlyCollection<int> VendorIds =
    [
        HyperXVendorId
    ];

    public static readonly IReadOnlyCollection<int> ProductIds =
    [
        ProductIdQuadCastSController,
        ProductIdQuadCastSControllerAlt,
        ProductIdQuadCast2Command,
        ProductIdQuadCast2SCommand,
        ProductIdQuadCast2SCommandAlt
    ];

    public static HidDeviceFilter Filter { get; } = new(null, ProductIds)
    {
        VendorIds = VendorIds
    };

    public static bool TryGetModel(HidDeviceInfo deviceInfo, out QuadCastModel model)
    {
        model = deviceInfo.ProductId switch
        {
            ProductIdQuadCastSController or ProductIdQuadCastSControllerAlt => QuadCastModel.QuadCastS,
            ProductIdQuadCast2Command => QuadCastModel.QuadCast2,
            ProductIdQuadCast2SCommand or ProductIdQuadCast2SCommandAlt => QuadCastModel.QuadCast2S,
            _ => default
        };

        return ProductIds.Contains(deviceInfo.ProductId) && VendorIds.Contains(deviceInfo.VendorId);
    }

    public static bool UsesFeatureReports(HidDeviceInfo deviceInfo)
    {
        return TryGetModel(deviceInfo, out var model) && model == QuadCastModel.QuadCastS;
    }

    public static bool UsesQuadCastSSecondSourceLayout(HidDeviceInfo deviceInfo)
    {
        return deviceInfo.ProductId is ProductIdQuadCastSController or ProductIdQuadCastSControllerAlt;
    }

    public static bool IsLikelyCommandInterface(HidDeviceInfo deviceInfo)
    {
        if (!TryGetModel(deviceInfo, out _))
        {
            return false;
        }

        return UsesFeatureReports(deviceInfo)
            ? deviceInfo.FeatureReportLength >= 18
            : deviceInfo.InputReportLength >= 3 && deviceInfo.OutputReportLength >= 3;
    }
}
