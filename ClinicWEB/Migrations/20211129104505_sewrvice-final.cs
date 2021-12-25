using Microsoft.EntityFrameworkCore.Migrations;

namespace ClinicWEB.Migrations
{
    public partial class sewrvicefinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sell_Patients_PatientId",
                table: "Sell");

            migrationBuilder.DropForeignKey(
                name: "FK_Sell_Service_ServiceId",
                table: "Sell");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Service",
                table: "Service");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sell",
                table: "Sell");

            migrationBuilder.RenameTable(
                name: "Service",
                newName: "Services");

            migrationBuilder.RenameTable(
                name: "Sell",
                newName: "Sells");

            migrationBuilder.RenameIndex(
                name: "IX_Sell_ServiceId",
                table: "Sells",
                newName: "IX_Sells_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Sell_PatientId",
                table: "Sells",
                newName: "IX_Sells_PatientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Services",
                table: "Services",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sells",
                table: "Sells",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sells_Patients_PatientId",
                table: "Sells",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sells_Services_ServiceId",
                table: "Sells",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sells_Patients_PatientId",
                table: "Sells");

            migrationBuilder.DropForeignKey(
                name: "FK_Sells_Services_ServiceId",
                table: "Sells");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Services",
                table: "Services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sells",
                table: "Sells");

            migrationBuilder.RenameTable(
                name: "Services",
                newName: "Service");

            migrationBuilder.RenameTable(
                name: "Sells",
                newName: "Sell");

            migrationBuilder.RenameIndex(
                name: "IX_Sells_ServiceId",
                table: "Sell",
                newName: "IX_Sell_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Sells_PatientId",
                table: "Sell",
                newName: "IX_Sell_PatientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Service",
                table: "Service",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sell",
                table: "Sell",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sell_Patients_PatientId",
                table: "Sell",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sell_Service_ServiceId",
                table: "Sell",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
