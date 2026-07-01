namespace OpenHyperX.Core;

public interface IHyperXTransport : IDisposable, IAsyncDisposable
{
    HidDeviceInfo DeviceInfo { get; }

    int InputReportLength { get; }

    int OutputReportLength { get; }

    Task WriteAsync(byte[] report, CancellationToken cancellationToken = default);

    Task<byte[]?> ReadAsync(TimeSpan timeout, CancellationToken cancellationToken = default);

    Task<byte[]?> QueryAsync(
        byte[] report,
        byte expectedCommand,
        TimeSpan timeout,
        CancellationToken cancellationToken = default);
}
