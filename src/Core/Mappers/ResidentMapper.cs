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

public class ResidentMapper
{
    public static Resident ToResident(ResidentResponseDto dto)
    {
            return new Resident
            {
                Id = dto.Id,
                Initials = dto.Initials,
                TrafficLightStatus = (TrafficLightStatus?)dto.TrafficLightStatus,
                Notes = dto.Notes?.Select(ToResidentNote).ToList() ?? new List<ResidentNote>()
            };
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
