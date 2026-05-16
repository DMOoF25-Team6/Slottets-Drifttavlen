// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Sar;

namespace Core.Mappers;

/// <summary>
/// Static mapper for assembling SAR export package DTOs (UC-010, GDPR Art. 15).
/// </summary>
/// <remarks>
/// SAR exports aggregate data from multiple sources; the package metadata DTO is built
/// from the export ID, generation timestamp, and file name produced by the manager.
/// </remarks>
public static class SarExportMapper
{
    public static SarExportPackageDto ToPackageDto(Guid exportId, DateTime generatedAt, string fileName) => new()
    {
        ExportId = exportId,
        GeneratedAt = generatedAt,
        FileName = fileName
    };

    /// <summary>
    /// Builds a SAR export package including the full Art. 15 JSON payload.
    /// </summary>
    public static SarExportPackageDto ToPackageDto(
        Guid exportId,
        DateTime generatedAt,
        string fileName,
        string payload) => new()
    {
        ExportId = exportId,
        GeneratedAt = generatedAt,
        FileName = fileName,
        Payload = payload
    };
}
