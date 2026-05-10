using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddResidentDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Department",
                table: "Residents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("694b9796-dc5a-4a68-bafb-0a59595e8fb3"),
                column: "Department",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-1234-56789abcdef0"),
                column: "Department",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("a6b7c8d9-0123-4567-89ab-cdef01234567"),
                column: "Department",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("b7c8d9e0-1234-5678-9abc-def012345678"),
                column: "Department",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("c2d3e4f5-6789-0123-4567-89abcdef0123"),
                column: "Department",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("c8d9e0f1-2345-6789-abcd-ef0123456789"),
                column: "Department",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("d3e4f5a6-7890-1234-5678-9abcdef01234"),
                column: "Department",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("d9e0f1a2-3456-789a-bcde-f01234567890"),
                column: "Department",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("e0f1a2b3-4567-89ab-cdef-012345678901"),
                column: "Department",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("e4f5a6b7-8901-2345-6789-abcdef012345"),
                column: "Department",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-5678-9abc-def0-123456789012"),
                column: "Department",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("f5a6b7c8-9012-3456-789a-bcdef0123456"),
                column: "Department",
                value: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "Residents");
        }
    }
}
