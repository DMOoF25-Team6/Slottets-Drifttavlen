// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Interfaces;

namespace Domain.Entities;

/// <summary>
/// Records a single login attempt for security audit and incident detection (UC-010).
/// </summary>
/// <remarks>
/// Created by <c>AccountController.Login</c> on every authentication attempt,
/// successful or not. The <see cref="IncidentDetectionService"/> reads these
/// rows to detect brute-force patterns and off-hours administrator access.
///
/// Privacy:
///   - <see cref="EmailHash"/> is a salted HMAC-SHA256 of the attempted email,
///     produced by <c>IPseudonymizationService</c>. Storing the hash rather than
///     the raw address satisfies Datatilsynet's "Log kun de nødvendige
///     oplysninger" guidance while still allowing correlation across attempts.
///   - <see cref="UserId"/> is set only when the email resolves to a known user;
///     for unknown emails the column is null and only the hash links attempts.
///   - The table inherits the <c>LoginLogs</c> retention policy (default 13
///     months, minimum 3 months) from UC-010 seed data.
/// </remarks>
public class LoginAttempt : IEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public DateTime AttemptedAt { get; set; }

    /// <summary>
    /// HMAC-SHA256 pseudonym of the attempted email address. Stable across attempts
    /// so brute-force detection can group rows by hash without retaining the address.
    /// </summary>
    [Required]
    [MaxLength(64)]
    public string EmailHash { get; set; } = string.Empty;

    /// <summary>Resolved user id, or <see langword="null"/> if no user matches the supplied email.</summary>
    [ForeignKey("User")]
    public Guid? UserId { get; set; }

    [Required]
    public bool Succeeded { get; set; }

    /// <summary>Remote IP address of the client, captured from <c>HttpContext.Connection.RemoteIpAddress</c>.</summary>
    [Required]
    [MaxLength(45)]   // IPv6 textual maximum
    public string IpAddress { get; set; } = string.Empty;

    /// <summary>Short failure category ("InvalidEmail", "InvalidPassword", "Locked"), or empty on success.</summary>
    [Required]
    [MaxLength(64)]
    public string FailureReason { get; set; } = string.Empty;
}
