using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateResidentSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("694b9796-dc5a-4a68-bafb-0a59595e8fb3"),
                columns: new[] { "FirstName", "Initials", "LastName" },
                values: new object[] { "Anna", "AA", "Andersen" });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-1234-56789abcdef0"),
                columns: new[] { "FirstName", "Initials", "LastName" },
                values: new object[] { "Birthe", "BB", "Brun" });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("a6b7c8d9-0123-4567-89ab-cdef01234567"),
                columns: new[] { "Department", "FirstName", "Initials", "LastName" },
                values: new object[] { 0, "Gunnar", "GG", "Gregersen" });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("b7c8d9e0-1234-5678-9abc-def012345678"),
                columns: new[] { "Department", "FirstName", "Initials", "LastName" },
                values: new object[] { 0, "Hanne", "HH", "Hansen" });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("c2d3e4f5-6789-0123-4567-89abcdef0123"),
                columns: new[] { "FirstName", "Initials", "LastName" },
                values: new object[] { "Carl", "CC", "Christensen" });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("c8d9e0f1-2345-6789-abcd-ef0123456789"),
                columns: new[] { "Department", "FirstName", "Initials", "LastName" },
                values: new object[] { 0, "Ida", "II", "Iversen" });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("d3e4f5a6-7890-1234-5678-9abcdef01234"),
                columns: new[] { "Department", "FirstName", "Initials", "LastName" },
                values: new object[] { 0, "Dorthe", "DD", "Dalgaard" });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("d9e0f1a2-3456-789a-bcde-f01234567890"),
                columns: new[] { "Department", "FirstName", "Initials", "LastName" },
                values: new object[] { 0, "Jens", "JJ", "Jensen" });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("e0f1a2b3-4567-89ab-cdef-012345678901"),
                columns: new[] { "Department", "FirstName", "Initials", "LastName" },
                values: new object[] { 0, "Karen", "KK", "Knudsen" });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("e4f5a6b7-8901-2345-6789-abcdef012345"),
                columns: new[] { "Department", "FirstName", "Initials", "LastName" },
                values: new object[] { 0, "Erik", "EE", "Eriksen" });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-5678-9abc-def0-123456789012"),
                columns: new[] { "FirstName", "Initials", "LastName" },
                values: new object[] { "Lars", "LL", "Larsen" });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("f5a6b7c8-9012-3456-789a-bcdef0123456"),
                columns: new[] { "Department", "FirstName", "Initials", "LastName" },
                values: new object[] { 0, "Frida", "FF", "Frederiksen" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("694b9796-dc5a-4a68-bafb-0a59595e8fb3"),
                columns: new[] { "FirstName", "Initials", "LastName" },
                values: new object[] { "", "A", "" });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-1234-56789abcdef0"),
                columns: new[] { "FirstName", "Initials", "LastName" },
                values: new object[] { "", "B", "" });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("a6b7c8d9-0123-4567-89ab-cdef01234567"),
                columns: new[] { "Department", "FirstName", "Initials", "LastName" },
                values: new object[] { 1, "", "GA", "" });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("b7c8d9e0-1234-5678-9abc-def012345678"),
                columns: new[] { "Department", "FirstName", "Initials", "LastName" },
                values: new object[] { 2, "", "H", "" });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("c2d3e4f5-6789-0123-4567-89abcdef0123"),
                columns: new[] { "FirstName", "Initials", "LastName" },
                values: new object[] { "", "C", "" });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("c8d9e0f1-2345-6789-abcd-ef0123456789"),
                columns: new[] { "Department", "FirstName", "Initials", "LastName" },
                values: new object[] { 2, "", "I", "" });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("d3e4f5a6-7890-1234-5678-9abcdef01234"),
                columns: new[] { "Department", "FirstName", "Initials", "LastName" },
                values: new object[] { 1, "", "D", "" });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("d9e0f1a2-3456-789a-bcde-f01234567890"),
                columns: new[] { "Department", "FirstName", "Initials", "LastName" },
                values: new object[] { 2, "", "J", "" });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("e0f1a2b3-4567-89ab-cdef-012345678901"),
                columns: new[] { "Department", "FirstName", "Initials", "LastName" },
                values: new object[] { 2, "", "K", "" });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("e4f5a6b7-8901-2345-6789-abcdef012345"),
                columns: new[] { "Department", "FirstName", "Initials", "LastName" },
                values: new object[] { 1, "", "E", "" });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-5678-9abc-def0-123456789012"),
                columns: new[] { "FirstName", "Initials", "LastName" },
                values: new object[] { "", "GB", "" });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("f5a6b7c8-9012-3456-789a-bcdef0123456"),
                columns: new[] { "Department", "FirstName", "Initials", "LastName" },
                values: new object[] { 1, "", "F", "" });
        }
    }
}
