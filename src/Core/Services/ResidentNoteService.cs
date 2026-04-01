// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

using Domain.Entities;

namespace Core.Services;

public class ResidentNoteService(IResidentNoteRepository residentNoteRepository) : IResidentNoteService
{
    private readonly IResidentNoteRepository _residentNoteRepository = residentNoteRepository;

    public async Task<IEnumerable<ResidentNote>> GetAllByResidentIdAsync(Guid residentId, CancellationToken cancellationToken = default)
    {
        IEnumerable<ResidentNote> allNotes = await _residentNoteRepository.GetAllAsync(cancellationToken);
        return allNotes.Where(n => n.ResidentId == residentId);
    }

    public async Task<bool> AddAsync(Guid residentId, string noteText, CancellationToken cancellationToken = default)
    {
        ResidentNote note = new()
        {
            Id = Guid.NewGuid(),
            ResidentId = residentId,
            Note = noteText,
            EditedAt = DateTime.UtcNow
        };
        _ = await _residentNoteRepository.AddAsync(note, cancellationToken);
        return true;
    }

    public async Task<bool> UpdateAsync(Guid residentId, Guid noteId, string newText, CancellationToken cancellationToken = default)
    {
        // Step 1 - Fetch the note
        ResidentNote? note = await _residentNoteRepository.GetByIdAsync(noteId, cancellationToken);

        // Step 2 - Check if note exists
        if (note is null)
        {
            return false;
        }

        // Step 3 - Update fields
        note.Note = newText;
        note.EditedAt = DateTime.UtcNow;

        // Step 4 - Save via repository
        await _residentNoteRepository.UpdateAsync(note, cancellationToken);

        // Step 5 - Return success
        return true;
    }

    public async Task<bool> DeleteAsync(Guid residentId, Guid noteId, CancellationToken cancellationToken = default)
    {
        // Step 1 - Fetch the note
        ResidentNote? note = await _residentNoteRepository.GetByIdAsync(noteId, cancellationToken);

        // Step 2 - Check if note exists
        if (note is null)
        {
            return false;
        }

        // Step 3 - Delete via repository
        await _residentNoteRepository.DeleteAsync(note, cancellationToken);

        // Step 4 - Return success
        return true;
    }
}