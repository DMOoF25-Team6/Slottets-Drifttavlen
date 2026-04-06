// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Core.DTOs.Accounts;

public class RegistrationResponseDto
{
    public bool IsSuccessful { get; set; }
    public IEnumerable<string> Errors { get; set; } = [];
}
