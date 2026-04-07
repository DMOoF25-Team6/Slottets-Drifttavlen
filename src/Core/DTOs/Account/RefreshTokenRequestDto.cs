// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Core.DTOs.Account;

/// <summary>
/// Represents a request to refresh an authentication token.
/// </summary>
/// <remarks>
/// Contains the refresh token required to obtain a new access token from the authentication service.
/// </remarks>
public class RefreshTokenRequestDto
{
    /// <summary>
    /// Gets or sets the refresh token used to request a new access token.
    /// </summary>
    /// <value>
    /// A string containing the refresh token. The value can be <see langword="null"/> if not provided.
    /// </value>
    public string? RefreshToken { get; set; }
}
