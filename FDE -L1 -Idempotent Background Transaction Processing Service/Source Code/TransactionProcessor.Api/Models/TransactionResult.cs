namespace TransactionProcessor.Api.Models;

public class TransactionResult
{
    public string TransactionId { get; set; } = string.Empty;

    public string RequestId { get; set; } = string.Empty;

    public TransactionStatus Status { get; set; }

    public string Message { get; set; } = string.Empty;
}