// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Persistent.Configurations;

/// <summary>
/// Provides Entity Framework Core configuration for the <see cref="ChangeDetail"/> entity.
/// </summary>
/// <remarks>
/// Configures the one-to-many relationship between <see cref="AuditEntry"/> and
/// <see cref="ChangeDetail"/>, with cascade delete so change details are removed
/// automatically when their parent audit entry is deleted. An index on
/// <c>AuditEntryId</c> supports efficient lookup of all change details for a single
/// audit entry (UC-009 View History and Traceability).
/// </remarks>
public class ChangeDetailConfiguration : IEntityTypeConfiguration<ChangeDetail>
{
    #region Constants

    private const int FieldMaxLength = 100;
    private const int ValueMaxLength = 2000;

    #endregion

    #region Methods

    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ChangeDetail> builder)
    {
        _ = builder.Property(c => c.Field)
            .IsRequired()
            .HasMaxLength(FieldMaxLength);

        _ = builder.Property(c => c.OldValue)
            .HasMaxLength(ValueMaxLength);

        _ = builder.Property(c => c.NewValue)
            .HasMaxLength(ValueMaxLength);

        _ = builder.Property(c => c.AuditEntryId)
            .IsRequired();

        _ = builder.HasOne<AuditEntry>()
            .WithMany(a => a.ChangeDetails)
            .HasForeignKey(c => c.AuditEntryId)
            .OnDelete(DeleteBehavior.Cascade);

        _ = builder.HasIndex(c => c.AuditEntryId)
            .HasDatabaseName("IX_ChangeDetails_AuditEntryId");
    }

    #endregion
}
