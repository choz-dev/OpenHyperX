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
    public void ReportRequestUsesLeadingReportIdFor65ByteHidReports()
    {
        var report = QuadCastProtocol.CreateReportRequest(QuadCastCommandIds.GetPolarPattern, 65);

        Assert.Equal(65, report.Length);
        Assert.Equal(0x00, report[0]);
        Assert.Equal(0x77, report[1]);
        Assert.Equal(0x85, report[2]);
    }

    [Fact]
    public void ReportValueParserAcceptsLeadingReportId()
    {
        var report = QuadCastProtocol.CreateValueReport(QuadCastCommandIds.GetMicrophoneMute, 0x01, 65);

        Assert.True(QuadCastProtocol.TryGetReportValue(report, QuadCastCommandIds.GetMicrophoneMute, out var value));
        Assert.Equal(0x01, value);
    }

    [Fact]
    public void QuadCast2SCommandInterfaceFilterRejectsPrimaryCollection()
    {
        var primary = CreateQuadCast2SDevice(@"\\?\hid#vid_03f0&pid_02b5&mi_00#primary#{guid}", 0x02B5);
        var secondary = CreateQuadCast2SDevice(@"\\?\hid#vid_03f0&pid_02b5&mi_01&col02#secondary#{guid}", 0x02B5);
        var codec = CreateQuadCast2SDevice(@"\\?\hid#vid_03f0&pid_03b5#codec#{guid}", 0x03B5);

        Assert.False(QuadCastDeviceIds.IsLikelyCommandInterface(primary));
        Assert.True(QuadCastDeviceIds.IsLikelyCommandInterface(secondary));
        Assert.True(QuadCastDeviceIds.IsLikelyCommandInterface(codec));
    }

    [Fact]
    public void QuadCast2SCommandInterfaceSelectionPrefersSecondaryCollection()
    {
        var secondary = CreateQuadCast2SDevice(@"\\?\hid#vid_03f0&pid_02b5&mi_01&col02#secondary#{guid}", 0x02B5);
        var codec = CreateQuadCast2SDevice(@"\\?\hid#vid_03f0&pid_03b5#codec#{guid}", 0x03B5);

        var selected = QuadCastDeviceIds.SelectPreferredCommandInterfaces([codec, secondary]);

        var device = Assert.Single(selected);
        Assert.Equal(secondary.Path, device.Path);
    }

    [Fact]
    public void QuadCast2SCommandInterfaceSelectionKeepsFallbackOnlyDevices()
    {
        var firstCodec = CreateQuadCast2SDevice(@"\\?\hid#vid_03f0&pid_03b5#codec-a#{guid}", 0x03B5);
        var secondCodec = CreateQuadCast2SDevice(@"\\?\hid#vid_03f0&pid_03b5#codec-b#{guid}", 0x03B5);

        var selected = QuadCastDeviceIds.SelectPreferredCommandInterfaces([firstCodec, secondCodec]);

        Assert.Equal(2, selected.Count);
        Assert.Contains(selected, device => device.Path == firstCodec.Path);
        Assert.Contains(selected, device => device.Path == secondCodec.Path);
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
                outputReportLength: 65,
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

    private static HidDeviceInfo CreateQuadCast2SDevice(string path, int productId)
    {
        return new HidDeviceInfo(
            path,
            QuadCastDeviceIds.HyperXVendorId,
            productId,
            "HyperX QuadCast 2 S",
            65,
            65,
            0);
    }

    private sealed class FakeQuadCastTransport : IHyperXTransport
    {
        private readonly IReadOnlyDictionary<byte, byte> _responses;
        private readonly int _outputReportLength;
        private byte _lastCommand;

        public FakeQuadCastTransport(
            int productId,
            int outputReportLength,
            IReadOnlyDictionary<byte, byte> responses)
        {
            _responses = responses;
            _outputReportLength = outputReportLength;
            DeviceInfo = new HidDeviceInfo(
                "fake",
                QuadCastDeviceIds.HyperXVendorId,
                productId,
                "Fake QuadCast",
                65,
                outputReportLength,
                264);
        }

        public HidDeviceInfo DeviceInfo { get; }

        public int InputReportLength => 65;

        public int OutputReportLength => _outputReportLength;

        public int FeatureReportLength => 264;

        public Task WriteAsync(byte[] report, CancellationToken cancellationToken = default)
        {
            if (!QuadCastProtocol.TryGetReportCommand(report, out _lastCommand))
            {
                throw new InvalidOperationException("Report did not contain a QuadCast command marker.");
            }

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
