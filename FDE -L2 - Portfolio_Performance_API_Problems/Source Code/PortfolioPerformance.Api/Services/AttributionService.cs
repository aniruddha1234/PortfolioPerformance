using PortfolioPerformance.Api.Models.Enums;
using PortfolioPerformance.Api.Models.Requests;
using PortfolioPerformance.Api.Models.Responses;
using PortfolioPerformance.Api.Services.Interfaces;

namespace PortfolioPerformance.Api.Services;

public class AttributionService : IAttributionService
{
    private readonly PricingService _pricing;
    private readonly IdempotencyService _cache;

    public AttributionService(
        PricingService pricing,
        IdempotencyService cache)
    {
        _pricing = pricing;
        _cache = cache;
    }

    public AttributionResponse Calculate(AttributionRequest request)
    {
        if (_cache.Exists(request.RequestId))
            return _cache.Get(request.RequestId);

        var response = new AttributionResponse
        {
            RequestId = request.RequestId,
            PortfolioId = request.PortfolioId,
            ValuationDate = request.ValuationDate,
            ProcessedAt = DateTime.UtcNow
        };

        decimal totalWeight = request.Groups.Sum(x => x.WeightPct);

        if (totalWeight < 99 || totalWeight > 101)
        {
            response.Status = ProcessingStatus.INVALID_INPUT;
            response.Warnings.Add("Total weight must be between 99 and 101.");
            return response;
        }

        decimal totalContribution = 0;
        int missingGroups = 0;

        foreach (var group in request.Groups)
        {
            var pricing = _pricing.GetReturn(
                group.ReturnPct,
                group.FallbackReturnPct,
                group.GroupName);

            if (pricing.Warning != null)
                response.Warnings.Add(pricing.Warning);

            if (pricing.Missing)
                missingGroups++;

            decimal contribution = pricing.Missing
                ? 0
                : (group.WeightPct * pricing.ReturnPct) / 100;

            totalContribution += contribution;

            response.GroupContributions.Add(new GroupContribution
            {
                GroupName = group.GroupName,
                ContributionPct = contribution,
                PricingMode = pricing.Mode
            });
        }

        response.TotalContributionPct = totalContribution;

        if (missingGroups == 0)
        {
            response.Status = ProcessingStatus.VALID;
        }
        else if (missingGroups == 1)
        {
            response.Status = ProcessingStatus.DEGRADED;
            response.Degraded = true;
        }
        else
        {
            response.Status = ProcessingStatus.REVIEW_REQUIRED;
            response.Degraded = true;
        }

        _cache.Save(request.RequestId, response);

        return response;
    }
}