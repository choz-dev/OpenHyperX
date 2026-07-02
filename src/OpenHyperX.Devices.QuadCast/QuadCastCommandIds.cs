namespace OpenHyperX.Devices.QuadCast;

public static class QuadCastCommandIds
{
    public const byte ReportMarker = 0x77;

    public const byte GetDeviceInfo = 0x81;
    public const byte GetPolarPattern = 0x85;
    public const byte GetMicrophoneMute = 0x86;
    public const byte GetHighPass = 0x87;
    public const byte GetMicrophoneGain = 0x88;
    public const byte GetMixBalance = 0x8A;

    public const byte SetPolarPattern = 0x05;
    public const byte SetMicrophoneMute = 0x06;
    public const byte SetHighPass = 0x07;
    public const byte SetMicrophoneGain = 0x08;
    public const byte SetMixBalance = 0x0A;

    public const byte FeatureReportId = 0x07;
    public const byte FeatureGetCurrentProfile = 0x56;
    public const byte FeatureGetDeviceStatus = 0x58;
}
