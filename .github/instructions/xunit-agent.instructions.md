---
agent: 'xunit-agent'
description: 'Instructions for using the XUnit agent, including a comprehensive template for best practices, enforcing trait usage, region grouping, helpers, and test structure.'
applyTo: 'tests/**/*Tests.cs'
---

# Instructions for XUnit Agent (summary)
- Enforce XUnit best practices for test structure, organization, and maintainability.
- Ensure all tests use traits for categorization and are grouped by trait type using regions.
- Place shared helpers and fixtures within the appropriate regions.
- Use the provided template as a reference for test class structure and organization.
- Ensure all tests follow the Arrange-Act-Assert pattern and are named according to the convention `MethodName_Scenario_ExpectedBehavior`.
- Use `ITestOutputHelper` for diagnostics and Moq for mocking dependencies.
- Skip tests conditionally when necessary using the `Skip` property in fact/theory attributes.

## Template: XUnit Test Class

```csharp
using Xunit;
using Moq;

public class CalculatorTests
{
    // Shared helpers/fixtures
    private readonly ITestOutputHelper _output;
    private readonly Mock<ICalculatorDependency> _mockDep;

    public CalculatorTests(ITestOutputHelper output)
    {
        _output = output;
        _mockDep = new Mock<ICalculatorDependency>();
    }

    #region Functionality
    [Fact]
    [Trait("Category", "Functionality")]
    public void Add_PositiveNumbers_ReturnsSum()
    {
        // Arrange
        var calc = new Calculator(_mockDep.Object);
        // Act
        var result = calc.Add(2, 3);
        // Assert
        Assert.Equal(5, result);
    }
    #endregion

    #region EdgeCase
    [Fact]
    [Trait("Category", "EdgeCase")]
    public void Add_MaxInt_ThrowsOverflowException()
    {
        // Arrange
        var calc = new Calculator(_mockDep.Object);
        // Act & Assert
        Assert.Throws<OverflowException>(() => calc.Add(int.MaxValue, 1));
    }
    #endregion

    #region DataDriven
    [Theory]
    [Trait("Category", "Functionality")]
    [InlineData(1, 2, 3)]
    [InlineData(-1, 1, 0)]
    public void Add_VariousInputs_ReturnsExpected(int a, int b, int expected)
    {
        // Arrange
        var calc = new Calculator(_mockDep.Object);
        // Act
        var result = calc.Add(a, b);
        // Assert
        Assert.Equal(expected, result);
    }
    #endregion
}
```

## Reference
- See [csharp-xunit.prompt.md](../prompts/csharp-xunit.prompt.md) for full best practices.

---
This agent ensures all XUnit tests are maintainable, well-organized, and consistent with project standards.
