using TransactionProcessor.Api.Services;

namespace TransactionProcessor.Api.Background;

public class TransactionWorker : BackgroundService
{
    private readonly IBackgroundQueue _queue;
    private readonly TransactionProcessorService _processor;
    private readonly ILogger<TransactionWorker> _logger;

    public TransactionWorker(
        IBackgroundQueue queue,
        TransactionProcessorService processor,
        ILogger<TransactionWorker> logger)
    {
        _queue = queue;
        _processor = processor;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Transaction Worker started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var request = await _queue.DequeueAsync(stoppingToken);

                var result = _processor.Process(request);

                _logger.LogInformation(
                    "Transaction {TransactionId} processed with status: {Status}",
                    result.TransactionId,
                    result.Status);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while processing transaction.");
            }
        }

        _logger.LogInformation("Transaction Worker stopped.");
    }
}