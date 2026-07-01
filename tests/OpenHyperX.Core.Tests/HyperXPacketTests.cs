using OpenHyperX.Core;

namespace OpenHyperX.Core.Tests;

public sealed class HyperXPacketTests
{
    [Fact]
    public void CreateReportPrefixesReportIdAndHyperXHeader()
    {
        var report = HyperXPacket.CreateReport(0x0B, 32);

        Assert.Equal(32, report.Length);
        Assert.Equal(0x00, report[0]);
        Assert.Equal(0x21, report[1]);
        Assert.Equal(0xBB, report[2]);
        Assert.Equal(0x0B, report[3]);
    }

    [Fact]
    public void CreateRawReportStartsWithHyperXHeader()
    {
        var report = HyperXPacket.CreateRawReport(0x0B, 31);

        Assert.Equal(31, report.Length);
        Assert.Equal(0x21, report[0]);
        Assert.Equal(0xBB, report[1]);
        Assert.Equal(0x0B, report[2]);
    }

    [Fact]
    public void TryGetCommandAcceptsRawNgenuityBuffer()
    {
        var response = new byte[] { 0x21, 0xBB, 0x0B, 87 };

        Assert.True(HyperXPacket.TryGetCommand(response, out var command));
        Assert.Equal(0x0B, command);
    }

    [Fact]
    public void TryGetPayloadAcceptsReportIdPrefixedBuffer()
    {
        var response = new byte[] { 0x00, 0x21, 0xBB, 0x0B, 87 };

        Assert.True(HyperXPacket.TryGetPayload(response, out var payload));
        Assert.Equal(87, payload[0]);
    }
}
