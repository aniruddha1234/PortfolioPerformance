# Transaction Processor API

## Overview

This project is a .NET 8 Web API that processes financial transaction requests asynchronously using a background worker. 
-----------

## How to Run

### Restore Packages
dotnet restore

### Build
dotnet build
```

### Run Application
dotnet run --project TransactionProcessor.Api
```

Swagger will be available at:

```
http://localhost:<port>/swagger
```

---

## Run Unit Tests
dotnet test

## API Endpoints

### Process Transactions
POST /api/transactions/process
Processes a collection of transaction requests asynchronously.

### Get Processing Summary
GET /api/transactions/summary

Returns the processing summary including processed, duplicate and failed validation counts.

---

## Idempotency Approach

The application uses an in-memory `ConcurrentDictionary` keyed by `TransactionId`.

* If a transaction ID has already been processed, the request is marked as **Duplicate**.
* Duplicate transactions are not processed again.

---

## Assumptions

* TransactionId uniquely identifies a transaction.
* In-memory storage is acceptable for this assessment.
* No database persistence is required.
* Processing is simulated within the background worker.

---

## Limitations

* Transaction data is not persisted after application shutdown.
* Duplicate detection works only while the application is running.
* No authentication or authorization is implemented since it was outside the scope of the assessment.

---

## Future Improvements
* Persist transactions using a database.
* Retry failed transactions.
* Add structured logging.
* Add health checks.

