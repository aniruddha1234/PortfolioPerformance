# Prompt Log

This document records how GitHub Copilot (and AI assistance) was used during the development of this assessment.

-------------------------------------------------------------

## Prompt 1

Create a .NET 8 Web API folder structure using Controllers, Services, Repositories, Models, Validators and Middleware and layered architecture.

### Outcome
Generated an initial project structure.
### Manual Changes
- Added Interfaces for Service and Repository.
- Renamed folders for consistency.
- Removed unnecessary template files.

-------------------------------------------------------------

## Prompt 2

Generate C# request and response models for a portfolio daily return API.
### Outcome
Generated DTO classes.
### Manual Changes

- Updated property names to match API contract.
- Used decimal for financial values.


## Prompt 3
Implement a service to calculate daily portfolio return using beginning value, ending value and net cash flow.

### Outcome

Generated the initial calculation logic.

### Manual Changes

- Corrected the formula.
- Added benchmark comparison.
- Added REVIEW_REQUIRED rule.
- Added INVALID_INPUT validation.
- Rounded decimal values to two places.


## Prompt 4
Create an ASP.NET Core API controller that injects a service and exposes POST /api/performance/daily-return.

### Outcome

Generated controller.

### Manual Changes

- Improved response handling.
- Added route attributes.
- Added dependency injection.


## Prompt 5
Write xUnit tests for PerformanceService.

### Outcome

Generated initial test methods.

### Manual Changes

- Added benchmark difference test.
- Added invalid input scenarios.
- Improved test naming.