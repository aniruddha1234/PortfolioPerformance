## Automated Test Cases

The solution includes xUnit-based automated tests covering the primary business scenarios:

- Valid attribution calculation
- Invalid portfolio weight validation
- Fallback pricing when primary return is unavailable
- Degraded processing (single missing price)
- Review Required processing (multiple missing prices)
- Idempotent request handling using the same requestId

Run the tests using:
dotnet test