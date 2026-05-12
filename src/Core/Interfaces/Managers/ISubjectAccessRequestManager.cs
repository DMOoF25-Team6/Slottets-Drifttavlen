// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Sar;

namespace Core.Interfaces.Managers;

/// <summary>
/// Defines a contract for managing GDPR Subject Access Requests (UC-010, Art. 15).
/// </summary>
public interface ISubjectAccessRequestManager
{
    Task<SarExportPackageDto> GenerateExportAsync(SarExportRequestDto dto, CancellationToken cancellationToken);
    Task<bool> MarkFulfilledAsync(SarFulfilledDto dto, CancellationToken cancellationToken);
}
