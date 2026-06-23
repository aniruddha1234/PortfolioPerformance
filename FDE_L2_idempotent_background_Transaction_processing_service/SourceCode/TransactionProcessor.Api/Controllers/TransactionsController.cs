using Microsoft.AspNetCore.Mvc;
using TransactionProcessor.Api.Models;
using TransactionProcessor.Services;

namespace TransactionProcessor.Controllers;

[ApiController]
[Route("transactions")]
public class TransactionsController : ControllerBase
{
    private readonly TransactionService _service;

    public TransactionsController(TransactionService service)
    {
        _service = service;
    }

[HttpPost("batch")]
public IActionResult Create(List<TransactionRequest> requests)
{
    if (requests == null || !requests.Any())
        return BadRequest("At least one transaction is required.");

    var errors = new List<string>();

    foreach (var request in requests)
    {
        if (string.IsNullOrWhiteSpace(request.TransactionId))
            errors.Add($"TransactionId is required for RequestId {request.RequestId}");

        if (string.IsNullOrWhiteSpace(request.RequestId))
            errors.Add("RequestId is required.");

        if (request.Amount <= 0)
            errors.Add($"Amount must be greater than zero for RequestId {request.RequestId}");
    }

    if (errors.Any())
        return BadRequest(errors);

    var responses = requests
        .Select(request => _service.Create(request))
        .ToList();

    return Accepted(responses);
}

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        var transaction = _service.Get(id);

        if (transaction == null)
            return NotFound();

        return Ok(transaction);
    }

[HttpGet("summary")]
public IActionResult Summary()
{
    return Ok(_service.GetSummary());
}
    [HttpGet("/health")]
    public IActionResult Health()
    {
        return Ok("Healthy");
    }
}