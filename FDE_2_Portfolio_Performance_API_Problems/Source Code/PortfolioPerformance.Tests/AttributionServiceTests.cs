using PortfolioPerformance.Api.Models.Enums;
using PortfolioPerformance.Api.Models.Requests;
using PortfolioPerformance.Api.Services;
using Xunit;

namespace PortfolioPerformance.Tests;

public class AttributionServiceTests
{
    private readonly AttributionService _service;

    public AttributionServiceTests()
    {
        _service = new AttributionService(
            new PricingService(),
            new IdempotencyService());
    }

    [Fact]
    public void Should_Return_Valid_Response()
    {
        var request = new AttributionRequest
        {
            RequestId = "REQ-1",
            PortfolioId = "PF-1",
            Groups = new()
            {
                new() { GroupName="Equity", WeightPct=60, ReturnPct=1.5m },
                new() { GroupName="Bond", WeightPct=40, ReturnPct=0.5m }
            }
        };

        var result = _service.Calculate(request);

        Assert.Equal(ProcessingStatus.VALID, result.Status);
        Assert.False(result.Degraded);
    }

    [Fact]
    public void Should_Use_Fallback()
    {
        var request = new AttributionRequest
        {
            RequestId = "REQ-2",
            PortfolioId = "PF-1",
            Groups = new()
            {
                new()
                {
                    GroupName="Cash",
                    WeightPct=100,
                    ReturnPct=null,
                    FallbackReturnPct=0.2m
                }
            }
        };

        var result = _service.Calculate(request);

        Assert.Equal(PricingMode.FALLBACK_USED,
            result.GroupContributions.First().PricingMode);
    }

    [Fact]
    public void Should_Return_Degraded()
    {
        var request = new AttributionRequest
        {
            RequestId = "REQ-3",
            PortfolioId = "PF-1",
            Groups = new()
            {
                new()
                {
                    GroupName="Cash",
                    WeightPct=100
                }
            }
        };

        var result = _service.Calculate(request);

        Assert.Equal(ProcessingStatus.DEGRADED, result.Status);
    }

    [Fact]
    public void Should_Return_ReviewRequired()
    {
        var request = new AttributionRequest
        {
            RequestId = "REQ-4",
            PortfolioId = "PF-1",
            Groups = new()
            {
                new(){GroupName="Equity",WeightPct=50},
                new(){GroupName="Bond",WeightPct=50}
            }
        };

        var result = _service.Calculate(request);

        Assert.Equal(
            ProcessingStatus.REVIEW_REQUIRED,
            result.Status);
    }

    [Fact]
    public void Should_Return_Invalid_Input()
    {
        var request = new AttributionRequest
        {
            RequestId="REQ-5",
            PortfolioId="PF-1",
            Groups=new()
            {
                new(){GroupName="Equity",WeightPct=60,ReturnPct=1},
                new(){GroupName="Bond",WeightPct=20,ReturnPct=1}
            }
        };

        var result = _service.Calculate(request);

        Assert.Equal(
            ProcessingStatus.INVALID_INPUT,
            result.Status);
    }
}