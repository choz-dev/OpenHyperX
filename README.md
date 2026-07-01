# OpenHyperX

OpenHyperX is an experimental, unofficial desktop app for controlling HyperX devices. The first target is the Cloud Alpha Wireless headset.

## Development

Open `OpenHyperX.sln` in Visual Studio and set `OpenHyperX.App` as the startup project.

The current solution targets .NET 9 because that is the SDK installed on this machine. The target framework is centralized in `Directory.Build.props`, so moving to .NET 10 later is a one-line change after installing the .NET 10 SDK.

## Projects

- `OpenHyperX.Core`: shared device and HID abstractions.
- `OpenHyperX.Hid`: HidSharp-backed HID enumeration and transport.
- `OpenHyperX.Devices.CloudAlphaWireless`: Cloud Alpha Wireless command protocol.
- `OpenHyperX.App`: Avalonia desktop app.
- `OpenHyperX.Cli`: quick HID enumeration and status probe.
- `OpenHyperX.Core.Tests`: packet and protocol sanity tests.

## CLI

List matching Cloud Alpha Wireless HID interfaces:

```powershell
dotnet run --project tools\OpenHyperX.Cli
```

Read status from the first matching interface:

```powershell
dotnet run --project tools\OpenHyperX.Cli -- --status
```

## Device Notes

The Cloud Alpha Wireless command map currently comes from `headset.alpha_wireless_clasess.md`. The app sends HID reports with report ID `0x00`, followed by `0x21 0xBB`, then the command byte and payload.

The core project includes an original command queue inspired by the device architecture notes: commands can delay, skip older pending duplicates, target a profile ID, force execution, report success, and call completion handlers. Device modules should add small command classes instead of putting every action directly into the UI.

This project is not affiliated with or endorsed by HP, HyperX, or NGenuity.
