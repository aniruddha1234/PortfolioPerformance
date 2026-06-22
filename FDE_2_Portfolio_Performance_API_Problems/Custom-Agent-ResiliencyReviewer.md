# Resiliency Reviewer

Review the implementation against the following checklist:

- Validate portfolio weights total between 99 and 101.
- Use primary return when available.
- Use fallback return when primary return is unavailable.
- Mark pricing as UNAVAILABLE if both values are missing.
- Return DEGRADED when one group has missing pricing.
- Return REVIEW_REQUIRED when multiple groups have missing pricing.
- Ensure duplicate requestIds return the cached response.
- Keep business logic in services and controllers lightweight.
- Include unit tests for valid, invalid, fallback, degraded, review-required, and idempotency scenarios.
- Return clear warnings and processing status in the API response.