using System.ComponentModel.DataAnnotations;

namespace PortfolioPerformance.Api.Models.Requests;

public class AttributionRequest
{
    [Required]
    public string RequestId { get; set; } = string.Empty;

    [Required]
    public string PortfolioId { get; set; } = string.Empty;

    public DateTime ValuationDate { get; set; }

    public List<GroupRequest> Groups { get; set; } = new();

    public string Currency { get; set; } = string.Empty;

    public string RequestedBy { get; set; } = string.Empty;
}