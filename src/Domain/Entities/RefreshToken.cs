// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Domain.Entities;

/// <summary>
/// Represents a refresh token for a user, used for JWT authentication persistence.
/// </summary>
public class RefreshToken
{
    /// <summary>
    /// Primary key for the refresh token.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The user identifier (foreign key).
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// The refresh token value.
    /// </summary>
    public string Token { get; set; } = string.Empty;

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
    /// Navigation property to the user.
    /// </summary>
    public virtual User? User { get; set; }
}
