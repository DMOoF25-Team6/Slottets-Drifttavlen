// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Repositories;

using Domain.Entities;

using Infrastructure.Data.Persistent;

namespace Infrastructure.Data.Repositories;

/// <summary>
/// Repository implementation for <see cref="SubjectAccessRequest"/> entities (UC-010).
/// </summary>
/// <remarks>
/// Persists the full SAR lifecycle (registered -> generated -> fulfilled) for the
/// GDPR Art. 30 record-of-processing trail. The default <c>Repository&lt;T&gt;</c>
/// CRUD surface is sufficient; specialised queries can be added here if access
/// patterns evolve (e.g. open requests older than the Art. 12(3) one-month deadline).
/// </remarks>
public sealed class SubjectAccessRequestRepository(AppDbContext context)
    : Repository<SubjectAccessRequest>(context), ISubjectAccessRequestRepository
{
}
