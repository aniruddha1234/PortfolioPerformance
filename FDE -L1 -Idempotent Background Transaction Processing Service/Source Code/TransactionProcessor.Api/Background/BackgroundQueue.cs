using System.Threading.Channels;
using TransactionProcessor.Api.Models;

namespace TransactionProcessor.Api.Background;

public class BackgroundQueue : IBackgroundQueue
{
    private readonly Channel<TransactionRequest> _queue =
        Channel.CreateUnbounded<TransactionRequest>();

    public async ValueTask QueueAsync(TransactionRequest request)
    {
        await _queue.Writer.WriteAsync(request);
    }

    public async ValueTask<TransactionRequest> DequeueAsync(CancellationToken cancellationToken)
    {
        return await _queue.Reader.ReadAsync(cancellationToken);
    }
}