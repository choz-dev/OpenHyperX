# OpenHyperX

OpenHyperX is an unofficial, open-source desktop app for controlling HyperX devices.

The first supported target was the **HyperX Cloud Alpha Wireless** headset. OpenHyperX now also has early **QuadCast microphone** support. The long-term goal is to grow into a full HyperX control suite with support for headsets, keyboards, mice, microphones, RGB devices, and other products commonly managed through NGenuity.

OpenHyperX is built as a clean-room, original implementation using reverse-engineered protocol knowledge. It is not affiliated with, endorsed by, or sponsored by HP, HyperX, or NGenuity.

## Status

This project is early and experimental. The app can currently discover and communicate with the Cloud Alpha Wireless HID command interface and several QuadCast microphone command interfaces.

| Device | Status | Current support |
| --- | --- | --- |
| Cloud Alpha Wireless | In progress | Connection state, battery, charging state, mic mute, mic boom status, microphone monitoring, voice prompts, auto-shutdown, product color code |
| QuadCast S | In progress | Discovery, read-only feature-report status for mute, polar pattern, brightness, and reverse-light state on known controller interfaces |
| QuadCast 2 | In progress | Discovery, mute status/control, polar pattern status/control, high-pass status/control |
| QuadCast 2 S | In progress | Discovery, mute status/control, polar pattern status/control, high-pass status/control, microphone gain readback, mix balance readback |

## Features

- Native desktop UI built with Avalonia
- Light and dark mode
- HID device discovery through HidSharp
- Auto-connect scanning for supported devices
- Multi-device session plumbing for future product modules
- Auto-saved device preferences
- Remembered close behavior with system tray support
- System tray quick controls for supported device microphone actions
- Optional per-user startup registration
- DTS spatial audio diagnostics and opt-in enable flow
- Cloud Alpha Wireless status reading
- Cloud Alpha Wireless automatic status refresh
- Cloud Alpha Wireless basic setting writes
- QuadCast microphone discovery and automatic status refresh
- QuadCast 2 / 2 S microphone controls for known safe command reports
- QuadCast S status reads through HID feature reports
- Command queue for device writes
- CLI tool for safe diagnostics
- xUnit tests for protocol and command behavior

## Screens And Controls

The current desktop app includes:

- Device picker
- Auto-connect scan and read-status controls
- Battery, charging, mic boom, and product color status
- Pair ID display
- Mic mute toggle
- Microphone monitoring toggle
- Voice prompt toggle
- Auto-shutdown selector
- General settings tab
- Close-to-tray or full-exit behavior selector
- System tray menu with open, refresh, mute, mic monitoring, and supported microphone filter toggles
- Start-with-Windows controls
- DTS spatial audio driver/APO status and enable toggle
- Light/dark theme switch
- QuadCast microphone status for mute, polar pattern, high-pass, gain, mix balance, and lighting state where supported
- QuadCast 2 / 2 S controls for mute, high-pass, and polar pattern

## Safety

OpenHyperX talks directly to USB HID devices. Read-only commands are generally low risk, but write commands can change persistent device settings.

Safe diagnostic operations include:

- Listing HID devices
- Reading connection state
- Reading battery and charging state
- Reading mic mute, mic boom, microphone monitoring, voice prompt, auto-shutdown, and product color state
- Reading known QuadCast mute, polar pattern, high-pass, gain, mix balance, brightness, and reverse-light state

Be careful with:

- Unknown command IDs
- Feature reports
- QuadCast feature reports beyond the known read-only status/profile probes
- QuadCast lighting, firmware, reset, gain, mix balance, or unknown command writes
- Pairing commands
- Firmware or bootloader commands
- Factory reset commands
- Any command that writes persistent state

Do not run experimental write commands against real hardware unless you understand the bytes being sent and the expected behavior.

Saved Cloud Alpha Wireless preferences are stored per user and are applied automatically when a matching headset is connected or reconnects through the dongle. Current saved device preferences include mic mute, microphone monitoring, voice prompts, and auto-shutdown.

DTS spatial audio support can detect the expected DTS driver packages, APO registration, service state, and DTS-marked or fully configured Windows render endpoints. When DTS spatial audio is enabled, OpenHyperX can install the local DTS driver packages through Windows if they are missing, start the DTS service, and activate DTS on detected render endpoints. First-time driver installation requires a PC restart before APO activation can complete. OpenHyperX does not bundle DTS driver files.

## DTS Spatial Audio Flow

DTS support is opt-in from the General settings tab.

When the DTS spatial audio toggle is enabled, OpenHyperX checks for:

- The expected DTS driver packages
- DTS APO COM registration
- `DtsHPXV2Apo4Service`
- A DTS-marked Windows render endpoint

If the DTS driver pieces are missing, OpenHyperX looks for a local `AudioDTS` source. Supported sources are:

- An `AudioDTS` folder beside the OpenHyperX app
- The installed HyperX NGenuity WindowsApps package

If a source is found, OpenHyperX asks for administrator approval and installs the DTS INF packages through Windows. After a first-time DTS driver install, the PC must be restarted before APO activation can complete.

After the driver is present, OpenHyperX ensures the DTS service is running, binds the DTS render endpoint, and sets the endpoint to DSP mode. APO/spatial mode flags are applied best-effort because Windows and the DTS controller may expose them differently across systems.

