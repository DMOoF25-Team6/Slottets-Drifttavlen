// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Repositories;

using Domain.Entities;

using Infrastructure.Data.Persistent;

namespace Infrastructure.Data.Repositories;

/// <summary>
/// Repository implementation for <see cref="RetentionPolicy"/> entities (UC-010).
/// </summary>
/// <remarks>
/// Provides EF Core data access for retention policies. Inherits generic CRUD
/// operations from <see cref="Repository{RetentionPolicy}"/>.
/// </remarks>
public class RetentionPolicyRepository(AppDbContext context) : Repository<RetentionPolicy>(context), IRetentionPolicyRepository
{
}
