using System.Diagnostics;
using OpenHyperX.Core;

namespace OpenHyperX.Devices.QuadCast;

public sealed class QuadCastClient : IDisposable, IAsyncDisposable
{
    private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(1);
    private readonly IHyperXTransport _transport;
    private readonly bool _usesFeatureReports;
    private readonly bool _usesQuadCastSSecondSourceLayout;

    public QuadCastClient(IHyperXTransport transport)
    {
        _transport = transport;

        if (!QuadCastDeviceIds.TryGetModel(transport.DeviceInfo, out var model))
        {
            throw new ArgumentException("The selected HID interface is not a supported QuadCast command interface.");
        }

        Model = model;
        _usesFeatureReports = QuadCastDeviceIds.UsesFeatureReports(transport.DeviceInfo);
        _usesQuadCastSSecondSourceLayout = QuadCastDeviceIds.UsesQuadCastSSecondSourceLayout(transport.DeviceInfo);
    }

    public HidDeviceInfo DeviceInfo => _transport.DeviceInfo;

    public QuadCastModel Model { get; }

    public async Task<QuadCastStatus> GetStatusAsync(CancellationToken cancellationToken = default)
    {
        if (_usesFeatureReports)
        {
            return await GetQuadCastSStatusAsync(cancellationToken).ConfigureAwait(false);
        }

        var polarPattern = await GetPolarPatternAsync(cancellationToken).ConfigureAwait(false);
        var muted = await GetMicrophoneMutedAsync(cancellationToken).ConfigureAwait(false);
        var highPassEnabled = await GetHighPassEnabledAsync(cancellationToken).ConfigureAwait(false);
        var microphoneGain = Model == QuadCastModel.QuadCast2S
            ? await GetMicrophoneGainAsync(cancellationToken).ConfigureAwait(false)
            : null;
        var mixBalance = Model == QuadCastModel.QuadCast2S
            ? await GetMixBalanceAsync(cancellationToken).ConfigureAwait(false)
            : null;

        return new QuadCastStatus(
            Model,
            muted,
            polarPattern,
            highPassEnabled,
            microphoneGain,
            mixBalance,
            BrightnessPercent: null,
            ReverseLights: null);
    }

    public async Task<QuadCastPolarPattern?> GetPolarPatternAsync(CancellationToken cancellationToken = default)
    {
        var value = await QueryReportValueAsync(QuadCastCommandIds.GetPolarPattern, cancellationToken)
            .ConfigureAwait(false);

        return value is not null && QuadCastProtocol.TryParseReportPolarPattern(Model, value.Value, out var pattern)
            ? pattern
            : null;
    }

    public async Task<bool?> GetMicrophoneMutedAsync(CancellationToken cancellationToken = default)
    {
        var value = await QueryReportValueAsync(QuadCastCommandIds.GetMicrophoneMute, cancellationToken)
            .ConfigureAwait(false);

        return value is null ? null : value.Value == 0x01;
    }

    public async Task<bool?> GetHighPassEnabledAsync(CancellationToken cancellationToken = default)
    {
        EnsureReportCommandSupported();

        var value = await QueryReportValueAsync(QuadCastCommandIds.GetHighPass, cancellationToken)
            .ConfigureAwait(false);

        return value is null ? null : value.Value == 0x01;
    }

    public async Task<int?> GetMicrophoneGainAsync(CancellationToken cancellationToken = default)
    {
        EnsureQuadCast2S();

        var value = await QueryReportValueAsync(QuadCastCommandIds.GetMicrophoneGain, cancellationToken)
            .ConfigureAwait(false);

        return value;
    }

    public async Task<int?> GetMixBalanceAsync(CancellationToken cancellationToken = default)
    {
        EnsureQuadCast2S();

        var value = await QueryReportValueAsync(QuadCastCommandIds.GetMixBalance, cancellationToken)
            .ConfigureAwait(false);

        return value;
    }

    public Task SetMicrophoneMutedAsync(bool muted, CancellationToken cancellationToken = default)
    {
        EnsureReportCommandSupported();
        return SendValueCommandAsync(QuadCastCommandIds.SetMicrophoneMute, muted ? (byte)0x01 : (byte)0x00, cancellationToken);
    }

    public Task SetHighPassEnabledAsync(bool enabled, CancellationToken cancellationToken = default)
    {
        EnsureReportCommandSupported();
        return SendValueCommandAsync(QuadCastCommandIds.SetHighPass, enabled ? (byte)0x01 : (byte)0x00, cancellationToken);
    }

    public Task SetPolarPatternAsync(QuadCastPolarPattern pattern, CancellationToken cancellationToken = default)
    {
        EnsureReportCommandSupported();

        if (!QuadCastProtocol.TryGetPolarPatternRaw(Model, pattern, out var raw))
        {
            throw new NotSupportedException("This QuadCast model does not support software polar-pattern changes yet.");
        }

        return SendValueCommandAsync(QuadCastCommandIds.SetPolarPattern, raw, cancellationToken);
    }

