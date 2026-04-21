// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Entities;

namespace Core.Interfaces.Repositories;

/// <summary>
/// Defines a contract for a repository that manages <see cref="ResidentNote"/> entities.
/// </summary>
/// <remarks>
/// This interface extends <see cref="IRepository{ResidentNote}"/> to provide
/// data access operations for resident notes within the Clean Architecture structure.
/// Implementations should encapsulate all data persistence logic for <see cref="ResidentNote"/>.
/// </remarks>
public interface IResidentNoteRepository : IRepository<ResidentNote>
{
}
