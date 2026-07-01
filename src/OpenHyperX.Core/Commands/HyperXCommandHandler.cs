namespace OpenHyperX.Core.Commands;

public delegate void HyperXCommandHandler<TContext>(IHyperXCommand<TContext> command, object? info);
