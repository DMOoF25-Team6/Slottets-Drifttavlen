// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs.Audit;
using Core.Interfaces.Managers;
using Core.Interfaces.Services;

namespace Core.Services;

/// <summary>
/// Provides business logic operations for reading audit history and explicit audit logging.
/// </summary>
/// <remarks>
/// Delegates all data access to <see cref="IAuditManager"/> in Infrastructure,
/// ensuring Core never depends on Infrastructure or data access concerns directly —
/// following Clean Architecture and the Dependency Inversion Principle.
/// Audit entries are normally written automatically by the audit interceptor;
/// <see cref="LogAsync"/> is retained for explicit logging needs.
/// </remarks>
public class AuditService(IAuditManager auditManager) : IAuditService
{
    #region Fields

    private readonly IAuditManager _auditManager = auditManager;

    #endregion

    #region Read methods

    /// <summary>
    /// Gets recent audit entries ordered chronologically (most recent first).
    /// </summary>
    /// <param name="limit">The maximum number of entries to return. Use <see langword="null"/> for no limit.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A collection of <see cref="AuditEntryDto"/> without change details.</returns>
    public async Task<IEnumerable<AuditEntryDto>> GetRecentAsync(int? limit, CancellationToken cancellationToken)
    {
        return await _auditManager.GetRecentAsync(limit, cancellationToken);
    }

    /// <summary>
    /// Gets audit entries filtered by the entity type that was changed.
    /// </summary>
    /// <param name="entityName">The entity type name to filter by (e.g. "Resident").</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A collection of <see cref="AuditEntryDto"/> for the specified entity type.</returns>
    public async Task<IEnumerable<AuditEntryDto>> GetByEntityNameAsync(string entityName, CancellationToken cancellationToken)
    {
        return await _auditManager.GetByEntityNameAsync(entityName, cancellationToken);
    }

    /// <summary>
    /// Gets a single audit entry including its associated change details.
    /// </summary>
    /// <param name="id">The identifier of the audit entry.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The <see cref="AuditEntryDto"/> with change details, or <see langword="null"/> if not found.</returns>
    public async Task<AuditEntryDto?> GetWithDetailsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _auditManager.GetWithDetailsAsync(id, cancellationToken);
    }

    #endregion

    #region Write methods

    /// <summary>
    /// Logs an audit event for a change to an entity.
    /// </summary>
    /// <param name="entityName">The name of the entity being changed.</param>
    /// <param name="changeType">The type of change (e.g., Created, Updated, Deleted).</param>
    /// <param name="changedBy">The user or process responsible for the change, or <see langword="null"/> if unknown.</param>
    /// <param name="description">A description of the change.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <remarks>
    /// Currently a no-op kept for backwards compatibility with the original <see cref="IAuditService"/> contract.
    /// Audit entries are written automatically by the audit interceptor on <c>SaveChanges</c>;
    /// explicit logging is reserved for future cross-cutting concerns.
    /// </remarks>
    public Task LogAsync(string entityName, string changeType, string? changedBy, string description)
    {
        return Task.CompletedTask;
    }

    #endregion
}
