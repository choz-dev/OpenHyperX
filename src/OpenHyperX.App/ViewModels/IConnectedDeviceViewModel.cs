using System.ComponentModel;

namespace OpenHyperX.App.ViewModels;

public interface IConnectedDeviceViewModel : INotifyPropertyChanged, IDisposable
{
    string DevicePath { get; }

    string DisplayName { get; }

    string DeviceType { get; }

    string DetailText { get; }

    string StatusMessage { get; }

    bool IsBusy { get; }

    bool IsConnected { get; }

    bool ControlsEnabled { get; }

    Task InitializeAsync();

    Task RefreshStatusAsync();
}
