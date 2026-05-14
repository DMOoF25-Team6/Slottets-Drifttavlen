// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Sar;

namespace Core.Interfaces.Services;

/// <summary>
/// Defines a contract for the GDPR Subject Access Request service (UC-010, Art. 15).
/// </summary>
public interface ISubjectAccessRequestService
{
    Task<SarExportPackageDto> GenerateExportAsync(SarExportRequestDto dto, CancellationToken cancellationToken);
    Task<bool> MarkFulfilledAsync(SarFulfilledDto dto, CancellationToken cancellationToken);
}
