// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.Mappers;
using Domain.Entities;
using Domain.Enums;
using Xunit;

namespace Core.Tests.Mappers;

/// <summary>
/// Unit tests for <see cref="TaskListMapper"/> (UC-002/006).
/// </summary>
public class TaskListMapperTests
{
    [Fact]
    public void ToTaskListDto_MapsAllFields()
    {
        TaskList entity = new()
        {
            Id = Guid.NewGuid(),
            Title = "Aftensmad",
            Description = "Lav aftensmad",
            TaskStatus = TaskListStatus.InProgress,
            DueTime = new DateTime(2026, 5, 23, 18, 0, 0, DateTimeKind.Utc),
            Department = Department.Slottet
        };

        TaskListDto dto = entity.ToTaskListDto();

        Assert.Equal(entity.Id, dto.Id);
        Assert.Equal("Aftensmad", dto.Title);
        Assert.Equal("Lav aftensmad", dto.Description);
        Assert.Equal(TaskListStatus.InProgress, dto.TaskListStatus);
        Assert.Equal(entity.DueTime, dto.DueTime);
        Assert.Equal(Department.Slottet, dto.Department);
    }

    [Theory]
    [InlineData(TaskListStatus.Success)]
    [InlineData(TaskListStatus.InProgress)]
    public void ToTaskListDto_PreservesStatus(TaskListStatus status)
    {
        TaskList entity = new() { Id = Guid.NewGuid(), Title = "t", Description = "d", TaskStatus = status };

        TaskListDto dto = entity.ToTaskListDto();

        Assert.Equal(status, dto.TaskListStatus);
    }
}
