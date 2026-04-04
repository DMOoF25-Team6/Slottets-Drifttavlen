// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Persistent;

public partial class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<Resident> Residents { get; set; }
    public DbSet<ResidentNote> ResidentNotes { get; set; }

    public DbSet<MedicineRecord> MedicineRecord { get; set; }
    public DbSet<PainkillerRecord> PainkillerRecord { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => base.SaveChangesAsync(cancellationToken);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        _ = modelBuilder.ApplyConfiguration(new Configurations.ResidentConfiguration());
        _ = modelBuilder.ApplyConfiguration(new Configurations.ResidentNoteConfiguration());
    }
}
