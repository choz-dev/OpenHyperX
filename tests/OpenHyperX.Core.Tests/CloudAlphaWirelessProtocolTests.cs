using OpenHyperX.Devices.CloudAlphaWireless;

namespace OpenHyperX.Core.Tests;

public sealed class CloudAlphaWirelessProtocolTests
{
    [Fact]
    public void KnownProductIdsIncludeReversedDongleIds()
    {
        Assert.Contains(0x098D, CloudAlphaWirelessDeviceIds.ProductIds);
        Assert.Contains(0x1743, CloudAlphaWirelessDeviceIds.ProductIds);
        Assert.Contains(0x1765, CloudAlphaWirelessDeviceIds.ProductIds);
    }

    [Fact]
    public void CommandIdsMatchCloudAlphaWirelessMap()
    {
        Assert.Equal(0x0B, CloudAlphaWirelessCommandIds.GetBatteryInfo);
        Assert.Equal(0x0C, CloudAlphaWirelessCommandIds.GetChargeStatus);
        Assert.Equal(0x15, CloudAlphaWirelessCommandIds.SetMicMute);
    }
}
