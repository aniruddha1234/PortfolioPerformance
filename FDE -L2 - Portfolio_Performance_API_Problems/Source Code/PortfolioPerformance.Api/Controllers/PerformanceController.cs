using Microsoft.AspNetCore.Mvc;
using PortfolioPerformance.Api.Models.Requests;
using PortfolioPerformance.Api.Services.Interfaces;

namespace PortfolioPerformance.Api.Controllers;

[ApiController]
[Route("api/performance")]
public class PerformanceController : ControllerBase
{
    private readonly IAttributionService _service;

    public PerformanceController(IAttributionService service)
    {
        _service = service;
    }

    [HttpPost("attribution")]
    public IActionResult Calculate(AttributionRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = _service.Calculate(request);

        return Ok(response);
    }
}