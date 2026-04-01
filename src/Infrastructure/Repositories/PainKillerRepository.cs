// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Repositories;

using Domain.Entities;

using Infrastructure.Persistent;

using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories;

public class PainKillerRepository(IAppDbContext context) : Repository<PainkillerRecord>(context), IPainkillerRepository
{
    public Task<IEnumerable<PainkillerRecord>> GetPainkillerStatusLast24HoursAsync(Guid residentId, CancellationToken cancellationToken = default)
    {
        return _context.PainkillerRecord
            .Where(m => m.ResidentId == residentId && m.NextAllowedTime >= DateTime.UtcNow.AddHours(-24))
            .OrderByDescending(m => m.NextAllowedTime)
            .ToListAsync(cancellationToken)
            .ContinueWith(t => (IEnumerable<PainkillerRecord>)t.Result, cancellationToken);
    }
}
