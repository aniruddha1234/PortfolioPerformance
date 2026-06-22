# Transaction Processor API

## Overview

This project is a .NET 8 Web API that processes financial transaction requests asynchronously using a background worker. The application ensures idempotent transaction processing by preventing duplicate transactions based on the `TransactionId`.

The solution was developed as part of the **FDE L1 – Idempotent Background Transaction Processing Service** assessment.

---

## Features

* Accepts a collection of transaction requests.
* Validates mandatory fields.
* Processes transactions asynchronously using `BackgroundService`.
* Prevents duplicate processing using `TransactionId`.
* Handles out-of-order transaction requests.
* Maintains transaction processing status.
* Provides processing summary.
* Includes unit tests.

---

## Technologies Used

* .NET 8
* ASP.NET Core Web API
* C#
* BackgroundService
* Channel<T>
* xUnit
* Swagger / OpenAPI

---

## Project Structure

```
TransactionProcessor
│
├── TransactionProcessor.Api
│
├── TransactionProcessor.Tests
│
└── README.md
```

---

## How to Run

### Restore Packages

```bash
dotnet restore
```

### Build

```bash
dotnet build
```

### Run Application

```bash
dotnet run --project TransactionProcessor.Api
```

Swagger will be available at:

```
http://localhost:<port>/swagger
```

---

## Run Unit Tests

```bash
dotnet test
```

---

## API Endpoints

### Process Transactions

```
POST /api/transactions/process
```

Processes a collection of transaction requests asynchronously.

---

### Get Processing Summary

```
GET /api/transactions/summary
```

Returns the processing summary including processed, duplicate and failed validation counts.

---

## Validation Rules

* TransactionId is required.
* RequestId is required.
* AccountId is required.
* TransactionType is required.
* Amount must be greater than zero.

Invalid transactions are marked as **FailedValidation**.

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
* Support distributed processing.
* Add Docker support.
