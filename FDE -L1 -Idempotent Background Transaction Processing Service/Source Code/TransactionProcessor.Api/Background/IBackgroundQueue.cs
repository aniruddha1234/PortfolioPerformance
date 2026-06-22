using System.Threading.Channels;
using TransactionProcessor.Api.Models;

namespace TransactionProcessor.Api.Background;

public interface IBackgroundQueue
{
    ValueTask QueueAsync(TransactionRequest request);

    ValueTask<TransactionRequest> DequeueAsync(
        CancellationToken cancellationToken);
}