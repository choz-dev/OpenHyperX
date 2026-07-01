using OpenHyperX.Core;

namespace OpenHyperX.App.ViewModels;

public sealed record DeviceListItemViewModel(HidDeviceInfo DeviceInfo)
{
    public string DisplayName => DeviceInfo.DisplayName;

    public string DetailText =>
        $"{DeviceInfo.VendorIdHex}:{DeviceInfo.ProductIdHex}  In {DeviceInfo.InputReportLength} / Out {DeviceInfo.OutputReportLength}";
}
