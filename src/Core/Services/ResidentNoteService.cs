// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.


using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

using Domain.Entities;

namespace Core.Services;

public class ResidentNoteService : IResidentNoteService
{
    
    private readonly IResidentNoteRepository _residentNoteRepository;


    public ResidentNoteService(IResidentNoteRepository residentNoteRepository)
    {
        // Constructor logic, if needed
        _residentNoteRepository = residentNoteRepository;
    }

    public Task<ResidentNote> AddAsync(Guid residentId, string noteText, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid residentId, Guid noteId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ResidentNote>> GetAllByResidentIdAsync(Guid residentId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guid residentId, Guid noteId, string newText, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
