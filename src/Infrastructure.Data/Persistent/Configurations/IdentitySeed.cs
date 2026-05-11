// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using System.Security.Claims;

using Domain.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Persistent.Configurations;

/// <summary>
/// Provides static seed data for Identity users, roles and claims.
/// </summary>
/// <remarks>
/// Password hashes, ConcurrencyStamps and SecurityStamps are pre-computed constants taken
/// from the initial migration. This makes seed data deterministic across builds — without
/// constants, <see cref="PasswordHasher{TUser}.HashPassword"/> would generate a new salted
/// hash on every build, causing every subsequent migration to scaffold redundant
/// <c>UpdateData</c> statements against <c>AspNetUsers</c>. All seeded users have the
/// password <c>Password123!</c> (development only — replace before production deployment).
/// </remarks>
public static class IdentitySeed
{
    #region Users

    public static readonly User adminUser = new()
    {
        Id = Guid.Parse("3a21f8e1-885b-4394-abf0-ed0baeea239b"),
        UserName = "PederRasmussen@example.com",
        NormalizedUserName = "PEDERRASMUSSEN@EXAMPLE.COM",
        Email = "PederRasmussen@example.com",
        NormalizedEmail = "PEDERRASMUSSEN@EXAMPLE.COM",
        EmailConfirmed = true,
        ConcurrencyStamp = "3f2de00f-6d2c-4f3d-a4e6-0c0a0b1fbd1c",
        PasswordHash = "AQAAAAIAAYagAAAAENTTirmPX3De5hmV/oT+Swwtap0kZ84qqwwOniU4UL53GHWkxgaySIGzevzhBBGmGw==",
        SecurityStamp = "d7cefa32-0e20-4ccc-b2e3-e092b6fa4d6b"
    };

    public static readonly User superUser = new()
    {
        Id = Guid.Parse("4711a300-711e-4132-86d4-cafd3f11deec"),
        UserName = "SanneJohansen@example.com",
        NormalizedUserName = "SANNEJOHANSEN@EXAMPLE.COM",
        Email = "SanneJohansen@example.com",
        NormalizedEmail = "SANNEJOHANSEN@EXAMPLE.COM",
        EmailConfirmed = true,
        ConcurrencyStamp = "91ce28f7-0cde-4102-8822-410d6d51a011",
        PasswordHash = "AQAAAAIAAYagAAAAENF2JKDK/0VWrkQpjgbotpODUrbQnhHb9IStKVMqBGx0ddH1gxQcX0Kfbw0WZftMJg==",
        SecurityStamp = "91b36f1f-fc24-43df-9c27-541cca61aaed"
    };

    public static readonly User normal1User = new()
    {
        Id = Guid.Parse("30cffcf9-5784-4fa9-9c10-c013ef3faf16"),
        UserName = "ThorDanrsøn@example.com",
        NormalizedUserName = "THORDANRSØN@EXAMPLE.COM",
        Email = "ThorDanrsøn@example.com",
        NormalizedEmail = "THORDANRSØN@EXAMPLE.COM",
        EmailConfirmed = true,
        ConcurrencyStamp = "f67063f6-8396-4fda-acb9-f8828704a5b8",
        PasswordHash = "AQAAAAIAAYagAAAAEHhZqSVVwABEaci/cdKiZtNkgWYtLVqXbqFBCNZIV4dqfQDL16QCSrotswlj69SrUg==",
        SecurityStamp = "c3abc4c0-86fe-4ef7-b5e4-7ee983137035"
    };

    public static readonly User normal2User = new()
    {
        Id = Guid.Parse("37155b80-7111-422a-aba6-89d7070f1644"),
        UserName = "PerNielsen@example.com",
        NormalizedUserName = "PERNIELSEN@EXAMPLE.COM",
        Email = "PerNielsen@example.com",
        NormalizedEmail = "PERNIELSEN@EXAMPLE.COM",
        EmailConfirmed = true,
        ConcurrencyStamp = "db2dea37-cbf4-41c1-bc59-73caee8a4e19",
        PasswordHash = "AQAAAAIAAYagAAAAEBL8M5DmvJzuQHt9PpsMEkme+soEFK8FDtbeExPk01Mvs3RUnwJnGlsmfR3F9mMwWQ==",
        SecurityStamp = "8f471647-de42-45a4-a1e7-19d2a64d4fad"
    };

