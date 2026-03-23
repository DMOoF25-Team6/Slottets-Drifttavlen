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
        _residentNoteRepository = residentNoteRepository;
    }

    public async Task<IEnumerable<ResidentNote>> GetAllByResidentIdAsync(Guid residentId, CancellationToken cancellationToken = default)
    {
        var allNotes = await _residentNoteRepository.GetAllAsync(cancellationToken);
        return allNotes.Where(n => n.ResidentId == residentId);
    }

    public async Task<bool> AddAsync(Guid residentId, string noteText, CancellationToken cancellationToken = default)
    {
        var note = new ResidentNote
        {
            Id = Guid.NewGuid(),
            ResidentId = residentId,
            Note = noteText
        };
        await _residentNoteRepository.AddAsync(note, cancellationToken);
        return true;
    }

    public async Task<bool> UpdateAsync(Guid residentId, Guid noteId, string newText, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(Guid residentId, Guid noteId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}