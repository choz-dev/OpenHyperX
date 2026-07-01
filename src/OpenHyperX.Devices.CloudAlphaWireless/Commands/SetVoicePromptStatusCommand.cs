namespace OpenHyperX.Devices.CloudAlphaWireless.Commands;

public sealed class SetVoicePromptStatusCommand : CloudAlphaWirelessCommandBase
{
    public SetVoicePromptStatusCommand(bool enabled)
    {
        Enabled = enabled;
        Skip = true;
    }

    public bool Enabled { get; }

    public override async Task ExecuteAsync(CloudAlphaWirelessClient context, CancellationToken cancellationToken = default)
    {
        await context.SendValueCommandAsync(CloudAlphaWirelessCommandIds.SetVoicePromptStatus, BoolToByte(Enabled), cancellationToken)
            .ConfigureAwait(false);
        Complete(Enabled);
    }

    private static byte BoolToByte(bool value)
    {
        return value ? (byte)0x01 : (byte)0x00;
    }
}
