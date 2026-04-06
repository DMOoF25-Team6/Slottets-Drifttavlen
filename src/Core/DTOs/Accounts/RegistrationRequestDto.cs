// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Accounts;

public class RegistrationRequestDto
{
    [Required(ErrorMessage = Constants.EmailRequired)]
    [EmailAddress(ErrorMessage = Constants.InvalidEmailAddress)]
    public required string EmailAddress { get; set; }

    [Required(ErrorMessage = Constants.PasswordRequired)]
    [MinLength(6, ErrorMessage = Constants.PasswordMinLength)]
    public required string Password { get; set; }

    [Required(ErrorMessage = Constants.ConfirmPasswordRequired)]
    [Compare("Password", ErrorMessage = Constants.PasswordsDoNotMatch)]
    public required string ConfirmPassword { get; set; }
}
