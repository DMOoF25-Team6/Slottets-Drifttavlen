// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Entities;

namespace Core.Interfaces.Repositories;

/// <summary>
/// Defines a contract for repository operations specific to <see cref="Resident"/> entities.
/// </summary>
/// <remarks>
/// This interface extends <see cref="IRepository{T}"/> to provide a strongly-typed repository for resident domain objects.
/// Implementations should encapsulate all data access logic for residents, supporting Clean Architecture separation of concerns.
/// </remarks>
public interface IResidentRepository : IRepository<Resident>
{
}
