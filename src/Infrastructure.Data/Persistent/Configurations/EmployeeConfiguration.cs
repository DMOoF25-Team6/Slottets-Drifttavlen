// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Domain.Entities;
using Domain.Enums;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Persistent.Configurations;

/// <summary>
/// Provides Entity Framework Core configuration and seed data for the <see cref="Employee"/> entity.
/// </summary>
/// <remarks>
/// Each seeded <see cref="Employee"/> is linked to one of the seeded Identity <see cref="User"/> records
/// from <see cref="IdentitySeed"/> and belongs to one of the three departments (Slottet, Skoven, Marken).
/// </remarks>
public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        _ = builder.HasKey(e => e.Id);

        _ = builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        _ = builder.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(50);

        _ = builder.Property(e => e.Initials)
            .IsRequired()
            .HasMaxLength(3);

        _ = builder.Property(e => e.Department)
            .IsRequired();

        // Each Employee references exactly one Identity User.
        _ = builder.HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        _ = builder.HasIndex(e => e.UserId)
            .IsUnique()
            .HasDatabaseName("IX_Employees_UserId");

        SeedingData(builder);
    }

    /// <summary>
    /// Seeds one <see cref="Employee"/> per seeded Identity user.
    /// </summary>
    private static void SeedingData(EntityTypeBuilder<Employee> builder)
    {
        _ = builder.HasData(
            // PederRasmussen – admin, not restricted to a department; assigned Slottet by default.
            new Employee
            {
                Id = Guid.Parse("11111111-0000-0000-0000-000000000001"),
                FirstName = "Peder",
                LastName = "Rasmussen",
                Initials = "PR",
                UserId = IdentitySeed.adminUser.Id,
                Department = Department.Slottet
            },
            // SanneJohansen – superuser (CanManageResidents), assigned Slottet.
            new Employee
            {
                Id = Guid.Parse("11111111-0000-0000-0000-000000000002"),
                FirstName = "Sanne",
                LastName = "Johansen",
                Initials = "SJ",
                UserId = IdentitySeed.superUser.Id,
                Department = Department.Slottet
            },
            // ThorDanrsøn – caretaker, Slottet.
            new Employee
            {
                Id = Guid.Parse("11111111-0000-0000-0000-000000000003"),
                FirstName = "Thor",
                LastName = "Danrsøn",
                Initials = "TD",
                UserId = IdentitySeed.normal1User.Id,
                Department = Department.Slottet
            },
            // PerNielsen – caretaker, Skoven.
            new Employee
            {
                Id = Guid.Parse("11111111-0000-0000-0000-000000000004"),
                FirstName = "Per",
                LastName = "Nielsen",
                Initials = "PN",
                UserId = IdentitySeed.normal2User.Id,
                Department = Department.Skoven
            },
            // AndersJensen – caretaker, Skoven.
            new Employee
            {
                Id = Guid.Parse("11111111-0000-0000-0000-000000000005"),
                FirstName = "Anders",
                LastName = "Jensen",
                Initials = "AJ",
                UserId = IdentitySeed.normal3User.Id,
                Department = Department.Skoven
            },
            // KasperHolm – substitute, Marken.
            new Employee
            {
                Id = Guid.Parse("11111111-0000-0000-0000-000000000006"),
                FirstName = "Kasper",
                LastName = "Holm",
                Initials = "KH",
                UserId = IdentitySeed.substitutUser.Id,
                Department = Department.Marken
            }
        );
    }
}
