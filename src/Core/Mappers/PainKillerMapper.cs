// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs;

using Domain.Entities;

namespace Core.Mappers;

/// <summary>
/// Provides mapping methods for converting painkiller records to DTOs.
/// </summary>
/// <remarks>
/// Contains static methods to transform <see cref="PainkillerRecord"/> collections into <see cref="PainkillerStatusDto"/> objects.
/// </remarks>
public static class PainKillerMapper
{
    /// <summary>
    /// Maps a collection of <see cref="PainkillerRecord"/> entities to a <see cref="PainkillerStatusDto"/>.
    /// </summary>
    /// <param name="residentId">A unique identifier for the resident.</param>
    /// <param name="records">A collection of painkiller records for the resident.</param>
    /// <returns>A <see cref="PainkillerStatusDto"/> representing the painkiller status for the resident.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="records"/> parameter is <see langword="null"/>.</exception>
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
