// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Interfaces;

namespace Domain.Entities;

/// <summary>
/// Records the full lifecycle of a GDPR Subject Access Request (UC-010, Art. 15).
/// </summary>
/// <remarks>
/// One row represents one SAR from registration through fulfilment. The row is
/// created when <c>SubjectAccessRequestController.GenerateExport</c> assembles the
/// export package, and updated when the Admin confirms delivery via
/// <c>MarkFulfilled</c>. Together with <c>AuditEntry</c> rows produced by the
/// <c>AuditInterceptor</c> (UC-009), this entity provides the GDPR Art. 30
/// record-of-processing trail required to evidence compliance with the one-month
/// response deadline of Art. 12(3).
///
/// References:
///   - GDPR Art. 12(3): response within one month, optionally extended by two
///     months for complex requests.
///   - GDPR Art. 15: scope of the right of access.
///   - GDPR Art. 30: records of processing activities.
///   - Datatilsynet "Ret til indsigt" PDF: documentation of fulfilment.
///   - EU Court of Justice ruling C-579/21 (22 June 2023): logs are part of the
///     Art. 15 scope.
/// </remarks>
public class SubjectAccessRequest : IEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [ForeignKey(nameof(Resident))]
    public Guid ResidentId { get; set; }

    /// <summary>Employee who registered/generated the export.</summary>
    [Required]
    public Guid RequestedByEmployeeId { get; set; }

    [Required]
    public DateTime RequestedAt { get; set; }

    /// <summary>
    /// Comma-separated scope identifiers ("Notes", "Medicine", "Painkiller", "Audit").
    /// Stored flat rather than as a child table because the value set is small and
    /// constant, and the column is queried as an opaque audit field.
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string ScopeOptions { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string ExportFileName { get; set; } = string.Empty;

    [Required]
    public DateTime ExportGeneratedAt { get; set; }

    /// <summary>UTC timestamp at which the Admin confirmed delivery, or <see langword="null"/> until then.</summary>
    public DateTime? FulfilledAt { get; set; }

    /// <summary>Employee who confirmed delivery, or <see langword="null"/> until fulfilment.</summary>
    public Guid? FulfilledByEmployeeId { get; set; }

    public virtual Resident? Resident { get; set; }
}
