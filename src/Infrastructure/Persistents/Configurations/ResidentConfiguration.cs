// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.
using Domain.Entities;
using Domain.Enums;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistents.Configurations;

/// <summary>
/// Provides configuration for the <see cref="Resident"/> entity using Entity Framework Core.
/// This includes entity property configuration and seeding of initial data for the Residents table.
/// </summary>
/// <remarks>
/// This configuration is applied in the Infrastructure layer to ensure consistent mapping and data seeding
/// for the Resident entity across different environments.
/// </remarks>
public class ResidentConfiguration : IEntityTypeConfiguration<Resident>
{
    public void Configure(EntityTypeBuilder<Resident> builder)
    {
        SeedingData(builder);
    }

    public static void SeedingData(EntityTypeBuilder<Resident> builder)
    {
        _ = builder.HasData(
            new Resident
            {
                Id = Guid.Parse("694B9796-DC5A-4A68-BAFB-0A59595E8FB3"),
                Initials = "A",
                TrafficLightStatus = TrafficLightStatus.Green
            },
            new Resident
            {
                Id = Guid.Parse("A1B2C3D4-E5F6-7890-1234-56789ABCDEF0"),
                Initials = "B",
                TrafficLightStatus = TrafficLightStatus.Red
            },
            new Resident
            {
                Id = Guid.Parse("C2D3E4F5-6789-0123-4567-89ABCDEF0123"),
                Initials = "C",
                TrafficLightStatus = TrafficLightStatus.Yellow
            },
            new Resident
            {
                Id = Guid.Parse("D3E4F5A6-7890-1234-5678-9ABCDEF01234"),
                Initials = "D",
                TrafficLightStatus = TrafficLightStatus.Green
            },
            new Resident
            {
                Id = Guid.Parse("E4F5A6B7-8901-2345-6789-ABCDEF012345"),
                Initials = "E",
                TrafficLightStatus = TrafficLightStatus.Green
            },
            new Resident
            {
                Id = Guid.Parse("F5A6B7C8-9012-3456-789A-BCDEF0123456"),
                Initials = "F",
                TrafficLightStatus = TrafficLightStatus.Yellow
            },
            new Resident
            {
                Id = Guid.Parse("A6B7C8D9-0123-4567-89AB-CDEF01234567"),
                Initials = "GA",
                TrafficLightStatus = TrafficLightStatus.Green
            },
            new Resident
            {
                Id = Guid.Parse("B7C8D9E0-1234-5678-9ABC-DEF012345678"),
                Initials = "H",
                TrafficLightStatus = TrafficLightStatus.Red
            },
            new Resident
            {
                Id = Guid.Parse("C8D9E0F1-2345-6789-ABCD-EF0123456789"),
                Initials = "I",
                TrafficLightStatus = TrafficLightStatus.Green
            },
            new Resident
            {
                Id = Guid.Parse("D9E0F1A2-3456-789A-BCDE-F01234567890"),
                Initials = "J",
                TrafficLightStatus = TrafficLightStatus.Green
            },
            new Resident
            {
                Id = Guid.Parse("E0F1A2B3-4567-89AB-CDEF-012345678901"),
                Initials = "K",
                TrafficLightStatus = TrafficLightStatus.Red
            },
            new Resident
            {
                Id = Guid.Parse("F1A2B3C4-5678-9ABC-DEF0-123456789012"),
                Initials = "GB",
                TrafficLightStatus = TrafficLightStatus.Yellow
            }
        );
    }
}