// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Dto.Identity;

namespace Core.DTOs.Identity;


/// <summary>
/// Represents the response returned after a logout operation.
/// </summary>
/// <remarks>
/// Contains information about the success of the logout and any error messages encountered.
/// </remarks>
public class LogoutResponseDto : ILogoutResult
{
    /// <summary>
    /// Gets or sets a value that indicates whether the logout operation was successful.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the logout was successful; otherwise, <see langword="false" />.
    /// </value>
    public bool IsSuccessful { get; set; } = true;
}
