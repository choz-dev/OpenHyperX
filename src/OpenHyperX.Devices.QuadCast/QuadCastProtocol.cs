namespace OpenHyperX.Devices.QuadCast;

public static class QuadCastProtocol
{
    public static byte[] CreateReportRequest(byte command, int reportLength)
    {
        return CreateReport(command, null, reportLength);
    }

    public static byte[] CreateValueReport(byte command, byte value, int reportLength)
    {
        return CreateReport(command, value, reportLength);
    }

    public static byte[] CreateFeatureRequest(byte command, int reportLength)
    {
        var length = Math.Max(reportLength <= 0 ? 264 : reportLength, 10);
        var report = new byte[length];
        report[0] = QuadCastCommandIds.FeatureReportId;
        report[1] = 0x04;
        report[2] = command;
        report[9] = 0x01;
        return report;
    }

    public static bool TryGetReportValue(ReadOnlySpan<byte> report, byte expectedCommand, out byte value)
    {
        var offset = FindReportMarker(report);
        if (offset < 0 || report.Length <= offset + 2 || report[offset + 1] != expectedCommand)
        {
            value = 0;
            return false;
        }

        value = report[offset + 2];
        return true;
    }

    public static bool TryGetReportCommand(ReadOnlySpan<byte> report, out byte command)
    {
        var offset = FindReportMarker(report);
        if (offset < 0 || report.Length <= offset + 1)
        {
            command = 0;
            return false;
        }

        command = report[offset + 1];
        return true;
    }

    public static bool TryParseReportPolarPattern(
        QuadCastModel model,
        byte raw,
        out QuadCastPolarPattern pattern)
    {
        pattern = model switch
        {
            QuadCastModel.QuadCast2 => raw switch
            {
                0x00 => QuadCastPolarPattern.Cardioid,
                0x01 => QuadCastPolarPattern.Omnidirectional,
                0x02 => QuadCastPolarPattern.Stereo,
                0x03 => QuadCastPolarPattern.Bidirectional,
                _ => default
            },
            QuadCastModel.QuadCast2S or QuadCastModel.QuadCastS => raw switch
            {
                0x00 => QuadCastPolarPattern.Bidirectional,
                0x01 => QuadCastPolarPattern.Cardioid,
                0x02 => QuadCastPolarPattern.Omnidirectional,
                0x03 => QuadCastPolarPattern.Stereo,
                _ => default
            },
            _ => default
        };

        return raw <= 0x03;
    }

    public static bool TryGetPolarPatternRaw(
        QuadCastModel model,
        QuadCastPolarPattern pattern,
        out byte raw)
    {
        raw = model switch
        {
            QuadCastModel.QuadCast2 => pattern switch
            {
                QuadCastPolarPattern.Cardioid => 0x00,
                QuadCastPolarPattern.Omnidirectional => 0x01,
                QuadCastPolarPattern.Stereo => 0x02,
                QuadCastPolarPattern.Bidirectional => 0x03,
                _ => 0
            },
            QuadCastModel.QuadCast2S => pattern switch
            {
                QuadCastPolarPattern.Bidirectional => 0x00,
                QuadCastPolarPattern.Cardioid => 0x01,
                QuadCastPolarPattern.Omnidirectional => 0x02,
                QuadCastPolarPattern.Stereo => 0x03,
                _ => 0
            },
            _ => 0
        };

        return model is QuadCastModel.QuadCast2 or QuadCastModel.QuadCast2S;
    }

    public static bool TryParseFeatureStatus(
        ReadOnlySpan<byte> report,
        bool secondSourceLayout,
        out bool muted,
        out QuadCastPolarPattern pattern,
        out int? brightnessPercent,
        out bool? reverseLights)
    {
        var mutedIndex = secondSourceLayout ? 6 : 13;
        var patternIndex = secondSourceLayout ? 7 : 17;
        if (!IsFeatureResponse(report, QuadCastCommandIds.FeatureGetDeviceStatus) || report.Length <= patternIndex)
        {
            muted = false;
            pattern = default;
            brightnessPercent = null;
            reverseLights = null;
            return false;
        }

        if (!TryParseReportPolarPattern(QuadCastModel.QuadCastS, report[patternIndex], out pattern))
        {
            muted = false;
            brightnessPercent = null;
            reverseLights = null;
            return false;
        }

        muted = report[mutedIndex] == 0x01;
        brightnessPercent = secondSourceLayout && report.Length > 8 ? ToPercent(report[8]) : null;
        reverseLights = secondSourceLayout && report.Length > 9 ? report[9] == 0x01 : null;
        return true;
    }

    public static bool TryParseFeatureProfile(
        ReadOnlySpan<byte> report,
        out int brightnessPercent,
        out bool reverseLights)
    {
        if (!IsFeatureResponse(report, QuadCastCommandIds.FeatureGetCurrentProfile) || report.Length <= 17)
        {
            brightnessPercent = 0;
            reverseLights = false;
            return false;
        }

        brightnessPercent = ToPercent(report[13]);
        reverseLights = report[17] == 0x01;
        return true;
    }

    private static byte[] CreateReport(byte command, byte? value, int reportLength)
    {
        var minimumLength = value is null ? 3 : 4;
        var length = Math.Max(reportLength <= 0 ? 64 : reportLength, minimumLength);
        var report = new byte[length];
        report[1] = QuadCastCommandIds.ReportMarker;
        report[2] = command;

        if (value is { } payload)
        {
            report[3] = payload;
        }

        return report;
    }

    private static int FindReportMarker(ReadOnlySpan<byte> report)
    {
        if (report.Length >= 2 && report[0] == QuadCastCommandIds.ReportMarker)
        {
            return 0;
        }

        if (report.Length >= 3 && report[1] == QuadCastCommandIds.ReportMarker)
        {
            return 1;
        }

        return -1;
    }

    private static bool IsFeatureResponse(ReadOnlySpan<byte> report, byte expectedCommand)
    {
        return report.Length > 4
            && report[0] == QuadCastCommandIds.FeatureReportId
            && report[2] == expectedCommand;
    }

    private static int ToPercent(byte raw)
    {
        return (int)Math.Round(raw * 100d / 255d, MidpointRounding.AwayFromZero);
    }
}
