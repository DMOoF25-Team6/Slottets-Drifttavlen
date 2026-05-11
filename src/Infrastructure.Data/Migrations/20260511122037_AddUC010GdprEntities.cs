using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUC010GdprEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RetentionPolicies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Category = table.Column<int>(type: "int", nullable: false),
                    RetentionPeriod = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    LegalMinimum = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    EffectiveFrom = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetentionPolicies", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SecurityIncidents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DetectedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Type = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Severity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    InvestigationNotes = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReportedByEmployeeId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    ResolvedByEmployeeId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityIncidents", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AnonymizationCandidates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ResidentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RetentionPolicyId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SuggestedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Reason = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnonymizationCandidates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnonymizationCandidates_Residents_ResidentId",
                        column: x => x.ResidentId,
                        principalTable: "Residents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnonymizationCandidates_RetentionPolicies_RetentionPolicyId",
                        column: x => x.RetentionPolicyId,
                        principalTable: "RetentionPolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RetentionPolicyAudits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RetentionPolicyId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ChangedByEmployeeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PreviousPeriod = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    NewPeriod = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Reason = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetentionPolicyAudits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetentionPolicyAudits_RetentionPolicies_RetentionPolicyId",
                        column: x => x.RetentionPolicyId,
                        principalTable: "RetentionPolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "RetentionPolicies",
                columns: new[] { "Id", "Category", "EffectiveFrom", "LegalMinimum", "RetentionPeriod" },
                values: new object[,]
                {
                    { new Guid("a1111111-0000-0000-0000-000000000001"), 0, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(3650, 0, 0, 0, 0), new TimeSpan(3650, 0, 0, 0, 0) },
                    { new Guid("a1111111-0000-0000-0000-000000000002"), 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(730, 0, 0, 0, 0), new TimeSpan(1825, 0, 0, 0, 0) },
                    { new Guid("a1111111-0000-0000-0000-000000000003"), 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(365, 0, 0, 0, 0), new TimeSpan(1095, 0, 0, 0, 0) },
                    { new Guid("a1111111-0000-0000-0000-000000000004"), 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(90, 0, 0, 0, 0), new TimeSpan(180, 0, 0, 0, 0) },
                    { new Guid("a1111111-0000-0000-0000-000000000005"), 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(180, 0, 0, 0, 0), new TimeSpan(365, 0, 0, 0, 0) },
                    { new Guid("a1111111-0000-0000-0000-000000000006"), 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(7, 0, 0, 0, 0), new TimeSpan(30, 0, 0, 0, 0) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnonymizationCandidates_ResidentId",
                table: "AnonymizationCandidates",
                column: "ResidentId");

            migrationBuilder.CreateIndex(
                name: "IX_AnonymizationCandidates_RetentionPolicyId",
                table: "AnonymizationCandidates",
                column: "RetentionPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_RetentionPolicies_Category",
                table: "RetentionPolicies",
                column: "Category",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RetentionPolicyAudits_RetentionPolicyId",
                table: "RetentionPolicyAudits",
                column: "RetentionPolicyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnonymizationCandidates");

            migrationBuilder.DropTable(
                name: "RetentionPolicyAudits");

            migrationBuilder.DropTable(
                name: "SecurityIncidents");

            migrationBuilder.DropTable(
                name: "RetentionPolicies");
        }
    }
}
