using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStaffAssignments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Initials = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StaffAssignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ResidentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EmployeeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ShiftType = table.Column<int>(type: "int", nullable: false),
                    AssignmentDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffAssignments_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StaffAssignments_Residents_ResidentId",
                        column: x => x.ResidentId,
                        principalTable: "Residents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("30cffcf9-5784-4fa9-9c10-c013ef3faf16"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "693210df-1b1c-4cf6-8251-d4abedf89245", "AQAAAAIAAYagAAAAEFj7xI+eiMTAw5ZWRaYp1WrX/+73TrtAxUtmRC43ArOji0z+8nH9OK6kkWUnt6/v6Q==", "d05b04f7-552a-4124-8708-da4a03595f0c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("37155b80-7111-422a-aba6-89d7070f1644"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "74416d35-0680-4d6c-8832-204358e38a5c", "AQAAAAIAAYagAAAAECiYZeqKwZFaE/Nr9qh/lI5qWT3J/aCb4zOYqHeZsS1fCDb/y1DXAzMxMbDxpXr1dA==", "ab2452e6-f258-49c8-a623-a7d359b10ed7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3a21f8e1-885b-4394-abf0-ed0baeea239b"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5e512519-0d57-4d68-8446-2f15fb1f61b7", "AQAAAAIAAYagAAAAED10IYlMSk0Zo9Ziy9rkLwKRcO/EIoCYZNxbmX2kfLEGz6KbzQBPrgZU17krbmwhxg==", "ce07aced-1f7b-444b-8fec-8453be0ec305" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4711a300-711e-4132-86d4-cafd3f11deec"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e99796e9-147d-4294-8714-93b6c8304ae3", "AQAAAAIAAYagAAAAEHMgDpP0hWi9oEr/Bd7N6IbQIpF1eu/2CUMvS0jrd16E3usiYww7IicGKDMRwFPE0g==", "4e3862cf-99b7-44c5-ae9a-3ae8b85c3651" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("48245a9c-f2a5-4e8f-9554-b6acc9206d37"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "edaa32e3-b6e5-4e4d-90c2-5daefe377990", "AQAAAAIAAYagAAAAEKvzkaA42TYs+jxK8QVZWBdiw2QRnRbHfikt/9AVxnDfPQrEXCq/l4IIMOyGOjYtpw==", "27fda1e1-eee9-4a54-908b-526bc7bf4324" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b836e975-e775-48bc-8b84-5d2bdd5bd87a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c28dda23-ba1d-44c9-958e-5abdea717e3d", "AQAAAAIAAYagAAAAEH8uxgJYr49zJZPk9uiQFYsemk7CeaMrSYoQYxjbw696UOD4xe/kLa2C/buibjOj8w==", "b5c7777a-bfeb-4ee9-89f5-edc642819961" });

            migrationBuilder.CreateIndex(
                name: "IX_StaffAssignments_EmployeeId",
                table: "StaffAssignments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffAssignments_ResidentId",
                table: "StaffAssignments",
                column: "ResidentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StaffAssignments");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("30cffcf9-5784-4fa9-9c10-c013ef3faf16"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f67063f6-8396-4fda-acb9-f8828704a5b8", "AQAAAAIAAYagAAAAEHhZqSVVwABEaci/cdKiZtNkgWYtLVqXbqFBCNZIV4dqfQDL16QCSrotswlj69SrUg==", "c3abc4c0-86fe-4ef7-b5e4-7ee983137035" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("37155b80-7111-422a-aba6-89d7070f1644"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "db2dea37-cbf4-41c1-bc59-73caee8a4e19", "AQAAAAIAAYagAAAAEBL8M5DmvJzuQHt9PpsMEkme+soEFK8FDtbeExPk01Mvs3RUnwJnGlsmfR3F9mMwWQ==", "8f471647-de42-45a4-a1e7-19d2a64d4fad" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3a21f8e1-885b-4394-abf0-ed0baeea239b"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3f2de00f-6d2c-4f3d-a4e6-0c0a0b1fbd1c", "AQAAAAIAAYagAAAAENTTirmPX3De5hmV/oT+Swwtap0kZ84qqwwOniU4UL53GHWkxgaySIGzevzhBBGmGw==", "d7cefa32-0e20-4ccc-b2e3-e092b6fa4d6b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4711a300-711e-4132-86d4-cafd3f11deec"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "91ce28f7-0cde-4102-8822-410d6d51a011", "AQAAAAIAAYagAAAAENF2JKDK/0VWrkQpjgbotpODUrbQnhHb9IStKVMqBGx0ddH1gxQcX0Kfbw0WZftMJg==", "91b36f1f-fc24-43df-9c27-541cca61aaed" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("48245a9c-f2a5-4e8f-9554-b6acc9206d37"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8b5cdde3-1a6b-41d9-94b0-692680149979", "AQAAAAIAAYagAAAAEMPle04qWx0hcDeIBXXKVes08Cj6PAWCOsMFEJrpw9jM4Qnp9AIMTNdf+NSyULPGgw==", "5e9a0fd8-e3f1-4d66-afe3-77e1e83a7446" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b836e975-e775-48bc-8b84-5d2bdd5bd87a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "464bbb79-d13e-4334-947f-623592a9e3ab", "AQAAAAIAAYagAAAAEDQ4ibv32SQaSnNOk25S6jkob7SqrXYx2X+SiwdNh7cGDwY+gAMdwjkYAGFs+jT1Ng==", "e60422b7-3153-4119-b618-1fb81cfcba64" });
        }
    }
}
