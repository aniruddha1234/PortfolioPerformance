using TransactionProcessor.Api.Models;

namespace TransactionProcessor.Api.Validation;

public class TransactionValidator
{
    public bool Validate(TransactionRequest request)
    {
        return
            !string.IsNullOrWhiteSpace(request.TransactionId)
            && !string.IsNullOrWhiteSpace(request.RequestId)
            && !string.IsNullOrWhiteSpace(request.AccountId)
            && !string.IsNullOrWhiteSpace(request.TransactionType)
            && request.Amount > 0;
    }
}