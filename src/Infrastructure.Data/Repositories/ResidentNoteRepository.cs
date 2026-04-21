// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Repositories;

using Domain.Entities;

using Infrastructure.Data.Persistent;

namespace Infrastructure.Data.Repositories;

/// <summary>
/// Repository implementation for managing <see cref="ResidentNote"/> entities.
/// </summary>
/// <remarks>
/// This class provides data access logic for resident notes, following the repository pattern.
/// It inherits from <see cref="Repository{ResidentNote}"/> to leverage common CRUD operations,
/// and implements <see cref="IResidentNoteRepository"/> for domain-specific queries or behaviors.
/// </remarks>
/// <example>
/// <code>
/// var repository = new ResidentNotesRepository();
/// var notes = repository.GetAll();
/// </code>
/// </example>
/// 
public class ResidentNoteRepository(AppDbContext context) : Repository<ResidentNote>(context), IResidentNoteRepository
{
}