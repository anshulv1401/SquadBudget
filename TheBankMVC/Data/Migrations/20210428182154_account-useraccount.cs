using Microsoft.EntityFrameworkCore.Migrations;

namespace TheBankMVC.Data.Migrations
{
    public partial class accountuseraccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "EMIHeaders");

            migrationBuilder.AddColumn<int>(
                name: "UserAccountId",
                table: "Transaction",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserAccountId",
                table: "EMIHeaders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "EMIHeaders");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Transaction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "EMIHeaders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
