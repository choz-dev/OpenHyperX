namespace OpenHyperX.Core.Commands;

public abstract class HyperXCommandBase<TContext> : IHyperXCommand<TContext>
{
    public int Delay { get; set; }

    public bool Skip { get; set; }

    public byte ProfileId { get; set; }

    public bool ResetLighting { get; set; }

    public bool Force { get; set; }

    public bool Succeeded { get; set; }

    public HyperXCommandHandler<TContext>? Handler { get; set; }

    public abstract Task ExecuteAsync(TContext context, CancellationToken cancellationToken = default);

    protected void Complete(object? info = null)
    {
        Succeeded = true;
        Handler?.Invoke(this, info);
    }
}
