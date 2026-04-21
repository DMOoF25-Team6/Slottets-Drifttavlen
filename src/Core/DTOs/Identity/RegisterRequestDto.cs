// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.ComponentModel.DataAnnotations;


namespace Core.DTOs.Identity;


/// <summary>
/// Represents the request data required to register a new user account.
/// </summary>
/// <remarks>
/// Contains the email address, password, and confirmation password for registration. Validation attributes ensure the data meets requirements.
/// </remarks>
public class RegisterRequestDto
{
    /// <summary>
    /// Gets or sets the email address for the new user account.
    /// </summary>
    /// <value>
    /// An email address string that uniquely identifies the user.
    /// </value>
    [Required(ErrorMessage = Constants.EmailRequired)]
    [EmailAddress(ErrorMessage = Constants.InvalidEmailAddress)]
    public required string Email { get; set; }

    /// <summary>
    /// Gets or sets the password for the new user account.
    /// </summary>
    /// <value>
    /// A password string that meets the minimum length requirement.
    /// </value>
    [Required(ErrorMessage = Constants.PasswordRequired)]
    [MinLength(6, ErrorMessage = Constants.PasswordMinLength)]
    public required string Password { get; set; }
}
