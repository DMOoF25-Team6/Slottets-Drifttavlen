// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Account;

/// <summary>
/// Represents a request to log in to the system.
/// </summary>
/// <remarks>
/// Contains the credentials required for user authentication.
/// </remarks>
public class LoginRequestDto
{
    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    /// <value>
    /// A string containing the user's email address.
    /// </value>
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public required string EmailAddress { get; set; }

    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    /// <value>
    /// A string containing the user's password.
    /// </value>
    public required string Password { get; set; }
}
