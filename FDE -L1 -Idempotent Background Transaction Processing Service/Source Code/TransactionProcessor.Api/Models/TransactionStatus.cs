namespace TransactionProcessor.Api.Models;

public enum TransactionStatus
{
    Received,
    Processing,
    Processed,
    Duplicate,
    FailedValidation,
    RetryPending
}