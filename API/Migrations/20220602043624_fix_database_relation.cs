using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class fix_database_relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Profilings_NIK",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Accounts_NIK",
                table: "Employees");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Employees_NIK",
                table: "Accounts",
                column: "NIK",
                principalTable: "Employees",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profilings_Accounts_NIK",
                table: "Profilings",
                column: "NIK",
                principalTable: "Accounts",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Employees_NIK",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Profilings_Accounts_NIK",
                table: "Profilings");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Profilings_NIK",
                table: "Accounts",
                column: "NIK",
                principalTable: "Profilings",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Accounts_NIK",
                table: "Employees",
                column: "NIK",
                principalTable: "Accounts",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
