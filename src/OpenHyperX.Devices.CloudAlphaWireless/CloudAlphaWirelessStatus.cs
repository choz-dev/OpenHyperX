namespace OpenHyperX.Devices.CloudAlphaWireless;

public sealed record CloudAlphaWirelessStatus(
    bool Connected,
    int? BatteryPercent,
    ChargingState ChargingState,
    bool? MicMuted,
    bool? SidetoneEnabled,
    bool? VoicePromptEnabled,
    byte? AutoShutdownMinutes,
    uint? PairId)
{
    public static CloudAlphaWirelessStatus Empty { get; } =
        new(false, null, ChargingState.Unknown, null, null, null, null, null);
}
