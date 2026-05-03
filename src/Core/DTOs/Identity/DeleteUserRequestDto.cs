// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.Text.Json.Serialization;

namespace Core.DTOs.Identity;

public class DeleteUserRequestDto
{
    [JsonPropertyName("userId")]
    public string UserId { get; set; } = string.Empty;
}
