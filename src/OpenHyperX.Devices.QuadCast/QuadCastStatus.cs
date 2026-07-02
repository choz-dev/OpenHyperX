namespace OpenHyperX.Devices.QuadCast;

public sealed record QuadCastStatus(
    QuadCastModel Model,
    bool? Muted,
    QuadCastPolarPattern? PolarPattern,
    bool? HighPassEnabled,
    int? MicrophoneGain,
    int? MixBalance,
    int? BrightnessPercent,
    bool? ReverseLights);
