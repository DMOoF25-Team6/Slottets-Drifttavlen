// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.
using Domain.Entities;
using Domain.Enums;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Persistent.Configurations;

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
        _ = builder.HasMany(r => r.Notes)
            .WithOne()
            .HasForeignKey("ResidentId")
            .OnDelete(DeleteBehavior.Cascade);

        _ = builder.HasMany(r => r.Medicines)
            .WithOne()
            .HasForeignKey("ResidentId")
            .OnDelete(DeleteBehavior.Cascade);

        _ = builder.HasMany(r => r.Painkillers)
            .WithOne()
            .HasForeignKey("ResidentId")
            .OnDelete(DeleteBehavior.Cascade);

        _ = builder.Navigation(r => r.Notes)
            .AutoInclude();

        _ = builder.Navigation(r => r.Medicines)
            .AutoInclude();

        _ = builder.Navigation(r => r.Painkillers)
            .AutoInclude();

        SeedingData(builder);
    }

    public static void SeedingData(EntityTypeBuilder<Resident> builder)
    {
        _ = builder.HasData(
            new Resident
            {
                Id = Guid.Parse("694B9796-DC5A-4A68-BAFB-0A59595E8FB3"),
                FirstName = "Anna",
                LastName = "Andersen",
                Initials = "AA",
                TrafficLightStatus = TrafficLightStatus.Green,
                Department = Department.Slottet
            },
            new Resident
            {
                Id = Guid.Parse("A1B2C3D4-E5F6-7890-1234-56789ABCDEF0"),
                FirstName = "Birthe",
                LastName = "Brun",
                Initials = "BB",
                TrafficLightStatus = TrafficLightStatus.Red,
                Department = Department.Slottet
            },
            new Resident
            {
                Id = Guid.Parse("C2D3E4F5-6789-0123-4567-89ABCDEF0123"),
                FirstName = "Carl",
                LastName = "Christensen",
                Initials = "CC",
                TrafficLightStatus = TrafficLightStatus.Yellow,
                Department = Department.Slottet
            },
            new Resident
            {
                Id = Guid.Parse("D3E4F5A6-7890-1234-5678-9ABCDEF01234"),
                FirstName = "Dorthe",
                LastName = "Dalgaard",
                Initials = "DD",
                TrafficLightStatus = TrafficLightStatus.Green,
                Department = Department.Slottet
            },
            new Resident
            {
                Id = Guid.Parse("E4F5A6B7-8901-2345-6789-ABCDEF012345"),
                FirstName = "Erik",
                LastName = "Eriksen",
                Initials = "EE",
                TrafficLightStatus = TrafficLightStatus.Green,
                Department = Department.Slottet
            },
            new Resident
            {
                Id = Guid.Parse("F5A6B7C8-9012-3456-789A-BCDEF0123456"),
                FirstName = "Frida",
                LastName = "Frederiksen",
                Initials = "FF",
                TrafficLightStatus = TrafficLightStatus.Yellow,
                Department = Department.Slottet
            },
            new Resident
            {
                Id = Guid.Parse("A6B7C8D9-0123-4567-89AB-CDEF01234567"),
                FirstName = "Gunnar",
                LastName = "Gregersen",
                Initials = "GG",
                TrafficLightStatus = TrafficLightStatus.Green,
                Department = Department.Slottet
            },
            new Resident
            {
                Id = Guid.Parse("B7C8D9E0-1234-5678-9ABC-DEF012345678"),
                FirstName = "Hanne",
                LastName = "Hansen",
                Initials = "HH",
                TrafficLightStatus = TrafficLightStatus.Red,
                Department = Department.Slottet
            },
            new Resident
            {
                Id = Guid.Parse("C8D9E0F1-2345-6789-ABCD-EF0123456789"),
                FirstName = "Ida",
                LastName = "Iversen",
                Initials = "II",
                TrafficLightStatus = TrafficLightStatus.Green,
                Department = Department.Slottet
            },
            new Resident
            {
                Id = Guid.Parse("D9E0F1A2-3456-789A-BCDE-F01234567890"),
                FirstName = "Jens",
                LastName = "Jensen",
                Initials = "JJ",
                TrafficLightStatus = TrafficLightStatus.Green,
                Department = Department.Slottet
            },
            new Resident
            {
                Id = Guid.Parse("E0F1A2B3-4567-89AB-CDEF-012345678901"),
                FirstName = "Karen",
                LastName = "Knudsen",
                Initials = "KK",
                TrafficLightStatus = TrafficLightStatus.Red,
                Department = Department.Slottet
            },
            new Resident
            {
                Id = Guid.Parse("F1A2B3C4-5678-9ABC-DEF0-123456789012"),
                FirstName = "Lars",
                LastName = "Larsen",
                Initials = "LL",
                TrafficLightStatus = TrafficLightStatus.Yellow,
                Department = Department.Slottet
            },

            // ── Skoven (7 residents) ──────────────────────────────────────────
            new Resident
            {
                Id = Guid.Parse("AA000001-0000-0000-0000-000000000001"),
                FirstName = "Mette",
                LastName = "Madsen",
                Initials = "MM",
                TrafficLightStatus = TrafficLightStatus.Green,
                Department = Department.Skoven
            },
            new Resident
            {
                Id = Guid.Parse("AA000001-0000-0000-0000-000000000002"),
                FirstName = "Niels",
                LastName = "Nielsen",
                Initials = "NN",
                TrafficLightStatus = TrafficLightStatus.Yellow,
                Department = Department.Skoven
            },
            new Resident
            {
                Id = Guid.Parse("AA000001-0000-0000-0000-000000000003"),
                FirstName = "Ole",
                LastName = "Olesen",
                Initials = "OO",
                TrafficLightStatus = TrafficLightStatus.Red,
                Department = Department.Skoven
            },
            new Resident
            {
                Id = Guid.Parse("AA000001-0000-0000-0000-000000000004"),
                FirstName = "Pia",
                LastName = "Petersen",
                Initials = "PP",
                TrafficLightStatus = TrafficLightStatus.Green,
                Department = Department.Skoven
            },
            new Resident
            {
                Id = Guid.Parse("AA000001-0000-0000-0000-000000000005"),
                FirstName = "Rasmus",
                LastName = "Rasmussen",
                Initials = "RR",
                TrafficLightStatus = TrafficLightStatus.Green,
                Department = Department.Skoven
            },
            new Resident
            {
                Id = Guid.Parse("AA000001-0000-0000-0000-000000000006"),
                FirstName = "Sofie",
                LastName = "Sørensen",
                Initials = "SS",
                TrafficLightStatus = TrafficLightStatus.Yellow,
                Department = Department.Skoven
            },
            new Resident
            {
                Id = Guid.Parse("AA000001-0000-0000-0000-000000000007"),
                FirstName = "Thomas",
                LastName = "Thomsen",
                Initials = "TT",
                TrafficLightStatus = TrafficLightStatus.Red,
                Department = Department.Skoven
            },

            // ── Marken (9 residents) ──────────────────────────────────────────
            new Resident
            {
                Id = Guid.Parse("BB000002-0000-0000-0000-000000000001"),
                FirstName = "Ulla",
                LastName = "Ulrichsen",
                Initials = "UU",
                TrafficLightStatus = TrafficLightStatus.Green,
                Department = Department.Marken
            },
            new Resident
            {
                Id = Guid.Parse("BB000002-0000-0000-0000-000000000002"),
                FirstName = "Viggo",
                LastName = "Vestergaard",
                Initials = "VV",
                TrafficLightStatus = TrafficLightStatus.Red,
                Department = Department.Marken
            },
            new Resident
            {
                Id = Guid.Parse("BB000002-0000-0000-0000-000000000003"),
                FirstName = "Winnie",
                LastName = "Winther",
                Initials = "WW",
                TrafficLightStatus = TrafficLightStatus.Yellow,
                Department = Department.Marken
            },
            new Resident
            {
                Id = Guid.Parse("BB000002-0000-0000-0000-000000000004"),
                FirstName = "Xenia",
                LastName = "Xu",
                Initials = "XX",
                TrafficLightStatus = TrafficLightStatus.Green,
                Department = Department.Marken
            },
            new Resident
            {
                Id = Guid.Parse("BB000002-0000-0000-0000-000000000005"),
                FirstName = "Yvonne",
                LastName = "Yilmaz",
                Initials = "YY",
                TrafficLightStatus = TrafficLightStatus.Green,
                Department = Department.Marken
            },
            new Resident
            {
                Id = Guid.Parse("BB000002-0000-0000-0000-000000000006"),
                FirstName = "Zenia",
                LastName = "Zahle",
                Initials = "ZZ",
                TrafficLightStatus = TrafficLightStatus.Red,
                Department = Department.Marken
            },
            new Resident
            {
                Id = Guid.Parse("BB000002-0000-0000-0000-000000000007"),
                FirstName = "Bent",
                LastName = "Bagger",
                Initials = "BA",
                TrafficLightStatus = TrafficLightStatus.Green,
                Department = Department.Marken
            },
            new Resident
            {
                Id = Guid.Parse("BB000002-0000-0000-0000-000000000008"),
                FirstName = "Connie",
                LastName = "Christoffersen",
                Initials = "CO",
                TrafficLightStatus = TrafficLightStatus.Yellow,
                Department = Department.Marken
            },
            new Resident
            {
                Id = Guid.Parse("BB000002-0000-0000-0000-000000000009"),
                FirstName = "Dagmar",
                LastName = "Damgaard",
                Initials = "DA",
                TrafficLightStatus = TrafficLightStatus.Green,
                Department = Department.Marken
            }
        );
    }
}
