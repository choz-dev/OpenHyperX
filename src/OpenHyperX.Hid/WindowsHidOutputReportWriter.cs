using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace OpenHyperX.Hid;

internal static class WindowsHidOutputReportWriter
{
    private const uint GenericRead = 0x80000000;
    private const uint GenericWrite = 0x40000000;
    private const uint FileShareRead = 0x00000001;
    private const uint FileShareWrite = 0x00000002;
    private const uint OpenExisting = 3;

    private static readonly uint[] DesiredAccessFallbacks =
    [
        GenericRead | GenericWrite,
        GenericWrite,
        0
    ];

    public static void Write(string devicePath, byte[] report, int outputReportLength)
    {
        if (!OperatingSystem.IsWindows())
        {
            throw new PlatformNotSupportedException("HID output-report fallback is only supported on Windows.");
        }

        var normalizedReport = NormalizeReport(report, outputReportLength);
        Exception? fileStreamException = null;
        var setOutputReportExceptions = new List<Exception>();

        try
        {
            WriteWithFileStream(devicePath, normalizedReport);
            return;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            fileStreamException = ex;
        }

        foreach (var desiredAccess in DesiredAccessFallbacks)
        {
            try
            {
                WriteWithSetOutputReport(devicePath, normalizedReport, desiredAccess);
                return;
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                setOutputReportExceptions.Add(ex);
            }
        }

        throw new IOException(
            BuildFailureMessage(fileStreamException, setOutputReportExceptions),
            setOutputReportExceptions.LastOrDefault() ?? fileStreamException);
    }

    private static string BuildFailureMessage(Exception? fileStreamException, IReadOnlyList<Exception> setOutputReportExceptions)
    {
        var fileStreamMessage = fileStreamException is null
            ? "not attempted"
            : $"{fileStreamException.GetType().Name}: {fileStreamException.Message}";
        var outputReportMessage = setOutputReportExceptions.Count == 0
            ? "not attempted"
            : string.Join("; ", setOutputReportExceptions.Select(ex => $"{ex.GetType().Name}: {ex.Message}"));

        return $"HID output-report fallback failed. FileStream: {fileStreamMessage}. HidD_SetOutputReport: {outputReportMessage}.";
    }

    private static byte[] NormalizeReport(byte[] report, int outputReportLength)
    {
        if (outputReportLength <= 0 || report.Length == outputReportLength)
        {
            return report;
        }

        var normalizedReport = new byte[outputReportLength];
        Array.Copy(report, normalizedReport, Math.Min(report.Length, normalizedReport.Length));
        return normalizedReport;
    }

    private static void WriteWithFileStream(string devicePath, byte[] report)
    {
        using var handle = OpenDevice(devicePath, GenericRead | GenericWrite);
        using var stream = new FileStream(handle, FileAccess.Write, report.Length, isAsync: false);
        stream.Write(report, 0, report.Length);
        stream.Flush();
    }

    private static void WriteWithSetOutputReport(string devicePath, byte[] report, uint desiredAccess)
    {
        using var handle = OpenDevice(devicePath, desiredAccess);
        if (!HidD_SetOutputReport(handle, report, report.Length))
        {
            throw CreateIOException("Set HID output report");
        }
    }

    private static SafeFileHandle OpenDevice(string devicePath, uint desiredAccess)
    {
        var handle = CreateFile(
            devicePath,
            desiredAccess,
            FileShareRead | FileShareWrite,
            IntPtr.Zero,
            OpenExisting,
            0,
            IntPtr.Zero);

        if (handle.IsInvalid)
        {
            throw CreateIOException("Open HID device for output report");
        }

        return handle;
    }

    private static IOException CreateIOException(string operation)
    {
        var errorCode = Marshal.GetLastWin32Error();
        return new IOException($"{operation} failed: {new Win32Exception(errorCode).Message}", errorCode);
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern SafeFileHandle CreateFile(
        string lpFileName,
        uint dwDesiredAccess,
        uint dwShareMode,
        IntPtr lpSecurityAttributes,
        uint dwCreationDisposition,
        uint dwFlagsAndAttributes,
        IntPtr hTemplateFile);

    [DllImport("hid.dll", SetLastError = true)]
    private static extern bool HidD_SetOutputReport(
        SafeFileHandle hidDeviceObject,
        byte[] reportBuffer,
        int reportBufferLength);
}
