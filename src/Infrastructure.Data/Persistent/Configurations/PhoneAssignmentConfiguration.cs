// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Persistent.Configurations;

/// <summary>
/// Provides Entity Framework Core configuration for the <see cref="PhoneAssignment"/> entity.
/// </summary>
public class PhoneAssignmentConfiguration : IEntityTypeConfiguration<PhoneAssignment>
{
    public void Configure(EntityTypeBuilder<PhoneAssignment> builder)
    {
        _ = builder.HasKey(pa => pa.Id);

        _ = builder.Property(pa => pa.PhoneNumber)
            .IsRequired();

        _ = builder.Property(pa => pa.ShiftType)
            .IsRequired();

        _ = builder.HasOne<Employee>()
            .WithMany()
            .HasForeignKey(pa => pa.CaregiverId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
