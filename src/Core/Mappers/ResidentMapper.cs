// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs;

using Domain.Entities;
using Domain.Enums;

namespace Core.Mappers;

/// <summary>
/// Provides mapping methods for converting between resident DTOs and domain entities.
/// </summary>
/// <remarks>
/// This class contains static methods to transform <see cref="ResidentResponseDto"/> and <see cref="ResidentNoteDto"/> objects to their corresponding domain entities.
/// </remarks>
public class ResidentMapper
{
    public static Resident ToResident(ResidentRequest dto)
    {
        return new Resident
        {
            Id = dto.Id,
            Initials = dto.Initials,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            TrafficLightStatus = dto.TrafficLightStatus
        };
    }

    public static ResidentResponseDto ToResidentResponseDto(Resident entity)
    {
        return new ResidentResponseDto
        {
            Id = entity.Id,
            Initials = entity.Initials,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            TrafficLightStatus = (int?)entity.TrafficLightStatus,
            Notes = entity.Notes?.Select(ToResidentNoteDto).ToList() ?? []
        };
    }

    public static ResidentNoteDto ToResidentNoteDto(ResidentNote note)
    {
        return new ResidentNoteDto
        {
            Id = note.Id,
            Note = note.Note,
            Timestamp = note.EditedAt
        };
    }
    /// <summary>
    /// Maps a <see cref="ResidentCreateDto"/> to a <see cref="Resident"/> domain entity.
    /// </summary>
    /// <param name="dto">A data transfer object for creating a resident.</param>
    /// <returns>A <see cref="Resident"/> domain entity mapped from the DTO.</returns>
    public static Resident ToResident(ResidentCreateDto dto)
    {
        return new Resident
        {
            Id = Guid.NewGuid(),
            Initials = dto.Initials,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            TrafficLightStatus = dto.TrafficLightStatus
        };
    }

    /// <summary>
    /// Maps a <see cref="ResidentResponseDto"/> to a <see cref="Resident"/> domain entity.
    /// </summary>
    /// <param name="dto">A data transfer object representing a resident.</param>
    /// <returns>A <see cref="Resident"/> domain entity mapped from the DTO.</returns>
    public static Resident ToResident(ResidentResponseDto dto)
    {
        return new Resident
        {
            Id = dto.Id,
            Initials = dto.Initials,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            TrafficLightStatus = (TrafficLightStatus?)dto.TrafficLightStatus,
            Notes = dto.Notes?.Select(ToResidentNote).ToList() ?? []
        };
    }

    /// <summary>
    /// Maps a <see cref="ResidentNoteDto"/> to a <see cref="ResidentNote"/> domain entity.
    /// </summary>
    /// <param name="dto">A data transfer object representing a resident note.</param>
    /// <returns>A <see cref="ResidentNote"/> domain entity mapped from the DTO.</returns>
    public static ResidentNote ToResidentNote(ResidentNoteDto dto)
    {
        return new ResidentNote
        {
            Id = dto.Id,
            Note = dto.Note,
            EditedAt = dto.Timestamp
        };
    }
}
