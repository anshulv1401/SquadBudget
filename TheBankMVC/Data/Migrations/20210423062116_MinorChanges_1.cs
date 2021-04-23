using Microsoft.EntityFrameworkCore.Migrations;

namespace TheBankMVC.Data.Migrations
{
    public partial class MinorChanges_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserAccount");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "UserAccount");

            migrationBuilder.AddColumn<string>(
                name: "UserAccountName",
                table: "UserAccount",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "BankUserMappings",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserAccountName",
                table: "UserAccount");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserAccount",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "UserAccount",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "BankUserMappings",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
