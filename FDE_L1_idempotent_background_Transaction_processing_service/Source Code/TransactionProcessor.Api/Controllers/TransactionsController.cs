using Microsoft.AspNetCore.Mvc;
using TransactionProcessor.Api.Background;
using TransactionProcessor.Api.Models;
using TransactionProcessor.Api.Services;
using TransactionProcessor.Api.Validation;

namespace TransactionProcessor.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly IBackgroundQueue _queue;
    private readonly TransactionValidator _validator;
    private readonly TransactionProcessorService _processor;

    public TransactionsController(
        IBackgroundQueue queue,
        TransactionValidator validator,
        TransactionProcessorService processor)
    {
        _queue = queue;
        _validator = validator;
        _processor = processor;
    }

    [HttpPost("process")]
    public async Task<IActionResult> ProcessTransactions(
        [FromBody] List<TransactionRequest> requests)
    {
        if (requests == null || requests.Count == 0)
        {
            return BadRequest("Request cannot be empty.");
        }

        foreach (var request in requests)
        {
            if (!_validator.Validate(request))
            {
                _processor.AddValidationFailure(request);
                continue;
            }

            await _queue.QueueAsync(request);
        }

        return Accepted(new
        {
            Message = "Transactions accepted for background processing."
        });
    }

    [HttpGet("summary")]
    public ActionResult<ProcessingSummary> GetSummary()
    {
        return Ok(_processor.GetSummary());
    }
}