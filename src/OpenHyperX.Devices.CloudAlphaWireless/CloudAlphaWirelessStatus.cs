namespace OpenHyperX.Devices.CloudAlphaWireless;

public sealed record CloudAlphaWirelessStatus(
    bool Connected,
    int? BatteryPercent,
    ChargingState ChargingState,
    bool? MicMuted,
    bool? MicrophoneMonitoringEnabled,
    bool? VoicePromptEnabled,
    byte? AutoShutdownMinutes,
    uint? PairId,
    bool? MicrophoneBoomAttached,
    byte? ProductColor)
{
    public static CloudAlphaWirelessStatus Empty { get; } =
        new(false, null, ChargingState.Unknown, null, null, null, null, null, null, null);
}