    public Task SetMicrophoneGainAsync(int gain, CancellationToken cancellationToken = default)
    {
        EnsureQuadCast2S();
        return SendValueCommandAsync(QuadCastCommandIds.SetMicrophoneGain, ClampByte(gain), cancellationToken);
    }

    public Task SetMixBalanceAsync(int level, CancellationToken cancellationToken = default)
    {
        EnsureQuadCast2S();
        return SendValueCommandAsync(QuadCastCommandIds.SetMixBalance, ClampByte(level), cancellationToken);
    }

    private async Task<QuadCastStatus> GetQuadCastSStatusAsync(CancellationToken cancellationToken)
    {
        var statusResponse = await QueryFeatureAsync(QuadCastCommandIds.FeatureGetDeviceStatus, cancellationToken)
            .ConfigureAwait(false);

        var muted = default(bool?);
        var polarPattern = default(QuadCastPolarPattern?);
        var brightnessPercent = default(int?);
        var reverseLights = default(bool?);

        if (statusResponse is not null
            && QuadCastProtocol.TryParseFeatureStatus(
                statusResponse,
                _usesQuadCastSSecondSourceLayout,
                out var parsedMuted,
                out var parsedPattern,
                out var parsedBrightness,
                out var parsedReverseLights))
        {
            muted = parsedMuted;
            polarPattern = parsedPattern;
            brightnessPercent = parsedBrightness;
            reverseLights = parsedReverseLights;
        }

        if (!_usesQuadCastSSecondSourceLayout)
        {
            var profileResponse = await QueryFeatureAsync(QuadCastCommandIds.FeatureGetCurrentProfile, cancellationToken)
                .ConfigureAwait(false);

            if (profileResponse is not null
                && QuadCastProtocol.TryParseFeatureProfile(
                    profileResponse,
                    out var profileBrightness,
                    out var profileReverseLights))
            {
                brightnessPercent = profileBrightness;
                reverseLights = profileReverseLights;
            }
        }

        return new QuadCastStatus(
            Model,
            muted,
            polarPattern,
            HighPassEnabled: null,
            MicrophoneGain: null,
            MixBalance: null,
            brightnessPercent,
            reverseLights);
    }

    private async Task<byte[]?> QueryFeatureAsync(byte command, CancellationToken cancellationToken)
    {
        var request = QuadCastProtocol.CreateFeatureRequest(command, _transport.FeatureReportLength);
        await _transport.SetFeatureReportAsync(request, cancellationToken).ConfigureAwait(false);
        await Task.Delay(10, cancellationToken).ConfigureAwait(false);
        return await _transport.GetFeatureReportAsync(QuadCastCommandIds.FeatureReportId, cancellationToken)
            .ConfigureAwait(false);
    }

    private async Task<byte?> QueryReportValueAsync(byte command, CancellationToken cancellationToken)
    {
        EnsureReportCommandSupported();

        var request = QuadCastProtocol.CreateReportRequest(command, GetReportWriteLength());
        await _transport.WriteAsync(request, cancellationToken).ConfigureAwait(false);

        var stopwatch = Stopwatch.StartNew();
        while (stopwatch.Elapsed < DefaultTimeout)
        {
            var remaining = DefaultTimeout - stopwatch.Elapsed;
            var response = await _transport.ReadAsync(remaining, cancellationToken).ConfigureAwait(false);
            if (response is null)
            {
                continue;
            }

            if (QuadCastProtocol.TryGetReportValue(response, command, out var value))
            {
                return value;
            }
        }

        return null;
    }

    private Task SendValueCommandAsync(byte command, byte value, CancellationToken cancellationToken)
    {
        var report = QuadCastProtocol.CreateValueReport(command, value, GetReportWriteLength());
        return _transport.WriteAsync(report, cancellationToken);
    }

    private int GetReportWriteLength()
    {
        return Model == QuadCastModel.QuadCast2S ? 264 : _transport.OutputReportLength;
    }

    private void EnsureReportCommandSupported()
    {
        if (_usesFeatureReports)
        {
            throw new NotSupportedException("This QuadCast model uses feature reports for this operation.");
        }
    }

    private void EnsureQuadCast2S()
    {
        EnsureReportCommandSupported();

        if (Model != QuadCastModel.QuadCast2S)
        {
            throw new NotSupportedException("This operation is only supported on QuadCast 2 S.");
        }
    }

    private static byte ClampByte(int value)
    {
        return (byte)Math.Clamp(value, byte.MinValue, byte.MaxValue);
    }

    public void Dispose()
    {
        _transport.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _transport.DisposeAsync().ConfigureAwait(false);
    }
}
