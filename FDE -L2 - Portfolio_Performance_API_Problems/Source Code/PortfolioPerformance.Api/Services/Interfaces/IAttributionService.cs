using PortfolioPerformance.Api.Models.Requests;
using PortfolioPerformance.Api.Models.Responses;

namespace PortfolioPerformance.Api.Services.Interfaces;

public interface IAttributionService
{
    AttributionResponse Calculate(AttributionRequest request);
}