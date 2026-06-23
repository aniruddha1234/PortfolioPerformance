using TransactionProcessor.Api.Models;
using TransactionProcessor.Services;
using Xunit;

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
            TransactionId = "TXN-1001",
            RequestId = "REQ-0001",
            Amount = 1000,
            SequenceNumber = 1
        };

        // Act
        var result = _service.Create(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("TXN-1001", result.TransactionId);
        Assert.Equal(TransactionStatus.Received.ToString(), result.Status);
        Assert.Equal(0, result.RetryCount);
        Assert.Single(_service.GetAll());
    }

    [Fact]
    public void Create_Should_Return_Existing_Transaction_For_Same_IdempotencyKey()
    {
        // Arrange
        var request = new TransactionRequest
        {
            TransactionId = "TXN-1001",
            RequestId = "REQ-0001",
            Amount = 1000,
            SequenceNumber = 1
        };

        // Act
        var first = _service.Create(request);
        var second = _service.Create(request);

        // Assert
        Assert.Equal(first.Id, second.Id);
        Assert.Single(_service.GetAll());
    }

    [Fact]
    public void Create_Should_Mark_Duplicate_When_TransactionId_Already_Exists()
    {
        // Arrange
        _service.Create(new TransactionRequest
        {
            TransactionId = "TXN-1001",
            RequestId = "REQ-0001",
            Amount = 1000,
            SequenceNumber = 1
        });

        // Act
        var duplicate = _service.Create(new TransactionRequest
        {
            TransactionId = "TXN-1001",
            RequestId = "REQ-0002",
            Amount = 1000,
            SequenceNumber = 2
        });

        // Assert
        Assert.Equal(TransactionStatus.Duplicate.ToString(), duplicate.Status);
    }

    [Fact]
    public void Get_Should_Return_Transaction_When_Id_Exists()
    {
        // Arrange
        var transaction = _service.Create(new TransactionRequest
        {
            TransactionId = "TXN-1002",
            RequestId = "REQ-0002",
            Amount = 500,
            SequenceNumber = 2
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

    [Fact]
    public void GetAll_Should_Return_All_Transactions()
    {
        // Arrange
        _service.Create(new TransactionRequest
        {
            TransactionId = "TXN-1001",
            RequestId = "REQ-0001",
            Amount = 100,
            SequenceNumber = 1
        });

        _service.Create(new TransactionRequest
        {
            TransactionId = "TXN-1002",
            RequestId = "REQ-0002",
            Amount = 200,
            SequenceNumber = 2
        });

        // Act
        var transactions = _service.GetAll();

        // Assert
        Assert.Equal(2, transactions.Count);
    }

    [Fact]
    public void Transaction_Should_Store_SequenceNumber()
    {
        // Arrange
        var request = new TransactionRequest
        {
            TransactionId = "TXN-2001",
            RequestId = "REQ-2001",
            Amount = 500,
            SequenceNumber = 10
        };

        // Act
        var transaction = _service.Create(request);

        // Assert
        Assert.Equal(10, transaction.SequenceNumber);
    }
}