namespace OpenHyperX.Devices.CloudAlphaWireless;

public static class CloudAlphaWirelessCommandIds
{
    // The device protocol names this capability sidetone; the app exposes it as microphone monitoring.
    public const byte GetWirelessState = 0x03;
    public const byte GetPairingInfoHeadset = 0x04;
    public const byte GetSidetoneStatus = 0x05;
    public const byte GetSidetoneVolume = 0x06;
    public const byte GetAutoShutdown = 0x07;
    public const byte GetMicBoomStatus = 0x08;
    public const byte GetVoicePromptStatus = 0x09;
    public const byte GetMicMute = 0x0A;
    public const byte GetBatteryInfo = 0x0B;
    public const byte GetChargeStatus = 0x0C;
    public const byte GetPairingInfoDongle = 0x0D;
    public const byte GetProductColor = 0x0E;

    public const byte SetSidetoneStatus = 0x10;
    public const byte SetSidetoneVolume = 0x11;
    public const byte SetAutoShutdown = 0x12;
    public const byte SetVoicePromptStatus = 0x13;
    public const byte SetMicMute = 0x15;
}
