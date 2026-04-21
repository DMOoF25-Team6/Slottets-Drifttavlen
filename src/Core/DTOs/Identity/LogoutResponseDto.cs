// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Core.DTOs.Identity;


/// <summary>
/// Represents the response returned after a logout operation.
/// </summary>
/// <remarks>
/// Contains information about the success of the logout and any error messages encountered.
/// </remarks>
public class LogoutResponseDto
{
    /// <summary>
    /// Gets or sets a value that indicates whether the logout operation was successful.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the logout was successful; otherwise, <see langword="false" />.
    /// </value>
    public bool IsSuccessful { get; set; }

    /// <summary>
    /// Gets or sets the collection of error messages encountered during logout.
    /// </summary>
    /// <value>
    /// A collection of error messages, or <see langword="null" /> if no errors occurred.
    /// </value>
    public IEnumerable<string>? ErrorMessages { get; set; }
}
