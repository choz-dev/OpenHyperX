namespace OpenHyperX.Devices.CloudAlphaWireless.Commands;

public sealed class SetMicMuteCommand : CloudAlphaWirelessCommandBase
{
    public SetMicMuteCommand(bool muted)
    {
        Muted = muted;
        Skip = true;
    }

    public bool Muted { get; }

    public override async Task ExecuteAsync(CloudAlphaWirelessClient context, CancellationToken cancellationToken = default)
    {
        await context.SendValueCommandAsync(CloudAlphaWirelessCommandIds.SetMicMute, BoolToByte(Muted), cancellationToken)
            .ConfigureAwait(false);
        Complete(Muted);
    }

    private static byte BoolToByte(bool value)
    {
        return value ? (byte)0x01 : (byte)0x00;
    }
}
