// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Core.DTOs;

/// <summary>
/// Represents the painkiller administration status for a resident, including types and next allowed administration time.
/// </summary>
/// <remarks>
/// This DTO is used to transfer painkiller status data for a specific resident, including which types are allowed and when the next dose can be administered.
/// </remarks>
public class PainkillerStatusDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the resident.
    /// </summary>
    /// <value>
    /// A unique identifier for the resident.
    /// </value>
    public Guid ResidentId { get; set; }

    /// <summary>
    /// Gets or sets the list of painkiller types associated with the resident.
    /// </summary>
    /// <value>
    /// An array of painkiller type names. The default is an empty array.
    /// </value>
    public IEnumerable<string> Types { get; set; } = [];

    /// <summary>
    /// Gets or sets the next allowed time for painkiller administration.
    /// </summary>
    /// <value>
    /// The <see cref="DateTime"/> value representing when the next painkiller can be administered.
    /// </value>
    public DateTime NextAllowedTime { get; set; }
}
