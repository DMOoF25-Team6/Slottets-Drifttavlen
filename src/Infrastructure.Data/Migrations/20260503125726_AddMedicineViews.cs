using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMedicineViews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql("""
                       DROP VIEW IF EXISTS medicinestatusview;
""");

            migrationBuilder.Sql("""
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

            migrationBuilder.Sql("""
DROP VIEW IF EXISTS painkillerstatusview;
""");

            migrationBuilder.Sql("""
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

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS medicinestatusview;");
            migrationBuilder.Sql("DROP VIEW IF EXISTS painkillerstatusview;");
        }
    }
}
