// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Persistent.Configurations;

/// <summary>
/// EF Core configuration for the RefreshToken entity.
/// </summary>
public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        _ = builder.HasKey(rt => rt.Id);

        _ = builder.Property(rt => rt.TokenHash)
            .IsRequired()
            .HasMaxLength(64) // SHA-256 hex string is 64 chars
            .HasColumnName("TokenHash");

        _ = builder.Property(rt => rt.ExpiresAt)
            .IsRequired();

        _ = builder.Property(rt => rt.CreatedAt)
            .IsRequired();

        _ = builder.Property(rt => rt.CreatedByIp)
            .HasMaxLength(64);

        _ = builder.Property(rt => rt.RevokedReason)
            .HasMaxLength(256);

        _ = builder.HasOne(rt => rt.User)
            .WithMany()
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        _ = builder.HasIndex(rt => rt.TokenHash).IsUnique();
    }
}
