public class TransactionRequest
{
    public string TransactionId { get; set; } = string.Empty;

    public string RequestId { get; set; } = string.Empty;
    public int SequenceNumber { get; set; }
    public string AccountId { get; set; } = string.Empty;

    public string TransactionType { get; set; } = string.Empty;

    public decimal Amount { get; set; }

}