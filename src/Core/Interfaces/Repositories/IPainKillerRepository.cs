// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Entities;

namespace Core.Interfaces.Repositories;

public interface IPainkillerRepository : IRepository<PainkillerRecord>
{
    Task<IEnumerable<PainkillerRecord>> GetPainkillerStatusLast24HoursAsync(Guid residentId, CancellationToken cancellationToken = default);
}
