---
title: XUnit Best Practices Skill
description: Comprehensive skill for writing and organizing XUnit tests, enforcing all best practices and conventions.
category: testing
related-prompts:
  - '../../prompts/csharp-xunit.prompt.md'
references:
  - 'https://xunit.net/docs/getting-started/v3/getting-started#create-the-unit-test-project'
  - '
---

# XUnit Best Practices Skill

This skill provides all rules and conventions for writing effective, maintainable, and well-organized XUnit tests, including standard and data-driven approaches.

## Project Setup
- Use a separate test project with naming convention `[ProjectName].Tests`.
- Reference Microsoft.NET.Test.Sdk, xunit, and xunit.runner.visualstudio packages.
- Create test classes that match the classes being tested (e.g., `CalculatorTests` for `Calculator`).
- Use .NET SDK test commands: `dotnet test` for running tests.

## Test Structure
- No test class attributes required (unlike MSTest/NUnit).
- Use `[Fact]` for simple tests.
- Use the Arrange-Act-Assert (AAA) pattern.
- Name tests using the pattern `MethodName_Scenario_ExpectedBehavior`.
- Use constructor for setup and `IDisposable.Dispose()` for teardown.
- Use `IClassFixture<T>` for shared context in a class.
- Use `ICollectionFixture<T>` for shared context between multiple classes.

## Standard Tests
- Focus each test on a single behavior.
- Avoid multiple behaviors in one test method.
- Use clear assertions that express intent.
- Only include necessary assertions.
- Make tests independent and idempotent.
- Avoid test interdependencies.

## Data-Driven Tests
- Use `[Theory]` with `[InlineData]`, `[MemberData]`, or `[ClassData]`.
- Create custom data attributes by implementing `DataAttribute`.
- Use meaningful parameter names in data-driven tests.

## Assertions
- Use `Assert.Equal` for value equality.
- Use `Assert.Same` for reference equality.
- Use `Assert.True`/`Assert.False` for boolean conditions.
- Use `Assert.Contains`/`Assert.DoesNotContain` for collections.
- Use `Assert.Matches`/`Assert.DoesNotMatch` for regex pattern matching.
- Use `Assert.Throws<T>` or `await Assert.ThrowsAsync<T>` to test exceptions.
- Use a fluent assertions library for more readable assertions if desired.

## Mocking and Isolation
- Use Moq or NSubstitute for mocking.
- Mock dependencies to isolate units under test.
- Use interfaces to facilitate mocking.
- Consider a DI container for complex setups.

## Test Organization
- Group tests by feature or component.
- Always use `[Trait("Category", "Functionality")]` for functionality tests and `[Trait("Category", "EdgeCase")]` for edge case tests.
- Use additional traits as needed (Concurrency, Integration, Performance, Priority).
- Group tests by trait type using `#region [TraitType]` and `#endregion` (e.g., `#region Functionality`, `#region EdgeCase`).
- Place helpers (output helpers, shared setup) within the appropriate region.
- Use collection fixtures for shared dependencies.
- Use `ITestOutputHelper` for test diagnostics.
- Skip tests conditionally with `Skip = "reason"` in fact/theory attributes.

---
This skill ensures all XUnit tests are maintainable, well-organized, and consistent with project standards.
