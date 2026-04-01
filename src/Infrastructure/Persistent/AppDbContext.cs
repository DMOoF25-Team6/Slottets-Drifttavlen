// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Entities;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistent;

public partial class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext(options), IAppDbContext
{
    public required DbSet<Resident> Residents { get; set; }
    public required DbSet<ResidentNote> ResidentNotes { get; set; }

    public DbSet<MedicineRecord> MedicineRecord { get; set; }
    public DbSet<PainkillerRecord> PainkillerAdministrationEntities { get; set; }
    public DbSet<PainkillerRecord> PainkillerRecord { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => base.SaveChangesAsync(cancellationToken);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        _ = modelBuilder.ApplyConfiguration(new Configurations.ResidentConfiguration());
        _ = modelBuilder.ApplyConfiguration(new Configurations.ResidentNoteConfiguration());
    }
}
