// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.


using Domain.Entities;

namespace Core.Interfaces.Services;

public interface IResidentNoteService
{
    Task<IEnumerable<ResidentNote>> GetAllByResidentIdAsync(Guid residentId, CancellationToken cancellationToken = default);
    Task<ResidentNote> AddAsync (Guid residentId, string noteText, CancellationToken cancellationToken = default);
    Task UpdateAsync (Guid residentId, Guid noteId, string newText, CancellationToken cancellationToken = default);
    Task DeleteAsync (Guid residentId, Guid noteId, CancellationToken cancellationToken = default);

}
