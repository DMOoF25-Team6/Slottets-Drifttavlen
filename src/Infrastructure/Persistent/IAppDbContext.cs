// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistent;

/// <summary>
/// Abstraction for AppDbContext to enable mocking and testing.
/// </summary>
public interface IAppDbContext
{
    DbSet<Resident> Residents { get; set; }
    DbSet<ResidentNote> ResidentNotes { get; set; }
    DbSet<MedicineRecord> MedicineRecord { get; set; }
    DbSet<PainkillerRecord> PainkillerRecord { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
