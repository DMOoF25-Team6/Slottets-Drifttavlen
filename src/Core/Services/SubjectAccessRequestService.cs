// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Sar;
using Core.Interfaces.Managers;
using Core.Interfaces.Services;

namespace Core.Services;

/// <summary>
/// Service for handling GDPR Subject Access Requests (UC-010, Art. 15).
/// </summary>
public class SubjectAccessRequestService : ISubjectAccessRequestService
{
    private readonly ISubjectAccessRequestManager _subjectAccessRequestManager;

    public SubjectAccessRequestService(ISubjectAccessRequestManager subjectAccessRequestManager)
    {
        ArgumentNullException.ThrowIfNull(subjectAccessRequestManager);
        _subjectAccessRequestManager = subjectAccessRequestManager;
    }

    public Task<SarExportPackageDto> GenerateExportAsync(SarExportRequestDto dto, CancellationToken cancellationToken)
        => _subjectAccessRequestManager.GenerateExportAsync(dto, cancellationToken);

    public Task<bool> MarkFulfilledAsync(SarFulfilledDto dto, CancellationToken cancellationToken)
        => _subjectAccessRequestManager.MarkFulfilledAsync(dto, cancellationToken);
}
