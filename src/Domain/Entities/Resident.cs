// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.ComponentModel.DataAnnotations;

using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Entities;

public class Resident : IEntity
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    [MaxLength(2)]
    public string Initials { get; set; } = string.Empty;
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;
    [Required]
    public TrafficLightStatus? TrafficLightStatus { get; set; }
    [Required]
    public Department Department { get; set; }

    /// <summary>
    /// UTC timestamp at which the resident was discharged from the care home, or
    /// <see langword="null"/> while the resident is still active.
    /// </summary>
    /// <remarks>
    /// Used by <c>RetentionBackgroundService</c> (UC-010) as the primary retention
    /// trigger: residents with a non-null <see cref="DischargedAt"/> become candidates
    /// for anonymisation once the configured retention period has elapsed. Aligns the
    /// data model with the "DischargedAt + 90 days" rule documented in
    /// <c>docs/use-cases/uc-010-ensure-data-security-and-gdpr-compliance/uc-010.usecase.da.md</c>.
    /// </remarks>
    /// <summary>Free-text activity shown on the dashboard card (e.g. Handle, Spadseretur).</summary>
    [MaxLength(100)]
    public string? Activity { get; set; }

    /// <summary>Initials or name of the staff member accompanying the resident for the activity.</summary>
    [MaxLength(50)]
    public string? Companion { get; set; }

    /// <summary>Amount allowed for the activity (free-text, e.g. "300kr").</summary>
    [MaxLength(20)]
    public string? Amount { get; set; }

    /// <summary>Additional free-text info shown on the dashboard card (e.g. Eget kort).</summary>
    [MaxLength(200)]
    public string? Info { get; set; }

    public DateTime? DischargedAt { get; set; }

    public virtual ICollection<ResidentNote> Notes { get; set; } = [];
    public virtual ICollection<MedicineRecord> Medicines { get; set; } = [];
    public virtual ICollection<PainkillerRecord> Painkillers { get; set; } = [];
    public virtual ICollection<StaffAssignment> StaffAssignments { get; set; } = [];
}
