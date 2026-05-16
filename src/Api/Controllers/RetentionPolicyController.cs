// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Retention;
using Core.Interfaces.Repositories;
using Core.Mappers;

using Domain.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// API controller for managing GDPR retention policies (UC-010).
/// </summary>
/// <remarks>
/// Restricted to Admin role. Provides endpoints to view and update retention policies
/// with legal-minimum enforcement (e.g., Autorisationsloven §22 for MedicineLogs).
/// </remarks>
[ApiController]
[Authorize(Roles = "admin")]
[Route("retentionpolicy")]
public class RetentionPolicyController : ControllerBase
{
    #region Fields

    private readonly IRetentionPolicyRepository _retentionPolicyRepository;
    private readonly IRetentionPolicyAuditRepository _retentionPolicyAuditRepository;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="RetentionPolicyController"/> class.
    /// </summary>
    /// <param name="retentionPolicyRepository">The retention policy repository.</param>
    /// <param name="retentionPolicyAuditRepository">The retention policy audit repository.</param>
    /// <exception cref="ArgumentNullException">A parameter is <see langword="null"/>.</exception>
    public RetentionPolicyController(
        IRetentionPolicyRepository retentionPolicyRepository,
        IRetentionPolicyAuditRepository retentionPolicyAuditRepository)
    {
        ArgumentNullException.ThrowIfNull(retentionPolicyRepository);
        ArgumentNullException.ThrowIfNull(retentionPolicyAuditRepository);
        _retentionPolicyRepository = retentionPolicyRepository;
        _retentionPolicyAuditRepository = retentionPolicyAuditRepository;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Retrieves all retention policies.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An <see cref="ActionResult{T}"/> containing the list of retention policies.</returns>
    /// <remarks>GET /retentionpolicy</remarks>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RetentionPolicyDto>>> GetAll(CancellationToken cancellationToken)
    {
        IEnumerable<RetentionPolicy> policies = await _retentionPolicyRepository.GetAllAsync(cancellationToken);
        IEnumerable<RetentionPolicyDto> result = policies.Select(RetentionPolicyMapper.ToDto);
        return Ok(result);
    }

    /// <summary>
    /// Updates a retention policy with legal-minimum validation.
    /// </summary>
    /// <param name="dto">The update payload containing category, new retention period, and reason.</param>
    /// <param name="changedByEmployeeId">The employee performing the update.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An <see cref="ActionResult{T}"/> with the updated policy, or 400/404 on validation failure.</returns>
    /// <remarks>PUT /retentionpolicy?changedByEmployeeId={guid}</remarks>
    [HttpPut]
    public async Task<ActionResult<RetentionPolicyDto>> Update(
        [FromBody] UpdateRetentionPolicyDto dto,
        [FromQuery] Guid changedByEmployeeId,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(dto);

        IEnumerable<RetentionPolicy> policies = await _retentionPolicyRepository.GetAllAsync(cancellationToken);
        RetentionPolicy? policy = policies.FirstOrDefault(p => p.Category == dto.Category);
        if (policy is null)
        {
            return NotFound($"No retention policy found for category {dto.Category}.");
        }

        // Enforce legal minimum (e.g., Autorisationsloven §22 for MedicineLogs)
        if (dto.RetentionPeriod < policy.LegalMinimum)
        {
            return BadRequest(
                $"Retention period must be at least the legal minimum of {policy.LegalMinimum.TotalDays} days.");
        }

        TimeSpan previousPeriod = policy.RetentionPeriod;
        policy.RetentionPeriod = dto.RetentionPeriod;
        policy.EffectiveFrom = DateTime.UtcNow;

        await _retentionPolicyRepository.UpdateAsync(policy, cancellationToken);

        RetentionPolicyAudit auditRecord = new()
        {
            Id = Guid.NewGuid(),
            RetentionPolicyId = policy.Id,
            ChangedByEmployeeId = changedByEmployeeId,
            PreviousPeriod = previousPeriod,
            NewPeriod = dto.RetentionPeriod,
            ChangedAt = DateTime.UtcNow,
            Reason = dto.Reason ?? string.Empty
        };
        await _retentionPolicyAuditRepository.CreateAsync(auditRecord, cancellationToken);

        return Ok(RetentionPolicyMapper.ToDto(policy));
    }

    #endregion
}
