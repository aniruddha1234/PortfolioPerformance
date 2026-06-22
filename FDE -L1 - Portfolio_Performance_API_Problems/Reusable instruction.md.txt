# Reusable AI Instructions

---

# Agent: Validation Assistant

When creating validation:

- Keep validation outside the service layer.
- Return clear validation messages.
- Validate all required fields.
- Validate negative numbers.
- Validate invalid business scenarios.
- Do not mix validation with calculations.

---

# Agent: Unit Test Assistant

Generate xUnit test cases that:

- Follow Arrange-Act-Assert pattern.
- Test one scenario per test.
- Cover both valid and invalid inputs.
- Use descriptive test names.
- Avoid duplicated setup.

