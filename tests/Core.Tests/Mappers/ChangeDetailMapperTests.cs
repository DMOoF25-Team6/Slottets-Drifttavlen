// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs.Audit;
using Core.Mappers;

using Domain.Entities;

namespace Core.Tests.Mappers;

/// <summary>
/// Unit tests for the <see cref="ChangeDetailMapper"/> static class.
/// </summary>
/// <remarks>
/// Verifies entity-to-DTO conversion for the field-level change records used by
/// UC-009 (View History and Traceability).
/// </remarks>
public class ChangeDetailMapperTests
{
    #region ToDto - Single Entity Tests

    [Fact]
    public void ToDto_AnyEntity_MapsId()
    {
        Guid expected = Guid.NewGuid();
        ChangeDetail entity = new() { Id = expected };

        ChangeDetailDto result = ChangeDetailMapper.ToDto(entity);

        Assert.Equal(expected, result.Id);
    }

    [Theory]
    [InlineData("Initials", "AB", "CD")]
    [InlineData("TrafficLightStatus", "Green", "Yellow")]
    [InlineData("Note", "Old text", "Updated text")]
    public void ToDto_AnyEntity_MapsFieldOldValueAndNewValue(string field, string oldValue, string newValue)
    {
        ChangeDetail entity = new() { Field = field, OldValue = oldValue, NewValue = newValue };

        ChangeDetailDto result = ChangeDetailMapper.ToDto(entity);

        Assert.Equal(field, result.Field);
        Assert.Equal(oldValue, result.OldValue);
        Assert.Equal(newValue, result.NewValue);
    }

    [Fact]
    public void ToDto_WithEmptyOldValue_MapsEmptyOldValue()
    {
        ChangeDetail entity = new() { Field = "Note", OldValue = string.Empty, NewValue = "Some text" };

        ChangeDetailDto result = ChangeDetailMapper.ToDto(entity);

        Assert.Equal(string.Empty, result.OldValue);
    }

    #endregion

    #region ToDtos - Collection Tests

    [Fact]
    public void ToDtos_WithMultipleEntities_MapsAll()
    {
        List<ChangeDetail> entities =
        [
            new ChangeDetail { Id = Guid.NewGuid(), Field = "Initials", OldValue = "AB", NewValue = "CD" },
            new ChangeDetail { Id = Guid.NewGuid(), Field = "Note", OldValue = "Old", NewValue = "New" },
            new ChangeDetail { Id = Guid.NewGuid(), Field = "TrafficLightStatus", OldValue = "Green", NewValue = "Red" }
        ];

        IEnumerable<ChangeDetailDto> result = ChangeDetailMapper.ToDtos(entities);

        Assert.Equal(3, result.Count());
    }

    [Fact]
    public void ToDtos_WithEmptyCollection_ReturnsEmptyCollection()
    {
        IEnumerable<ChangeDetail> entities = [];

        IEnumerable<ChangeDetailDto> result = ChangeDetailMapper.ToDtos(entities);

        Assert.Empty(result);
    }

    [Fact]
    public void ToDtos_PreservesIdsAcrossMapping()
    {
        Guid firstId = Guid.NewGuid();
        Guid secondId = Guid.NewGuid();
        List<ChangeDetail> entities =
        [
            new ChangeDetail { Id = firstId, Field = "A" },
            new ChangeDetail { Id = secondId, Field = "B" }
        ];

        List<ChangeDetailDto> result = [.. ChangeDetailMapper.ToDtos(entities)];

        Assert.Equal(firstId, result[0].Id);
        Assert.Equal(secondId, result[1].Id);
    }

    #endregion
}
