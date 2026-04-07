// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Core.Interfaces.Services;

public interface IResidentNoteService
{
    Task<bool> AddAsync(Guid residentId, string noteText, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid noteId, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Guid noteId, string newText, CancellationToken cancellationToken);
    Task<IEnumerable<Domain.Entities.ResidentNote>> GetAllByResidentIdAsync(Guid residentId, CancellationToken cancellationToken);

}