using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDashboardUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("d0a5b0a1-0000-4000-8000-000000000001"), null, "dashboard", "DASHBOARD" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Department", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), 0, "c0ffee00-dead-beef-cafe-000000000001", null, "dashboard@slottet.dk", true, false, null, "DASHBOARD@SLOTTET.DK", "DASHBOARD@SLOTTET.DK", "AQAAAAEAACcQAAAAEEGYLbEVvDkMGpWxvBizSJSS95uMkciMO3NcZV2yi+7goH8chkCEacnfd4IcKtrBaQ==", null, false, "c0ffee00-dead-beef-cafe-000000000002", false, "dashboard@slottet.dk" });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { 4, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "CanViewMedicine", new Guid("d0a5b0a1-0000-4000-8000-000000000001") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("d0a5b0a1-0000-4000-8000-000000000001"), new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("d0a5b0a1-0000-4000-8000-000000000001"), new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d0a5b0a1-0000-4000-8000-000000000001"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"));
        }
    }
}
