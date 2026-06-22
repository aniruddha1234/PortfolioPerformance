using PortfolioPerformance.Api.Models.Enums;

namespace PortfolioPerformance.Api.Services;

public class PricingService
{
    public (decimal ReturnPct, PricingMode Mode, string? Warning, bool Missing) GetReturn(
        decimal? primaryReturn,
        decimal? fallbackReturn,
        string groupName)
    {
        if (primaryReturn.HasValue)
        {
            return (primaryReturn.Value,
                PricingMode.PRIMARY,
                null,
                false);
        }

        if (fallbackReturn.HasValue)
        {
            return (fallbackReturn.Value,
                PricingMode.FALLBACK_USED,
                $"Fallback pricing used for {groupName}",
                false);
        }

        return (0,
            PricingMode.UNAVAILABLE,
            $"Pricing unavailable for {groupName}",
            true);
    }
}