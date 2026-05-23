// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.Interfaces.Repositories;
using Core.Services;
using Domain.Entities;
using Moq;
using Xunit;

namespace Core.Tests.Services;

public class MedicineDeliveryServiceTests
{
    private readonly Mock<IMedicineRepository> _repositoryMock;
    private readonly MedicineDeliveryService _sut;

    public MedicineDeliveryServiceTests()
    {
        _repositoryMock = new Mock<IMedicineRepository>();
        _sut = new MedicineDeliveryService(_repositoryMock.Object);
    }

    [Fact]
    public async Task CreateAsync_PersistsRecord_ReturnsDto()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        MedicineDeliveryCreateRequestDto dto = new()
        {
            ResidentId = Guid.NewGuid(),
            MedicineName = "Panodil",
            Timestamp = DateTime.UtcNow,
            Given = true
        };
        _repositoryMock
            .Setup(r => r.CreateAsync(It.IsAny<MedicineRecord>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((MedicineRecord r, CancellationToken _) => r);

        MedicineDeliveryResponseDto result = await _sut.CreateAsync(dto, ct);

        Assert.Equal(dto.MedicineName, result.MedicineName);
        Assert.Equal(dto.ResidentId, result.ResidentId);
        _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<MedicineRecord>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WhenNotFound_ReturnsFalse()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        _repositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((MedicineRecord?)null);

        MedicineDeliveryUpdateRequestDto dto = new()
        {
            ResidentId = Guid.NewGuid(),
            MedicineName = "X",
            Timestamp = DateTime.UtcNow
        };

        bool result = await _sut.UpdateAsync(Guid.NewGuid(), dto, ct);

        Assert.False(result);
    }

    [Fact]
    public async Task UpdateAsync_WhenFound_UpdatesAndReturnsTrue()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        MedicineRecord existing = new()
        {
            Id = Guid.NewGuid(),
            ResidentId = Guid.NewGuid(),
            MedicineName = "Old",
            Timestamp = DateTime.UtcNow,
            Given = false
        };
        _repositoryMock
            .Setup(r => r.GetByIdAsync(existing.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existing);

        MedicineDeliveryUpdateRequestDto dto = new()
        {
            ResidentId = existing.ResidentId,
            MedicineName = "New",
            Timestamp = existing.Timestamp,
            Given = true
        };

        bool result = await _sut.UpdateAsync(existing.Id, dto, ct);

        Assert.True(result);
        Assert.Equal("New", existing.MedicineName);
        Assert.True(existing.Given);
        _repositoryMock.Verify(r => r.UpdateAsync(existing, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WhenFound_DeletesAndReturnsTrue()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        MedicineRecord existing = new()
        {
            Id = Guid.NewGuid(),
            ResidentId = Guid.NewGuid(),
            MedicineName = "X",
            Timestamp = DateTime.UtcNow
        };
        _repositoryMock
            .Setup(r => r.GetByIdAsync(existing.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existing);

        bool result = await _sut.DeleteAsync(existing.Id, ct);

        Assert.True(result);
        _repositoryMock.Verify(r => r.DeleteAsync(existing, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WhenNotFound_ReturnsFalse()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        _repositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((MedicineRecord?)null);

        bool result = await _sut.DeleteAsync(Guid.NewGuid(), ct);

        Assert.False(result);
    }
}
