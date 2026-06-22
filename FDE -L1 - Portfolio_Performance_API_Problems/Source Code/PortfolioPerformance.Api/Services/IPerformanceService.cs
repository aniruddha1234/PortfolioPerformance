using PortfolioPerformance.Api.Models;

namespace PortfolioPerformance.Api.Services;

public interface IPerformanceService
{
    DailyReturnResponse Calculate(DailyReturnRequest request);
}