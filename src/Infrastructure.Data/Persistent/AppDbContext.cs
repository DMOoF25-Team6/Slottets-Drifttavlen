// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Persistent;

public partial class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
{
    // No need to redeclare DbSet<User> Users; it is inherited from IdentityDbContext<User, ...>
    public DbSet<Resident> Residents { get; set; }
    public DbSet<ResidentNote> ResidentNotes { get; set; }
    public DbSet<MedicineRecord> MedicineRecord { get; set; }
    public DbSet<PainkillerRecord> PainkillerRecord { get; set; }
    public DbSet<PhoneAssignment> PhoneAssignments { get; set; }

    // Identity-related DbSet for refresh tokens
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public DbSet<AuditLog> AuditLogs { get; set; }

    // Why do we have these views as DbSet? They are not entities!
    public DbSet<MedicineStatusView> MedicineStatusView { get; set; }
    public DbSet<PainkillerStatusView> PainkillerStatusView { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => base.SaveChangesAsync(cancellationToken);

    /// <summary>
    /// Configures the model and seeds example Identity roles and claims.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        _ = modelBuilder.ApplyConfiguration(new Configurations.ResidentConfiguration());
        _ = modelBuilder.ApplyConfiguration(new Configurations.ResidentNoteConfiguration());
        _ = modelBuilder.ApplyConfiguration(new Configurations.MedicineRecordConfiguration());
        _ = modelBuilder.ApplyConfiguration(new Configurations.PainkillerRecordConfiguration());

        _ = modelBuilder.Entity<MedicineStatusView>()
            .HasNoKey()
            .ToView("medicinestatusview");

        _ = modelBuilder.Entity<PainkillerStatusView>()
            .HasNoKey()
            .ToView("painkillerstatusview");

        // Seed Identity roles and claims
        IdentityRoleClaimSeed.Seed(modelBuilder);
    }
}
