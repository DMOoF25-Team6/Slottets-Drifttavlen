// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs;

using Domain.Entities;

namespace Core.Mappers;

public static class PainKillerMapper
{
    public static PainkillerStatusDto ToPainkillerStatusDto(Guid residentId, IEnumerable<PainkillerRecord> records)
    {
        ArgumentNullException.ThrowIfNull(records);
        return new PainkillerStatusDto
        {
            ResidentId = residentId,
            Types = [.. records.Select(p => p.Type)],
            NextAllowedTime = records.Any() ? records.Max(p => p.NextAllowedTime).AddHours(4) : DateTime.UtcNow
        };
    }
}