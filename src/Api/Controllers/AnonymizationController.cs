// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Anonymization;
using Core.Interfaces.Repositories;
using Core.Mappers;

using Domain.Entities;
using Domain.Enums;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// API controller for managing GDPR anonymization candidates (UC-010).
/// </summary>
/// <remarks>
/// Restricted to Admin role. MedicineLogs are pseudonymized rather than fully anonymized
/// per Autorisationsloven §22 — preserved by RetentionPolicy enforcement at the repository level.
/// </remarks>
[ApiController]
[Authorize(Roles = "Admin")]
[Route("anonymization")]
public class AnonymizationController : ControllerBase
{
    #region Fields

    private readonly IAnonymizationCandidateRepository _candidateRepository;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="AnonymizationController"/> class.
    /// </summary>
    /// <param name="candidateRepository">The anonymization candidate repository.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="candidateRepository"/> parameter is <see langword="null"/>.</exception>
    public AnonymizationController(IAnonymizationCandidateRepository candidateRepository)
    {
        ArgumentNullException.ThrowIfNull(candidateRepository);
        _candidateRepository = candidateRepository;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Retrieves all anonymization candidates pending Admin review.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A collection of anonymization candidates.</returns>
    /// <remarks>GET /anonymization/candidates</remarks>
    [HttpGet("candidates")]
    public async Task<ActionResult<IEnumerable<AnonymizationCandidateDto>>> GetCandidates(CancellationToken cancellationToken)
    {
        IEnumerable<AnonymizationCandidate> candidates = await _candidateRepository.GetAllAsync(cancellationToken);
        IEnumerable<AnonymizationCandidateDto> result = candidates.Select(AnonymizationCandidateMapper.ToDto);
        return Ok(result);
    }

    /// <summary>
    /// Approves anonymization of a candidate.
    /// </summary>
    /// <param name="candidateId">The unique identifier of the candidate to approve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The anonymization result with timestamp and outcome.</returns>
    /// <remarks>POST /anonymization/{candidateId}/approve</remarks>
    [HttpPost("{candidateId:guid}/approve")]
    public async Task<ActionResult<AnonymizationResultDto>> Approve(Guid candidateId, CancellationToken cancellationToken)
    {
        AnonymizationCandidate? candidate = await _candidateRepository.GetByIdAsync(candidateId, cancellationToken);
        if (candidate is null)
        {
            return NotFound($"No anonymization candidate found with id {candidateId}.");
        }

        if (candidate.Status != AnonymizationStatus.Pending)
        {
            return Conflict($"Candidate is in status {candidate.Status} and cannot be approved.");
        }

        candidate.Status = AnonymizationStatus.Approved;
        await _candidateRepository.UpdateAsync(candidate, cancellationToken);

        // STUB: actual anonymization/pseudonymization of resident data happens in
        // a follow-up branch. AuditInterceptor (UC-009) captures the status change.
        AnonymizationResultDto result = new()
        {
            CandidateId = candidateId,
            CompletedAt = DateTime.UtcNow,
            Outcome = "Approved (stub — anonymization scheduled)"
        };
        return Ok(result);
    }

    /// <summary>
    /// Rejects an anonymization candidate with a documented reason.
    /// </summary>
    /// <param name="candidateId">The unique identifier of the candidate to reject.</param>
    /// <param name="reason">A short explanation for the rejection.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns><see langword="true"/> if the candidate was rejected; otherwise an error response.</returns>
    /// <remarks>POST /anonymization/{candidateId}/reject?reason=...</remarks>
    [HttpPost("{candidateId:guid}/reject")]
    public async Task<ActionResult<bool>> Reject(
        Guid candidateId,
        [FromQuery] string reason,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(reason))
        {
            return BadRequest("Rejection reason is required.");
        }

        AnonymizationCandidate? candidate = await _candidateRepository.GetByIdAsync(candidateId, cancellationToken);
        if (candidate is null)
        {
            return NotFound($"No anonymization candidate found with id {candidateId}.");
        }

        candidate.Status = AnonymizationStatus.Rejected;
        candidate.Reason = reason;
        await _candidateRepository.UpdateAsync(candidate, cancellationToken);

        return Ok(true);
    }

    #endregion
}
