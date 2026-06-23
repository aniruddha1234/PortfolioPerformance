using System.Collections.Concurrent;
using TransactionProcessor.Api.Models;

public class TransactionStore
{
    public ConcurrentDictionary<string, Transaction> Transactions { get; }
        = new();
}