// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Core.DTOs.Identity;

/// <summary>
/// Represents the response returned after a user registration attempt.
/// </summary>
/// <remarks>
/// Contains information about the success of the registration and any error messages encountered.
/// </remarks>
public class RegistrationResponseDto
{
    /// <summary>
    /// Gets or sets a value that indicates whether the registration was successful.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the registration succeeded; otherwise, <see langword="false" />.
    /// </value>
    public bool IsSuccessful { get; set; }

    /// <summary>
    /// Gets or sets the collection of error messages encountered during registration.
    /// </summary>
    /// <value>
    /// A collection of error messages. The collection is empty if the registration was successful.
    /// </value>
    public IEnumerable<string> ErrorMessages { get; set; } = [];
}
