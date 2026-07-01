namespace OpenHyperX.Core;

public static class HyperXPacket
{
    public const byte ReportId = 0x00;
    public const byte Header0 = 0x21;
    public const byte Header1 = 0xBB;
    public const int DefaultReportLength = 32;

    public static byte[] CreateReport(byte command, int reportLength)
    {
        return CreateReport(command, reportLength, ReadOnlySpan<byte>.Empty);
    }

    public static byte[] CreateReport(byte command, int reportLength, ReadOnlySpan<byte> payload)
    {
        return CreateReport(command, reportLength, payload, includeReportId: true);
    }

    public static byte[] CreateRawReport(byte command, int reportLength)
    {
        return CreateReport(command, reportLength, ReadOnlySpan<byte>.Empty, includeReportId: false);
    }

    public static byte[] CreateRawReport(byte command, int reportLength, ReadOnlySpan<byte> payload)
    {
        return CreateReport(command, reportLength, payload, includeReportId: false);
    }

    public static byte[] CreateReport(
        byte command,
        int reportLength,
        ReadOnlySpan<byte> payload,
        bool includeReportId)
    {
        var length = Math.Max(reportLength <= 0 ? DefaultReportLength : reportLength, 4 + payload.Length);
        var report = new byte[length];

        if (includeReportId)
        {
            report[0] = ReportId;
            report[1] = Header0;
            report[2] = Header1;
            report[3] = command;
            payload.CopyTo(report.AsSpan(4));
        }
        else
        {
            report[0] = Header0;
            report[1] = Header1;
            report[2] = command;
            payload.CopyTo(report.AsSpan(3));
        }

        return report;
    }

    public static bool TryGetCommand(ReadOnlySpan<byte> report, out byte command)
    {
        var headerOffset = FindHeaderOffset(report);
        if (headerOffset < 0)
        {
            command = 0;
            return false;
        }

        command = report[headerOffset + 2];
        return true;
    }

    public static bool TryGetPayload(ReadOnlySpan<byte> report, out ReadOnlySpan<byte> payload)
    {
        var headerOffset = FindHeaderOffset(report);
        if (headerOffset < 0)
        {
            payload = ReadOnlySpan<byte>.Empty;
            return false;
        }

        payload = report[(headerOffset + 3)..];
        return true;
    }

    private static int FindHeaderOffset(ReadOnlySpan<byte> report)
    {
        if (report.Length >= 3 && report[0] == Header0 && report[1] == Header1)
        {
            return 0;
        }

        if (report.Length >= 4 && report[0] == ReportId && report[1] == Header0 && report[2] == Header1)
        {
            return 1;
        }

        return -1;
    }
}
