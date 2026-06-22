using TransactionProcessor.Api.Models;
using TransactionProcessor.Api.Services;

namespace TransactionProcessor.Tests.Services;

public class TransactionProcessorServiceTests
{
    private readonly TransactionProcessorService _service = new();

    [Fact]
    public void Process_Should_Return_Processed_For_New_Transaction()
    {
        var request = new TransactionRequest
        {
            TransactionId = "TXN001",
            RequestId = "REQ001"
        };

        var result = _service.Process(request);

        Assert.Equal(TransactionStatus.Processed, result.Status);
    }

    [Fact]
    public void Process_Should_Return_Duplicate_For_Same_Transaction()
    {
        var request = new TransactionRequest
        {
            TransactionId = "TXN001",
            RequestId = "REQ001"
        };

        _service.Process(request);

        var result = _service.Process(request);

        Assert.Equal(TransactionStatus.Duplicate, result.Status);
    }

    [Fact]
    public void Summary_Should_Return_Correct_Counts()
    {
        _service.Process(new TransactionRequest
        {
            TransactionId = "TXN100",
            RequestId = "REQ100"
        });

        _service.Process(new TransactionRequest
        {
            TransactionId = "TXN100",
            RequestId = "REQ101"
        });

        var summary = _service.GetSummary();

        Assert.Equal(2, summary.TotalRequests);
        Assert.Equal(1, summary.Processed);
        Assert.Equal(1, summary.Duplicates);
    }
}