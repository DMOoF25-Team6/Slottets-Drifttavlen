// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Core.DTOs;

/// <summary>
/// Represents the medicine administration status for a resident, including medicine names, timestamps, and administration status.
/// </summary>
/// <remarks>
/// This DTO is used to transfer medicine administration data for a specific resident, including which medicines were given and when.
/// </remarks>
public class MedicineEntryDto
{
    public string Name { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public bool Given { get; set; }
}

/// <summary>
/// Represents the medicine administration status for a resident, including medicine entries.
/// </summary>
public class MedicineStatusDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the resident.
    /// </summary>
    public Guid ResidentId { get; set; }

    /// <summary>
    /// Gets or sets the medicine entries for the resident.
    /// </summary>
    public List<MedicineEntryDto> Entries { get; set; } = [];
}
