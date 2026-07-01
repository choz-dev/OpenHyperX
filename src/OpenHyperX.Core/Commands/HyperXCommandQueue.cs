namespace OpenHyperX.Core.Commands;

public sealed class HyperXCommandQueue<TContext> : IAsyncDisposable, IDisposable
{
    private readonly TContext _context;
    private readonly List<QueuedCommand> _commands = [];
    private readonly SemaphoreSlim _signal = new(0);
    private readonly object _sync = new();
    private CancellationTokenSource? _cts;
    private Task? _worker;
    private bool _paused;
    private bool _stopped = true;

    public HyperXCommandQueue(TContext context)
    {
        _context = context;
    }

    public bool IsRunning => _worker is not null && !_worker.IsCompleted && !_stopped;

    public int Count
    {
        get
        {
            lock (_sync)
            {
                return _commands.Count;
            }
        }
    }

    public void Start()
    {
        if (IsRunning)
        {
            return;
        }

        _cts = new CancellationTokenSource();
        _paused = false;
        _stopped = false;
        _worker = Task.Run(() => ProcessAsync(_cts.Token));
    }

    public void Pause()
    {
        _paused = true;
    }

    public void Resume()
    {
        _paused = false;
        _signal.Release();
    }

    public Task EnqueueAsync(IHyperXCommand<TContext> command)
    {
        var queued = new QueuedCommand(command);

        lock (_sync)
        {
            if (_stopped && !command.Force)
            {
                queued.Completion.TrySetCanceled();
                return queued.Completion.Task;
            }

            if (command.Skip)
            {
                RemovePendingDuplicates(command);
            }

            _commands.Add(queued);
        }

        _signal.Release();
        return queued.Completion.Task;
    }

    public void Clear()
    {
        List<QueuedCommand> removed;
        lock (_sync)
        {
            removed = [.. _commands];
            _commands.Clear();
        }

        foreach (var queued in removed)
        {
            queued.Completion.TrySetCanceled();
        }
    }

    public async Task StopAsync()
    {
        _stopped = true;
        _cts?.Cancel();
        _signal.Release();

        if (_worker is not null)
        {
            try
            {
                await _worker.ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
            }
        }

        Clear();
        _worker = null;
        _cts?.Dispose();
        _cts = null;
    }

    public void Dispose()
    {
        StopAsync().GetAwaiter().GetResult();
        _signal.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await StopAsync().ConfigureAwait(false);
        _signal.Dispose();
    }

    private async Task ProcessAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await _signal.WaitAsync(cancellationToken).ConfigureAwait(false);
            await WaitWhilePausedAsync(cancellationToken).ConfigureAwait(false);

            var queued = TakeNextCommand();
            if (queued is null)
            {
                continue;
            }

            try
            {
                if (queued.Command.Delay > 0)
                {
                    await Task.Delay(queued.Command.Delay, cancellationToken).ConfigureAwait(false);
                }

                await queued.Command.ExecuteAsync(_context, cancellationToken).ConfigureAwait(false);
                queued.Command.Succeeded = true;
                queued.Completion.TrySetResult(null);
            }
            catch (OperationCanceledException ex)
            {
                queued.Command.Succeeded = false;
                queued.Completion.TrySetCanceled(ex.CancellationToken);
            }
            catch (Exception ex)
            {
                queued.Command.Succeeded = false;
                queued.Completion.TrySetException(ex);
            }
        }
    }

    private async Task WaitWhilePausedAsync(CancellationToken cancellationToken)
    {
        while (_paused && !cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(25, cancellationToken).ConfigureAwait(false);
        }
    }

    private QueuedCommand? TakeNextCommand()
    {
        lock (_sync)
        {
            if (_commands.Count == 0)
            {
                return null;
            }

            var queued = _commands[0];
            _commands.RemoveAt(0);
            return queued;
        }
    }

    private void RemovePendingDuplicates(IHyperXCommand<TContext> command)
    {
        for (var index = _commands.Count - 1; index >= 0; index--)
        {
            var queued = _commands[index];
            if (queued.Command.GetType() != command.GetType() || queued.Command.ProfileId != command.ProfileId)
            {
                continue;
            }

            _commands.RemoveAt(index);
            queued.Completion.TrySetCanceled();
        }
    }

    private sealed class QueuedCommand
    {
        public QueuedCommand(IHyperXCommand<TContext> command)
        {
            Command = command;
        }

        public IHyperXCommand<TContext> Command { get; }

        public TaskCompletionSource<object?> Completion { get; } =
            new(TaskCreationOptions.RunContinuationsAsynchronously);
    }
}
