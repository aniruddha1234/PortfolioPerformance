namespace TransactionProcessor.Api.Models;

public class TransactionRequest
{
    public string TransactionId { get; set; } = string.Empty;

    public string RequestId { get; set; } = string.Empty;

    public string AccountId { get; set; } = string.Empty;

    public string TransactionType { get; set; } = string.Empty;

    public decimal Amount { get; set; }

     public string Currency { get; set; } = string.Empty;

    public string SequenceNumber { get; set; } = string.Empty;


}