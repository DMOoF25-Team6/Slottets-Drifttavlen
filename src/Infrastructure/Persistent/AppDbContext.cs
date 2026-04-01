// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Database.Entities;

namespace Infrastructure.Persistent;

public partial class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext(options)
{
    public required DbSet<Domain.Entities.Resident> Residents { get; set; }
    public required DbSet<Domain.Entities.ResidentNote> ResidentNotes { get; set; }

    public DbSet<Infrastructure.Database.Entities.Resident> ResidentEntities { get; set; }
    public DbSet<Infrastructure.Database.Entities.MedicineAdministration> MedicineAdministrationEntities { get; set; }
    public DbSet<Infrastructure.Database.Entities.PainkillerAdministration> PainkillerAdministrationEntities { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        _ = modelBuilder.ApplyConfiguration(new Configurations.ResidentConfiguration());
        _ = modelBuilder.ApplyConfiguration(new Configurations.ResidentNoteConfiguration());


        // Configure ResidentEntity → MedicineAdministration
        modelBuilder.Entity<Resident>()
            .HasMany(r => r.Medicines)
            .WithOne(m => m.Resident)
            .HasForeignKey(m => m.ResidentId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure ResidentEntity → PainkillerAdministration
        modelBuilder.Entity<Resident>()
            .HasMany(r => r.Painkillers)
            .WithOne(p => p.Resident)
            .HasForeignKey(p => p.ResidentId)
            .OnDelete(DeleteBehavior.Cascade);

        //enum values as string instead of int TrafficLight
        modelBuilder.Entity<Resident>()
            .Property(r => r.TrafficLight)
            .HasConversion<string>();
    }
}
