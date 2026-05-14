// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Entities;
using Domain.Enums;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Persistent.Configurations;

/// <summary>
/// Provides configuration for the <see cref="RetentionPolicy"/> entity using Entity Framework Core (UC-010).
/// Includes seed data with legal minimum retention periods per Autorisationsloven §22.
/// </summary>
public class RetentionPolicyConfiguration : IEntityTypeConfiguration<RetentionPolicy>
{
    public void Configure(EntityTypeBuilder<RetentionPolicy> builder)
    {
        // Unique constraint: only one active policy per data category
        _ = builder.HasIndex(p => p.Category).IsUnique();

        // Relationships
        _ = builder.HasMany(p => p.AuditHistory)
            .WithOne(a => a.RetentionPolicy)
            .HasForeignKey(a => a.RetentionPolicyId)
            .OnDelete(DeleteBehavior.Cascade);

        _ = builder.HasMany(p => p.Candidates)
            .WithOne(c => c.RetentionPolicy)
            .HasForeignKey(c => c.RetentionPolicyId)
            .OnDelete(DeleteBehavior.Restrict);

        SeedingData(builder);
    }

    /// <summary>
    /// Seeds default retention policies with legal minimums per Autorisationsloven §22 and GDPR best practice.
    /// </summary>
    private static void SeedingData(EntityTypeBuilder<RetentionPolicy> builder)
    {
        DateTime effectiveFrom = new(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        _ = builder.HasData(
            new RetentionPolicy
            {
                Id = Guid.Parse("a1111111-0000-0000-0000-000000000001"),
                Category = RetentionDataCategory.MedicineLogs,
                RetentionPeriod = TimeSpan.FromDays(365 * 10), // 10 years (Autorisationsloven §22)
                LegalMinimum = TimeSpan.FromDays(365 * 10),
                EffectiveFrom = effectiveFrom
            },
            new RetentionPolicy
            {
                Id = Guid.Parse("a1111111-0000-0000-0000-000000000002"),
                Category = RetentionDataCategory.ResidentNotes,
                RetentionPeriod = TimeSpan.FromDays(365 * 5), // 5 years default
                LegalMinimum = TimeSpan.FromDays(365 * 2), // 2 years minimum
                EffectiveFrom = effectiveFrom
            },
            new RetentionPolicy
            {
                Id = Guid.Parse("a1111111-0000-0000-0000-000000000003"),
                Category = RetentionDataCategory.AuditLogs,
                RetentionPeriod = TimeSpan.FromDays(365 * 3), // 3 years default
                LegalMinimum = TimeSpan.FromDays(365 * 1), // 1 year minimum (GDPR Art. 30)
                EffectiveFrom = effectiveFrom
            },
            new RetentionPolicy
            {
                Id = Guid.Parse("a1111111-0000-0000-0000-000000000004"),
                Category = RetentionDataCategory.LoginLogs,
                RetentionPeriod = TimeSpan.FromDays(180), // 6 months default
                LegalMinimum = TimeSpan.FromDays(90), // 3 months minimum
                EffectiveFrom = effectiveFrom
            },
            new RetentionPolicy
            {
                Id = Guid.Parse("a1111111-0000-0000-0000-000000000005"),
                Category = RetentionDataCategory.InactiveUsers,
                RetentionPeriod = TimeSpan.FromDays(365), // 1 year default
                LegalMinimum = TimeSpan.FromDays(180), // 6 months minimum
                EffectiveFrom = effectiveFrom
            },
            new RetentionPolicy
            {
                Id = Guid.Parse("a1111111-0000-0000-0000-000000000006"),
                Category = RetentionDataCategory.AnonymizationTrigger,
                RetentionPeriod = TimeSpan.FromDays(30), // 30 days review window
                LegalMinimum = TimeSpan.FromDays(7), // 7 days minimum
                EffectiveFrom = effectiveFrom
            }
        );
    }
}
