using System.Buffers.Binary;
using OpenHyperX.Core.Commands;
using OpenHyperX.Core;
using OpenHyperX.Devices.CloudAlphaWireless.Commands;

namespace OpenHyperX.Devices.CloudAlphaWireless;

public sealed class CloudAlphaWirelessClient : IDisposable, IAsyncDisposable
{
    private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(1);
    private readonly IHyperXTransport _transport;
    private readonly HyperXCommandQueue<CloudAlphaWirelessClient> _commandQueue;

    public CloudAlphaWirelessClient(IHyperXTransport transport)
    {
        _transport = transport;
        _commandQueue = new HyperXCommandQueue<CloudAlphaWirelessClient>(this);
        _commandQueue.Start();
    }

    public HidDeviceInfo DeviceInfo => _transport.DeviceInfo;

    public async Task<CloudAlphaWirelessStatus> GetStatusAsync(CancellationToken cancellationToken = default)
    {
        var connected = await GetWirelessStateAsync(cancellationToken).ConfigureAwait(false) ?? false;
        var pairId = await GetPairIdAsync(cancellationToken).ConfigureAwait(false);
        var autoShutdown = await GetAutoShutdownAsync(cancellationToken).ConfigureAwait(false);

        if (!connected)
        {
            return CloudAlphaWirelessStatus.Empty with
            {
                Connected = false,
                PairId = pairId,
                AutoShutdownMinutes = autoShutdown
            };
        }

        var battery = await GetBatteryPercentAsync(cancellationToken).ConfigureAwait(false);
        var charging = await GetChargingStateAsync(cancellationToken).ConfigureAwait(false);
        var sidetone = await GetSidetoneEnabledAsync(cancellationToken).ConfigureAwait(false);
        var micMuted = await GetMicMutedAsync(cancellationToken).ConfigureAwait(false);
        var voicePrompt = await GetVoicePromptEnabledAsync(cancellationToken).ConfigureAwait(false);

        return new CloudAlphaWirelessStatus(
            connected,
            battery,
            charging,
            micMuted,
            sidetone,
            voicePrompt,
            autoShutdown,
            pairId);
    }

    public async Task<bool?> GetWirelessStateAsync(CancellationToken cancellationToken = default)
    {
        if (!CloudAlphaWirelessDeviceIds.IsDongle(_transport.DeviceInfo.ProductId))
        {
            return true;
        }

        var payload = await QueryPayloadAsync(CloudAlphaWirelessCommandIds.GetWirelessState, cancellationToken)
            .ConfigureAwait(false);

        return payload is { Length: > 0 } ? payload[0] == 0x02 : null;
    }

    public async Task<uint?> GetPairIdAsync(CancellationToken cancellationToken = default)
    {
        var command = CloudAlphaWirelessDeviceIds.IsDongle(_transport.DeviceInfo.ProductId)
            ? CloudAlphaWirelessCommandIds.GetPairingInfoDongle
            : CloudAlphaWirelessCommandIds.GetPairingInfoHeadset;

        var payload = await QueryPayloadAsync(command, cancellationToken).ConfigureAwait(false);
        if (payload is null || payload.Length < 6)
        {
            return null;
        }

        return BinaryPrimitives.ReadUInt32LittleEndian(payload.AsSpan(2, 4));
    }

    public async Task<byte?> GetAutoShutdownAsync(CancellationToken cancellationToken = default)
    {
        var payload = await QueryPayloadAsync(CloudAlphaWirelessCommandIds.GetAutoShutdown, cancellationToken)
            .ConfigureAwait(false);

        return payload is { Length: > 0 } ? payload[0] : null;
    }

    public async Task<int?> GetBatteryPercentAsync(CancellationToken cancellationToken = default)
    {
        var payload = await QueryPayloadAsync(CloudAlphaWirelessCommandIds.GetBatteryInfo, cancellationToken)
            .ConfigureAwait(false);

        return payload is { Length: > 0 } ? payload[0] : null;
    }

