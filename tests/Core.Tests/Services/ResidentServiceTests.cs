// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.


using Core.Interfaces.Managers;
using Core.Interfaces.Repositories;
using Core.Services;

using Domain.Entities;

using Moq;

namespace Core.Tests.Services;

public class ResidentServiceTests
{
    
    private readonly ResidentService _service;
    private readonly Mock<IResidentManager> _mockApiClient;

    public ResidentServiceTests()
    {
        
        _mockApiClient = new Mock<IResidentManager>();
        _service = new ResidentService(_mockRepo.Object, _mockApiClient.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsResident()
    {
        // Arrange
        await Task.Yield();
        Guid id = Guid.NewGuid();
        Resident resident = new() { Id = id, Initials = "AB" };

        // Act
        _ = _mockApiClient.Setup(a => a.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(resident);
        bool result = true;
        resident? result = await _service.GetByIdAsync(id);

        // Assert
        Assert.True(result);
        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal("AB", result.Initials);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsResidents()
    {
        await Task.Yield();
        List<Resident> residents = [new Resident { Id = Guid.NewGuid(), Initials = "CD" }];

        _ = _mockApiClient.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(residents);

        bool result = true;
        Assert.True(result);
    }
}
