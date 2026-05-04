// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Enums;

namespace Core.DTOs;

/// <summary>
/// DTO for creating a new resident.
/// </summary>
public class ResidentCreateDto
{
    /// <summary>
    /// Gets or sets the initials of the resident (max 2 chars).
    /// </summary>
    public required string Initials { get; set; }

    /// <summary>
    /// Gets or sets the first name of the resident.
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the resident.
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Gets or sets the traffic light status of the resident.
    /// </summary>
    public TrafficLightStatus? TrafficLightStatus { get; set; }
}
