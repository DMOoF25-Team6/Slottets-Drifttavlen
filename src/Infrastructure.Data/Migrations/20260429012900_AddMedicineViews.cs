// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class AddMedicineViews : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {

        _ = migrationBuilder.Sql("""
DROP VIEW IF EXISTS medicinestatusview;
""");

        _ = migrationBuilder.Sql("""
CREATE VIEW medicinestatusview AS
SELECT
    r.Id AS ResidentId,
    r.Initials AS Initials,
    m.MedicineName AS MedicineName,
    m.Timestamp AS Timestamp,
    m.Given AS Given
FROM `Residents` r
JOIN `MedicineRecord` m ON r.Id = m.ResidentId
WHERE m.Timestamp >= (NOW() - INTERVAL 24 HOUR);
""");

        _ = migrationBuilder.Sql("""
DROP VIEW IF EXISTS painkillerstatusview;
""");

        _ = migrationBuilder.Sql("""
CREATE VIEW painkillerstatusview AS
SELECT
    r.Id AS ResidentId,
    r.Initials AS Initials,
    p.Type AS Type,
    p.GivenAt AS GivenAt,
    p.NextAllowedTime AS NextAllowedTime
FROM `Residents` r
JOIN `PainkillerRecord` p ON r.Id = p.ResidentId;
""");

        _ = migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("30cffcf9-5784-4fa9-9c10-c013ef3faf16"),
            columns: ["ConcurrencyStamp", "PasswordHash"],
            values: ["b248b605-954d-40ed-a321-6d5569421ac8", "AQAAAAIAAYagAAAAEHoEaJsg4MX8c/fKc/ZGSm0wgjIKuVV6ujjFycYqXH4ABYimPtffwMWB3p7IMmFjdQ=="]);

        _ = migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("37155b80-7111-422a-aba6-89d7070f1644"),
            columns: ["ConcurrencyStamp", "PasswordHash"],
            values: ["6d5acdbb-7e92-42ba-b4a4-5c1c23cad5b8", "AQAAAAIAAYagAAAAEAzmkgl9i/6/9VlVliXAgz6VqfhQMTRMzh0IE+1M9LCMrm3OLGJP31QB0fm0Vu/hIg=="]);

        _ = migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("3a21f8e1-885b-4394-abf0-ed0baeea239b"),
            columns: ["ConcurrencyStamp", "PasswordHash"],
            values: ["ae34196e-117c-492a-93bd-8e0c748e390d", "AQAAAAIAAYagAAAAEPPZ+Nrg4yhBl/ZAUKRVl2/+2i0gZvREQoyJagx6M3iivLZSa6WvZzwah0xxBzTDqQ=="]);

        _ = migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("4711a300-711e-4132-86d4-cafd3f11deec"),
            columns: ["ConcurrencyStamp", "PasswordHash"],
            values: ["44cce98d-1c70-489c-a616-8195aea4412b", "AQAAAAIAAYagAAAAEF7yL9h+mGcHTcyhmz4w6/J2ecuXM9wlOT6FqR39th+Pc6ZLaXrZQVcXeRFixldY3g=="]);

        _ = migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("48245a9c-f2a5-4e8f-9554-b6acc9206d37"),
            columns: ["ConcurrencyStamp", "PasswordHash"],
            values: ["f5226ba2-0f58-4384-98a5-7296b57bb802", "AQAAAAIAAYagAAAAEJoLbhMnJl4gS8ZbtYJGvDH+v3gT353NWEDcrclltZyK+4m/QMLFE3mQrtYTiyGlRg=="]);

        _ = migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("b836e975-e775-48bc-8b84-5d2bdd5bd87a"),
            columns: ["ConcurrencyStamp", "PasswordHash"],
            values: ["5784991c-9c4f-4339-aebf-6a2d2602897c", "AQAAAAIAAYagAAAAEHJRZj69hv9uE9P9eUnveddje0fuXTGg6UIintZ5in1s/hRCaX6nWRCAES5ZYbhnIw=="]);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.Sql("DROP VIEW IF EXISTS medicinestatusview;");
        _ = migrationBuilder.Sql("DROP VIEW IF EXISTS painkillerstatusview;");

        _ = migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("30cffcf9-5784-4fa9-9c10-c013ef3faf16"),
            columns: ["ConcurrencyStamp", "PasswordHash"],
            values: ["d673d5fa-2510-4309-a83b-bd8d7c7ae901", "AQAAAAIAAYagAAAAENCFEsdm62Th/ALwAnsygJAMqRGKcgGaXr5Uu6EiKxCEsVVUvswncSpD2TPC7g3h/g=="]);

        _ = migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("37155b80-7111-422a-aba6-89d7070f1644"),
            columns: ["ConcurrencyStamp", "PasswordHash"],
            values: ["7909355b-8002-4518-b35b-efbd22e0d770", "AQAAAAIAAYagAAAAEATpfHdrqE+y2JgwFgJRrDP7gouGjLuLojPwrR4ZX701EnEWveWm7/epWRAZ/pVFFQ=="]);

        _ = migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("3a21f8e1-885b-4394-abf0-ed0baeea239b"),
            columns: ["ConcurrencyStamp", "PasswordHash"],
            values: ["fd8e4e86-24e0-4370-8b2a-14ee454895ba", "AQAAAAIAAYagAAAAEAutYpQDOVYhG4UBgpIRrzDEXYsrXbyUb2SIVvMibjkcoEQcMap4658Pj6eMgMxv/Q=="]);

        _ = migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("4711a300-711e-4132-86d4-cafd3f11deec"),
            columns: ["ConcurrencyStamp", "PasswordHash"],
            values: ["1744bc8e-e309-4168-b7ab-658bd6097b7f", "AQAAAAIAAYagAAAAEIwODEqVfXAUy9S5JCq/n2czH5QhDhLNo+p0Ns1xXYxgRUDJYH7oYQd0lQ8a0HyDTA=="]);

        _ = migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("48245a9c-f2a5-4e8f-9554-b6acc9206d37"),
            columns: ["ConcurrencyStamp", "PasswordHash"],
            values: ["034d08c2-264f-4505-8529-4f2097401b58", "AQAAAAIAAYagAAAAEOaYCzxaHaQGqGMYQSNxQ7f1yPkXfwQhh309Wd6aHp9Fv44S6oI7i65jLjyCPyL8BQ=="]);

        _ = migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("b836e975-e775-48bc-8b84-5d2bdd5bd87a"),
            columns: ["ConcurrencyStamp", "PasswordHash"],
            values: ["6bc3b61d-534d-4281-b265-d63ced233cf0", "AQAAAAIAAYagAAAAEKkkur+eelQ0AbQg30ZNM/hwOKAX7QMvuP5d+iEBL1I47Ypp8DttMIOCj6BqmNQ5VA=="]);
    }
}
