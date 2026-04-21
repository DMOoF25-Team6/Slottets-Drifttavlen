// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Core.DTOs.Identity;


/// <summary>
/// Represents the request data required to perform a logout operation.
/// </summary>
/// <remarks>
/// Contains the JWT token that identifies the session to be terminated.
/// </remarks>
public class LogoutRequestDto
{
    /// <summary>
    /// Gets or sets the JWT token associated with the user session to be logged out.
    /// </summary>
    /// <value>
    /// A JWT token string that identifies the session.
    /// </value>
    public required string JwtToken { get; set; }
}
