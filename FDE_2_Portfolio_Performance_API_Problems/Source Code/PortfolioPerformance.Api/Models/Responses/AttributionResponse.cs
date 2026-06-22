using PortfolioPerformance.Api.Models.Enums;
using System.Text.Json.Serialization;

namespace PortfolioPerformance.Api.Models.Responses;

public class AttributionResponse
{
    public string RequestId { get; set; } = string.Empty;

    public string PortfolioId { get; set; } = string.Empty;

    public DateTime ValuationDate { get; set; }

    public decimal TotalContributionPct { get; set; }

    public List<GroupContribution> GroupContributions { get; set; } = new();

     [JsonConverter(typeof(JsonStringEnumConverter))]
    public ProcessingStatus Status { get; set; }

    public bool Degraded { get; set; }

    public List<string> Warnings { get; set; } = new();

    public DateTime ProcessedAt { get; set; }
}