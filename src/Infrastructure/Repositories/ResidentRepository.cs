// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Repositories;

using Domain.Entities;

namespace Infrastructure.Repositories;

/// <summary>
/// Repository implementation for <see cref="Resident"/> entities.
/// </summary>
/// <remarks>
/// This class provides data access logic for <see cref="Resident"/> objects,
/// following the repository pattern as part of the Clean Architecture approach.
/// It inherits generic CRUD operations from <see cref="Repository{Resident}"/>
/// and implements <see cref="IResidentRepository"/> for domain-specific queries.
/// </remarks>
public class ResidentRepository : Repository<Resident>, IResidentRepository
{
}
