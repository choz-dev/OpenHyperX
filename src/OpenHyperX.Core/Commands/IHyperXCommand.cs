namespace OpenHyperX.Core.Commands;

public interface IHyperXCommand<TContext>
{
    int Delay { get; set; }

    bool Skip { get; set; }

    byte ProfileId { get; set; }

    bool ResetLighting { get; set; }

    bool Force { get; set; }

    bool Succeeded { get; set; }

    HyperXCommandHandler<TContext>? Handler { get; set; }

    Task ExecuteAsync(TContext context, CancellationToken cancellationToken = default);
}
