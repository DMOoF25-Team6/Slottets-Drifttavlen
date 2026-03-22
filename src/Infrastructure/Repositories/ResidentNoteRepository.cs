// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Repositories;

using Domain.Entities;

using Infrastructure.Persistents;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

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



public class ResidentNoteRepository : Repository<ResidentNote>, IResidentNoteRepository
{

    private readonly AppDbContext _context;

    //The constructor receives AppDbContext through dependency injection
    public ResidentNoteRepository(AppDbContext context) 
    {
        _context = context;
    }


    public async Task<IEnumerable<ResidentNote>> GetAllbyResidentAsync()
    {
        // get all resident notes with their associated resident. 
        return await _context.ResidentNotes.Include (n=> n.Resident).ToListAsync();

    }

}


