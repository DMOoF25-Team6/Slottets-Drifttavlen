// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs.Audit;

namespace Core.Interfaces.Managers;

/// <summary>
/// Provides infrastructure-level operations for retrieving audit history.
/// </summary>
/// <remarks>
/// Implemented in the Infrastructure layer; Core consumes this contract via
/// <see cref="Core.Services.AuditService"/> to keep data access concerns out of Core.
/// </remarks>
public interface IAuditManager
{
    /// <summary>
    /// Gets recent audit entries ordered chronologically (most recent first).
    /// </summary>
    /// <param name="limit">The maximum number of entries to return. Use <see langword="null"/> for no limit.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A collection of <see cref="AuditEntryDto"/> without change details.</returns>
    Task<IEnumerable<AuditEntryDto>> GetRecentAsync(int? limit, CancellationToken cancellationToken);

    /// <summary>
    /// Gets audit entries filtered by the entity type that was changed.
    /// </summary>
    /// <param name="entityName">The entity type name to filter by (e.g. "Resident").</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A collection of <see cref="AuditEntryDto"/> for the specified entity type.</returns>
    Task<IEnumerable<AuditEntryDto>> GetByEntityNameAsync(string entityName, CancellationToken cancellationToken);

    /// <summary>
    /// Gets a single audit entry including its associated change details.
    /// </summary>
    /// <param name="id">The identifier of the audit entry.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The <see cref="AuditEntryDto"/> with change details, or <see langword="null"/> if not found.</returns>
    Task<AuditEntryDto?> GetWithDetailsAsync(Guid id, CancellationToken cancellationToken);
}
