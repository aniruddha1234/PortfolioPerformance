using PortfolioPerformance.Api.Models;
using PortfolioPerformance.Api.Services;
using Xunit;
using PortfolioPerformance.Api.Validators;
using PortfolioPerformance.Api.Enums;


public class PerformanceServiceTests
{
    [Fact]
    public void Should_Return_Valid()
    {
        var validator = new DailyReturnValidator();
        var service = new PerformanceService(validator);

        var request = new DailyReturnRequest
        {
            PortfolioStartValue = 100000,
            PortfolioEndValue = 102500,
            NetCashFlow = 1000,
            BenchmarkReturn = 1.8m,
            Currency = "USD"
        };

        var result = service.Calculate(request);

        Assert.Equal(PortfolioStatus.VALID.ToString(), result.Status);
    }

        [Fact]
    public void Should_Return_InValid()
    {
        var validator = new DailyReturnValidator();
        var service = new PerformanceService(validator);

        var request = new DailyReturnRequest
        {
            PortfolioStartValue = 100000,
            PortfolioEndValue = 102500,
            NetCashFlow = 1000,
            BenchmarkReturn = 1.8m,
            Currency = ""
        };

        var result = service.Calculate(request);

        Assert.Equal(PortfolioStatus.INVALID_INPUT.ToString(), result.Status);
    }

[Fact]
        public void Should_Return_Review_Required()
    {
        var validator = new DailyReturnValidator();
        var service = new PerformanceService(validator);

        var request = new DailyReturnRequest
        {
            PortfolioStartValue = 10000,
            PortfolioEndValue = 102500,
            NetCashFlow = 1000,
            BenchmarkReturn = 1.8m,
            Currency = "USD"
        };

        var result = service.Calculate(request);

        Assert.Equal(PortfolioStatus.REVIEW_REQUIRED.ToString(), result.Status);
    }
}