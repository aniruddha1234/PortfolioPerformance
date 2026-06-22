using System.Collections.Concurrent;
using TransactionProcessor.Api.Models;

namespace TransactionProcessor.Api.Services;

public class TransactionProcessorService
{
    // Stores successfully accepted transactions (for idempotency)
    private readonly ConcurrentDictionary<string, TransactionResult> _processedTransactions = new();

    // Stores all processing results (processed, duplicate, validation failure)
    private readonly ConcurrentBag<TransactionResult> _results = new();

    public TransactionResult Process(TransactionRequest request)
    {
        var processingResult = new TransactionResult
        {
            TransactionId = request.TransactionId,
            RequestId = request.RequestId,
            Status = TransactionStatus.Processing,
            Message = "Processing transaction."
        };

        // Atomic operation - prevents race conditions
        if (!_processedTransactions.TryAdd(request.TransactionId, processingResult))
        {
            var duplicate = new TransactionResult
            {
                TransactionId = request.TransactionId,
                RequestId = request.RequestId,
                Status = TransactionStatus.Duplicate,
                Message = "Duplicate transaction."
            };

            _results.Add(duplicate);

            return duplicate;
        }

        // Simulate processing
        Thread.Sleep(500);

        processingResult.Status = TransactionStatus.Processed;
        processingResult.Message = "Transaction processed successfully.";

        _results.Add(processingResult);

        return processingResult;
    }

    public void AddValidationFailure(TransactionRequest request)
    {
        _results.Add(new TransactionResult
        {
            TransactionId = request.TransactionId,
            RequestId = request.RequestId,
            Status = TransactionStatus.FailedValidation,
            Message = "Validation failed."
        });
    }

    public ProcessingSummary GetSummary()
    {
        var results = _results.ToList();

        return new ProcessingSummary
        {
            TotalRequests = results.Count,
            Processed = results.Count(x => x.Status == TransactionStatus.Processed),
            Duplicates = results.Count(x => x.Status == TransactionStatus.Duplicate),
            Failed = results.Count(x => x.Status == TransactionStatus.FailedValidation),
            Results = results
        };
    }
}