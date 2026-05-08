// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Enums;

namespace Core.DTOs;

/// <summary>
/// Represents a request to create a new resident.
/// </summary>
public class ResidentCreateRequestDto
{
    /// <summary>
    /// Gets or sets the initials of the resident.
    /// </summary>
    /// <value>The initials of the resident.</value>
    public required string Initials { get; set; }

    /// <summary>
    /// Gets or sets the first name of the resident.
    /// </summary>
    /// <value>The first name of the resident.</value>
    public required string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the resident.
    /// </summary>
    /// <value>The last name of the resident.</value>
    public required string LastName { get; set; }

    /// <summary>
    /// Gets or sets the status of the resident's traffic light.
    /// </summary>
    /// <value>One of the <see cref="TrafficLightStatus"/> enumeration values that specifies the resident's status. <see langword="null"/> if no status is set.</value>
    public TrafficLightStatus? TrafficLightStatus { get; set; } = default;
}

/// <summary>
/// Represents a request to update an existing resident.
/// </summary>
public class ResidentUpdateRequestDto
{
    /// <summary>
    /// Gets or sets the initials of the resident.
    /// </summary>
    /// <value>The initials of the resident.</value>
    public required string Initials { get; set; }

    /// <summary>
    /// Gets or sets the first name of the resident.
    /// </summary>
    /// <value>The first name of the resident.</value>
    public required string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the resident.
    /// </summary>
    /// <value>The last name of the resident.</value>
    public required string LastName { get; set; }

    /// <summary>
    /// Gets or sets the status of the resident's traffic light.
    /// </summary>
    /// <value>One of the <see cref="TrafficLightStatus"/> enumeration values that specifies the resident's status. <see langword="null"/> if no status is set.</value>
    public required TrafficLightStatus? TrafficLightStatus { get; set; }
}

/// <summary>
/// Represents a data transfer object for resident responses.
/// </summary>
public class ResidentResponseDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the resident.
    /// </summary>
    /// <value>The unique identifier of the resident.</value>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the initials of the resident.
    /// </summary>
    /// <value>The initials of the resident.</value>
    public string Initials { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the first name of the resident.
    /// </summary>
    /// <value>The first name of the resident.</value>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the last name of the resident.
    /// </summary>
    /// <value>The last name of the resident.</value>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the status of the resident's traffic light, where 0 = green, 1 = yellow, 2 = red, and <see langword="null"/> = no status.
    /// </summary>
    /// <value>An integer representing the traffic light status, or <see langword="null"/> if no status is set.</value>
    public int? TrafficLightStatus { get; set; }

    /// <summary>
    /// Gets or sets the list of resident notes associated with the resident.
    /// </summary>
    /// <value>A list of <see cref="ResidentNoteDto"/> objects associated with the resident. The list may be empty if there are no notes.</value>
    public List<ResidentNoteDto> Notes { get; set; } = [];
}
