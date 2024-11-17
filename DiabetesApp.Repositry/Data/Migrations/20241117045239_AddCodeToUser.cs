using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiabetesApp.Repositry.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCodeToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "patients",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "patients");
        }
    }
}
