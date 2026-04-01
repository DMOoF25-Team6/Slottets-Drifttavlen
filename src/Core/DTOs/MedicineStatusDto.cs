// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Core.DTOs;

/// <summary>
/// Represents the medicine administration status for a resident, including medicine names, timestamps, and administration status.
/// </summary>
/// <remarks>
/// This DTO is used to transfer medicine administration data for a specific resident, including which medicines were given and when.
/// </remarks>
public class MedicineStatusDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the resident.
    /// </summary>
    /// <value>
    /// A string that uniquely identifies the resident. The default is <see cref="string.Empty"/>.
    /// </value>
    public string ResidentId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of medicine names associated with the resident.
    /// </summary>
    /// <value>
    /// An array of medicine names. The default is an empty array.
    /// </value>
    public string[] Medicine { get; set; } = [];

    /// <summary>
    /// Gets or sets the timestamps for each medicine administration event.
    /// </summary>
    /// <value>
    /// An array of <see cref="DateTime"/> values representing when each medicine was administered. The default is an empty array.
    /// </value>
    public DateTime[] Timestamps { get; set; } = [];

    /// <summary>
    /// Gets or sets a value that indicates whether each medicine was given.
    /// </summary>
    /// <value>
    /// An array of <see langword="true"/> or <see langword="false"/> values indicating if the medicine was given. The default is an empty array.
    /// </value>
    public bool[] Given { get; set; } = [];
}
