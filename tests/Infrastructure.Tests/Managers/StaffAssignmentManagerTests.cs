// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.Interfaces.Repositories;

using Domain.Entities;
using Domain.Enums;

using Infrastructure.Managers;

using Moq;

namespace Infrastructure.Tests.Managers;

public class StaffAssignmentManagerTests
{
    private readonly Mock<IStaffAssignmentRepository> _repo = new(MockBehavior.Strict);
    private readonly StaffAssignmentManager _manager;

    public StaffAssignmentManagerTests() => _manager = new StaffAssignmentManager(_repo.Object);

    private static StaffAssignmentDto NewDto() => new()
    {
        ResidentId = Guid.NewGuid(),
        EmployeeId = Guid.NewGuid(),
        ShiftType = ShiftType.Day,
        AssignmentDate = DateTime.UtcNow
    };

    private static StaffAssignment WithDetails(Guid id) => new()
    {
        Id = id,
        ResidentId = Guid.NewGuid(),
        EmployeeId = Guid.NewGuid(),
        ShiftType = ShiftType.Day,
        AssignmentDate = DateTime.UtcNow.Date,
        Resident = new Resident { Initials = "AB" },
        Employee = new Employee { FirstName = "Jane", LastName = "Doe" }
    };

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task AssignAsync_WhenNoDuplicate_CreatesAndReturnsDto()
    {
        StaffAssignmentDto dto = NewDto();
        Guid createdId = Guid.NewGuid();
        _ = _repo.Setup(r => r.GetExistingAssignmentAsync(dto.ResidentId, dto.EmployeeId, dto.ShiftType, It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((StaffAssignment?)null);
        _ = _repo.Setup(r => r.CreateAsync(It.IsAny<StaffAssignment>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new StaffAssignment { Id = createdId });
        _ = _repo.Setup(r => r.GetByIdWithDetailsAsync(createdId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(WithDetails(createdId));

        AssignmentOverviewDto result = await _manager.AssignAsync(dto, CancellationToken.None);

        Assert.Equal("AB", result.ResidentInitials);
        Assert.Equal("Jane Doe", result.EmployeeName);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task AssignAsync_WhenDuplicate_Throws()
    {
        StaffAssignmentDto dto = NewDto();
        _ = _repo.Setup(r => r.GetExistingAssignmentAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<ShiftType>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new StaffAssignment());

        _ = await Assert.ThrowsAsync<InvalidOperationException>(() => _manager.AssignAsync(dto, CancellationToken.None));
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task AssignAsync_WhenCreatedNotFound_Throws()
    {
        StaffAssignmentDto dto = NewDto();
        Guid createdId = Guid.NewGuid();
        _ = _repo.Setup(r => r.GetExistingAssignmentAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<ShiftType>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((StaffAssignment?)null);
        _ = _repo.Setup(r => r.CreateAsync(It.IsAny<StaffAssignment>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new StaffAssignment { Id = createdId });
        _ = _repo.Setup(r => r.GetByIdWithDetailsAsync(createdId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((StaffAssignment?)null);

        _ = await Assert.ThrowsAsync<KeyNotFoundException>(() => _manager.AssignAsync(dto, CancellationToken.None));
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetAssignmentsByShiftAsync_ReturnsMappedDtos()
    {
        _ = _repo.Setup(r => r.GetByShiftAsync(ShiftType.Evening, It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([WithDetails(Guid.NewGuid()), WithDetails(Guid.NewGuid())]);

        IEnumerable<AssignmentOverviewDto> result =
            await _manager.GetAssignmentsByShiftAsync(ShiftType.Evening, DateTime.UtcNow, CancellationToken.None);

        Assert.Equal(2, result.Count());
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task DeleteAssignmentAsync_WhenExists_Deletes()
    {
        Guid id = Guid.NewGuid();
        StaffAssignment existing = new() { Id = id };
        _ = _repo.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(existing);
        _ = _repo.Setup(r => r.DeleteAsync(existing, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        await _manager.DeleteAssignmentAsync(id, CancellationToken.None);

        _repo.Verify(r => r.DeleteAsync(existing, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task DeleteAssignmentAsync_WhenNotFound_Throws()
    {
        Guid id = Guid.NewGuid();
        _ = _repo.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync((StaffAssignment?)null);

        _ = await Assert.ThrowsAsync<KeyNotFoundException>(() => _manager.DeleteAssignmentAsync(id, CancellationToken.None));
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task UpdateAssignmentAsync_WhenExists_UpdatesAndReturns()
    {
        Guid id = Guid.NewGuid();
        StaffAssignmentDto dto = NewDto();
        _ = _repo.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(new StaffAssignment { Id = id });
        _ = _repo.Setup(r => r.UpdateAsync(It.IsAny<StaffAssignment>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        _ = _repo.Setup(r => r.GetByIdWithDetailsAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(WithDetails(id));

        AssignmentOverviewDto result = await _manager.UpdateAssignmentAsync(id, dto, CancellationToken.None);

        Assert.Equal("AB", result.ResidentInitials);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task UpdateAssignmentAsync_WhenNotFound_Throws()
    {
        Guid id = Guid.NewGuid();
        _ = _repo.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync((StaffAssignment?)null);

        _ = await Assert.ThrowsAsync<KeyNotFoundException>(
            () => _manager.UpdateAssignmentAsync(id, NewDto(), CancellationToken.None));
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task UpdateAssignmentAsync_WhenUpdatedNotFound_Throws()
    {
        Guid id = Guid.NewGuid();
        _ = _repo.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(new StaffAssignment { Id = id });
        _ = _repo.Setup(r => r.UpdateAsync(It.IsAny<StaffAssignment>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        _ = _repo.Setup(r => r.GetByIdWithDetailsAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync((StaffAssignment?)null);

        _ = await Assert.ThrowsAsync<KeyNotFoundException>(
            () => _manager.UpdateAssignmentAsync(id, NewDto(), CancellationToken.None));
    }
}
