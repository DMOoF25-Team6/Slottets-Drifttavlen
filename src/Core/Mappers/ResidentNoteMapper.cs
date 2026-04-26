// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Core.DTOs;

using Domain.Entities;

namespace Core.Mappers;

/// <summary>
/// Provides mapping operations between <see cref="ResidentNote"/> domain entities and <see cref="ResidentNoteDto"/> data transfer objects.
/// </summary>
/// <remarks>
/// Keeps mapping logic centralised in Core so neither the service nor the manager
/// needs to know how fields are translated — follows Single Responsibility Principle.
/// </remarks>
/// <example>
/// <code language="csharp">
/// ResidentNoteDto dto = ResidentNoteMapper.ToDto(entity);
/// ResidentNote entity = ResidentNoteMapper.ToNewEntity(residentId, "Note text");
/// </code>
/// </example>
public static class ResidentNoteMapper
{
    #region Methods

    /// <summary>
    /// Maps a <see cref="ResidentNote"/> entity to a <see cref="ResidentNoteDto"/>.
    /// </summary>
    /// <param name="entity">The <see cref="ResidentNote"/> entity to map.</param>
    /// <returns>A <see cref="ResidentNoteDto"/> populated from the entity.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="entity"/> parameter is <see langword="null"/>.</exception>
    public static ResidentNoteDto ToDto(ResidentNote entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return new ResidentNoteDto
        {
            Id = entity.Id,
            Note = entity.Note,
            Timestamp = entity.EditedAt,
            Initials = string.Empty
        };
    }

    /// <summary>
    /// Maps an enumerable collection of <see cref="ResidentNote"/> entities to <see cref="ResidentNoteDto"/> objects.
    /// </summary>
    /// <param name="entities">The collection of <see cref="ResidentNote"/> entities to map.</param>
    /// <returns>An enumerable collection of <see cref="ResidentNoteDto"/> objects.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="entities"/> parameter is <see langword="null"/>.</exception>
    public static IEnumerable<ResidentNoteDto> ToDtos(IEnumerable<ResidentNote> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        return entities.Select(ToDto);
    }

    /// <summary>
    /// Creates a new <see cref="ResidentNote"/> entity from a resident identifier and note text.
    /// </summary>
    /// <param name="residentId">A unique identifier for the resident.</param>
    /// <param name="noteText">A string containing the note text.</param>
    /// <returns>A new <see cref="ResidentNote"/> entity ready for persistence.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="noteText"/> parameter is <see langword="null"/>.</exception>
    public static ResidentNote ToNewEntity(Guid residentId, string noteText)
    {
        ArgumentNullException.ThrowIfNull(noteText);

        return new ResidentNote
        {
            Id = Guid.NewGuid(),
            ResidentId = residentId,
            Note = noteText,
            EditedAt = DateTime.UtcNow
        };
    }

    #endregion
}
