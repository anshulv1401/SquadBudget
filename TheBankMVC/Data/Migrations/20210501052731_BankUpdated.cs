using Microsoft.EntityFrameworkCore.Migrations;

namespace TheBankMVC.Data.Migrations
{
    public partial class BankUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EMIType",
                table: "EMIHeaders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "InstallmentDayOfMonth",
                table: "EMIHeaders",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "InstallmentDayOfMonth",
                table: "Bank",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EMIType",
                table: "EMIHeaders");

            migrationBuilder.DropColumn(
                name: "InstallmentDayOfMonth",
                table: "EMIHeaders");

            migrationBuilder.DropColumn(
                name: "InstallmentDayOfMonth",
                table: "Bank");
        }
    }
}
