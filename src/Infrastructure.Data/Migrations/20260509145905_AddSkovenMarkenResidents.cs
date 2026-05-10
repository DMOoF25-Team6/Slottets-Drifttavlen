using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSkovenMarkenResidents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // This migration currently has no schema or data changes.
            // Keep an explicit no-op operation so the migration is not silently empty.
            // If Skoven/Marken resident seed data should be introduced, replace this
            // with InsertData operations and update the model snapshot accordingly.
            migrationBuilder.Sql("-- No-op migration: AddSkovenMarkenResidents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Reverse of the explicit no-op above.
            // If seed data is later added to Up, replace this with the matching DeleteData operations.
            migrationBuilder.Sql("-- No-op rollback: AddSkovenMarkenResidents");
        }
    }
}
