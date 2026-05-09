using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Initials = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Department = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Department", "FirstName", "Initials", "LastName", "UserId" },
                values: new object[,]
                {
                    { new Guid("11111111-0000-0000-0000-000000000001"), 0, "Peder", "PR", "Rasmussen", new Guid("3a21f8e1-885b-4394-abf0-ed0baeea239b") },
                    { new Guid("11111111-0000-0000-0000-000000000002"), 0, "Sanne", "SJ", "Johansen", new Guid("4711a300-711e-4132-86d4-cafd3f11deec") },
                    { new Guid("11111111-0000-0000-0000-000000000003"), 0, "Thor", "TD", "Danrsøn", new Guid("30cffcf9-5784-4fa9-9c10-c013ef3faf16") },
                    { new Guid("11111111-0000-0000-0000-000000000004"), 1, "Per", "PN", "Nielsen", new Guid("37155b80-7111-422a-aba6-89d7070f1644") },
                    { new Guid("11111111-0000-0000-0000-000000000005"), 1, "Anders", "AJ", "Jensen", new Guid("b836e975-e775-48bc-8b84-5d2bdd5bd87a") },
                    { new Guid("11111111-0000-0000-0000-000000000006"), 2, "Kasper", "KH", "Holm", new Guid("48245a9c-f2a5-4e8f-9554-b6acc9206d37") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
