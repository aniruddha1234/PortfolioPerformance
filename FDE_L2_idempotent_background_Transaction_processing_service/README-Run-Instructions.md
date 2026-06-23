# Transaction Processing Service

## Overview

A simple transaction processing service demonstrating:

- Idempotent request handling
- Background processing
- Retry mechanism
- Duplicate detection
- Out-of-order transaction handling
- Thread-safe in-memory storage

## Technology

- .NET 8 Web API
- BackgroundService
- ConcurrentDictionary
- xUnit
- Swagger

## Run

Restore packages

```bash
dotnet restore
```

Build

```bash
dotnet build
```

Run

```bash
dotnet run
```

Swagger

```
https://localhost:<port>/swagger
```

## API

### POST /transactions

Creates one or more transactions.

### GET /transactions

Returns all transactions.

### GET /transactions/{id}

Returns a transaction.

### GET /transactions/summary

Returns transaction summary.

## Transaction Status

- Received
- Processing
- Processed
- RetryPending
- Failed
- Duplicate

## Features

- Input validation
- Idempotent processing
- Duplicate detection
- Background worker
- Retry mechanism
- Sequence-based processing
- Thread-safe storage