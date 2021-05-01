using Microsoft.EntityFrameworkCore.Migrations;

namespace TheBankMVC.Data.Migrations
{
    public partial class InstallmentUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BankId",
                table: "Installments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Fine",
                table: "Installments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "UserAccountId",
                table: "Installments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankId",
                table: "Installments");

            migrationBuilder.DropColumn(
                name: "Fine",
                table: "Installments");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "Installments");
        }
    }
}
