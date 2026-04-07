// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Accounts;

public class LoginRequestDto
{
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public required string EmailAddress { get; set; }
    public required string Password { get; set; }
}
