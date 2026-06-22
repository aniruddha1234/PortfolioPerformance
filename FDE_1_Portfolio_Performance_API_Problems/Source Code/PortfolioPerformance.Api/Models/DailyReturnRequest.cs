namespace PortfolioPerformance.Api.Models;

public class DailyReturnRequest
{
    public string PortfolioId { get; set; } = string.Empty;

    public DateTime ValuationDate { get; set; }

    public decimal PortfolioStartValue { get; set; }

    public decimal PortfolioEndValue { get; set; }

    public decimal NetCashFlow { get; set; }

    public decimal BenchmarkReturn { get; set; }

    public string Currency { get; set; } = string.Empty;

    public string RequestedBy { get; set; } = string.Empty;
}