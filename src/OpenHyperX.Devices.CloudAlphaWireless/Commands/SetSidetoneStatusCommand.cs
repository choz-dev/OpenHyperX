namespace OpenHyperX.Devices.CloudAlphaWireless.Commands;

public sealed class SetSidetoneStatusCommand : CloudAlphaWirelessCommandBase
{
    public SetSidetoneStatusCommand(bool enabled)
    {
        Enabled = enabled;
        Skip = true;
    }

    public bool Enabled { get; }

    public override async Task ExecuteAsync(CloudAlphaWirelessClient context, CancellationToken cancellationToken = default)
    {
        await context.SendValueCommandAsync(CloudAlphaWirelessCommandIds.SetSidetoneStatus, BoolToByte(Enabled), cancellationToken)
            .ConfigureAwait(false);
        Complete(Enabled);
    }

    private static byte BoolToByte(bool value)
    {
        return value ? (byte)0x01 : (byte)0x00;
    }
}
