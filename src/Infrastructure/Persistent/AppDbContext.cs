// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistent;

public partial class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext(options)
{
    public required DbSet<Domain.Entities.Resident> Residents { get; set; }
    public required DbSet<Domain.Entities.ResidentNote> ResidentNotes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        _ = modelBuilder.ApplyConfiguration(new Configurations.ResidentConfiguration());
        _ = modelBuilder.ApplyConfiguration(new Configurations.ResidentNoteConfiguration());
    }
}
