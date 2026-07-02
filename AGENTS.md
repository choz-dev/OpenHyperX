# AGENTS.md

Guidelines for AI agents working on OpenHyperX.

## Project Goal

OpenHyperX is an unofficial, open-source desktop application for controlling HyperX devices. It currently targets the Cloud Alpha Wireless headset, but the long-term goal is to become a fully fledged alternative control suite that can support the same broad range of devices as NGenuity.

Design every change with that future in mind: device support should grow through clear modules, shared abstractions, and documented protocols rather than one-off code paths.

## Clean-Room Rule

This project must remain a clean-room, original implementation.

Reverse-engineered behavior notes may be used to understand device protocols, command IDs, report layouts, and state transitions. Do not copy proprietary NGenuity source code, assets, branding, UI text, icons, binaries, or implementation structure verbatim.

When translating protocol knowledge into OpenHyperX:

- Write original code.
- Use original names where possible.
- Keep comments factual and protocol-focused.
- Avoid copying decompiled method bodies, class layouts, or proprietary abstractions.
- Do not add HyperX, HP, or NGenuity logos/assets unless the project owner explicitly provides legally safe assets.

## Stack

Keep the project on this stack unless the project owner explicitly approves a change:

- C#
- .NET
- Avalonia for the desktop UI
- HidSharp for HID access
- xUnit for tests

Do not introduce Electron, C++, Rust, Python runtime dependencies, web app frameworks, or alternate HID libraries without asking first. Small development scripts are fine when useful, but the product should remain a C#/.NET desktop app.

## Hardware Safety

Ask before running commands that write to a real device.

Safe by default:

- Building the solution
- Running tests
- Listing HID devices
- Reading passive status such as battery, charging state, connection state, mute state, microphone monitoring state, voice prompt state, and auto-shutdown setting
- Reading known QuadCast status fields such as mute, polar pattern, high-pass state, microphone gain, mix balance, brightness, and reverse-light state

Ask first:

- Changing mic mute
- Changing microphone monitoring
- Changing voice prompts
- Changing auto-shutdown
- Changing QuadCast polar pattern, high-pass state, microphone gain, mix balance, or lighting
- Sending unknown command IDs
- Trying feature reports or output reports not already known to be safe, including QuadCast feature reports beyond the known status/profile reads
- Pairing, firmware, factory reset, bootloader, or update-related commands
- Any command that could alter persistent device state

If a command is experimental, say exactly what bytes will be sent and what behavior is expected before asking for approval.

## Windows Audio And DTS Safety

DTS spatial audio support touches Windows driver, service, registry, and APO state. Treat those operations separately from HID device reads.

Safe by default:

- Reading DTS driver package presence from DriverStore.
- Reading DTS APO COM registration.
- Reading `DtsHPXV2Apo4Service` registration/running state.
- Reading Windows render endpoint registry markers.
- Read-only APO binding probes that do not call setters or change endpoint config.

Ask first:

- Installing DTS INF packages with `pnputil`.
- Removing or replacing DTS driver packages.
- Starting, stopping, or changing startup mode for DTS services.
- Calling APO setters such as endpoint `SetConfig`, APO enable, spatial enable, EQ, or restore-default operations.
- Writing Windows audio endpoint registry values.

OpenHyperX must not bundle proprietary DTS, HyperX, HP, or NGenuity driver files. The app may look for a local user-installed or user-provided `AudioDTS` source, but agents should not copy driver binaries from NGenuity or WindowsApps into the repository.

## Architecture

Prefer this shape:

- `OpenHyperX.Core`: shared device models, packet helpers, command queue abstractions, and interfaces.
- `OpenHyperX.Hid`: HidSharp-backed enumeration and transport.
- `OpenHyperX.Devices.*`: device-specific protocol modules.
- `OpenHyperX.App`: Avalonia UI.
- `OpenHyperX.Cli`: safe diagnostics and protocol testing.
- `OpenHyperX.Core.Tests`: protocol and shared behavior tests.

Device modules should expose friendly capabilities rather than leaking raw packets into the UI. For example, the UI should call a client/capability method such as `SetMicrophoneMonitoringEnabledAsync`, not manually build a HID report.

## Device Support

Cloud Alpha Wireless is the first device, not the template for every device.

When adding support for another product:

- Create a dedicated device module or namespace.
- Document known VID/PID values, report lengths, command IDs, and payload layouts.
- Keep command IDs in one obvious place.
- Prefer capability interfaces for shared concepts like battery, lighting, DPI, profiles, audio, microphone monitoring, and firmware info.
- Keep device quirks inside the device module.
- Add tests for packet building, command IDs, parsing, and state interpretation.

Do not force unrelated products into the Cloud Alpha Wireless model. Headsets, keyboards, mice, microphones, and RGB devices may need different capabilities and report handling.

## Command Handling

Use the project command queue for device writes when practical.

Commands should be small, named classes with clear intent. They may use:

- `Delay` for device timing requirements
- `Skip` to replace stale pending commands, such as rapid toggle or slider changes
- `ProfileId` for profile-scoped commands
- `Force` only when a command should run despite normal paused/stopped state
- `Succeeded` and handlers for completion status

Avoid putting command sequencing in the UI. If a workflow needs multiple device commands, keep that workflow in the device/client layer.

## UI Guidelines

The app should feel like a useful desktop utility, not a landing page.

Keep UI work:

- Clear
- Responsive
- Accessible
- Practical for repeated use
- Consistent with the existing Avalonia style

Avoid decorative rewrites, marketing sections, or large visual redesigns unless requested. When adding controls, make sure they map to real device capabilities and have safe disabled/loading/error states.

## Testing And Verification

For code changes:

- Run `dotnet build OpenHyperX.sln`.
- Run `dotnet test OpenHyperX.sln --no-build` after a successful build.
- For HID read-only changes, the CLI status probe may be used when hardware is connected.
- For HID write changes, ask before running against hardware.

For documentation-only changes, tests are not required unless the documentation changes examples, commands, or project structure that should be verified.

## Dependency Discipline

Keep dependencies boring and justified.

Before adding a package, check whether the existing stack already solves the problem. If a new dependency is truly useful, explain why it is needed and keep it limited to the project that uses it.

## Legal And Branding Notes

OpenHyperX is unofficial and should not imply endorsement by HP, HyperX, or NGenuity.

Use neutral wording such as "HyperX-compatible" or "for HyperX devices" where appropriate. Do not present the app as official software.

## Agent Behavior

Before making broad architectural changes, read the existing code and follow the current patterns. Keep edits scoped to the request. Prefer small, reviewable steps over sweeping rewrites.

If hardware behavior is uncertain, stop and ask. If legal cleanliness is uncertain, stop and ask. If a proposed change would move the project away from C#/.NET/Avalonia/HidSharp, stop and ask.
