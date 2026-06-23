using TransactionProcessor.Api.Models;
using TransactionProcessor.Services;

namespace TransactionProcessor.Tests;

public class TransactionServiceTests
{
    private readonly TransactionService _service;
    private readonly TransactionStore _store;

    public TransactionServiceTests()
    {
        _store = new TransactionStore();
        _service = new TransactionService(_store);
    }

    [Fact]
    public void Create_Should_Create_New_Transaction()
    {
        // Arrange
        var request = new TransactionRequest
        {
            TransactionId = "TX001",
            IdempotencyKey = "KEY001",
            Amount = 100
        };

        // Act
        var result = _service.Create(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("TX001", result.TransactionId);
        Assert.Equal(TransactionStatus.Pending, result.Status);
        Assert.Equal(0, result.RetryCount);
    }

    [Fact]
    public void Create_Should_Return_Existing_Transaction_For_Duplicate_IdempotencyKey()
    {
        // Arrange
        var request = new TransactionRequest
        {
            TransactionId = "TX001",
            IdempotencyKey = "KEY001",
            Amount = 100
        };

        // Act
        var first = _service.Create(request);
        var second = _service.Create(request);

        // Assert
        Assert.Equal(first.Id, second.Id);
        Assert.Single(_service.GetAll());
    }

    [Fact]
    public void GetAll_Should_Return_All_Transactions()
    {
        // Arrange
        _service.Create(new TransactionRequest
        {
            TransactionId = "TX001",
            IdempotencyKey = "KEY001",
            Amount = 100
        });

        _service.Create(new TransactionRequest
        {
            TransactionId = "TX002",
            IdempotencyKey = "KEY002",
            Amount = 200
        });

        // Act
        var result = _service.GetAll();

        // Assert
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void Get_Should_Return_Transaction_When_Id_Exists()
    {
        // Arrange
        var transaction = _service.Create(new TransactionRequest
        {
            TransactionId = "TX001",
            IdempotencyKey = "KEY001",
            Amount = 100
        });

        // Act
        var result = _service.Get(transaction.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(transaction.Id, result!.Id);
    }

    [Fact]
    public void Get_Should_Return_Null_When_Id_Does_Not_Exist()
    {
        // Act
        var result = _service.Get(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }
}