OpenHyperX does not bundle DTS driver files, and it does not uninstall DTS drivers when the toggle is disabled.

## Clean-Room Policy

OpenHyperX must remain a clean-room implementation.

Reverse-engineered notes may be used to understand behavior, command IDs, report layouts, and device state. Do not copy proprietary NGenuity source code, assets, UI text, logos, binaries, or implementation details.

Contributions should:

- Use original code
- Use original names where practical
- Keep protocol documentation factual
- Avoid copied decompiled method bodies or class layouts
- Avoid official branding or assets

## Tech Stack

- C#
- .NET
- Avalonia
- HidSharp
- xUnit

The solution currently targets `.NET 9` because that is the SDK used during initial development. The target framework is centralized in `Directory.Build.props`, so moving to a newer .NET SDK later should be straightforward.

## Repository Layout

```text
OpenHyperX.sln
src/
  OpenHyperX.App/                        Avalonia desktop app
  OpenHyperX.Core/                       Shared abstractions, packets, command queue
  OpenHyperX.Hid/                        HidSharp-backed HID transport
  OpenHyperX.Devices.CloudAlphaWireless/ Cloud Alpha Wireless protocol module
  OpenHyperX.Devices.QuadCast/           QuadCast microphone protocol module
tools/
  OpenHyperX.Cli/                        Diagnostic CLI
tests/
  OpenHyperX.Core.Tests/                 Protocol and command tests
```

## Getting Started

### Requirements

- Visual Studio 2022 or newer
- .NET 9 SDK
- A supported HyperX device for hardware testing

### Open In Visual Studio

Open `OpenHyperX.sln`, then set `OpenHyperX.App` as the startup project.

### Build

```powershell
dotnet build OpenHyperX.sln
```

### Test

```powershell
dotnet test OpenHyperX.sln --no-build
```

### Run The Desktop App

```powershell
dotnet run --project src\OpenHyperX.App
```

## CLI Diagnostics

List matching supported HID interfaces:

```powershell
dotnet run --project tools\OpenHyperX.Cli
```

Read status from the first matching interface:

```powershell
dotnet run --project tools\OpenHyperX.Cli -- --status
```

The status command is intended to be read-only.

## Cloud Alpha Wireless Notes

The Cloud Alpha Wireless protocol currently uses HID reports with the HyperX packet header:

```text
0x21 0xBB <command> <payload...>
```

Some HID APIs expose reports with a leading report ID byte, so OpenHyperX packet helpers support both raw and report-ID-prefixed buffers.

Known command groups currently implemented:

- Wireless state
- Pairing info
- Battery info
- Charge status
- Mic mute
- Mic boom status
- Microphone monitoring status
- Voice prompt status
- Auto-shutdown
- Product color code

The command map was derived from reverse-engineered behavior notes in `headset.alpha_wireless_classes.md`.

## QuadCast Notes

QuadCast support is split into a dedicated `OpenHyperX.Devices.QuadCast` module.

Known command interfaces currently handled:

- QuadCast S controller endpoints: `VID_03F0` with `PID_028C` or `PID_068C`
- QuadCast 2 command endpoint: `VID_03F0` with `PID_09AF`
- QuadCast 2 S command endpoints: `VID_03F0` with `PID_02B5` or `PID_03B5`

QuadCast 2 and QuadCast 2 S use reports shaped like:

```text
0x77 <command> <payload...>
```

QuadCast S uses HID feature report `0x07` for the current status/profile probes. Those feature reports are read-only in the app right now.

The QuadCast command map was derived from reverse-engineered behavior notes in `quadcast/hyperx_quadcast.md`.

## Adding Device Support

Cloud Alpha Wireless is the first module, not the universal template.

When adding a new product:

1. Create a dedicated device module or namespace.
2. Document VID/PID values, HID report sizes, command IDs, and payload layouts.
3. Keep command IDs in one clear place.
4. Put device quirks inside the device module.
5. Expose friendly capabilities to the app rather than raw packets.
6. Add tests for packet creation, parsing, and state interpretation.

Shared concepts such as battery, lighting, profiles, DPI, audio, microphone monitoring, and firmware information should eventually become reusable capability interfaces in `OpenHyperX.Core`.

## Command Queue

Device writes should usually flow through the command queue in `OpenHyperX.Core`.

Commands can support:

- Delays for device timing
- Skipping stale duplicate commands
- Profile-scoped execution
- Forced execution
- Success tracking
- Completion handlers

This keeps device workflows out of the UI and makes rapid controls like toggles and sliders safer to implement.

## Contributing

Contributions are welcome, especially:

- Protocol documentation
- Device IDs
- Safe read-only probes
- New device modules
- Tests
- UI improvements
- Packaging work

Before contributing hardware writes, document what the command sends, what device it was tested on, and what changed on the device.

AI coding agents should follow `AGENTS.md`.

## License

OpenHyperX is licensed under the **GNU General Public License v3.0**. See `LICENSE`.

## Disclaimer

OpenHyperX is unofficial software. HyperX, HP, NGenuity, and related names are trademarks of their respective owners. This project is provided without warranty.
