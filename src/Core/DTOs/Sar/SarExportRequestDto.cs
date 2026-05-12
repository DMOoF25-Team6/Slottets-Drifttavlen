// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Core.DTOs.Sar;

/// <summary>
/// Request payload for generating a GDPR Subject Access Request export (Art. 15).
/// </summary>
public class SarExportRequestDto
{
    public Guid ResidentId { get; set; }
    public string[] ScopeOptions { get; set; } = [];
}
