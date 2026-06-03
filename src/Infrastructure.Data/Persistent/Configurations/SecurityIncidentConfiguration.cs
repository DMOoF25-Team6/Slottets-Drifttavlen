// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Persistent.Configurations;

/// <summary>
/// EF Core configuration for the <see cref="SecurityIncident"/> entity (UC-010).
/// </summary>
public sealed class SecurityIncidentConfiguration : IEntityTypeConfiguration<SecurityIncident>
{
    public void Configure(EntityTypeBuilder<SecurityIncident> builder)
    {
        _ = builder.Property(i => i.Type)
            .IsRequired()
            .HasMaxLength(200);

        _ = builder.Property(i => i.InvestigationNotes)
            .IsRequired()
            .HasMaxLength(2000);

        _ = builder.HasOne<Employee>()
            .WithMany()
            .HasForeignKey(i => i.ReportedByEmployeeId)
            .OnDelete(DeleteBehavior.SetNull);

        _ = builder.HasOne<Employee>()
            .WithMany()
            .HasForeignKey(i => i.ResolvedByEmployeeId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
