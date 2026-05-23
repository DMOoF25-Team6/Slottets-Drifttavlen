// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.Interfaces.Managers;
using Core.Services;
using Domain.Entities;
using Moq;
using Xunit;

namespace Core.Tests.Services;

public class MedicineDeliveryServiceTests
{
    private readonly Mock<IMedicineDeliveryManager> _managerMock;
    private readonly MedicineDeliveryService _sut;

    public MedicineDeliveryServiceTests()
    {
        _managerMock = new Mock<IMedicineDeliveryManager>();
        _sut = new MedicineDeliveryService(_managerMock.Object);
    }

    [Fact]
    public async Task CreateAsync_DelegatesToManager()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        MedicineDeliveryCreateRequestDto dto = new()
        {
            ResidentId = Guid.NewGuid(),
            MedicineName = "Panodil",
            Timestamp = DateTime.UtcNow,
            Given = true
        };

        await _sut.CreateAsync(dto, ct);

        _managerMock.Verify(m => m.CreateAsync(dto, ct), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_DelegatesToManager()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        Guid id = Guid.NewGuid();
        MedicineDeliveryUpdateRequestDto dto = new()
        {
            ResidentId = Guid.NewGuid(),
            MedicineName = "Ipren",
            Timestamp = DateTime.UtcNow,
            Given = false
        };

        await _sut.UpdateAsync(id, dto, ct);

        _managerMock.Verify(m => m.UpdateAsync(id, dto, ct), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_DelegatesToManager()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        Guid id = Guid.NewGuid();

        await _sut.DeleteAsync(id, ct);

        _managerMock.Verify(m => m.DeleteAsync(id, ct), Times.Once);
    }

    [Fact]
    public async Task GetAllAsync_MapsResponseDtosToEntities()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        Guid residentId = Guid.NewGuid();
        List<MedicineDeliveryResponseDto> dtos =
        [
            new() { Id = Guid.NewGuid(), ResidentId = residentId, MedicineName = "Panodil", Timestamp = DateTime.UtcNow, Given = true }
        ];
        _managerMock.Setup(m => m.GetAllAsync(ct)).ReturnsAsync(dtos);

        List<MedicineRecord> result = (await _sut.GetAllAsync(ct)).ToList();

        Assert.Single(result);
        Assert.Equal("Panodil", result[0].MedicineName);
        Assert.Equal(residentId, result[0].ResidentId);
    }

    [Fact]
    public async Task GetByIdAsync_WhenManagerReturnsNull_ReturnsNull()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        _managerMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>(), ct)).ReturnsAsync((MedicineDeliveryResponseDto?)null);

        MedicineRecord? result = await _sut.GetByIdAsync(Guid.NewGuid(), ct);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetByIdAsync_WhenFound_ReturnsMappedEntity()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        Guid id = Guid.NewGuid();
        MedicineDeliveryResponseDto dto = new()
        {
            Id = id,
            ResidentId = Guid.NewGuid(),
            MedicineName = "Ipren",
            Timestamp = DateTime.UtcNow,
            Given = false
        };
        _managerMock.Setup(m => m.GetByIdAsync(id, ct)).ReturnsAsync(dto);

        MedicineRecord? result = await _sut.GetByIdAsync(id, ct);

        Assert.NotNull(result);
        Assert.Equal(id, result!.Id);
        Assert.Equal("Ipren", result.MedicineName);
    }
}
