---
skill: xunit-test
summary: "Best practices and skills for writing effective xUnit tests in .NET projects."
---

# xUnit Test Skills

## Project Setup
- Use a separate test project named `[ProjectName].Tests`.
- Reference `Microsoft.NET.Test.Sdk`, `xunit`, and `xunit.runner.visualstudio` NuGet packages.
- Mirror test class names to the classes under test (e.g., `CalculatorTests` for `Calculator`).
- Run tests using `dotnet test`.

## Test Structure
- Use `[Fact]` for standard tests and `[Theory]` for data-driven tests.
- Follow the Arrange-Act-Assert (AAA) pattern.
- Name tests as `MethodName_Scenario_ExpectedBehavior`.
- Use constructors for setup and `IDisposable.Dispose()` for teardown.
- Use `IClassFixture<T>` and `ICollectionFixture<T>` for shared context.

## Standard Tests
- Focus each test on a single behavior.
- Make tests independent and idempotent.
- Use clear, minimal assertions.
- Test async code with `async Task` and `await`.
- Use `Assert.ThrowsAsync<T>` for async exception testing.
- Cover edge cases and error conditions.

## Data-Driven Tests
- Use `[Theory]` with `[InlineData]`, `[MemberData]`, or `[ClassData]`.
- Use meaningful parameter names.
- Implement custom `DataAttribute` for advanced scenarios.

## Assertions
- Use `Assert.Equal`, `Assert.Same`, `Assert.True`, `Assert.False`, `Assert.Contains`, `Assert.Throws<T>`, etc.
- Prefer fluent assertions for readability when possible.

## Mocking and Isolation
- Use Moq or NSubstitute for mocking dependencies.
- Mock interfaces to isolate units under test.
- Use DI containers for complex setups.

## Integration Testing
- Place integration tests in `[ProjectName].IntegrationTests`.
- Use fixtures for shared resources (e.g., databases, web servers).
- Use `TestServer` for ASP.NET integration tests.

## Test Organization
- Group tests by feature or component.
- Use `[Trait("Category", "CategoryName")]` for categorization.
- Use `ITestOutputHelper` for diagnostics.
- Skip tests conditionally with `Skip = "reason"`.

---
For more details, see the project documentation and xUnit official docs.
