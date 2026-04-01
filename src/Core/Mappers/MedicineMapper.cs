// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs;

using Domain.Entities;

namespace Core.Mappers;

/// <summary>
/// Maps MedicineRecord collections to MedicineStatusDto.
/// </summary>
public static class MedicineMapper
{
    /// <summary>
    /// Maps a collection of MedicineRecord to a MedicineStatusDto.
    /// </summary>
    /// <param name="residentId">The resident's unique identifier.</param>
    /// <param name="records">The medicine records to map.</param>
    /// <returns>A MedicineStatusDto representing the mapped data.</returns>
    public static MedicineStatusDto ToMedicineStatusDto(Guid residentId, IEnumerable<MedicineRecord> records)
    {
        ArgumentNullException.ThrowIfNull(records);
        return new MedicineStatusDto
        {
            ResidentId = residentId.ToString(),
            Medicine = [.. records.Select(m => m.MedicineName)],
            Timestamps = [.. records.Select(m => m.Timestamp)],
            Given = [.. records.Select(m => m.Given)]
        };
    }
}
