---
agent: 'xunit-agent'
description: 'Agent for enforcing XUnit best practices, including trait usage, region grouping, and helper placement.'
tools:
  - 'codebase'
  - 'edit/editFiles'
  - 'githubRepo'
references:
  - '../instructions/xunit-agent.instructions.md'
---

This agent ensures all XUnit tests in the repository follow the documented best practices for structure, organization, and maintainability.

## Scope
- Applies to all test projects using XUnit (projects with `.Tests` suffix).
- Enforces conventions for test structure, trait usage, code organization, and test naming.

## Key Rules

### Project Setup
- Use a separate test project with naming convention `[ProjectName].Tests`.
- Reference Microsoft.NET.Test.Sdk, xunit, and xunit.runner.visualstudio packages.
- Test classes should match the classes being tested (e.g., `CalculatorTests` for `Calculator`).

### Test Structure
- No test class attributes required (unlike MSTest/NUnit).
- Use `[Fact]` for simple tests, `[Theory]` for data-driven tests.
- Follow Arrange-Act-Assert (AAA) pattern.
- Name tests as `MethodName_Scenario_ExpectedBehavior`.
- Use constructor for setup and `IDisposable.Dispose()` for teardown.
- Use `IClassFixture<T>` for shared context in a class, `ICollectionFixture<T>` for multiple classes.

### Standard Tests
- Focus each test on a single behavior.
- Avoid multiple behaviors in one test method.
- Use clear, intent-revealing assertions.
- Only include necessary assertions.
- Make tests independent and idempotent.
- Avoid test interdependencies.

### Data-Driven Tests
- Use `[Theory]` with `[InlineData]`, `[MemberData]`, or `[ClassData]`.
- Create custom data attributes by implementing `DataAttribute`.
- Use meaningful parameter names.

### Assertions
- Use `Assert.Equal`, `Assert.Same`, `Assert.True`, `Assert.False`, `Assert.Contains`, `Assert.DoesNotContain`, `Assert.Matches`, `Assert.DoesNotMatch`, `Assert.Throws<T>`, `Assert.ThrowsAsync<T>` as appropriate.
- Use a fluent assertions library for more readable assertions if desired.

### Mocking and Isolation
- Use Moq or NSubstitute for mocking.
- Mock dependencies to isolate units under test.
- Use interfaces to facilitate mocking.
- Consider DI container for complex setups.

### Test Organization
- Group tests by feature or component.
- **Always use `[Trait("Category", "Functionality")]` for functionality tests and `[Trait("Category", "EdgeCase")]` for edge case tests.**
- Use additional traits as needed (Concurrency, Integration, Performance, Priority).
- **Group tests by trait type using `#region [TraitType]` and `#endregion` (e.g., `#region Functionality`, `#region EdgeCase`).**
- Place helpers (output helpers, shared setup) within the appropriate region.
- Use collection fixtures for shared dependencies.
- Use `ITestOutputHelper` for test diagnostics.
- Skip tests conditionally with `Skip = "reason"` in fact/theory attributes.
