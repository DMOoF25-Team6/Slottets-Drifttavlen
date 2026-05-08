using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class NonUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("30cffcf9-5784-4fa9-9c10-c013ef3faf16"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7151b135-e860-4008-b012-362558087793", "AQAAAAIAAYagAAAAEJp0qY/ojC3GnLHJf8u/UcoTFMA5nMYXsO3JqrFDm5uCnQSlbOFWUbFHX5keXfIETg==", "02461595-01fd-4f1d-9690-25c8a880a028" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("37155b80-7111-422a-aba6-89d7070f1644"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0785f8ec-df74-4fdb-b52f-fcd5231d661d", "AQAAAAIAAYagAAAAEHJmtb0kQ2HhWdWSGndqy4Zf9mTK+Ofo03yaDcRRKMWp8A32UreAhCqrjq0NYZ4bng==", "075f520c-4caf-47de-899e-33fd75238ece" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3a21f8e1-885b-4394-abf0-ed0baeea239b"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "58aae55d-ad90-41be-8e64-9d94ada7e832", "AQAAAAIAAYagAAAAEFN2Wif0wuIRad0j591ikUANv+raXAlnkKgVxBP6qZ4CO5L+xeFzueR/IdLALw6APQ==", "077a969e-86b5-4f12-bf6e-cdd46bf63242" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4711a300-711e-4132-86d4-cafd3f11deec"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a6c57499-c010-43f8-b5cb-92cbba9be9c2", "AQAAAAIAAYagAAAAEOM6Y0/0XvSj7AXZwavTmA/IwFxk6OIB+DZOCpRg+esMlITRUuGME/DlKJrI3R1kkg==", "9d4e16de-73d6-4669-85e5-547cd914b9ba" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("48245a9c-f2a5-4e8f-9554-b6acc9206d37"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ee85feb9-85dd-4d9b-bc26-75db4706d364", "AQAAAAIAAYagAAAAEP9DF169rkXHIFd7Wi3076g5TX7Uljph7O/TQgxo9Z2LIR7v7dWAwXlLzN+n77y88Q==", "8945603d-4759-4eda-b1fc-c6a9793c493e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b836e975-e775-48bc-8b84-5d2bdd5bd87a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2326bfdf-fe6b-4652-b299-95a97af5d90a", "AQAAAAIAAYagAAAAEKLNMwHFvVJAhbNMciumr2qnSUIoS7ZeuQtWVy+2ByFIPw9Un8B/JTR9HDVKlM4WIw==", "b7bd3bbb-2a4c-4363-beca-86cc8cb66f7d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("30cffcf9-5784-4fa9-9c10-c013ef3faf16"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c8178e80-14a3-4d5e-aac7-2f61ec3f95d0", "AQAAAAIAAYagAAAAEAqpMy8xptCulkIfDl/e/BM6r9y9ACP4nl3HNoiHPpI4PTHM37gjFuG5yav3eWjEgQ==", "a1a893f4-fc1c-4ff7-8f01-dfe7fd50e64c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("37155b80-7111-422a-aba6-89d7070f1644"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4e766dfc-fbf5-4cb5-8d41-4be95035207b", "AQAAAAIAAYagAAAAEGOXqsxmahw0pubRYxwaKiSk9iMl3sRdEVqCnDAjlgTZ0Z2sUguu8ha/7Fr9mUsXXQ==", "2d23e29a-e6ea-4ae5-8169-9e5e5bcfdfdc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3a21f8e1-885b-4394-abf0-ed0baeea239b"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cefa6ccf-2a18-4886-8a0d-3a3c48a78e1e", "AQAAAAIAAYagAAAAEN3OHVxiFL7WdDKJ4uouLTSwxM62Y70n1BF5ls9apFWiAt2ZSDz2kjfE1Ds4Gngo/w==", "35fecfcc-3bb3-4cfa-b389-e29e2ede1e8a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4711a300-711e-4132-86d4-cafd3f11deec"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b56c7e7d-908a-400e-bc70-20b8348e054d", "AQAAAAIAAYagAAAAENnW9Lqqioz4a7P5h+ftmTr1d7AcmhQWeqCafGfJkvlWhqhB0C2ywJramSuUSG6UxA==", "47043eed-c997-4bdb-be22-e9585e840f6c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("48245a9c-f2a5-4e8f-9554-b6acc9206d37"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "86b92d63-4845-4bda-b1f5-2748732cfeb6", "AQAAAAIAAYagAAAAEHhAcZDyk1KmEwRd+twNXJ6yHCWCDsw38jtX+MTpk/RmdUdt5WZAV46UZNdXvHsV+A==", "897045a9-a34e-42a1-9bee-97a94bb40c87" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b836e975-e775-48bc-8b84-5d2bdd5bd87a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "21b6dc1f-dce9-48af-88ae-32f06dc435e4", "AQAAAAIAAYagAAAAEEKKDiP8OUipbaXmWKmJuEX+pCCHblRGWpA57B4oK8j012tx1xrAvkBgDKCTU9t7mw==", "007a5b1f-95c7-4377-b0be-8f030591168b" });
        }
    }
}
