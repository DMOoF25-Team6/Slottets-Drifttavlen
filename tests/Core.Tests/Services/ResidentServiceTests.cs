// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Repositories;
using Core.Services;

using Domain.Entities;

using Moq;

namespace Core.Tests.Services;

public class ResidentServiceTests
{
    private readonly Mock<IResidentRepository> _mockRepo;
    private readonly ResidentService _service;

    public ResidentServiceTests()
    {
        _mockRepo = new Mock<IResidentRepository>();
        _service = new ResidentService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsResident()
    {
        Guid id = Guid.NewGuid();
        Resident resident = new() { Id = id, Initials = "AB" };
        _ = _mockRepo.Setup(r => r.GetAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(resident);

        Resident? result = await _service.GetAsync(id, TestContext.Current.CancellationToken);

        Assert.Equal(resident, result);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsResidents()
    {
        List<Resident> residents = [new Resident { Id = Guid.NewGuid(), Initials = "CD" }];
        _ = _mockRepo.Setup(r => r.GetAsync(It.IsAny<CancellationToken>())).ReturnsAsync(residents);

        IEnumerable<Resident> result = await _service.GetAsync(TestContext.Current.CancellationToken);

        Assert.Equal(residents, result);
    }

    [Fact]
    public async Task AddAsync_CallsRepository()
    {
        Resident resident = new() { Initials = "EF" };
        _ = _mockRepo.Setup(r => r.AddAsync(resident, It.IsAny<CancellationToken>())).ReturnsAsync(resident);

        Resident result = await _service.AddAsync(resident, TestContext.Current.CancellationToken);

        Assert.Equal(resident, result);
        _mockRepo.Verify(r => r.AddAsync(resident, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_CallsRepository()
    {
        Resident resident = new() { Id = Guid.NewGuid(), Initials = "GH" };
        _ = _mockRepo.Setup(r => r.UpdateAsync(resident, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        await _service.UpdateAsync(resident, TestContext.Current.CancellationToken);

        _mockRepo.Verify(r => r.UpdateAsync(resident, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_CallsRepository()
    {
        Resident resident = new() { Id = Guid.NewGuid(), Initials = "IJ" };
        _ = _mockRepo.Setup(r => r.DeleteAsync(resident, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        await _service.DeleteAsync(resident, TestContext.Current.CancellationToken);

        _mockRepo.Verify(r => r.DeleteAsync(resident, It.IsAny<CancellationToken>()), Times.Once);
    }
}
