namespace OpenHyperX.Core;

public interface IHidDeviceEnumerator
{
    IReadOnlyList<HidDeviceInfo> ListDevices(HidDeviceFilter filter);

    IHyperXTransport Open(HidDeviceInfo deviceInfo);
}
