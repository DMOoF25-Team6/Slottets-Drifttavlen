using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class NormalizedUserName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("30cffcf9-5784-4fa9-9c10-c013ef3faf16"),
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "3ff4376f-b0ea-41f5-918b-f9da48137c89", "ThorDanrsøn@example.com", "THORDANRSØN@EXAMPLE.COM", "THORDANRSØN@EXAMPLE.COM", "AQAAAAIAAYagAAAAECATdRwOyZZcfrVbyq/dq1+huJQw67dUhgDWMScikDMs+qdP0NUC6XFWJ6fttnPKLg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("37155b80-7111-422a-aba6-89d7070f1644"),
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "770cc175-a571-4d1e-ac5f-83bda541d05b", "PerNielsen@example.com", "PERNIELSEN@EXAMPLE.COM", "PERNIELSEN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEGHkTkzU/zdlwTEwIuSdGLIregR3ZdvB4ewMn+sMd046mdIsrEZ3b7yXNjgguVCaAQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3a21f8e1-885b-4394-abf0-ed0baeea239b"),
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "c0a8de98-72fc-4e74-a74b-9a9d1ade23d2", "PederRasmussen@example.com", "PEDERRASMUSSEN@EXAMPLE.COM", "PEDERRASMUSSEN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEAzXP4vRpawd3y9YDIrB1jmIDhRCsI885hy9jup5IpEdXfisqEE3qJ65KMKAE72bSQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4711a300-711e-4132-86d4-cafd3f11deec"),
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "6305cea9-712a-4b84-8ab9-086f2902242e", "SanneJohansen@example.com", "SANNEJOHANSEN@EXAMPLE.COM", "SANNEJOHANSEN@EXAMPLE.COM", "AQAAAAIAAYagAAAAENsAahI0ZU7aS/TvmVM1DIuWb9Es5q5EsPhtLL3G9jGsgnLwLWmEuQrGvqnaCBUf7w==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("48245a9c-f2a5-4e8f-9554-b6acc9206d37"),
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "94ed32c5-60d9-4486-aa29-241c84b6080b", "KasperHolm@example.com", "KASPERHOLM@EXAMPLE.COM", "KASPERHOLM@EXAMPLE.COM", "AQAAAAIAAYagAAAAELv2mMfYgroVccFldkLmvuGF0mgXPtsdgw8IfRUyObpUcyYNEnCPHEHX+ALvm2oqhA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b836e975-e775-48bc-8b84-5d2bdd5bd87a"),
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "1dd96a17-8933-4908-a4bb-531a0907577f", "AndersJensen@example.com", "ANDERSJENSEN@EXAMPLE.COM", "ANDERSJENSEN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEL+x62yuIxBr4jap3O/HovRFLKLWQxQg24T8Ae0BHymQ0rCAs7+ZOtq15oJ5gfg9Vw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("30cffcf9-5784-4fa9-9c10-c013ef3faf16"),
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "57a10480-7b7c-4379-a459-ceada1635cce", null, null, null, "AQAAAAIAAYagAAAAENf0vVLiDQXG70ILDUt8VfYLzAx2H9SydSYYDwgye0KUGV6b26w9J/DjMw0QeFxrqA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("37155b80-7111-422a-aba6-89d7070f1644"),
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "96f0cdd8-6f5b-49ae-9282-68949e93cac6", null, null, null, "AQAAAAIAAYagAAAAEP7PLnBt+7zceoVQ3ha2nW7MtqR2ZSVvIIBG+bp8Dvmze06+u0IbonkL35Ax+4A0Jg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3a21f8e1-885b-4394-abf0-ed0baeea239b"),
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "6aded8b7-18dc-418c-90f6-2376cb5e44a1", null, null, null, "AQAAAAIAAYagAAAAELevKvgWaL3+l924sqZ5eD85dZvd8z/+us05btKintElFQy19hQVXs9iWgYpM7RZaQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4711a300-711e-4132-86d4-cafd3f11deec"),
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "1d2be799-94ad-468d-a3a5-796c2d1de679", null, null, null, "AQAAAAIAAYagAAAAEEujNoZRbF6b5TkjKN8hjqVUaySJYdPQ+Eiq6za74GB4m0bpbLoMQrAKNjuiyLqvkw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("48245a9c-f2a5-4e8f-9554-b6acc9206d37"),
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "ae2df830-7730-4966-bcfc-d3c6ed9266c4", null, null, null, "AQAAAAIAAYagAAAAEBFopGCfuONAAheg8qAaNxOVlp25473iel+xFlix2jz0NvD9L91GtzbV2UTnQncpPQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b836e975-e775-48bc-8b84-5d2bdd5bd87a"),
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "0b8dcf1a-cb5a-448e-909c-c87029185002", null, null, null, "AQAAAAIAAYagAAAAEMKVxLy7OfFBDJukTU16SMcBlwF0/LpFUG1yE891hVBV560J2YdiCuIFoXuvBEOn1g==" });
        }
    }
}
