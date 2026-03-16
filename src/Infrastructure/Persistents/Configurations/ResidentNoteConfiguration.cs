// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistents.Configurations;

public class ResidentNoteConfiguration : IEntityTypeConfiguration<ResidentNote>
{
    public void Configure(EntityTypeBuilder<ResidentNote> builder)
    {
        SeedingData(builder);
    }

    public static void SeedingData(EntityTypeBuilder<ResidentNote> builder)
    {
        _ = builder.HasData(
            new ResidentNote
            {
                Id = Guid.Parse("F5A6B7C8-9012-3456-7890-ABCDEF012345"),
                ResidentId = Guid.Parse("694B9796-DC5A-4A68-BAFB-0A59595E8FB3"),
                Note = "Resident A's note."
            },
            new ResidentNote
            {
                Id = Guid.Parse("A6B7C8D9-0123-4567-8901-BCDEF0123456"),
                ResidentId = Guid.Parse("A1B2C3D4-E5F6-7890-1234-56789ABCDEF0"),
                Note = "Resident B's note."
            },
            new ResidentNote
            {
                Id = Guid.Parse("B7C8D9E0-1234-5678-9012-CDEF01234567"),
                ResidentId = Guid.Parse("C2D3E4F5-6789-0123-4567-89ABCDEF0123"),
                Note = "Resident C's note."
            },
            new ResidentNote
            {
                Id = Guid.Parse("C8D9E0F1-2345-6789-0123-DEF012345678"),
                ResidentId = Guid.Parse("D3E4F5A6-7890-1234-5678-9ABCDEF01234"),
                Note = "Resident D's note."
            },
            new ResidentNote
            {
                Id = Guid.Parse("D9E0F1A2-3456-7890-1234-EF0123456789"),
                ResidentId = Guid.Parse("E4F5A6B7-8901-2345-6789-ABCDEF012345"),
                Note = "Resident E's note."
            },
            new ResidentNote
            {
                Id = Guid.Parse("E0F1A2B3-4567-8901-2345-F0123456789A"),
                ResidentId = Guid.Parse("F5A6B7C8-9012-3456-789A-BCDEF0123456"),
                Note = "Resident F's note."
            },
            new ResidentNote
            {
                Id = Guid.Parse("F1A2B3C4-5678-9012-3456-0123456789AB"),
                ResidentId = Guid.Parse("A6B7C8D9-0123-4567-89AB-CDEF01234567"),
                Note = "Resident GA's note."
            },
            new ResidentNote
            {
                Id = Guid.Parse("A2B3C4D5-6789-0123-4567-123456789ABC"),
                ResidentId = Guid.Parse("B7C8D9E0-1234-5678-9ABC-DEF012345678"),
                Note = "Resident H's note."
            },
            new ResidentNote
            {
                Id = Guid.Parse("B3C4D5E6-7890-1234-5678-23456789ABCD"),
                ResidentId = Guid.Parse("C8D9E0F1-2345-6789-ABCD-EF0123456789"),
                Note = "Resident I's note."
            },
            new ResidentNote
            {
                Id = Guid.Parse("C4D5E6F7-8901-2345-6789-3456789ABCDE"),
                ResidentId = Guid.Parse("D9E0F1A2-3456-789A-BCDE-F01234567890"),
                Note = "Resident J's note."
            },
            new ResidentNote
            {
                Id = Guid.Parse("D5E6F7A8-9012-3456-7890-456789ABCDE0"),
                ResidentId = Guid.Parse("E0F1A2B3-4567-89AB-CDEF-012345678901"),
                Note = "Resident K's note."
            },
            new ResidentNote
            {
                Id = Guid.Parse("E6F7A8B9-0123-4567-8901-56789ABCDE01"),
                ResidentId = Guid.Parse("F1A2B3C4-5678-9ABC-DEF0-12345678901"),
                Note = "Resident GB's note."
            },
            new ResidentNote
            {
                Id = Guid.Parse("F7A8B9C0-1234-5678-9012-6789ABCDE012"),
                ResidentId = Guid.Parse("F1A2B3C4-5678-9ABC-DEF0-12345678901"),
                Note = "Resident GB's note 2."
            }
        );
    }
}
