# OpenHyperX

OpenHyperX is an unofficial, open-source desktop app for controlling HyperX devices.

The first supported target is the **HyperX Cloud Alpha Wireless** headset. The long-term goal is to grow into a full HyperX control suite with support for headsets, keyboards, mice, microphones, RGB devices, and other products commonly managed through NGenuity.

OpenHyperX is built as a clean-room, original implementation using reverse-engineered protocol knowledge. It is not affiliated with, endorsed by, or sponsored by HP, HyperX, or NGenuity.

## Status

This project is early and experimental. The app can currently discover and communicate with the Cloud Alpha Wireless HID command interface.

| Device | Status | Current support |
| --- | --- | --- |
| Cloud Alpha Wireless | In progress | Connection state, battery, charging state, mic mute, sidetone, voice prompts, auto-shutdown |

## Features

- Native desktop UI built with Avalonia
- Light and dark mode
- HID device discovery through HidSharp
- Cloud Alpha Wireless status reading
- Cloud Alpha Wireless basic setting writes
- Command queue for device writes
- CLI tool for safe diagnostics
- xUnit tests for protocol and command behavior

## Screens And Controls

The current desktop app includes:

- Device picker
- Connect and refresh controls
- Battery and charging status
- Pair ID display
- Mic mute toggle
- Sidetone toggle
- Voice prompt toggle
- Auto-shutdown selector
- Light/dark theme switch

## Safety

OpenHyperX talks directly to USB HID devices. Read-only commands are generally low risk, but write commands can change persistent device settings.

Safe diagnostic operations include:

- Listing HID devices
- Reading connection state
- Reading battery and charging state
- Reading mic mute, sidetone, voice prompt, and auto-shutdown state

Be careful with:

- Unknown command IDs
- Feature reports
- Pairing commands
- Firmware or bootloader commands
- Factory reset commands
- Any command that writes persistent state

Do not run experimental write commands against real hardware unless you understand the bytes being sent and the expected behavior.

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

List matching Cloud Alpha Wireless HID interfaces:

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
- Sidetone status
- Voice prompt status
- Auto-shutdown

The command map was derived from reverse-engineered behavior notes in `headset.alpha_wireless_clasess.md`.

## Adding Device Support

Cloud Alpha Wireless is the first module, not the universal template.

When adding a new product:

1. Create a dedicated device module or namespace.
2. Document VID/PID values, HID report sizes, command IDs, and payload layouts.
3. Keep command IDs in one clear place.
4. Put device quirks inside the device module.
5. Expose friendly capabilities to the app rather than raw packets.
6. Add tests for packet creation, parsing, and state interpretation.

Shared concepts such as battery, lighting, profiles, DPI, audio, sidetone, and firmware information should eventually become reusable capability interfaces in `OpenHyperX.Core`.

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
