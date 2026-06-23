# Assumptions

- In-memory storage is used (ConcurrentDictionary).
- RequestId acts as the idempotency key.
- TransactionId uniquely identifies a business transaction.
- Duplicate TransactionIds are marked as Duplicate.
- Background processing is asynchronous.
- Transactions are processed according to SequenceNumber.
- Retry limit is three attempts.
- Processing success/failure is simulated randomly.
- No database persistence is implemented.