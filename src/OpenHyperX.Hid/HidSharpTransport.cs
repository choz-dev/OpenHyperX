using System.Diagnostics;
using System.IO;
using HidSharp;
using OpenHyperX.Core;

namespace OpenHyperX.Hid;

public sealed class HidSharpTransport : IHyperXTransport
{
    private readonly object _sync = new();
    private readonly HidStream _stream;
    private bool _disposed;

    public HidSharpTransport(HidDeviceInfo deviceInfo, HidStream stream)
    {
        DeviceInfo = deviceInfo;
        _stream = stream;
        InputReportLength = deviceInfo.InputReportLength <= 0 ? HyperXPacket.DefaultReportLength : deviceInfo.InputReportLength;
        OutputReportLength = deviceInfo.OutputReportLength <= 0 ? HyperXPacket.DefaultReportLength : deviceInfo.OutputReportLength;
        FeatureReportLength = deviceInfo.FeatureReportLength <= 0 ? HyperXPacket.DefaultReportLength : deviceInfo.FeatureReportLength;
    }

    public HidDeviceInfo DeviceInfo { get; }

    public int InputReportLength { get; }

    public int OutputReportLength { get; }

    public int FeatureReportLength { get; }

    public Task WriteAsync(byte[] report, CancellationToken cancellationToken = default)
    {
        return Task.Run(
            () =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                lock (_sync)
                {
                    ThrowIfDisposed();
                    try
                    {
                        _stream.Write(report, 0, report.Length);
                        _stream.Flush();
                    }
                    catch (IOException) when (OperatingSystem.IsWindows())
                    {
                        WindowsHidOutputReportWriter.Write(DeviceInfo.Path, report, OutputReportLength);
                    }
                }
            },
            cancellationToken);
    }

    public Task<byte[]?> ReadAsync(TimeSpan timeout, CancellationToken cancellationToken = default)
    {
        return Task.Run(
            () =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                lock (_sync)
                {
                    ThrowIfDisposed();

                    var previousTimeout = _stream.ReadTimeout;
                    _stream.ReadTimeout = Math.Max(1, (int)timeout.TotalMilliseconds);

                    try
                    {
                        var buffer = new byte[InputReportLength];
                        var count = _stream.Read(buffer, 0, buffer.Length);
                        if (count <= 0)
                        {
                            return null;
                        }

                        if (count == buffer.Length)
                        {
                            return buffer;
                        }

                        return buffer[..count];
                    }
                    catch (TimeoutException)
                    {
                        return null;
                    }
                    catch (IOException)
                    {
                        return null;
                    }
                    finally
                    {
                        _stream.ReadTimeout = previousTimeout;
                    }
                }
            },
            cancellationToken);
    }

    public async Task<byte[]?> QueryAsync(
        byte[] report,
        byte expectedCommand,
        TimeSpan timeout,
        CancellationToken cancellationToken = default)
    {
        await WriteAsync(report, cancellationToken).ConfigureAwait(false);

        var stopwatch = Stopwatch.StartNew();
        while (stopwatch.Elapsed < timeout)
        {
            var remaining = timeout - stopwatch.Elapsed;
            var response = await ReadAsync(remaining, cancellationToken).ConfigureAwait(false);
            if (response is null)
            {
                continue;
            }

            if (HyperXPacket.TryGetCommand(response, out var command) && command == expectedCommand)
            {
                return response;
            }
        }

        return null;
    }

    public Task SetFeatureReportAsync(byte[] report, CancellationToken cancellationToken = default)
    {
        return Task.Run(
            () =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                lock (_sync)
                {
                    ThrowIfDisposed();
                    _stream.SetFeature(report);
                }
            },
            cancellationToken);
    }

    public Task<byte[]?> GetFeatureReportAsync(byte reportId, CancellationToken cancellationToken = default)
    {
        return Task.Run(
            () =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                lock (_sync)
                {
                    ThrowIfDisposed();

                    try
                    {
                        var buffer = new byte[FeatureReportLength];
                        buffer[0] = reportId;
                        _stream.GetFeature(buffer);
                        return buffer;
                    }
                    catch (TimeoutException)
                    {
                        return null;
                    }
                    catch (IOException)
                    {
                        return null;
                    }
                }
            },
            cancellationToken);
    }

    public void Dispose()
    {
        lock (_sync)
        {
            if (_disposed)
            {
                return;
            }

            _stream.Dispose();
            _disposed = true;
        }
    }

    public ValueTask DisposeAsync()
    {
        Dispose();
        return ValueTask.CompletedTask;
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(HidSharpTransport));
        }
    }
}
