// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Core.DTOs.Sar;

/// <summary>
/// Generated SAR export package (UC-010, GDPR Art. 15).
/// </summary>
/// <remarks>
/// The <see cref="Payload"/> property carries a JSON document structured per
/// GDPR Art. 15(1)(a)-(h): purposes, categories of personal data, recipients,
/// retention information, sources, rights, and transfer-to-third-country status.
/// Datatilsynet "Ret til indsigt" guidance and the EU Court of Justice ruling
/// of 22 June 2023 require that a copy of all processed personal data be made
/// available, including data held in log files.
/// </remarks>
public class SarExportPackageDto
{
    public Guid ExportId { get; set; }
    public DateTime GeneratedAt { get; set; }
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// The Art. 15 export JSON document. Empty when only metadata is requested.
    /// </summary>
    public string Payload { get; set; } = string.Empty;
}
