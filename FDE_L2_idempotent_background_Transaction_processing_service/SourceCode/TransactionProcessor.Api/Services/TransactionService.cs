using TransactionProcessor.Api.Models;

namespace TransactionProcessor.Services;

public class TransactionService
{
    private readonly TransactionStore _store;

    public TransactionService(TransactionStore store)
    {
        _store = store;
    }

public TransactionResponse Create(TransactionRequest request)
{
    // 1. Idempotent request (same RequestId/IdempotencyKey)
    if (_store.Transactions.TryGetValue(request.RequestId, out var existingRequest))
    {
        return new TransactionResponse
        {
            Id = existingRequest.Id,
            TransactionId = existingRequest.TransactionId,
            Status = existingRequest.Status.ToString(),
            IsDuplicate = true,
            SequenceNumber = existingRequest.SequenceNumber,
            RetryCount = existingRequest.RetryCount
        };
    }

    // 2. Duplicate business transaction (same TransactionId)
    var duplicateTransaction = _store.Transactions.Values
        .FirstOrDefault(t => t.TransactionId == request.TransactionId);

    if (duplicateTransaction != null)
    {
        duplicateTransaction.Status = TransactionStatus.Duplicate;

        return new TransactionResponse
        {
            Id = duplicateTransaction.Id,
            TransactionId = duplicateTransaction.TransactionId,
            Status = TransactionStatus.Duplicate.ToString(),
            IsDuplicate = true,
            RetryCount = duplicateTransaction.RetryCount,
            SequenceNumber = duplicateTransaction.SequenceNumber
        };
    }

    // 3. Create a new transaction
    var transaction = new Transaction
    {
        TransactionId = request.TransactionId,
        RequestId = request.RequestId,
        Amount = request.Amount,
        SequenceNumber = request.SequenceNumber,
        Status = TransactionStatus.Received,
        RetryCount = 0
    };

    _store.Transactions.TryAdd(request.RequestId, transaction);

    return new TransactionResponse
    {
        Id = transaction.Id,
        TransactionId = transaction.TransactionId,
        Status = transaction.Status.ToString(),
        IsDuplicate = false,
        SequenceNumber = transaction.SequenceNumber,
        RetryCount = transaction.RetryCount
    };
}

    public List<Transaction> GetAll()
    {
        return _store.Transactions.Values.ToList();
    }

    public Transaction? Get(Guid id)
    {
        return _store.Transactions.Values
            .FirstOrDefault(t => t.Id == id);
    }

    public object GetSummary()
{
    var transactions = _store.Transactions.Values;

    return new
    {
        Received = transactions.Count(t => t.Status == TransactionStatus.Received),
        Processing = transactions.Count(t => t.Status == TransactionStatus.Processing),
        Processed = transactions.Count(t => t.Status == TransactionStatus.Processed),
        RetryPending = transactions.Count(t => t.Status == TransactionStatus.RetryPending),
        Failed = transactions.Count(t => t.Status == TransactionStatus.Failed),
        Duplicate = transactions.Count(t => t.Status == TransactionStatus.Duplicate)
    };
}
}