    public static readonly User normal3User = new()
    {
        Id = Guid.Parse("b836e975-e775-48bc-8b84-5d2bdd5bd87a"),
        UserName = "AndersJensen@example.com",
        NormalizedUserName = "ANDERSJENSEN@EXAMPLE.COM",
        Email = "AndersJensen@example.com",
        NormalizedEmail = "ANDERSJENSEN@EXAMPLE.COM",
        EmailConfirmed = true,
        ConcurrencyStamp = "464bbb79-d13e-4334-947f-623592a9e3ab",
        PasswordHash = "AQAAAAIAAYagAAAAEDQ4ibv32SQaSnNOk25S6jkob7SqrXYx2X+SiwdNh7cGDwY+gAMdwjkYAGFs+jT1Ng==",
        SecurityStamp = "e60422b7-3153-4119-b618-1fb81cfcba64"
    };

    public static readonly User substitutUser = new()
    {
        Id = Guid.Parse("48245a9c-f2a5-4e8f-9554-b6acc9206d37"),
        UserName = "KasperHolm@example.com",
        NormalizedUserName = "KASPERHOLM@EXAMPLE.COM",
        Email = "KasperHolm@example.com",
        NormalizedEmail = "KASPERHOLM@EXAMPLE.COM",
        EmailConfirmed = true,
        ConcurrencyStamp = "8b5cdde3-1a6b-41d9-94b0-692680149979",
        PasswordHash = "AQAAAAIAAYagAAAAEMPle04qWx0hcDeIBXXKVes08Cj6PAWCOsMFEJrpw9jM4Qnp9AIMTNdf+NSyULPGgw==",
        SecurityStamp = "5e9a0fd8-e3f1-4d66-afe3-77e1e83a7446"
    };

    #endregion

    #region Roles

    public static readonly IdentityRole<Guid> adminRole = new()
    {
        Id = Guid.Parse("fabc2277-7992-491b-ae4a-bc78f8de56aa"),
        Name = "admin",
        NormalizedName = "ADMIN"
    };

    public static readonly IdentityRole<Guid> superUserRole = new()
    {
        Id = Guid.Parse("d1c9e8b5-3f4a-4c2e-9a1b-5e6f7a8b9c0d"),
        Name = "superuser",
        NormalizedName = "SUPERUSER"
    };

    public static readonly IdentityRole<Guid> careTakerRole = new()
    {
        Id = Guid.Parse("ee697c76-947a-4fe2-8b14-40194c30bdae"),
        Name = "caretaker",
        NormalizedName = "CARETAKER"
    };

    #endregion

    #region Role Claims

    public static readonly IdentityRoleClaim<Guid> careTakerClaim1 = new()
    {
        Id = 1,
        RoleId = careTakerRole.Id,
        ClaimType = ClaimTypes.Role,
        ClaimValue = "CanViewMedicine"
    };

    public static readonly IdentityRoleClaim<Guid> adminClaim1 = new()
    {
        Id = 2,
        RoleId = superUserRole.Id,
        ClaimType = ClaimTypes.Role,
        ClaimValue = "CanManageResidents"
    };

    public static readonly IdentityRoleClaim<Guid> superUserClaim1 = new()
    {
        Id = 3,
        RoleId = superUserRole.Id,
        ClaimType = ClaimTypes.Role,
        ClaimValue = "CanViewMedicine"
    };

    #endregion

    #region Seed methods

    /// <summary>
    /// Seeds the example users using EF Core's HasData.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    public static void UserSeed(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<User>().HasData(
            adminUser,
            superUser,
            normal1User,
            normal2User,
            normal3User,
            substitutUser);
    }

    /// <summary>
    /// Seeds example roles and claims using EF Core's HasData.
    /// Call from OnModelCreating in your DbContext.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    public static void RolesClaimSeed(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<IdentityRole<Guid>>().HasData(
            adminRole,
            superUserRole,
            careTakerRole);
        _ = modelBuilder.Entity<IdentityRoleClaim<Guid>>().HasData(
            careTakerClaim1,
            adminClaim1,
            superUserClaim1);
    }

    /// <summary>
    /// Seeds the user-role assignment for the admin user.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    public static void UserRoleSeed(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
            new IdentityUserRole<Guid>
            {
                UserId = adminUser.Id,
                RoleId = adminRole.Id
            },
            new IdentityUserRole<Guid>
            {
                UserId = superUser.Id,
                RoleId = superUserRole.Id
            },
            // Default role for regular employees is caretaker
            new IdentityUserRole<Guid>
            {
                UserId = normal1User.Id,
                RoleId = careTakerRole.Id
            },
            new IdentityUserRole<Guid>
            {
                UserId = normal2User.Id,
                RoleId = careTakerRole.Id
            },
            new IdentityUserRole<Guid>
            {
                UserId = normal3User.Id,
                RoleId = careTakerRole.Id
            },
            new IdentityUserRole<Guid>
            {
                UserId = substitutUser.Id,
                RoleId = careTakerRole.Id
            }
        );
    }

    #endregion
}
