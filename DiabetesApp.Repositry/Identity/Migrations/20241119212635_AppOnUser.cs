using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiabetesApp.Repositry.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AppOnUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HospitailId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HospitalId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Hospitail",
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
                    table.PrimaryKey("PK_Hospitail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
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
                    LatestHealthStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HospitalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patient_Hospitail_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhysiologicalIndicators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time", nullable: false),
                    HealthStatusScore = table.Column<double>(type: "float", nullable: true),
                    HealthStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GlucoseLevel = table.Column<double>(type: "float", nullable: false),
                    BloodPressure = table.Column<double>(type: "float", nullable: false),
                    Temperature = table.Column<double>(type: "float", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysiologicalIndicators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhysiologicalIndicators_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_HospitailId",
                table: "AspNetUsers",
                column: "HospitailId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_HospitalId",
                table: "Patient",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysiologicalIndicators_PatientId",
                table: "PhysiologicalIndicators",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Hospitail_HospitailId",
                table: "AspNetUsers",
                column: "HospitailId",
                principalTable: "Hospitail",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Hospitail_HospitailId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "PhysiologicalIndicators");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "Hospitail");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_HospitailId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HospitailId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HospitalId",
                table: "AspNetUsers");
        }
    }
}
