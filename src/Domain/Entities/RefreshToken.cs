// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Interfaces;

namespace Domain.Entities;

/// <summary>
/// Represents a refresh token for a user, used for JWT authentication persistence.
/// </summary>
public class RefreshToken : IEntity
{
    /// <summary>
    /// Primary key for the refresh token.
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// The user identifier (foreign key).
    /// </summary>
    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    /// <summary>
    /// The SHA-256 hash of the refresh token value.
    /// </summary>
    public string TokenHash { get; set; } = string.Empty;

    /// <summary>
    /// The date and time when the token expires.
    /// </summary>
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// The date and time when the token was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }


    /// <summary>
    /// The date and time when the token was revoked, if any.
    /// </summary>
    public DateTime? RevokedAt { get; set; }

    /// <summary>
    /// The IP address that created the token.
    /// </summary>
    public string? CreatedByIp { get; set; }

    /// <summary>
    /// The reason for revocation, if any.
    /// </summary>
    public string? RevokedReason { get; set; }

    /// <summary>
    /// Navigation property to the user.
    /// </summary>
    public virtual User? User { get; set; }
}
