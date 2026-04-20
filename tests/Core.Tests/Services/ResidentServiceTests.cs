// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Managers;
using Core.Services;

using Domain.Entities;

using Moq;

namespace Core.Tests.Services;

public class ResidentServiceTests
{
    private readonly Mock<IResidentManager> _mockManager;
    private readonly ResidentService _service;

    public ResidentServiceTests()
    {
        _mockManager = new Mock<IResidentManager>();
        _service = new ResidentService(_mockManager.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsResident()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Resident resident = new() { Id = id, Initials = "AB" };
        _mockManager.Setup(a => a.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(resident);

        // Act
        Resident? result = await _service.GetByIdAsync(id, TestContext.Current.CancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal("AB", result.Initials);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsResidents()
    {
        // Arrange
        List<Resident> residents = [new Resident { Id = Guid.NewGuid(), Initials = "CD" }];
        _mockManager.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(residents);

        // Act
        IEnumerable<Resident> result = await _service.GetAllAsync(TestContext.Current.CancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }
}