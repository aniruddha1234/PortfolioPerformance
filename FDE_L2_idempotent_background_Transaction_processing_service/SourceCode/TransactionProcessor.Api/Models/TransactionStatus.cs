namespace TransactionProcessor.Api.Models;

public enum TransactionStatus
{
    Received,
    Processing,
    Processed,
    RetryPending,
    Failed,
    Duplicate
}