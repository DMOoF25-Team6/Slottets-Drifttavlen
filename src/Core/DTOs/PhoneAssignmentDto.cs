// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Core.DTOs;

/// <summary>
/// Represents a data transfer object for sending phone assignment data to the client dashboard.
/// </summary>
public class PhoneAssignmentDto
{
    /// <summary>
    /// Gets or sets the fixed work phone number.
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the shift type for this phone assignment (Day, Evening, Night).
    /// </summary>
    public string ShiftType { get; set; } = string.Empty;
}
