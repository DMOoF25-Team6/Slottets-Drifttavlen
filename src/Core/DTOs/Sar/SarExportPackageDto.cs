// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Core.DTOs.Sar;

/// <summary>
/// Generated SAR export package metadata (UC-010, GDPR Art. 15).
/// </summary>
public class SarExportPackageDto
{
    public Guid ExportId { get; set; }
    public DateTime GeneratedAt { get; set; }
    public string FileName { get; set; } = string.Empty;
}
