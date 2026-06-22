using TransactionProcessor.Api.Models;
using TransactionProcessor.Api.Validation;
using Xunit;

namespace TransactionProcessor.Tests.Validation;

public class TransactionValidatorTests
{
    private readonly TransactionValidator _validator = new();

    [Fact]
    public void Validate_Should_Return_True_For_Valid_Request()
    {
        var request = new TransactionRequest
        {
            TransactionId = "TXN001",
            RequestId = "REQ001",
            AccountId = "ACC001",
            TransactionType = "Credit",
            Amount = 100
        };

        var result = _validator.Validate(request);

        Assert.True(result);
    }

    [Fact]
    public void Validate_Should_Return_False_When_Amount_Is_Zero()
    {
        var request = new TransactionRequest
        {
            TransactionId = "TXN001",
            RequestId = "REQ001",
            AccountId = "ACC001",
            TransactionType = "Credit",
            Amount = 0
        };

        var result = _validator.Validate(request);

        Assert.False(result);
    }

    [Fact]
    public void Validate_Should_Return_False_When_TransactionId_Is_Empty()
    {
        var request = new TransactionRequest
        {
            TransactionId = "",
            RequestId = "REQ001",
            AccountId = "ACC001",
            TransactionType = "Credit",
            Amount = 100
        };

        var result = _validator.Validate(request);

        Assert.False(result);
    }
}