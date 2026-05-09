// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs.Audit;

using Domain.Entities;

namespace Core.Mappers;

/// <summary>
/// Provides mapping methods between <see cref="ChangeDetail"/> domain entities and <see cref="ChangeDetailDto"/> objects.
/// </summary>
/// <remarks>
/// Used together with <see cref="AuditMapper"/> when an audit entry is loaded with its
/// associated field-level change details for UC-009 (View History and Traceability).
/// </remarks>
public static class ChangeDetailMapper
{
    /// <summary>
    /// Maps a <see cref="ChangeDetail"/> entity to a <see cref="ChangeDetailDto"/>.
    /// </summary>
    /// <param name="entity">The change detail entity to map.</param>
    /// <returns>A <see cref="ChangeDetailDto"/> representing the entity.</returns>
    public static ChangeDetailDto ToDto(ChangeDetail entity)
    {
        return new ChangeDetailDto
        {
            Id = entity.Id,
            Field = entity.Field,
            OldValue = entity.OldValue,
            NewValue = entity.NewValue
        };
    }

    /// <summary>
    /// Maps a collection of <see cref="ChangeDetail"/> entities to <see cref="ChangeDetailDto"/> objects.
    /// </summary>
    /// <param name="entities">The change detail entities to map.</param>
    /// <returns>A collection of <see cref="ChangeDetailDto"/> objects.</returns>
    public static IEnumerable<ChangeDetailDto> ToDtos(IEnumerable<ChangeDetail> entities)
    {
        return entities.Select(ToDto);
    }
}
