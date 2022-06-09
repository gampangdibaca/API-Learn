using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class remove_PK_from_account_role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRole_Accounts_NIK",
                table: "AccountRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountRole",
                table: "AccountRole");

            migrationBuilder.DropIndex(
                name: "IX_AccountRole_NIK",
                table: "AccountRole");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AccountRole");

            migrationBuilder.AlterColumn<string>(
                name: "NIK",
                table: "AccountRole",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountRole",
                table: "AccountRole",
                columns: new[] { "NIK", "Roles_Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRole_Accounts_NIK",
                table: "AccountRole",
                column: "NIK",
                principalTable: "Accounts",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRole_Accounts_NIK",
                table: "AccountRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountRole",
                table: "AccountRole");

            migrationBuilder.AlterColumn<string>(
                name: "NIK",
                table: "AccountRole",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AccountRole",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountRole",
                table: "AccountRole",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AccountRole_NIK",
                table: "AccountRole",
                column: "NIK");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRole_Accounts_NIK",
                table: "AccountRole",
                column: "NIK",
                principalTable: "Accounts",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
