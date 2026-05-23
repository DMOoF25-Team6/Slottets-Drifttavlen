// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Core.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace Core.Mappers;

public class ResidentMapper
{
    public static Resident ToResident(ResidentResponseDto dto)
    {
        return new Resident
        {
            Id = dto.Id,
            Initials = dto.Initials,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            TrafficLightStatus = (TrafficLightStatus?)dto.TrafficLightStatus,
            Department = dto.Department,
            Activity = dto.Activity,
            Companion = dto.Companion,
            Amount = dto.Amount,
            Info = dto.Info
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
            Notes = entity.Notes?.Select(ToResidentNoteDto).ToList() ?? [],
            Department = entity.Department,
            Activity = entity.Activity,
            Companion = entity.Companion,
            Amount = entity.Amount,
            Info = entity.Info
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

    public static Resident ToResident(ResidentCreateRequestDto dto)
    {
        return new Resident
        {
            Id = Guid.NewGuid(),
            Initials = dto.Initials,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            TrafficLightStatus = dto.TrafficLightStatus,
            Department = dto.Department,
            Activity = dto.Activity,
            Companion = dto.Companion,
            Amount = dto.Amount,
            Info = dto.Info
        };
    }

    public static void ApplyUpdate(Resident entity, ResidentUpdateRequestDto dto)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(dto);
        entity.Initials = dto.Initials;
        entity.FirstName = dto.FirstName;
        entity.LastName = dto.LastName;
        entity.TrafficLightStatus = dto.TrafficLightStatus;
        entity.Department = dto.Department;
        entity.Activity = dto.Activity;
        entity.Companion = dto.Companion;
        entity.Amount = dto.Amount;
        entity.Info = dto.Info;
    }

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
