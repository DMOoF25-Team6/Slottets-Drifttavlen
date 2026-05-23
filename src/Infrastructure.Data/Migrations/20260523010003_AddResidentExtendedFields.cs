using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddResidentExtendedFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Activity",
                table: "Residents",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Amount",
                table: "Residents",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Companion",
                table: "Residents",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Info",
                table: "Residents",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TaskLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TaskStatus = table.Column<int>(type: "int", nullable: false),
                    DueTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Department = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskLists", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("694b9796-dc5a-4a68-bafb-0a59595e8fb3"),
                columns: new[] { "Activity", "Amount", "Companion", "Info" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-1234-56789abcdef0"),
                columns: new[] { "Activity", "Amount", "Companion", "Info" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("a6b7c8d9-0123-4567-89ab-cdef01234567"),
                columns: new[] { "Activity", "Amount", "Companion", "Info" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("aa000001-0000-0000-0000-000000000001"),
                columns: new[] { "Activity", "Amount", "Companion", "Info" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("aa000001-0000-0000-0000-000000000002"),
                columns: new[] { "Activity", "Amount", "Companion", "Info" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("aa000001-0000-0000-0000-000000000003"),
                columns: new[] { "Activity", "Amount", "Companion", "Info" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("aa000001-0000-0000-0000-000000000004"),
                columns: new[] { "Activity", "Amount", "Companion", "Info" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("aa000001-0000-0000-0000-000000000005"),
                columns: new[] { "Activity", "Amount", "Companion", "Info" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("aa000001-0000-0000-0000-000000000006"),
                columns: new[] { "Activity", "Amount", "Companion", "Info" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("aa000001-0000-0000-0000-000000000007"),
                columns: new[] { "Activity", "Amount", "Companion", "Info" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("b7c8d9e0-1234-5678-9abc-def012345678"),
                columns: new[] { "Activity", "Amount", "Companion", "Info" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("bb000002-0000-0000-0000-000000000001"),
                columns: new[] { "Activity", "Amount", "Companion", "Info" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("bb000002-0000-0000-0000-000000000002"),
                columns: new[] { "Activity", "Amount", "Companion", "Info" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("bb000002-0000-0000-0000-000000000003"),
                columns: new[] { "Activity", "Amount", "Companion", "Info" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("bb000002-0000-0000-0000-000000000004"),
                columns: new[] { "Activity", "Amount", "Companion", "Info" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("bb000002-0000-0000-0000-000000000005"),
                columns: new[] { "Activity", "Amount", "Companion", "Info" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("bb000002-0000-0000-0000-000000000006"),
                columns: new[] { "Activity", "Amount", "Companion", "Info" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("bb000002-0000-0000-0000-000000000007"),
                columns: new[] { "Activity", "Amount", "Companion", "Info" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("bb000002-0000-0000-0000-000000000008"),
                columns: new[] { "Activity", "Amount", "Companion", "Info" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("bb000002-0000-0000-0000-000000000009"),
                columns: new[] { "Activity", "Amount", "Companion", "Info" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("c2d3e4f5-6789-0123-4567-89abcdef0123"),
                columns: new[] { "Activity", "Amount", "Companion", "Info" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("c8d9e0f1-2345-6789-abcd-ef0123456789"),
                columns: new[] { "Activity", "Amount", "Companion", "Info" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("d3e4f5a6-7890-1234-5678-9abcdef01234"),
                columns: new[] { "Activity", "Amount", "Companion", "Info" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("d9e0f1a2-3456-789a-bcde-f01234567890"),
                columns: new[] { "Activity", "Amount", "Companion", "Info" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("e0f1a2b3-4567-89ab-cdef-012345678901"),
                columns: new[] { "Activity", "Amount", "Companion", "Info" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("e4f5a6b7-8901-2345-6789-abcdef012345"),
                columns: new[] { "Activity", "Amount", "Companion", "Info" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-5678-9abc-def0-123456789012"),
                columns: new[] { "Activity", "Amount", "Companion", "Info" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("f5a6b7c8-9012-3456-789a-bcdef0123456"),
                columns: new[] { "Activity", "Amount", "Companion", "Info" },
                values: new object[] { null, null, null, null });

            migrationBuilder.InsertData(
                table: "TaskLists",
                columns: new[] { "Id", "Department", "Description", "DueTime", "TaskStatus", "Title" },
                values: new object[,]
                {
                    { new Guid("11223344-5566-7788-99aa-bbccddeeff00"), 1, "Tasks to be completed in the evening.", new DateTime(2026, 5, 23, 9, 0, 2, 555, DateTimeKind.Local).AddTicks(5260), 1, "lave aftensmad" },
                    { new Guid("11223344-5566-7788-99aa-bbccddeeff20"), 0, "Tasks to be completed in the evening.", new DateTime(2026, 5, 23, 9, 0, 2, 555, DateTimeKind.Local).AddTicks(5270), 1, "lave aftensmad" },
                    { new Guid("11223344-5566-7788-99aa-bbccddeeff30"), 2, "Tasks to be completed in the evening.", new DateTime(2026, 5, 23, 9, 0, 2, 555, DateTimeKind.Local).AddTicks(5280), 1, "lave aftensmad" },
                    { new Guid("12345678-1234-5678-1234-567812345678"), 1, "Tasks to be completed in the morning.", new DateTime(2026, 5, 23, 5, 0, 2, 555, DateTimeKind.Local).AddTicks(5210), 1, "indkøb" },
                    { new Guid("12345678-1234-5678-1234-598812345678"), 0, "Tasks to be completed in the morning.", new DateTime(2026, 5, 23, 5, 0, 2, 555, DateTimeKind.Local).AddTicks(5260), 1, "indkøb" },
                    { new Guid("65345678-1234-5678-1234-598812345678"), 2, "Tasks to be completed in the morning.", new DateTime(2026, 5, 23, 5, 0, 2, 555, DateTimeKind.Local).AddTicks(5270), 1, "indkøb" },
                    { new Guid("87654321-4321-8765-4321-598543218765"), 0, "Tasks to be completed in the afternoon.", new DateTime(2026, 5, 23, 7, 0, 2, 555, DateTimeKind.Local).AddTicks(5270), 1, "rengøring" },
                    { new Guid("87654321-4321-8765-4321-876543218765"), 1, "Tasks to be completed in the afternoon.", new DateTime(2026, 5, 23, 7, 0, 2, 555, DateTimeKind.Local).AddTicks(5250), 1, "rengøring" },
                    { new Guid("87695221-4321-8765-4321-598543218765"), 2, "Tasks to be completed in the afternoon.", new DateTime(2026, 5, 23, 7, 0, 2, 555, DateTimeKind.Local).AddTicks(5270), 1, "rengøring" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskLists");

            migrationBuilder.DropColumn(
                name: "Activity",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "Companion",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "Info",
                table: "Residents");
        }
    }
}
