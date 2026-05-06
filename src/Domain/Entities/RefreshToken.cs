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
    /// <summary>
    /// Sets the TokenHash property from a plaintext token using SHA-256.
    /// </summary>
    /// <param name="token">The plaintext token.</param>
    public void SetTokenFromPlaintext(string token)
    {
        TokenHash = ComputeSha256Hash(token);
    }

    /// <summary>
    /// Computes the SHA-256 hash of a given string and returns it as a hex string.
    /// </summary>
    /// <param name="input">The input string to hash.</param>
    /// <returns>Hex-encoded SHA-256 hash.</returns>
    public static string ComputeSha256Hash(string input)
    {
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input);
        byte[] hash = System.Security.Cryptography.SHA256.HashData(bytes);
        return string.Concat(hash.Select(b => b.ToString("x2")));
    }
}
