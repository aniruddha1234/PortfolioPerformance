using PortfolioPerformance.Api.Models.Enums;
using System.Text.Json.Serialization;

namespace PortfolioPerformance.Api.Models.Responses;

public class GroupContribution
{
    public string GroupName { get; set; } = string.Empty;

    public decimal ContributionPct { get; set; }


     [JsonConverter(typeof(JsonStringEnumConverter))]
    public PricingMode PricingMode { get; set; }
}