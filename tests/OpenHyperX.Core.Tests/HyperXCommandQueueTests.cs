using OpenHyperX.Core.Commands;

namespace OpenHyperX.Core.Tests;

public sealed class HyperXCommandQueueTests
{
    [Fact]
    public async Task EnqueueAsyncExecutesCommandsInOrder()
    {
        var log = new List<string>();
        await using var queue = new HyperXCommandQueue<List<string>>(log);

        queue.Start();

        await queue.EnqueueAsync(new RecordingCommand("first"));
        await queue.EnqueueAsync(new RecordingCommand("second"));

        Assert.Equal(["first", "second"], log);
    }

    [Fact]
    public async Task SkipRemovesOlderPendingCommandOfSameTypeAndProfile()
    {
        var log = new List<string>();
        await using var queue = new HyperXCommandQueue<List<string>>(log);

        queue.Start();
        queue.Pause();

        var first = queue.EnqueueAsync(new RecordingCommand("old") { Skip = true, ProfileId = 2 });
        var second = queue.EnqueueAsync(new RecordingCommand("new") { Skip = true, ProfileId = 2 });

        Assert.Equal(1, queue.Count);

        queue.Resume();
        await second;

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => first);
        Assert.Equal(["new"], log);
    }

    [Fact]
    public async Task CompleteInvokesHandler()
    {
        var log = new List<string>();
        object? handlerInfo = null;
        await using var queue = new HyperXCommandQueue<List<string>>(log);

        var command = new RecordingCommand("handled")
        {
            Handler = (_, info) => handlerInfo = info
        };

        queue.Start();
        await queue.EnqueueAsync(command);

        Assert.True(command.Succeeded);
        Assert.Equal("handled", handlerInfo);
    }

    private sealed class RecordingCommand : HyperXCommandBase<List<string>>
    {
        private readonly string _value;

        public RecordingCommand(string value)
        {
            _value = value;
        }

        public override Task ExecuteAsync(List<string> context, CancellationToken cancellationToken = default)
        {
            context.Add(_value);
            Complete(_value);
            return Task.CompletedTask;
        }
    }
}
