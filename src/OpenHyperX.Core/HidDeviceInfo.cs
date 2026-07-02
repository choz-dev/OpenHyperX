namespace OpenHyperX.Core;

public sealed record HidDeviceInfo(
    string Path,
    int VendorId,
    int ProductId,
    string DisplayName,
    int InputReportLength,
    int OutputReportLength,
    int FeatureReportLength = 0)
{
    public string VendorIdHex => $"0x{VendorId:X4}";

    public string ProductIdHex => $"0x{ProductId:X4}";
}
