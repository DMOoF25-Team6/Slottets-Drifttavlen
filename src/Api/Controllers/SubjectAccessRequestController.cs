// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Sar;
using Core.Interfaces.Repositories;
using Core.Mappers;

using Domain.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// API controller for handling GDPR Subject Access Requests (UC-010, Art. 15).
/// </summary>
/// <remarks>
/// Restricted to Admin role. Generates an export package containing all personal data
/// for a given resident across the scopes specified in the request.
/// </remarks>
[ApiController]
[Authorize(Roles = "admin")]
[Route("subjectaccessrequest")]
public class SubjectAccessRequestController : ControllerBase
{
    #region Fields

    private readonly IResidentRepository _residentRepository;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="SubjectAccessRequestController"/> class.
    /// </summary>
    /// <param name="residentRepository">The resident repository for fetching subject data.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="residentRepository"/> parameter is <see langword="null"/>.</exception>
    public SubjectAccessRequestController(IResidentRepository residentRepository)
    {
        ArgumentNullException.ThrowIfNull(residentRepository);
        _residentRepository = residentRepository;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Generates a GDPR Article 15 Subject Access Request export package for a resident.
    /// </summary>
    /// <param name="dto">The SAR request including resident id and data scopes to include.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The generated export package metadata (file name, generation timestamp, export id).</returns>
    /// <remarks>POST /subjectaccessrequest/export</remarks>
    [HttpPost("export")]
    public async Task<ActionResult<SarExportPackageDto>> GenerateExport(
        [FromBody] SarExportRequestDto dto,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(dto);

        if (dto.ResidentId == Guid.Empty)
        {
            return BadRequest("ResidentId is required.");
        }

        Resident? resident = await _residentRepository.GetByIdAsync(dto.ResidentId, cancellationToken);
        if (resident is null)
        {
            return NotFound($"No resident found with id {dto.ResidentId}.");
        }

        // STUB: actual export assembly (residency, notes, medicine logs, painkillers, audit entries)
        // is implemented in a follow-up branch. We return package metadata for the UI to display.
        Guid exportId = Guid.NewGuid();
        DateTime generatedAt = DateTime.UtcNow;
        string fileName = $"sar-export-{resident.Initials}-{generatedAt:yyyyMMddHHmmss}.json";

        SarExportPackageDto package = SarExportMapper.ToPackageDto(exportId, generatedAt, fileName);
        return Ok(package);
    }

    /// <summary>
    /// Marks a Subject Access Request as fulfilled (delivered to the data subject).
    /// </summary>
    /// <param name="dto">The fulfillment payload with SAR id and timestamp.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns><see langword="true"/> if the SAR was marked fulfilled.</returns>
    /// <remarks>POST /subjectaccessrequest/fulfilled</remarks>
    [HttpPost("fulfilled")]
    public Task<ActionResult<bool>> MarkFulfilled(
        [FromBody] SarFulfilledDto dto,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(dto);

        if (dto.SarId == Guid.Empty)
        {
            return Task.FromResult<ActionResult<bool>>(BadRequest("SarId is required."));
        }

        // STUB: persistence of SAR fulfillment status pending a SubjectAccessRequest entity
        // in a follow-up branch. AuditInterceptor (UC-009) provides chronology in the meantime.
        return Task.FromResult<ActionResult<bool>>(Ok(true));
    }

    #endregion
}
