// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.


namespace Core.DTOs.Account;


/// <summary>
/// Represents the response returned after a refresh token operation.
/// </summary>
/// <remarks>
/// Contains the new JWT token, refresh token, and any error messages encountered during the operation.
/// </remarks>
public class RefreshTokenResponseDto
{
    /// <summary>
    /// Gets or sets the new JWT token issued after a successful refresh operation.
    /// </summary>
    /// <value>
    /// A JWT token string, or <see langword="null" /> if the operation failed.
    /// </value>
    public string? JwtToken { get; set; }

    /// <summary>
    /// Gets or sets the new refresh token issued after a successful refresh operation.
    /// </summary>
    /// <value>
    /// A refresh token string, or <see langword="null" /> if the operation failed.
    /// </value>
    public string? RefreshToken { get; set; }

    /// <summary>
    /// Gets or sets the collection of error messages encountered during the refresh token operation.
    /// </summary>
    /// <value>
    /// A collection of error messages, or <see langword="null" /> if no errors occurred.
    /// </value>
    public IEnumerable<string>? ErrorMessages { get; set; }
}
