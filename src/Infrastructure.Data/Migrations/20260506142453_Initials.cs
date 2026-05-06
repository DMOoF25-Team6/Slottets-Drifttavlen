using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AuditEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Metadata = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartTimeUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndTimeUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Succeeded = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ErrorMessage = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditEntries", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PhoneAssignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CaregiverId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShiftType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneAssignments", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Residents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Initials = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FirstName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TrafficLightStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residents", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RoleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TokenHash = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExpiresAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedByIp = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RevokedReason = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MedicineRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ResidentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    MedicineName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Timestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Given = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicineRecord_Residents_ResidentId",
                        column: x => x.ResidentId,
                        principalTable: "Residents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PainkillerRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ResidentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GivenAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    NextAllowedTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PainkillerRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PainkillerRecord_Residents_ResidentId",
                        column: x => x.ResidentId,
                        principalTable: "Residents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ResidentNotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Note = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EditedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ResidentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResidentNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResidentNotes_Residents_ResidentId",
                        column: x => x.ResidentId,
                        principalTable: "Residents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("d1c9e8b5-3f4a-4c2e-9a1b-5e6f7a8b9c0d"), null, "superuser", "SUPERUSER" },
                    { new Guid("ee697c76-947a-4fe2-8b14-40194c30bdae"), null, "user", "USER" },
                    { new Guid("fabc2277-7992-491b-ae4a-bc78f8de56aa"), null, "admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("30cffcf9-5784-4fa9-9c10-c013ef3faf16"), 0, "c8178e80-14a3-4d5e-aac7-2f61ec3f95d0", "ThorDanrsøn@example.com", true, false, null, "THORDANRSØN@EXAMPLE.COM", "THORDANRSØN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEAqpMy8xptCulkIfDl/e/BM6r9y9ACP4nl3HNoiHPpI4PTHM37gjFuG5yav3eWjEgQ==", null, false, "a1a893f4-fc1c-4ff7-8f01-dfe7fd50e64c", false, "thordanrsøn@example.com" },
                    { new Guid("37155b80-7111-422a-aba6-89d7070f1644"), 0, "4e766dfc-fbf5-4cb5-8d41-4be95035207b", "PerNielsen@example.com", true, false, null, "PERNIELSEN@EXAMPLE.COM", "PERNIELSEN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEGOXqsxmahw0pubRYxwaKiSk9iMl3sRdEVqCnDAjlgTZ0Z2sUguu8ha/7Fr9mUsXXQ==", null, false, "2d23e29a-e6ea-4ae5-8169-9e5e5bcfdfdc", false, "pernielsen@example.com" },
                    { new Guid("3a21f8e1-885b-4394-abf0-ed0baeea239b"), 0, "cefa6ccf-2a18-4886-8a0d-3a3c48a78e1e", "PederRasmussen@example.com", true, false, null, "PEDERRASMUSSEN@EXAMPLE.COM", "PEDERRASMUSSEN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEN3OHVxiFL7WdDKJ4uouLTSwxM62Y70n1BF5ls9apFWiAt2ZSDz2kjfE1Ds4Gngo/w==", null, false, "35fecfcc-3bb3-4cfa-b389-e29e2ede1e8a", false, "pederrasmussen@example.com" },
                    { new Guid("4711a300-711e-4132-86d4-cafd3f11deec"), 0, "b56c7e7d-908a-400e-bc70-20b8348e054d", "SanneJohansen@example.com", true, false, null, "SANNEJOHANSEN@EXAMPLE.COM", "SANNEJOHANSEN@EXAMPLE.COM", "AQAAAAIAAYagAAAAENnW9Lqqioz4a7P5h+ftmTr1d7AcmhQWeqCafGfJkvlWhqhB0C2ywJramSuUSG6UxA==", null, false, "47043eed-c997-4bdb-be22-e9585e840f6c", false, "sannejohansen@example.com" },
                    { new Guid("48245a9c-f2a5-4e8f-9554-b6acc9206d37"), 0, "86b92d63-4845-4bda-b1f5-2748732cfeb6", "KasperHolm@example.com", true, false, null, "KASPERHOLM@EXAMPLE.COM", "KASPERHOLM@EXAMPLE.COM", "AQAAAAIAAYagAAAAEHhAcZDyk1KmEwRd+twNXJ6yHCWCDsw38jtX+MTpk/RmdUdt5WZAV46UZNdXvHsV+A==", null, false, "897045a9-a34e-42a1-9bee-97a94bb40c87", false, "kasperholm@example.com" },
                    { new Guid("b836e975-e775-48bc-8b84-5d2bdd5bd87a"), 0, "21b6dc1f-dce9-48af-88ae-32f06dc435e4", "AndersJensen@example.com", true, false, null, "ANDERSJENSEN@EXAMPLE.COM", "ANDERSJENSEN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEEKKDiP8OUipbaXmWKmJuEX+pCCHblRGWpA57B4oK8j012tx1xrAvkBgDKCTU9t7mw==", null, false, "007a5b1f-95c7-4377-b0be-8f030591168b", false, "andersjensen@example.com" }
                });

            migrationBuilder.InsertData(
                table: "Residents",
                columns: new[] { "Id", "FirstName", "Initials", "LastName", "TrafficLightStatus" },
                values: new object[,]
                {
                    { new Guid("694b9796-dc5a-4a68-bafb-0a59595e8fb3"), "", "A", "", 0 },
                    { new Guid("a1b2c3d4-e5f6-7890-1234-56789abcdef0"), "", "B", "", 2 },
                    { new Guid("a6b7c8d9-0123-4567-89ab-cdef01234567"), "", "GA", "", 0 },
                    { new Guid("b7c8d9e0-1234-5678-9abc-def012345678"), "", "H", "", 2 },
                    { new Guid("c2d3e4f5-6789-0123-4567-89abcdef0123"), "", "C", "", 1 },
                    { new Guid("c8d9e0f1-2345-6789-abcd-ef0123456789"), "", "I", "", 0 },
                    { new Guid("d3e4f5a6-7890-1234-5678-9abcdef01234"), "", "D", "", 0 },
                    { new Guid("d9e0f1a2-3456-789a-bcde-f01234567890"), "", "J", "", 0 },
                    { new Guid("e0f1a2b3-4567-89ab-cdef-012345678901"), "", "K", "", 2 },
                    { new Guid("e4f5a6b7-8901-2345-6789-abcdef012345"), "", "E", "", 0 },
                    { new Guid("f1a2b3c4-5678-9abc-def0-123456789012"), "", "GB", "", 1 },
                    { new Guid("f5a6b7c8-9012-3456-789a-bcdef0123456"), "", "F", "", 1 }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "CanViewMedicine", new Guid("ee697c76-947a-4fe2-8b14-40194c30bdae") },
                    { 2, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "CanManageResidents", new Guid("d1c9e8b5-3f4a-4c2e-9a1b-5e6f7a8b9c0d") },
                    { 3, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "CanViewMedicine", new Guid("d1c9e8b5-3f4a-4c2e-9a1b-5e6f7a8b9c0d") }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("fabc2277-7992-491b-ae4a-bc78f8de56aa"), new Guid("3a21f8e1-885b-4394-abf0-ed0baeea239b") });

            migrationBuilder.InsertData(
                table: "MedicineRecord",
                columns: new[] { "Id", "Given", "MedicineName", "ResidentId", "Timestamp" },
                values: new object[,]
                {
                    { new Guid("4bbd018f-b297-4973-85e9-5be5b0499834"), false, "Bisoprolol", new Guid("d3e4f5a6-7890-1234-5678-9abcdef01234"), new DateTime(2026, 4, 2, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a0684cf2-f249-41f6-af83-0a75e0a5e7d3"), true, "Atenolol", new Guid("694b9796-dc5a-4a68-bafb-0a59595e8fb3"), new DateTime(2026, 4, 1, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c9f58ba2-c394-4f8c-b058-c13766689c79"), false, "Quetiapine", new Guid("f5a6b7c8-9012-3456-789a-bcdef0123456"), new DateTime(2026, 4, 2, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c9f94fc6-c542-4ae9-ade6-3c40cd2262d8"), true, "Sertraline", new Guid("694b9796-dc5a-4a68-bafb-0a59595e8fb3"), new DateTime(2026, 4, 1, 9, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ResidentNotes",
                columns: new[] { "Id", "CreatedAt", "EditedAt", "Note", "ResidentId" },
                values: new object[,]
                {
                    { new Guid("a2b3c4d5-6789-0123-4567-123456789abc"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Resident H's note.", new Guid("b7c8d9e0-1234-5678-9abc-def012345678") },
                    { new Guid("a6b7c8d9-0123-4567-8901-bcdef0123456"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Resident B's note.", new Guid("a1b2c3d4-e5f6-7890-1234-56789abcdef0") },
                    { new Guid("b3c4d5e6-7890-1234-5678-23456789abcd"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Resident I's note.", new Guid("c8d9e0f1-2345-6789-abcd-ef0123456789") },
                    { new Guid("b7c8d9e0-1234-5678-9012-cdef01234567"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Resident C's note.", new Guid("c2d3e4f5-6789-0123-4567-89abcdef0123") },
                    { new Guid("c4d5e6f7-8901-2345-6789-3456789abcde"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Resident J's note.", new Guid("d9e0f1a2-3456-789a-bcde-f01234567890") },
                    { new Guid("c8d9e0f1-2345-6789-0123-def012345678"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Resident D's note.", new Guid("d3e4f5a6-7890-1234-5678-9abcdef01234") },
                    { new Guid("d5e6f7a8-9012-3456-7890-456789abcde0"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Resident K's note.", new Guid("e0f1a2b3-4567-89ab-cdef-012345678901") },
                    { new Guid("d9e0f1a2-3456-7890-1234-ef0123456789"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Resident E's note.", new Guid("e4f5a6b7-8901-2345-6789-abcdef012345") },
                    { new Guid("e0f1a2b3-4567-8901-2345-f0123456789a"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Resident F's note.", new Guid("f5a6b7c8-9012-3456-789a-bcdef0123456") },
                    { new Guid("e6f7a8b9-0123-4567-8901-56789abcde01"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Resident GB's note.", new Guid("f1a2b3c4-5678-9abc-def0-123456789012") },
                    { new Guid("f1a2b3c4-5678-9012-3456-0123456789ab"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Resident GA's note.", new Guid("a6b7c8d9-0123-4567-89ab-cdef01234567") },
                    { new Guid("f5a6b7c8-9012-3456-7890-abcdef012345"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Resident A's note.", new Guid("694b9796-dc5a-4a68-bafb-0a59595e8fb3") },
                    { new Guid("f7a8b9c0-1234-5678-9012-6789abcde012"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Resident GB's note 2.", new Guid("f1a2b3c4-5678-9abc-def0-123456789012") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicineRecord_ResidentId",
                table: "MedicineRecord",
                column: "ResidentId");

            migrationBuilder.CreateIndex(
                name: "IX_PainkillerRecord_ResidentId",
                table: "PainkillerRecord",
                column: "ResidentId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_TokenHash",
                table: "RefreshTokens",
                column: "TokenHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResidentNotes_ResidentId",
                table: "ResidentNotes",
                column: "ResidentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AuditEntries");

            migrationBuilder.DropTable(
                name: "MedicineRecord");

            migrationBuilder.DropTable(
                name: "PainkillerRecord");

            migrationBuilder.DropTable(
                name: "PhoneAssignments");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "ResidentNotes");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Residents");
        }
    }
}
