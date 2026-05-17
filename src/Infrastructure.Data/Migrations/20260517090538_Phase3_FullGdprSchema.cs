using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Phase3_FullGdprSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DischargedAt",
                table: "Residents",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LoginAttempts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AttemptedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EmailHash = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Succeeded = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IpAddress = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FailureReason = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginAttempts", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SubjectAccessRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ResidentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RequestedByEmployeeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RequestedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ScopeOptions = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExportFileName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExportGeneratedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FulfilledAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    FulfilledByEmployeeId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectAccessRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectAccessRequests_Residents_ResidentId",
                        column: x => x.ResidentId,
                        principalTable: "Residents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("694b9796-dc5a-4a68-bafb-0a59595e8fb3"),
                column: "DischargedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-1234-56789abcdef0"),
                column: "DischargedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("a6b7c8d9-0123-4567-89ab-cdef01234567"),
                column: "DischargedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("aa000001-0000-0000-0000-000000000001"),
                column: "DischargedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("aa000001-0000-0000-0000-000000000002"),
                column: "DischargedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("aa000001-0000-0000-0000-000000000003"),
                column: "DischargedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("aa000001-0000-0000-0000-000000000004"),
                column: "DischargedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("aa000001-0000-0000-0000-000000000005"),
                column: "DischargedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("aa000001-0000-0000-0000-000000000006"),
                column: "DischargedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("aa000001-0000-0000-0000-000000000007"),
                column: "DischargedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("b7c8d9e0-1234-5678-9abc-def012345678"),
                column: "DischargedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("bb000002-0000-0000-0000-000000000001"),
                column: "DischargedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("bb000002-0000-0000-0000-000000000002"),
                column: "DischargedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("bb000002-0000-0000-0000-000000000003"),
                column: "DischargedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("bb000002-0000-0000-0000-000000000004"),
                column: "DischargedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("bb000002-0000-0000-0000-000000000005"),
                column: "DischargedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("bb000002-0000-0000-0000-000000000006"),
                column: "DischargedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("bb000002-0000-0000-0000-000000000007"),
                column: "DischargedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("bb000002-0000-0000-0000-000000000008"),
                column: "DischargedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("bb000002-0000-0000-0000-000000000009"),
                column: "DischargedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("c2d3e4f5-6789-0123-4567-89abcdef0123"),
                column: "DischargedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("c8d9e0f1-2345-6789-abcd-ef0123456789"),
                column: "DischargedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("d3e4f5a6-7890-1234-5678-9abcdef01234"),
                column: "DischargedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("d9e0f1a2-3456-789a-bcde-f01234567890"),
                column: "DischargedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("e0f1a2b3-4567-89ab-cdef-012345678901"),
                column: "DischargedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("e4f5a6b7-8901-2345-6789-abcdef012345"),
                column: "DischargedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-5678-9abc-def0-123456789012"),
                column: "DischargedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Residents",
                keyColumn: "Id",
                keyValue: new Guid("f5a6b7c8-9012-3456-789a-bcdef0123456"),
                column: "DischargedAt",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_LoginAttempts_AttemptedAt",
                table: "LoginAttempts",
                column: "AttemptedAt");

            migrationBuilder.CreateIndex(
                name: "IX_LoginAttempts_EmailHash_AttemptedAt",
                table: "LoginAttempts",
                columns: new[] { "EmailHash", "AttemptedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_LoginAttempts_IpAddress_AttemptedAt",
                table: "LoginAttempts",
                columns: new[] { "IpAddress", "AttemptedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectAccessRequests_FulfilledAt",
                table: "SubjectAccessRequests",
                column: "FulfilledAt");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectAccessRequests_ResidentId",
                table: "SubjectAccessRequests",
                column: "ResidentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoginAttempts");

            migrationBuilder.DropTable(
                name: "SubjectAccessRequests");

            migrationBuilder.DropColumn(
                name: "DischargedAt",
                table: "Residents");
        }
    }
}
