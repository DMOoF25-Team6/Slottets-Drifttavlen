// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.Interfaces.Managers;
using Core.Services;
using Domain.Entities;
using Domain.Enums;
using Moq;
using Xunit;

namespace Core.Tests.Services;

/// <summary>
/// Unit tests for <see cref="ResidentService"/> (UC-001/014/015/016).
/// Verifies orchestration between the service and <see cref="IResidentManager"/>.
/// </summary>
public class ResidentServiceTests
{
    private readonly Mock<IResidentManager> _managerMock;
    private readonly ResidentService _sut;

    public ResidentServiceTests()
    {
        _managerMock = new Mock<IResidentManager>();
        _sut = new ResidentService(_managerMock.Object);
    }

    #region GetByIdAsync

    [Fact]
    public async Task GetByIdAsync_EmptyId_ThrowsArgumentException()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;

        await Assert.ThrowsAsync<ArgumentException>(() => _sut.GetByIdAsync(Guid.Empty, ct));
    }

    [Fact]
    public async Task GetByIdAsync_ManagerReturnsNull_ReturnsNull()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        _managerMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((ResidentResponseDto?)null);

        Resident? result = await _sut.GetByIdAsync(Guid.NewGuid(), ct);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetByIdAsync_Found_ReturnsMappedResident()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        Guid id = Guid.NewGuid();
        _managerMock.Setup(m => m.GetByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ResidentResponseDto { Id = id, Initials = "AB", Department = Department.Slottet });

        Resident? result = await _sut.GetByIdAsync(id, ct);

        Assert.NotNull(result);
        Assert.Equal(id, result!.Id);
        Assert.Equal("AB", result.Initials);
    }

    #endregion

    #region GetAllAsync / GetByDepartmentsAsync

    [Fact]
    public async Task GetAllAsync_MapsManagerDtosToEntities()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        _managerMock.Setup(m => m.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(
            [
                new ResidentResponseDto { Id = Guid.NewGuid(), Initials = "AA", Department = Department.Slottet },
                new ResidentResponseDto { Id = Guid.NewGuid(), Initials = "BB", Department = Department.Skoven }
            ]);

        List<Resident> result = (await _sut.GetAllAsync(ct)).ToList();

        Assert.Equal(2, result.Count);
        Assert.Contains(result, r => r.Initials == "AA");
        Assert.Contains(result, r => r.Initials == "BB");
    }

    [Fact]
    public async Task GetByDepartmentsAsync_DelegatesToManager()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        List<Department> departments = [Department.Skoven];
        _managerMock.Setup(m => m.GetByDepartmentsAsync(departments, It.IsAny<CancellationToken>()))
            .ReturnsAsync([new ResidentResponseDto { Id = Guid.NewGuid(), Initials = "SK", Department = Department.Skoven }]);

        List<Resident> result = (await _sut.GetByDepartmentsAsync(departments, ct)).ToList();

        Assert.Single(result);
        _managerMock.Verify(m => m.GetByDepartmentsAsync(departments, It.IsAny<CancellationToken>()), Times.Once);
    }

    #endregion

    #region CreateAsync

    [Fact]
    public async Task CreateAsync_NullDto_Throws()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;

        await Assert.ThrowsAsync<ArgumentNullException>(() => _sut.CreateAsync(null!, ct));
    }

    [Fact]
    public async Task CreateAsync_ValidDto_CallsManager()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        ResidentCreateRequestDto dto = new()
        {
            Initials = "AB",
            FirstName = "Anders",
            LastName = "Bjerg",
            Department = Department.Slottet
        };
        _managerMock.Setup(m => m.CreateAsync(dto, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        await _sut.CreateAsync(dto, ct);

        _managerMock.Verify(m => m.CreateAsync(dto, It.IsAny<CancellationToken>()), Times.Once);
    }

    #endregion

    #region UpdateAsync

    [Fact]
    public async Task UpdateAsync_DelegatesToManager()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        Guid id = Guid.NewGuid();
        ResidentUpdateRequestDto dto = new()
        {
            Initials = "AB",
            FirstName = "Anders",
            LastName = "Bjerg",
            TrafficLightStatus = TrafficLightStatus.Green,
            Department = Department.Slottet
        };
        _managerMock.Setup(m => m.UpdateAsync(id, dto, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        await _sut.UpdateAsync(id, dto, ct);

        _managerMock.Verify(m => m.UpdateAsync(id, dto, It.IsAny<CancellationToken>()), Times.Once);
    }

    #endregion

    #region DeleteAsync

    [Fact]
    public async Task DeleteAsync_EmptyId_ThrowsArgumentException()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;

        await Assert.ThrowsAsync<ArgumentException>(() => _sut.DeleteAsync(Guid.Empty, ct));
    }

    [Fact]
    public async Task DeleteAsync_WhenResidentNotFound_DoesNotCallManagerDelete()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        _managerMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((ResidentResponseDto?)null);

        await _sut.DeleteAsync(Guid.NewGuid(), ct);

        _managerMock.Verify(m => m.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task DeleteAsync_WhenResidentExists_CallsManagerDelete()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        Guid id = Guid.NewGuid();
        _managerMock.Setup(m => m.GetByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ResidentResponseDto { Id = id, Initials = "AB", Department = Department.Slottet });
        _managerMock.Setup(m => m.DeleteAsync(id, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        await _sut.DeleteAsync(id, ct);

        _managerMock.Verify(m => m.DeleteAsync(id, It.IsAny<CancellationToken>()), Times.Once);
    }

    #endregion

    #region Concurrency

    [Fact]
    public async Task GetAllAsync_ConcurrentCalls_AllSucceed()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        _managerMock.Setup(m => m.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync([new ResidentResponseDto { Id = Guid.NewGuid(), Initials = "AA", Department = Department.Slottet }]);

        IEnumerable<Task<IEnumerable<Resident>>> tasks =
            Enumerable.Range(0, 50).Select(_ => _sut.GetAllAsync(ct));
        IEnumerable<Resident>[] results = await Task.WhenAll(tasks);

        Assert.All(results, r => Assert.Single(r));
    }

    #endregion
}
