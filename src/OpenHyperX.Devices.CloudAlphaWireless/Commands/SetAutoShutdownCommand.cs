namespace OpenHyperX.Devices.CloudAlphaWireless.Commands;

public sealed class SetAutoShutdownCommand : CloudAlphaWirelessCommandBase
{
    public SetAutoShutdownCommand(byte minutes)
    {
        Minutes = minutes;
        Skip = true;
    }

    public byte Minutes { get; }

    public override async Task ExecuteAsync(CloudAlphaWirelessClient context, CancellationToken cancellationToken = default)
    {
        await context.SendValueCommandAsync(CloudAlphaWirelessCommandIds.SetAutoShutdown, Minutes, cancellationToken)
            .ConfigureAwait(false);
        Complete(Minutes);
    }
}
