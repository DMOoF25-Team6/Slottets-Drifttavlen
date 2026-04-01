// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Repositories;

using Domain.Entities;

using Infrastructure.Persistent;

using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories;

/// <summary>
/// EF Core implementation of IMedicineRepository.
/// Uses AppDbContext to access the database.
/// </summary>

public class MedicineRepository(IAppDbContext context) : Repository<MedicineRecord>(context), IMedicineRepository
{
    Task<IEnumerable<MedicineRecord>> IMedicineRepository.GetMedicineStatusLast24HoursAsync(Guid residentId, CancellationToken cancellationToken)
    {
        // get medicine records for the resident in the last 24 hours
        return _context.MedicineRecord
            .Where(m => m.ResidentId == residentId && m.Timestamp >= DateTime.UtcNow.AddHours(-24))
            .OrderByDescending(m => m.Timestamp)
            .ToListAsync(cancellationToken)
            .ContinueWith(t => (IEnumerable<MedicineRecord>)t.Result, cancellationToken);
    }
}
