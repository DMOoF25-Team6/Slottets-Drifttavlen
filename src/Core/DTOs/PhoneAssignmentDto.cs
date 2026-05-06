// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

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
    /// <summary>
    /// Enum representation of the shift type (Day, Evening, Night).
    /// </summary>
    public Domain.Enums.ShiftType ShiftTypeEnum
    {
        get => ShiftType switch
        {
            "Dag" => Domain.Enums.ShiftType.Day,
            "Aften" => Domain.Enums.ShiftType.Evening,
            "Nat" => Domain.Enums.ShiftType.Night,
            _ => Domain.Enums.ShiftType.Day
        };
        set => ShiftType = value.ToString();
    }

    /// <summary>
    /// Gets or sets the name of the staff member assigned to this phone number for the active shift.
    /// An empty string indicates the phone is unassigned.
    /// </summary>
    public string AssignedStaffName { get; set; } = string.Empty;
}
