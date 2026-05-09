// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Domain.Entities;

namespace Core.Interfaces.Repositories;

/// <summary>
/// Defines a repository interface for reading audit entries and their change details.
/// </summary>
/// <remarks>
/// Used by UC-009 (View History and Traceability). Audit entries are written automatically
/// by the <c>AuditInterceptor</c>; this repository exposes read-only queries against them.
/// </remarks>
public interface IAuditRepository : IRepository<AuditEntry>
{
    /// <summary>
    /// Retrieves all audit entries ordered chronologically (most recent first), bounded by an optional limit.
    /// </summary>
    /// <param name="limit">The maximum number of entries to return. Use <see langword="null"/> for no limit.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A collection of audit entries.</returns>
    Task<IEnumerable<AuditEntry>> GetRecentAsync(int? limit, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves audit entries that match a given entity name (e.g. "Resident").
    /// </summary>
    /// <param name="entityName">The name of the entity type to filter by.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A collection of audit entries for the specified entity type.</returns>
    Task<IEnumerable<AuditEntry>> GetByEntityNameAsync(string entityName, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a single audit entry including its associated <see cref="ChangeDetail"/> records.
    /// </summary>
    /// <param name="id">The identifier of the audit entry.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The audit entry with change details, or <see langword="null"/> if not found.</returns>
    Task<AuditEntry?> GetWithDetailsAsync(Guid id, CancellationToken cancellationToken);
}
