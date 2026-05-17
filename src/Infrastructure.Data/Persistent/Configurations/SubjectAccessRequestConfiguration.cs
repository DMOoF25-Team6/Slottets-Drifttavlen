// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Persistent.Configurations;

/// <summary>
/// EF Core configuration for the <see cref="SubjectAccessRequest"/> entity (UC-010, GDPR Art. 15).
/// </summary>
public sealed class SubjectAccessRequestConfiguration : IEntityTypeConfiguration<SubjectAccessRequest>
{
    public void Configure(EntityTypeBuilder<SubjectAccessRequest> builder)
    {
        // Lookup by resident is the most common access pattern for an Admin reviewing
        // a subject's past SARs (e.g. verifying that an Art. 15 deadline was met).
        _ = builder.HasIndex(s => s.ResidentId);

        // Reporting on open requests (FulfilledAt IS NULL) needs to scan only the
        // unfulfilled subset; an index on the partial column keeps that query fast.
        _ = builder.HasIndex(s => s.FulfilledAt);

        _ = builder.HasOne(s => s.Resident)
            .WithMany()
            .HasForeignKey(s => s.ResidentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
