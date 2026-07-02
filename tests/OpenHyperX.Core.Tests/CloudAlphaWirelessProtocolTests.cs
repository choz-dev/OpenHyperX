using OpenHyperX.Core;
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
        Assert.Equal(0x08, CloudAlphaWirelessCommandIds.GetMicBoomStatus);
        Assert.Equal(0x0E, CloudAlphaWirelessCommandIds.GetProductColor);
        Assert.Equal(0x15, CloudAlphaWirelessCommandIds.SetMicMute);
    }

    [Fact]
    public async Task GetMicrophoneBoomAttachedAsyncParsesOneBytePayload()
    {
        await using var client = new CloudAlphaWirelessClient(
            new FakeTransport(
                new Dictionary<byte, byte[]>
                {
                    [CloudAlphaWirelessCommandIds.GetMicBoomStatus] =
                        HyperXPacket.CreateReport(CloudAlphaWirelessCommandIds.GetMicBoomStatus, 32, [0x01])
                }));

        var attached = await client.GetMicrophoneBoomAttachedAsync();

        Assert.True(attached);
    }

    [Fact]
    public async Task GetProductColorAsyncParsesOneBytePayload()
    {
        await using var client = new CloudAlphaWirelessClient(
            new FakeTransport(
                new Dictionary<byte, byte[]>
                {
                    [CloudAlphaWirelessCommandIds.GetProductColor] =
                        HyperXPacket.CreateReport(CloudAlphaWirelessCommandIds.GetProductColor, 32, [0x03])
                }));

        var color = await client.GetProductColorAsync();

        Assert.Equal((byte)0x03, color);
    }

    [Fact]
    public async Task GetStatusAsyncIncludesMicBoomAndProductColor()
    {
        await using var client = new CloudAlphaWirelessClient(
            new FakeTransport(
                new Dictionary<byte, byte[]>
                {
                    [CloudAlphaWirelessCommandIds.GetWirelessState] =
                        HyperXPacket.CreateReport(CloudAlphaWirelessCommandIds.GetWirelessState, 32, [0x02]),
                    [CloudAlphaWirelessCommandIds.GetPairingInfoDongle] =
                        HyperXPacket.CreateReport(CloudAlphaWirelessCommandIds.GetPairingInfoDongle, 32, [0x00, 0x00, 0x78, 0x56, 0x34, 0x12]),
                    [CloudAlphaWirelessCommandIds.GetAutoShutdown] =
                        HyperXPacket.CreateReport(CloudAlphaWirelessCommandIds.GetAutoShutdown, 32, [0x14]),
                    [CloudAlphaWirelessCommandIds.GetBatteryInfo] =
                        HyperXPacket.CreateReport(CloudAlphaWirelessCommandIds.GetBatteryInfo, 32, [0x57]),
                    [CloudAlphaWirelessCommandIds.GetChargeStatus] =
                        HyperXPacket.CreateReport(CloudAlphaWirelessCommandIds.GetChargeStatus, 32, [0x00]),
                    [CloudAlphaWirelessCommandIds.GetSidetoneStatus] =
                        HyperXPacket.CreateReport(CloudAlphaWirelessCommandIds.GetSidetoneStatus, 32, [0x01]),
                    [CloudAlphaWirelessCommandIds.GetMicMute] =
                        HyperXPacket.CreateReport(CloudAlphaWirelessCommandIds.GetMicMute, 32, [0x00]),
                    [CloudAlphaWirelessCommandIds.GetVoicePromptStatus] =
                        HyperXPacket.CreateReport(CloudAlphaWirelessCommandIds.GetVoicePromptStatus, 32, [0x01]),
                    [CloudAlphaWirelessCommandIds.GetMicBoomStatus] =
                        HyperXPacket.CreateReport(CloudAlphaWirelessCommandIds.GetMicBoomStatus, 32, [0x01]),
                    [CloudAlphaWirelessCommandIds.GetProductColor] =
                        HyperXPacket.CreateReport(CloudAlphaWirelessCommandIds.GetProductColor, 32, [0x03])
                }));

        var status = await client.GetStatusAsync();

        Assert.True(status.MicrophoneBoomAttached);
        Assert.Equal((byte)0x03, status.ProductColor);
    }

    private sealed class FakeTransport : IHyperXTransport
    {
        private readonly IReadOnlyDictionary<byte, byte[]> _responses;

        public FakeTransport(IReadOnlyDictionary<byte, byte[]> responses)
        {
            _responses = responses;
        }

        public HidDeviceInfo DeviceInfo { get; } =
            new("fake", CloudAlphaWirelessDeviceIds.VendorId, CloudAlphaWirelessDeviceIds.ProductIdUsbDongle, "Fake", 32, 32);

        public int InputReportLength => 32;

        public int OutputReportLength => 32;

        public int FeatureReportLength => 32;

        public Task WriteAsync(byte[] report, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task<byte[]?> ReadAsync(TimeSpan timeout, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<byte[]?>(null);
        }

        public Task<byte[]?> QueryAsync(
            byte[] report,
            byte expectedCommand,
            TimeSpan timeout,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_responses.GetValueOrDefault(expectedCommand));
        }

        public Task SetFeatureReportAsync(byte[] report, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task<byte[]?> GetFeatureReportAsync(byte reportId, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<byte[]?>(null);
        }

        public void Dispose()
        {
        }

        public ValueTask DisposeAsync()
        {
            return ValueTask.CompletedTask;
        }
    }
}
