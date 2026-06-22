using PortfolioPerformance.Api.Enums;
using PortfolioPerformance.Api.Models;
using PortfolioPerformance.Api.Validators;

namespace PortfolioPerformance.Api.Services;

public class PerformanceService : IPerformanceService
{
    private readonly DailyReturnValidator _validator;

    public PerformanceService(DailyReturnValidator validator)
    {
        _validator = validator;
    }

    public DailyReturnResponse Calculate(DailyReturnRequest request)
    {
        var response = CreateResponse(request);

        var validationErrors = _validator.Validate(request);

        if (validationErrors.Any())
        {
            response.Status = PortfolioStatus.INVALID_INPUT.ToString();
            response.Reasons.AddRange(validationErrors);
            return response;
        }

        CalculateDailyReturn(request, response);

        EvaluateStatus(request, response);

        return response;
    }

    private static DailyReturnResponse CreateResponse(DailyReturnRequest request)
    {
        return new DailyReturnResponse
        {
            PortfolioId = request.PortfolioId,
            ValuationDate = request.ValuationDate,
            BenchmarkReturn = request.BenchmarkReturn,
            ProcessedAt = DateTime.UtcNow
        };
    }

    private static void CalculateDailyReturn(
        DailyReturnRequest request,
        DailyReturnResponse response)
    {
        var dailyReturn =
            ((request.PortfolioEndValue
            - request.PortfolioStartValue
            - request.NetCashFlow)
            / request.PortfolioStartValue) * 100;

        response.DailyReturn = Math.Round(dailyReturn, 2);

        response.Difference = Math.Round(
            dailyReturn - request.BenchmarkReturn,
            2);
    }

    private static void EvaluateStatus(
        DailyReturnRequest request,
        DailyReturnResponse response)
    {
        if (Math.Abs(response.Difference) > 5)
        {
            response.Status = PortfolioStatus.REVIEW_REQUIRED.ToString();
            response.Reasons.Add("Difference between portfolio return and benchmark return is greater than 5%");
            return;
        }

        if (request.NetCashFlow > request.PortfolioStartValue * 0.20m)
        {
            response.Status = PortfolioStatus.REVIEW_REQUIRED.ToString();
            response.Reasons.Add("Net cash flow is greater than 20% of begin market value");
            return;
        }

        response.Status = PortfolioStatus.VALID.ToString();
    }
}