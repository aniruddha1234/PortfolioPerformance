using TransactionProcessor.Api.Models;
using TransactionProcessor.Services;

namespace TransactionProcessor.BackgroundServices;

public class TransactionProcessorService : BackgroundService
{
    private readonly TransactionStore _store;
    private readonly ILogger<TransactionProcessorService> _logger;
    private readonly Random _random = new();

    public TransactionProcessorService(
        TransactionStore store,
        ILogger<TransactionProcessorService> logger)
    {
        _store = store;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Process newly received and retry-pending transactions
            var transactionsToProcess = _store.Transactions.Values
                .Where(t =>
                    t.Status == TransactionStatus.Received ||
                    t.Status == TransactionStatus.RetryPending)
                .OrderBy(t => t.SequenceNumber) // Remove if not implementing sequence handling
                .ToList();

            foreach (var transaction in transactionsToProcess)
            {
                transaction.Status = TransactionStatus.Processing;

                _logger.LogInformation(
                    "Processing transaction {TransactionId}",
                    transaction.TransactionId);

                await Task.Delay(1000, stoppingToken);

                bool success = _random.Next(100) >= 30;

                if (success)
                {
                    transaction.Status = TransactionStatus.Processed;

                    _logger.LogInformation(
                        "Transaction {TransactionId} processed successfully.",
                        transaction.TransactionId);
                }
                else
                {
                    transaction.RetryCount++;

                    if (transaction.RetryCount >= 3)
                    {
                        transaction.Status = TransactionStatus.Failed;

                        _logger.LogError(
                            "Transaction {TransactionId} failed after maximum retries.",
                            transaction.TransactionId);
                    }
                    else
                    {
                        transaction.Status = TransactionStatus.RetryPending;

                        _logger.LogWarning(
                            "Retry {RetryCount}/3 scheduled for transaction {TransactionId}.",
                            transaction.RetryCount,
                            transaction.TransactionId);
                    }
                }
            }

            await Task.Delay(2000, stoppingToken);
        }
    }
}