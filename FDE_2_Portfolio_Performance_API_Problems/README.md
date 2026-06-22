# Portfolio Performance Attribution API

## Technology

- ASP.NET Core 8
- C#
- xUnit

---

## Run

```bash
dotnet restore
dotnet build
dotnet run --project PortfolioPerformance.Api
```

Swagger

```
https://localhost:xxxx/swagger
```

---

## Endpoint

POST

```
/api/performance/attribution
```

---

## Business Rules

- Total Weight between 99 and 101
- Contribution = Weight × Return /100
- Fallback pricing supported
- Degraded mode
- Review Required
- Idempotency

---

## Assumptions

- In-memory cache
- No database
- UTC timestamps