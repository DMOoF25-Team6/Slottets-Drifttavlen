// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Persistent.Configurations;

/// <summary>
/// Configures the entity mapping for <see cref="PainkillerRecord"/> in the database context.
/// </summary>
/// <remarks>
/// Used by Entity Framework Core to apply configuration for the <see cref="PainkillerRecord"/> entity.
/// </remarks>
public class PainkillerRecordConfiguration : IEntityTypeConfiguration<PainkillerRecord>
{
    /// <summary>
    /// Configures the <see cref="PainkillerRecord"/> entity.
    /// </summary>
    /// <param name="builder">An entity type builder for <see cref="PainkillerRecord"/>.</param>
    public void Configure(EntityTypeBuilder<PainkillerRecord> builder)
    {
        //SeedingData(builder);
    }

    //public static void SeedingData(EntityTypeBuilder<PainkillerRecord> builder)
    //{
    //}
}