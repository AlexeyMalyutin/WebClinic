using Microsoft.EntityFrameworkCore.Migrations;

namespace ClinicWEB.Migrations
{
    public partial class FinalFinalFinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Doctors_DoctorID",
                table: "Registrations");

            migrationBuilder.RenameColumn(
                name: "DoctorID",
                table: "Registrations",
                newName: "DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Registrations_DoctorID",
                table: "Registrations",
                newName: "IX_Registrations_DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Doctors_DoctorId",
                table: "Registrations",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Doctors_DoctorId",
                table: "Registrations");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "Registrations",
                newName: "DoctorID");

            migrationBuilder.RenameIndex(
                name: "IX_Registrations_DoctorId",
                table: "Registrations",
                newName: "IX_Registrations_DoctorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Doctors_DoctorID",
                table: "Registrations",
                column: "DoctorID",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
