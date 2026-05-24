// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.Interfaces.Managers;
using Core.Services;

using Domain.Enums;

using Moq;

namespace Core.Tests.Services;

public class TaskListServiceTests
{
    private readonly Mock<ITaskListManager> _manager = new();
    private readonly TaskListService _service;

    public TaskListServiceTests() => _service = new TaskListService(_manager.Object);

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetAvailableTasksByDepartmentAsync_DelegatesWithDepartment()
    {
        TaskListDto[] expected = [new(), new()];
        _ = _manager.Setup(m => m.GetDashboardTasksByDepartmentAsync(Department.Slottet, It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        IEnumerable<TaskListDto> result = await _service.GetAvailableTasksByDepartmentAsync(Department.Slottet, CancellationToken.None);

        Assert.Equal(2, result.Count());
        _manager.Verify(m => m.GetDashboardTasksByDepartmentAsync(Department.Slottet, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Theory]
    [Trait("Category", "EdgeCase")]
    [InlineData(Department.Slottet)]
    [InlineData(Department.Skoven)]
    [InlineData(Department.Marken)]
    public async Task GetAvailableTasksByDepartmentAsync_AnyDepartment_ReturnsEmptyWhenNone(Department department)
    {
        _ = _manager.Setup(m => m.GetDashboardTasksByDepartmentAsync(department, It.IsAny<CancellationToken>())).ReturnsAsync([]);

        IEnumerable<TaskListDto> result = await _service.GetAvailableTasksByDepartmentAsync(department, CancellationToken.None);

        Assert.Empty(result);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task GetAvailableTasksByDepartmentAsync_WhenManagerThrows_Propagates()
    {
        _ = _manager.Setup(m => m.GetDashboardTasksByDepartmentAsync(It.IsAny<Department>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new HttpRequestException());

        _ = await Assert.ThrowsAsync<HttpRequestException>(
            () => _service.GetAvailableTasksByDepartmentAsync(Department.Slottet, CancellationToken.None));
    }

    [Fact]
    [Trait("Category", "Concurrency")]
    public async Task GetAvailableTasksByDepartmentAsync_ManyParallelCalls_AllSucceed()
    {
        _ = _manager.Setup(m => m.GetDashboardTasksByDepartmentAsync(It.IsAny<Department>(), It.IsAny<CancellationToken>())).ReturnsAsync([]);

        IEnumerable<TaskListDto>[] results = await Task.WhenAll(
            Enumerable.Range(0, 100).Select(_ => _service.GetAvailableTasksByDepartmentAsync(Department.Marken, CancellationToken.None)));

        Assert.All(results, Assert.NotNull);
        _manager.Verify(m => m.GetDashboardTasksByDepartmentAsync(It.IsAny<Department>(), It.IsAny<CancellationToken>()), Times.Exactly(100));
    }
}
