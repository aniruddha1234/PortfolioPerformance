using System.ComponentModel.DataAnnotations;

namespace PortfolioPerformance.Api.Models.Requests;

public class GroupRequest
{
    [Required]
    public string GroupName { get; set; } = string.Empty;

    public decimal WeightPct { get; set; }

    public decimal? ReturnPct { get; set; }

    public decimal? FallbackReturnPct { get; set; }
}