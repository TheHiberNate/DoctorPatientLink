using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorLink.Migrations
{
    /// <inheritdoc />
    public partial class changeDbStructureForPatients2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Patients",
                newName: "PatientId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Medication",
                newName: "MedicationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Patients",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "MedicationId",
                table: "Medication",
                newName: "Id");
        }
    }
}
