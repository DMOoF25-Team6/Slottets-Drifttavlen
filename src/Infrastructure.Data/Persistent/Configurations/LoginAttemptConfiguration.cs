// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Persistent.Configurations;

/// <summary>
/// EF Core configuration for the <see cref="LoginAttempt"/> entity (UC-010).
/// </summary>
public sealed class LoginAttemptConfiguration : IEntityTypeConfiguration<LoginAttempt>
{
    public void Configure(EntityTypeBuilder<LoginAttempt> builder)
    {
        // Brute-force detection scans the most recent window of attempts grouped
        // by hash or by IP; both queries are kept fast with composite indexes.
        _ = builder.HasIndex(a => new { a.EmailHash, a.AttemptedAt });
        _ = builder.HasIndex(a => new { a.IpAddress, a.AttemptedAt });
        _ = builder.HasIndex(a => a.AttemptedAt);
    }
}
