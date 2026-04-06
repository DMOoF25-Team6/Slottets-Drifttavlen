// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Persistent.Configurations;

public class PainkillerRecordConfiguration : IEntityTypeConfiguration<PainkillerRecord>
{
    public void Configure(EntityTypeBuilder<PainkillerRecord> builder)
    {
        SeedingData(builder);
    }

    public static void SeedingData(EntityTypeBuilder<PainkillerRecord> builder)
    {
    }
}