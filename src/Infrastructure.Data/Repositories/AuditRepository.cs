// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.Interfaces.Repositories;

using Domain.Entities;

using Infrastructure.Data.Persistent;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

/// <summary>
/// Repository implementation for reading <see cref="AuditEntry"/> entities and their change details.
/// </summary>
/// <remarks>
/// Inherits common CRUD operations from <see cref="Repository{AuditEntry}"/> and adds
/// read-only queries used by UC-009 (View History and Traceability). Audit entries are
/// written automatically by the audit interceptor; this repository exposes filtered
/// queries against them.
/// </remarks>
public class AuditRepository(AppDbContext context) : Repository<AuditEntry>(context), IAuditRepository
{
    #region Methods

    /// <summary>
    /// Retrieves audit entries ordered chronologically (most recent first), bounded by an optional limit.
    /// </summary>
    /// <param name="limit">The maximum number of entries to return. Use <see langword="null"/> for no limit.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A collection of <see cref="AuditEntry"/> records without change details.</returns>
    public async Task<IEnumerable<AuditEntry>> GetRecentAsync(int? limit, CancellationToken cancellationToken)
    {
        IQueryable<AuditEntry> query = _dbSet
            .AsNoTracking()
            .OrderByDescending(e => e.StartTimeUtc);

        if (limit.HasValue && limit.Value > 0)
        {
            query = query.Take(limit.Value);
        }

        return await query.ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Retrieves audit entries that match a given entity name (e.g. "Resident").
    /// </summary>
    /// <param name="entityName">The name of the entity type to filter by.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A collection of <see cref="AuditEntry"/> records for the specified entity type.</returns>
    /// <remarks>
    /// The audit interceptor stores metadata in the format "EntityName - State". This query
    /// uses a prefix match against that format to filter by entity type.
    /// </remarks>
    public async Task<IEnumerable<AuditEntry>> GetByEntityNameAsync(string entityName, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(entityName);

        string prefix = $"{entityName} -";

        return await _dbSet
            .AsNoTracking()
            .Where(e => e.Metadata.StartsWith(prefix))
            .OrderByDescending(e => e.StartTimeUtc)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Retrieves a single audit entry including its associated <see cref="ChangeDetail"/> records.
    /// </summary>
    /// <param name="id">The identifier of the audit entry.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The audit entry with change details, or <see langword="null"/> if not found.</returns>
    public async Task<AuditEntry?> GetWithDetailsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .Include(e => e.ChangeDetails)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    #endregion
}
