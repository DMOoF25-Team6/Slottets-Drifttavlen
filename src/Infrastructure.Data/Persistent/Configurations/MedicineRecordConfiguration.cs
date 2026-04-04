// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Persistent.Configurations;

public class MedicineRecordConfiguration : IEntityTypeConfiguration<MedicineRecord>
{
    public void Configure(EntityTypeBuilder<MedicineRecord> builder)
    {
        SeedingData(builder);
    }

    public static void SeedingData(EntityTypeBuilder<MedicineRecord> builder)
    {
        _ = builder.HasData(
        new MedicineRecord
        {
            Id = Guid.Parse("A0684CF2-F249-41F6-AF83-0A75E0A5E7D3"),
            ResidentId = Guid.Parse("694B9796-DC5A-4A68-BAFB-0A59595E8FB3"),
            MedicineName = "Atenolol", // Heart
            Timestamp = new DateTime(2026, 4, 1, 8, 0, 0),
            Given = true
        },
        new MedicineRecord
        {
            Id = Guid.Parse("C9F94FC6-C542-4AE9-ADE6-3C40CD2262D8"),
            ResidentId = Guid.Parse("694B9796-DC5A-4A68-BAFB-0A59595E8FB3"),
            MedicineName = "Sertraline", // Mental
            Timestamp = new DateTime(2026, 4, 1, 9, 0, 0),
            Given = true
        },
        new MedicineRecord
        {
            Id = Guid.Parse("4BBD018F-B297-4973-85E9-5BE5B0499834"),
            ResidentId = Guid.Parse("D3E4F5A6-7890-1234-5678-9ABCDEF01234"),
            MedicineName = "Bisoprolol", // Heart
            Timestamp = new DateTime(2026, 4, 2, 8, 0, 0),
            Given = false
        },
        new MedicineRecord
        {
            Id = Guid.Parse("C9F58BA2-C394-4F8C-B058-C13766689C79"),
            ResidentId = Guid.Parse("F5A6B7C8-9012-3456-789A-BCDEF0123456"),
            MedicineName = "Quetiapine", // Mental
            Timestamp = new DateTime(2026, 4, 2, 9, 0, 0),
            Given = false
        }
        );
    }
}