    public async Task<ChargingState> GetChargingStateAsync(CancellationToken cancellationToken = default)
    {
        var payload = await QueryPayloadAsync(CloudAlphaWirelessCommandIds.GetChargeStatus, cancellationToken)
            .ConfigureAwait(false);

        return payload is { Length: > 0 }
            ? payload[0] switch
            {
                0x00 => ChargingState.NotCharging,
                0x01 => ChargingState.WiredCharging,
                _ => ChargingState.Unknown
            }
            : ChargingState.Unknown;
    }

    public async Task<bool?> GetMicMutedAsync(CancellationToken cancellationToken = default)
    {
        var payload = await QueryPayloadAsync(CloudAlphaWirelessCommandIds.GetMicMute, cancellationToken)
            .ConfigureAwait(false);

        return payload is { Length: > 0 } ? payload[0] == 0x01 : null;
    }

    public async Task<bool?> GetSidetoneEnabledAsync(CancellationToken cancellationToken = default)
    {
        var payload = await QueryPayloadAsync(CloudAlphaWirelessCommandIds.GetSidetoneStatus, cancellationToken)
            .ConfigureAwait(false);

        return payload is { Length: > 0 } ? payload[0] == 0x01 : null;
    }

    public async Task<bool?> GetVoicePromptEnabledAsync(CancellationToken cancellationToken = default)
    {
        var payload = await QueryPayloadAsync(CloudAlphaWirelessCommandIds.GetVoicePromptStatus, cancellationToken)
            .ConfigureAwait(false);

        return payload is { Length: > 0 } ? payload[0] == 0x01 : null;
    }

    public Task SetAutoShutdownAsync(byte minutes, CancellationToken cancellationToken = default)
    {
        return EnqueueCommandAsync(new SetAutoShutdownCommand(minutes), cancellationToken);
    }

    public Task SetMicMuteAsync(bool muted, CancellationToken cancellationToken = default)
    {
        return EnqueueCommandAsync(new SetMicMuteCommand(muted), cancellationToken);
    }

    public Task SetSidetoneEnabledAsync(bool enabled, CancellationToken cancellationToken = default)
    {
        return EnqueueCommandAsync(new SetSidetoneStatusCommand(enabled), cancellationToken);
    }

    public Task SetVoicePromptEnabledAsync(bool enabled, CancellationToken cancellationToken = default)
    {
        return EnqueueCommandAsync(new SetVoicePromptStatusCommand(enabled), cancellationToken);
    }

    public Task EnqueueCommandAsync(
        IHyperXCommand<CloudAlphaWirelessClient> command,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return _commandQueue.EnqueueAsync(command);
    }

    private async Task<byte[]?> QueryPayloadAsync(byte command, CancellationToken cancellationToken)
    {
        var report = CreateCommandReport(command);
        var response = await _transport.QueryAsync(report, command, DefaultTimeout, cancellationToken)
            .ConfigureAwait(false);

        if (response is null)
        {
            return null;
        }

        return HyperXPacket.TryGetPayload(response, out var payload) ? payload.ToArray() : null;
    }

    internal Task SendValueCommandAsync(byte command, byte value, CancellationToken cancellationToken)
    {
        var report = CreateCommandReport(command, [value]);
        return _transport.WriteAsync(report, cancellationToken);
    }

    private byte[] CreateCommandReport(byte command, ReadOnlySpan<byte> payload = default)
    {
        var includeReportId = _transport.OutputReportLength > HyperXPacket.DefaultReportLength - 1;
        return HyperXPacket.CreateReport(command, _transport.OutputReportLength, payload, includeReportId);
    }

    public void Dispose()
    {
        _commandQueue.Dispose();
        _transport.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _commandQueue.DisposeAsync().ConfigureAwait(false);
        await _transport.DisposeAsync().ConfigureAwait(false);
    }
}
