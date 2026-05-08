// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs;

using Domain.Entities;
using Domain.Enums;

namespace WebUI.Client.Components.Pages;

public partial class Dashboard
{
    private IEnumerable<Resident> _residents = [];

    protected override async Task OnInitializedAsync()
    {
        IEnumerable<ResidentResponseDto> residentDtos = await ResidentService.GetAllAsync();
        _residents = residentDtos.Select(dto => new Resident
        {
            Id = dto.Id,
            Initials = dto.Initials,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            TrafficLightStatus = dto.TrafficLightStatus.HasValue
                ? (TrafficLightStatus)dto.TrafficLightStatus.Value
                : null,
            Notes = dto.Notes.Select(n => new ResidentNote
            {
                Id = n.Id,
                Note = n.Note,
                EditedAt = n.Timestamp,
                ResidentId = dto.Id
            }).ToList()
        }).ToList();
    }
}
