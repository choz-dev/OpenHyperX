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
    private const uint FileShareDelete = 0x00000004;
    private const uint OpenExisting = 3;

    private static readonly uint[] DesiredAccessFallbacks =
    [
        GenericRead | GenericWrite,
        GenericWrite,
        0
    ];

    public static void Write(string devicePath, byte[] report)
    {
        if (!OperatingSystem.IsWindows())
        {
            throw new PlatformNotSupportedException("HID output-report fallback is only supported on Windows.");
        }

        Exception? lastException = null;
        foreach (var desiredAccess in DesiredAccessFallbacks)
        {
            try
            {
                WriteWithAccess(devicePath, report, desiredAccess);
                return;
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                lastException = ex;
            }
        }

        throw new IOException("HID SetOutputReport fallback failed.", lastException);
    }

    private static void WriteWithAccess(string devicePath, byte[] report, uint desiredAccess)
    {
        using var handle = CreateFile(
            devicePath,
            desiredAccess,
            FileShareRead | FileShareWrite | FileShareDelete,
            IntPtr.Zero,
            OpenExisting,
            0,
            IntPtr.Zero);

        if (handle.IsInvalid)
        {
            throw CreateIOException("Open HID device for output report");
        }

        if (!HidD_SetOutputReport(handle, report, report.Length))
        {
            throw CreateIOException("Set HID output report");
        }
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
