// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Persistent.Configurations;

/// <summary>
/// Provides Entity Framework Core configuration for the <see cref="AuditLog"/> entity.
/// </summary>
/// <remarks>
/// Pins string column lengths so MySQL does not fall back to <c>longtext</c>, and
/// adds indexes on the columns most often used to filter audit queries
/// (entity name, timestamp, changed-by user).
/// </remarks>
public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    #region Constants

    private const int EntityNameMaxLength = 100;
    private const int ChangeTypeMaxLength = 20;
    private const int ChangedByMaxLength = 100;
    private const int DescriptionMaxLength = 1000;

    #endregion

    #region Methods

    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        _ = builder.HasKey(a => a.Id);

        _ = builder.Property(a => a.EntityName)
            .IsRequired()
            .HasMaxLength(EntityNameMaxLength);

        _ = builder.Property(a => a.ChangeType)
            .IsRequired()
            .HasMaxLength(ChangeTypeMaxLength);

        _ = builder.Property(a => a.ChangedBy)
            .HasMaxLength(ChangedByMaxLength);

        _ = builder.Property(a => a.Timestamp)
            .IsRequired();

        _ = builder.Property(a => a.Description)
            .IsRequired()
            .HasMaxLength(DescriptionMaxLength);

        _ = builder.HasIndex(a => a.EntityName)
            .HasDatabaseName("IX_AuditLogs_EntityName");

        _ = builder.HasIndex(a => a.Timestamp)
            .HasDatabaseName("IX_AuditLogs_Timestamp");

        _ = builder.HasIndex(a => a.ChangedBy)
            .HasDatabaseName("IX_AuditLogs_ChangedBy");
    }

    #endregion
}
