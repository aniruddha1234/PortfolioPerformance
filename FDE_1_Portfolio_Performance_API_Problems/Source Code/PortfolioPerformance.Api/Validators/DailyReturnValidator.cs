using PortfolioPerformance.Api.Models;

namespace PortfolioPerformance.Api.Validators;

public class DailyReturnValidator
{
    public List<string> Validate(DailyReturnRequest request)
    {
        var errors = new List<string>();

        if (request.PortfolioStartValue < 0)
            errors.Add("Portfolio start value cannot be negative.");

        if (request.PortfolioEndValue < 0)
            errors.Add("Portfolio end value cannot be negative.");

        if (string.IsNullOrWhiteSpace(request.Currency))
            errors.Add("Currency is mandatory.");

        if (request.PortfolioStartValue == 0 &&
            request.PortfolioEndValue != 0)
        {
            errors.Add("Portfolio start value cannot be zero when end value is greater than zero.");
        }

        return errors;
    }
}