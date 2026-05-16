// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Repositories;

using Domain.Entities;

using Infrastructure.Data.Persistent;

namespace Infrastructure.Data.Repositories;

/// <summary>
/// Repository implementation for <see cref="RetentionPolicyAudit"/> entities (UC-010).
/// </summary>
/// <remarks>
/// Persists immutable audit records each time a retention policy is changed,
/// preserving full traceability for Datatilsynet inspections.
/// </remarks>
public class RetentionPolicyAuditRepository(AppDbContext context)
    : Repository<RetentionPolicyAudit>(context), IRetentionPolicyAuditRepository
{
}
