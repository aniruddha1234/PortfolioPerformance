using PortfolioPerformance.Api.Models.Requests;
using Xunit;

namespace PortfolioPerformance.Tests;

public class ValidationTests
{
    [Fact]
    public void Weight_Should_Be_100()
    {
        var request = new AttributionRequest
        {
            Groups = new()
            {
                new(){WeightPct=50},
                new(){WeightPct=50}
            }
        };

        Assert.Equal(100,
            request.Groups.Sum(x=>x.WeightPct));
    }
}