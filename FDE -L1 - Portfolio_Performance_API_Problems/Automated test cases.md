## Automated Test Cases

The solution includes automated unit tests using **xUnit** to verify the core business logic of the Portfolio Performance Service.

### Test Coverage

The following scenarios are covered:

| Test Case | Expected Result |
|-----------|-----------------|
| Valid portfolio request | Returns `VALID` status |
| Negative portfolio start value | Returns `INVALID_INPUT` |
| Negative portfolio end value | Returns `INVALID_INPUT` |
| Missing currency | Returns `INVALID_INPUT` |
| Zero start value with non-zero end value | Returns `INVALID_INPUT` |
| Benchmark difference greater than 5% | Returns `REVIEW_REQUIRED` |
| Net cash flow greater than 20% of start value | Returns `REVIEW_REQUIRED` |