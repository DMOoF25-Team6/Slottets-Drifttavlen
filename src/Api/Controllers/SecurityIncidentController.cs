// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Security;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Mappers;

using Domain.Entities;
using Domain.Enums;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// API controller for managing security incidents (UC-010).
/// </summary>
/// <remarks>
/// Restricted to Admin role. Escalation with isBreach=true triggers GDPR Art. 33
/// notification to the Data Protection Officer via <see cref="IArt33NotificationService"/>.
/// </remarks>
[ApiController]
[Authorize(Roles = "admin")]
[Route("securityincident")]
public class SecurityIncidentController : ControllerBase
{
    #region Fields

    private readonly ISecurityIncidentRepository _incidentRepository;
    private readonly IArt33NotificationService _art33NotificationService;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="SecurityIncidentController"/> class.
    /// </summary>
    /// <param name="incidentRepository">The security incident repository.</param>
    /// <param name="art33NotificationService">The Art. 33 breach notification service.</param>
    /// <exception cref="ArgumentNullException">Any parameter is <see langword="null"/>.</exception>
    public SecurityIncidentController(
        ISecurityIncidentRepository incidentRepository,
        IArt33NotificationService art33NotificationService)
    {
        ArgumentNullException.ThrowIfNull(incidentRepository);
        ArgumentNullException.ThrowIfNull(art33NotificationService);
        _incidentRepository = incidentRepository;
        _art33NotificationService = art33NotificationService;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Retrieves all security incidents.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A collection of security incidents.</returns>
    /// <remarks>GET /securityincident</remarks>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SecurityIncidentDto>>> GetAll(CancellationToken cancellationToken)
    {
        IEnumerable<SecurityIncident> incidents = await _incidentRepository.GetAllAsync(cancellationToken);
        IEnumerable<SecurityIncidentDto> result = incidents.Select(SecurityIncidentMapper.ToDto);
        return Ok(result);
    }

    /// <summary>
    /// Escalates a security incident, optionally triggering GDPR Art. 33 breach notification.
    /// </summary>
    /// <param name="incidentId">The unique identifier of the incident to escalate.</param>
    /// <param name="dto">The escalation payload including whether this is a personal data breach.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The updated incident.</returns>
    /// <remarks>POST /securityincident/{incidentId}/escalate</remarks>
    [HttpPost("{incidentId:guid}/escalate")]
    public async Task<ActionResult<SecurityIncidentDto>> Escalate(
        Guid incidentId,
        [FromBody] EscalateIncidentDto dto,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(dto);

        SecurityIncident? incident = await _incidentRepository.GetByIdAsync(incidentId, cancellationToken);
        if (incident is null)
        {
            return NotFound($"No security incident found with id {incidentId}.");
        }

        if (incident.Status == IncidentStatus.Closed || incident.Status == IncidentStatus.BreachNotified)
        {
            return Conflict($"Incident is in status {incident.Status} and cannot be escalated.");
        }

        if (dto.IsBreach)
        {
            // GDPR Art. 33 — must notify DPO within 72 hours of becoming aware of breach
            // DPO email is configured server-side; using a placeholder for stub implementation
            string dpoEmail = "dpo@slottet.example.com";
            bool notified = await _art33NotificationService.SendNotificationAsync(incidentId, dpoEmail, cancellationToken);
            if (!notified)
            {
                return StatusCode(500, "Failed to send Art. 33 breach notification.");
            }
            incident.Status = IncidentStatus.BreachNotified;
        }
        else
        {
            incident.Status = IncidentStatus.Escalated;
        }

        await _incidentRepository.UpdateAsync(incident, cancellationToken);
        return Ok(SecurityIncidentMapper.ToDto(incident));
    }

    /// <summary>
    /// Appends investigation notes to a security incident.
    /// </summary>
    /// <param name="incidentId">The unique identifier of the incident.</param>
    /// <param name="dto">The notes payload.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The updated incident.</returns>
    /// <remarks>POST /securityincident/{incidentId}/notes</remarks>
    [HttpPost("{incidentId:guid}/notes")]
    public async Task<ActionResult<SecurityIncidentDto>> AddNotes(
        Guid incidentId,
        [FromBody] AddInvestigationNotesDto dto,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(dto);

        if (string.IsNullOrWhiteSpace(dto.Notes))
        {
            return BadRequest("Notes content is required.");
        }

        SecurityIncident? incident = await _incidentRepository.GetByIdAsync(incidentId, cancellationToken);
        if (incident is null)
        {
            return NotFound($"No security incident found with id {incidentId}.");
        }

        // Append rather than replace — investigation builds over time
        string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm 'UTC'");
        string entry = $"[{timestamp}] {dto.Notes}";
        incident.InvestigationNotes = string.IsNullOrWhiteSpace(incident.InvestigationNotes)
            ? entry
            : $"{incident.InvestigationNotes}\n{entry}";

        if (incident.Status == IncidentStatus.Open)
        {
            incident.Status = IncidentStatus.UnderInvestigation;
        }

        await _incidentRepository.UpdateAsync(incident, cancellationToken);
        return Ok(SecurityIncidentMapper.ToDto(incident));
    }

    /// <summary>
    /// Closes a security incident.
    /// </summary>
    /// <param name="incidentId">The unique identifier of the incident.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The closed incident.</returns>
    /// <remarks>POST /securityincident/{incidentId}/close</remarks>
    [HttpPost("{incidentId:guid}/close")]
    public async Task<ActionResult<SecurityIncidentDto>> Close(Guid incidentId, CancellationToken cancellationToken)
    {
        SecurityIncident? incident = await _incidentRepository.GetByIdAsync(incidentId, cancellationToken);
        if (incident is null)
        {
            return NotFound($"No security incident found with id {incidentId}.");
        }

        if (incident.Status == IncidentStatus.Closed)
        {
            return Conflict("Incident is already closed.");
        }

        incident.Status = IncidentStatus.Closed;
        await _incidentRepository.UpdateAsync(incident, cancellationToken);
        return Ok(SecurityIncidentMapper.ToDto(incident));
    }

    #endregion
}
