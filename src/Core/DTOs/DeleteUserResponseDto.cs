// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Core.DTOs;

public class DeleteUserResponseDto
{
    public bool IsSuccessful { get; set; }
    public IEnumerable<string> ErrorMessages { get; set; } = [];
}
