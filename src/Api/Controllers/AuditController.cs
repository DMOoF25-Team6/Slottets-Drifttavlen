// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs.Audit;
using Core.Interfaces.Repositories;
using Core.Mappers;

using Domain.Entities;

using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// API controller for reading audit history (UC-009 View History and Traceability).
/// </summary>
/// <remarks>
/// Exposes read-only endpoints over <see cref="IAuditRepository"/>. Audit entries are
/// written automatically by the audit interceptor on <c>SaveChanges</c>; this controller
/// does not expose any write operations.
/// </remarks>
/// <example>
/// <code language="csharp">
/// // Example usage in HTTP client:
/// // GET /audit/recent?limit=50
/// // GET /audit/entity/Resident
/// // GET /audit/{id}
/// </code>
/// </example>
[ApiController]
[Route("[controller]")]
public class AuditController(IAuditRepository auditRepository) : ControllerBase
{
    #region Fields

    private readonly IAuditRepository _auditRepository = auditRepository
        ?? throw new ArgumentNullException(nameof(auditRepository));

    #endregion

    #region Methods

    /// <summary>
    /// Retrieves recent audit entries ordered chronologically (most recent first).
    /// </summary>
    /// <param name="limit">The maximum number of entries to return. Use <see langword="null"/> for no limit.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A collection of <see cref="AuditEntryDto"/> without change details.</returns>
    /// <remarks>GET /audit/recent?limit=N</remarks>
    [HttpGet("recent")]
    public async Task<IActionResult> GetRecent([FromQuery] int? limit, CancellationToken cancellationToken)
    {
        IEnumerable<AuditEntry> entries = await _auditRepository.GetRecentAsync(limit, cancellationToken);
        IEnumerable<AuditEntryDto> dtos = AuditMapper.ToDtos(entries);
        return Ok(dtos);
    }

    /// <summary>
    /// Retrieves audit entries filtered by the entity type that was changed.
    /// </summary>
    /// <param name="entityName">The entity type name to filter by (e.g. "Resident").</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A collection of <see cref="AuditEntryDto"/> for the specified entity type.</returns>
    /// <remarks>GET /audit/entity/{entityName}</remarks>
    [HttpGet("entity/{entityName}")]
    public async Task<IActionResult> GetByEntityName(string entityName, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(entityName))
        {
            return BadRequest("Entity name must be provided.");
        }

        IEnumerable<AuditEntry> entries = await _auditRepository.GetByEntityNameAsync(entityName, cancellationToken);
        IEnumerable<AuditEntryDto> dtos = AuditMapper.ToDtos(entries);
        return Ok(dtos);
    }

    /// <summary>
    /// Retrieves a single audit entry including its associated change details.
    /// </summary>
    /// <param name="id">The identifier of the audit entry.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>The <see cref="AuditEntryDto"/> with change details, or <see cref="NotFoundResult"/> if not found.</returns>
    /// <remarks>GET /audit/{id}</remarks>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetWithDetails(Guid id, CancellationToken cancellationToken)
    {
        AuditEntry? entry = await _auditRepository.GetWithDetailsAsync(id, cancellationToken);

        if (entry is null)
        {
            return NotFound();
        }

        AuditEntryDto dto = AuditMapper.ToDto(entry);
        return Ok(dto);
    }

    #endregion
}
