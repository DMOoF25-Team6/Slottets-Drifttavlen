// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs.Audit;

namespace Core.Interfaces.Services;

/// <summary>
/// Defines a contract for logging audit events and reading audit history in the system.
/// </summary>
/// <remarks>
/// Implementations should persist audit logs for entity changes and expose read access for UC-009
/// (View History and Traceability). Write operations are typically performed automatically by the
/// audit interceptor; the <see cref="LogAsync"/> method is retained for explicit logging needs.
/// </remarks>
public interface IAuditService
{
    /// <summary>
    /// Logs an audit event for a change to an entity.
    /// </summary>
    /// <param name="entityName">A name of the entity being changed.</param>
    /// <param name="changeType">A type of change (e.g., Created, Updated, Deleted).</param>
    /// <param name="changedBy">A user or process responsible for the change, or <see langword="null"/> if unknown.</param>
    /// <param name="description">A description of the change.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task LogAsync(string entityName, string changeType, string? changedBy, string description);

    /// <summary>
    /// Retrieves recent audit entries ordered chronologically, most recent first.
    /// </summary>
    /// <param name="limit">The maximum number of entries to return. Use <see langword="null"/> for no limit.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A collection of audit entry DTOs without change details.</returns>
    Task<IEnumerable<AuditEntryDto>> GetRecentAsync(int? limit, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves audit entries filtered by the name of the entity that was changed.
    /// </summary>
    /// <param name="entityName">The entity type name to filter by (e.g. "Resident").</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A collection of audit entry DTOs for the specified entity type.</returns>
    Task<IEnumerable<AuditEntryDto>> GetByEntityNameAsync(string entityName, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a single audit entry including its associated change details.
    /// </summary>
    /// <param name="id">The identifier of the audit entry.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The audit entry DTO with change details, or <see langword="null"/> if not found.</returns>
    Task<AuditEntryDto?> GetWithDetailsAsync(Guid id, CancellationToken cancellationToken);
}
