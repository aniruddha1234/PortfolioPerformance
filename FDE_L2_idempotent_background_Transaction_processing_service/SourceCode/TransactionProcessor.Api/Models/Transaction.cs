using System.Text.Json.Serialization;

namespace TransactionProcessor.Api.Models;

public class Transaction
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string TransactionId { get; set; } = string.Empty;

    public string RequestId { get; set; } = string.Empty;

    public decimal Amount { get; set; }

[JsonConverter(typeof(JsonStringEnumConverter))]
    public TransactionStatus Status { get; set; } = TransactionStatus.Received;

    public int SequenceNumber { get; set; }

    public int RetryCount { get; set; }
}