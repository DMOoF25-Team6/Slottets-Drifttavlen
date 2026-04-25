// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Core.DTOs.Identity;

/// <summary>
/// Represents a response to a login request.
/// </summary>
/// <remarks>
/// Contains authentication tokens and error messages returned from the authentication service.
/// </remarks>
public class LoginResponseDto
{
    /// <summary>
    /// Gets or sets the JSON Web Token (JWT) issued upon successful authentication.
    /// </summary>
    /// <value>
    /// A string containing the JWT token. The value can be <see langword="null"/> if authentication failed.
    /// </value>
    public string? JwtToken { get; set; }

    /// <summary>
    /// Gets or sets the refresh token issued for obtaining new access tokens.
    /// </summary>
    /// <value>
    /// A string containing the refresh token. The value can be <see langword="null"/> if authentication failed.
    /// </value>
    public string? RefreshToken { get; set; }

    /// <summary>
    /// Gets or sets the collection of error messages returned from the authentication service.
    /// </summary>
    /// <value>
    /// A collection of error messages. The value can be <see langword="null"/> if there are no errors.
    /// </value>
    public IEnumerable<string>? ErrorMessages { get; set; }
}
