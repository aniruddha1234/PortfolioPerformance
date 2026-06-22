# Assumptions

1. TransactionId uniquely identifies a transaction.
2. Duplicate detection is based only on TransactionId.
3. Transactions are processed independently.
4. Processing order is not guaranteed.
5. Background processing is simulated using BackgroundService.
6. In-memory storage is sufficient for the scope of this assessment.
7. Failed validation requests are excluded from processing.
