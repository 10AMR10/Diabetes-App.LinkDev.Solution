using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiabetesApp.Repositry.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddHealthStatusTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "HealthStatusScore",
                table: "physiologicalIndicators",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LatestHealthStatus",
                table: "patients",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HealthStatusScore",
                table: "physiologicalIndicators");

            migrationBuilder.DropColumn(
                name: "LatestHealthStatus",
                table: "patients");
        }
    }
}
