using PortfolioPerformance.Api.Models.Requests;
using PortfolioPerformance.Api.Services;
using Xunit;

namespace PortfolioPerformance.Tests;

public class IdempotencyTests
{
    [Fact]
    public void Same_Request_Should_Return_Cached_Response()
    {
        var service = new AttributionService(
            new PricingService(),
            new IdempotencyService());

        var request = new AttributionRequest
        {
            RequestId = "CACHE-1",
            PortfolioId = "PF",
            Groups = new()
            {
                new()
                {
                    GroupName="Equity",
                    WeightPct=100,
                    ReturnPct=1
                }
            }
        };

        var first = service.Calculate(request);

        Thread.Sleep(100);

        var second = service.Calculate(request);

        Assert.Equal(first.ProcessedAt,
                     second.ProcessedAt);
    }
}