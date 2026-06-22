using PortfolioPerformance.Api.Enums;

public class DailyReturnResponse
{
    public string PortfolioId { get; set; } = string.Empty;

    public DateTime ValuationDate { get; set; }

    public decimal DailyReturn { get; set; }

    public decimal BenchmarkReturn { get; set; }

    public decimal Difference { get; set; }

    public string Status { get; set; } = string.Empty;

    public List<string> Reasons { get; set; } = new();

    public DateTime ProcessedAt { get; set; }
}