using Microsoft.AspNetCore.Mvc;
using PortfolioPerformance.Api.Models;
using PortfolioPerformance.Api.Services;

namespace PortfolioPerformance.Api.Controllers;

[ApiController]
[Route("api/performance")]
public class PerformanceController : ControllerBase
{
    private readonly IPerformanceService _service;

    public PerformanceController(IPerformanceService service)
    {
        _service = service;
    }

    [HttpPost("daily-return")]
    public IActionResult Calculate(DailyReturnRequest request)
    {
        var result = _service.Calculate(request);
        return Ok(result);
    }
}