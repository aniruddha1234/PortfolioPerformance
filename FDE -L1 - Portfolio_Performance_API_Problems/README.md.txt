# Portfolio Performance Service

## Overview

The service validates incoming requests, calculates the portfolio's daily return

---

## Features

- Calculate portfolio daily return

---

## Technology Stack

- .NET 8
- ASP.NET Core Web API
- C#
- xUnit
- Swagger
- Visual Studio Code 


## Running the Application

Navigate to the API project:

cd PortfolioPerformance.Api

Run the application:

dotnet run


Swagger will be available at:

https://localhost:<port>/swagger

---

## Running Unit Tests
dotnet test


## Assumptions

- All calculations use `decimal` to maintain financial precision.

---

## Design Decisions

- Validation logic is separated into a dedicated `DailyReturnValidator`.
- Business logic resides in `PerformanceService`.
- The project follows SOLID principles to improve maintainability and testability.

---

## Future Improvements

- Global exception handling middleware

## Author

Aniruddha Mahadev Chavan