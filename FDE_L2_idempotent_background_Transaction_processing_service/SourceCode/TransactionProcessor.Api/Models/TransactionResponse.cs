namespace TransactionProcessor.Api.Models;
public class TransactionResponse
{
    public Guid Id { get; set; }

    public string TransactionId { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public bool IsDuplicate { get; set; }
        public int RetryCount { get; set; }

        public int SequenceNumber { get; set; }
}