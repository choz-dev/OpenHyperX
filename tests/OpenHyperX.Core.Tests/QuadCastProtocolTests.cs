using OpenHyperX.Core;
using OpenHyperX.Devices.QuadCast;

namespace OpenHyperX.Core.Tests;

public sealed class QuadCastProtocolTests
{
    [Fact]
    public void KnownProductIdsIncludeSupportedQuadCastCommandInterfaces()
    {
        Assert.Contains(0x028C, QuadCastDeviceIds.ProductIds);
        Assert.Contains(0x068C, QuadCastDeviceIds.ProductIds);
        Assert.Contains(0x09AF, QuadCastDeviceIds.ProductIds);
        Assert.Contains(0x02B5, QuadCastDeviceIds.ProductIds);
        Assert.Contains(0x03B5, QuadCastDeviceIds.ProductIds);
    }

    [Fact]
    public void ReportRequestUsesQuadCastMarkerAndCommandByte()
    {
        var report = QuadCastProtocol.CreateReportRequest(QuadCastCommandIds.GetPolarPattern, 64);

        Assert.Equal(64, report.Length);
        Assert.Equal(0x77, report[0]);
        Assert.Equal(0x85, report[1]);
    }

    [Fact]
    public void QuadCast2PolarPatternMappingMatchesProtocol()
    {
        Assert.True(QuadCastProtocol.TryParseReportPolarPattern(QuadCastModel.QuadCast2, 0x00, out var cardioid));
        Assert.Equal(QuadCastPolarPattern.Cardioid, cardioid);

        Assert.True(QuadCastProtocol.TryParseReportPolarPattern(QuadCastModel.QuadCast2, 0x03, out var bidirectional));
        Assert.Equal(QuadCastPolarPattern.Bidirectional, bidirectional);

        Assert.True(QuadCastProtocol.TryGetPolarPatternRaw(QuadCastModel.QuadCast2, QuadCastPolarPattern.Stereo, out var raw));
        Assert.Equal(0x02, raw);
    }

    [Fact]
    public void QuadCast2SPolarPatternMappingMatchesProtocol()
    {
        Assert.True(QuadCastProtocol.TryParseReportPolarPattern(QuadCastModel.QuadCast2S, 0x00, out var bidirectional));
        Assert.Equal(QuadCastPolarPattern.Bidirectional, bidirectional);

        Assert.True(QuadCastProtocol.TryParseReportPolarPattern(QuadCastModel.QuadCast2S, 0x03, out var stereo));
        Assert.Equal(QuadCastPolarPattern.Stereo, stereo);

        Assert.True(QuadCastProtocol.TryGetPolarPatternRaw(QuadCastModel.QuadCast2S, QuadCastPolarPattern.Cardioid, out var raw));
        Assert.Equal(0x01, raw);
    }

    [Fact]
    public async Task QuadCast2SStatusReadsGainAndMixBalance()
    {
        await using var client = new QuadCastClient(
            new FakeQuadCastTransport(
                QuadCastDeviceIds.ProductIdQuadCast2SCommand,
                new Dictionary<byte, byte>
                {
                    [QuadCastCommandIds.GetPolarPattern] = 0x01,
                    [QuadCastCommandIds.GetMicrophoneMute] = 0x01,
                    [QuadCastCommandIds.GetHighPass] = 0x01,
                    [QuadCastCommandIds.GetMicrophoneGain] = 0x36,
                    [QuadCastCommandIds.GetMixBalance] = 0x40
                }));

        var status = await client.GetStatusAsync();

        Assert.Equal(QuadCastModel.QuadCast2S, status.Model);
        Assert.Equal(QuadCastPolarPattern.Cardioid, status.PolarPattern);
        Assert.True(status.Muted);
        Assert.True(status.HighPassEnabled);
        Assert.Equal(0x36, status.MicrophoneGain);
        Assert.Equal(0x40, status.MixBalance);
    }

    [Fact]
    public void QuadCastSSecondSourceFeatureStatusUsesCompactOffsets()
    {
        var report = new byte[264];
        report[0] = QuadCastCommandIds.FeatureReportId;
        report[2] = QuadCastCommandIds.FeatureGetDeviceStatus;
        report[6] = 0x01;
        report[7] = 0x03;
        report[8] = 0x80;
        report[9] = 0x01;

        var parsed = QuadCastProtocol.TryParseFeatureStatus(
            report,
            secondSourceLayout: true,
            out var muted,
            out var pattern,
            out var brightness,
            out var reverseLights);

        Assert.True(parsed);
        Assert.True(muted);
        Assert.Equal(QuadCastPolarPattern.Stereo, pattern);
        Assert.Equal(50, brightness);
        Assert.True(reverseLights);
    }

    private sealed class FakeQuadCastTransport : IHyperXTransport
    {
        private readonly IReadOnlyDictionary<byte, byte> _responses;
        private byte _lastCommand;

        public FakeQuadCastTransport(int productId, IReadOnlyDictionary<byte, byte> responses)
        {
            _responses = responses;
            DeviceInfo = new HidDeviceInfo(
                "fake",
                QuadCastDeviceIds.HyperXVendorId,
                productId,
                "Fake QuadCast",
                64,
                64,
                264);
        }

        public HidDeviceInfo DeviceInfo { get; }

        public int InputReportLength => 64;

        public int OutputReportLength => 64;

        public int FeatureReportLength => 264;

        public Task WriteAsync(byte[] report, CancellationToken cancellationToken = default)
        {
            _lastCommand = report[1];
            return Task.CompletedTask;
        }

        public Task<byte[]?> ReadAsync(TimeSpan timeout, CancellationToken cancellationToken = default)
        {
            if (!_responses.TryGetValue(_lastCommand, out var value))
            {
                return Task.FromResult<byte[]?>(null);
            }

            var report = QuadCastProtocol.CreateValueReport(_lastCommand, value, InputReportLength);
            return Task.FromResult<byte[]?>(report);
        }

        public Task SetFeatureReportAsync(byte[] report, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task<byte[]?> GetFeatureReportAsync(byte reportId, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<byte[]?>(null);
        }

        public Task<byte[]?> QueryAsync(
            byte[] report,
            byte expectedCommand,
            TimeSpan timeout,
            CancellationToken cancellationToken = default)
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
