// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Anonymization;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
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
[Authorize(Roles = "admin")]
[Route("anonymization")]
public class AnonymizationController : ControllerBase
{
    #region Fields

    private readonly IAnonymizationCandidateRepository _candidateRepository;
    private readonly IResidentRepository _residentRepository;
    private readonly IResidentNoteRepository _residentNoteRepository;
    private readonly IPseudonymizationService _pseudonymizationService;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="AnonymizationController"/> class.
    /// </summary>
    public AnonymizationController(
        IAnonymizationCandidateRepository candidateRepository,
        IResidentRepository residentRepository,
        IResidentNoteRepository residentNoteRepository,
        IPseudonymizationService pseudonymizationService)
    {
        ArgumentNullException.ThrowIfNull(candidateRepository);
        ArgumentNullException.ThrowIfNull(residentRepository);
        ArgumentNullException.ThrowIfNull(residentNoteRepository);
        ArgumentNullException.ThrowIfNull(pseudonymizationService);
        _candidateRepository = candidateRepository;
        _residentRepository = residentRepository;
        _residentNoteRepository = residentNoteRepository;
        _pseudonymizationService = pseudonymizationService;
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

        // Load the resident BEFORE we anonymise so we can produce a stable pseudonym.
        Resident? resident = await _residentRepository.GetByIdAsync(candidate.ResidentId, cancellationToken);
        if (resident is null)
        {
            return NotFound($"No resident found with id {candidate.ResidentId}; anonymisation cannot proceed.");
        }

        // GDPR Art. 17 (right to erasure) combined with Autorisationsloven §22:
        //   - Directly identifying PII on the Resident row is replaced with a stable
        //     pseudonym so existing FK relationships (MedicineRecord, PainkillerRecord)
        //     remain intact for the 10-year medical journal retention.
        //   - Free-text ResidentNote bodies are pseudonymised (Art. 4(5)) because
        //     they may contain identifying narrative data; the timestamps are kept
        //     for journal integrity.
        //   - MedicineRecord and PainkillerRecord are NOT mutated: they reference
        //     the pseudonymised Resident row and so are themselves pseudonymised by
        //     transitivity, while preserving the §22-mandated drug-administration log.
        string pseudonym = _pseudonymizationService.Pseudonymize(resident.Id.ToString());
        string shortPseudonym = _pseudonymizationService.PseudonymizeShort(resident.Id.ToString());

        resident.FirstName = $"ANON-{pseudonym[..8]}";
        resident.LastName = "ANONYMISED";
        resident.Initials = shortPseudonym;
        await _residentRepository.UpdateAsync(resident, cancellationToken);

        IEnumerable<ResidentNote> notes = await _residentNoteRepository.GetAllAsync(cancellationToken);
        foreach (ResidentNote note in notes.Where(n => n.ResidentId == resident.Id))
        {
            note.Note = "[ANONYMISED]";
            await _residentNoteRepository.UpdateAsync(note, cancellationToken);
        }

        candidate.Status = AnonymizationStatus.Completed;
        await _candidateRepository.UpdateAsync(candidate, cancellationToken);

        // AuditInterceptor (UC-009) captures the Resident, ResidentNote, and
        // AnonymizationCandidate row updates above on SaveChanges, providing the
        // GDPR Art. 30 record-of-processing trail for this anonymisation event.
        AnonymizationResultDto result = new()
        {
            CandidateId = candidateId,
            CompletedAt = DateTime.UtcNow,
            Outcome = $"Resident pseudonymised (pseudonym {shortPseudonym}); medical journal preserved per Autorisationsloven §22"
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
