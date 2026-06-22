namespace TransactionProcessor.Api.Models;

public class ProcessingSummary
{
    public int TotalRequests { get; set; }

    public int Processed { get; set; }

    public int Duplicates { get; set; }

    public int Failed { get; set; }

    public List<TransactionResult> Results { get; set; } = new();
}