// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.DTOs;

using Domain.Enums;
using Domain.Entities;

namespace Core.Mappers;

/// <summary>
/// Provides mapping methods for converting between resident DTOs and domain entities.
/// </summary>
/// <remarks>
/// This class contains static methods to transform <see cref="ResidentResponseDto"/> and <see cref="ResidentNoteDto"/> objects to their corresponding domain entities.
/// </remarks>
public class ResidentMapper
{
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
