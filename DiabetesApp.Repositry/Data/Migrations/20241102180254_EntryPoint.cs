using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiabetesApp.Repositry.Data.Migrations
{
    /// <inheritdoc />
    public partial class EntryPoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "hospitals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hospitals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthCount = table.Column<int>(type: "int", nullable: false),
                    IsPregnant = table.Column<bool>(type: "bit", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HospitalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_patients_hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "physiologicalIndicators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time", nullable: false),
                    HealthStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GlucoseLevel = table.Column<double>(type: "float", nullable: false),
                    BloodPressure = table.Column<double>(type: "float", nullable: false),
                    Temperature = table.Column<double>(type: "float", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_physiologicalIndicators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_physiologicalIndicators_patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_patients_HospitalId",
                table: "patients",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_physiologicalIndicators_PatientId",
                table: "physiologicalIndicators",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "physiologicalIndicators");

            migrationBuilder.DropTable(
                name: "patients");

            migrationBuilder.DropTable(
                name: "hospitals");
        }
    }
}
