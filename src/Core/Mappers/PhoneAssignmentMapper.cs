// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs;

using Domain.Entities;

namespace Core.Mappers;

/// <summary>
/// Provides mapping methods between <see cref="PhoneAssignment"/> domain entities and <see cref="PhoneAssignmentDto"/> data transfer objects.
/// </summary>
/// <remarks>
/// Centralises all entity-to-DTO conversion for phone assignments in Core,
/// ensuring domain entities are never exposed directly across layer boundaries.
/// </remarks>
public static class PhoneAssignmentMapper
{
    /// <summary>
    /// Maps a <see cref="PhoneAssignment"/> entity to a <see cref="PhoneAssignmentDto"/>.
    /// </summary>
    /// <param name="entity">The phone assignment entity to map.</param>
    /// <returns>A <see cref="PhoneAssignmentDto"/> representing the entity.</returns>
    public static PhoneAssignmentDto ToDto(PhoneAssignment entity)
    {
        return new PhoneAssignmentDto
        {
            PhoneNumber = entity.PhoneNumber,
            ShiftType = entity.ShiftType
        };
    }

    /// <summary>
    /// Maps a collection of <see cref="PhoneAssignment"/> entities to <see cref="PhoneAssignmentDto"/> objects.
    /// </summary>
    /// <param name="entities">The collection of phone assignment entities to map.</param>
    /// <returns>A collection of <see cref="PhoneAssignmentDto"/> objects.</returns>
    public static IEnumerable<PhoneAssignmentDto> ToDtos(IEnumerable<PhoneAssignment> entities)
    {
        return entities.Select(ToDto);
    }
}
