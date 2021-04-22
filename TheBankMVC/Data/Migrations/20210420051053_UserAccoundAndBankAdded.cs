using Microsoft.EntityFrameworkCore.Migrations;

namespace TheBankMVC.Data.Migrations
{
    public partial class UserAccoundAndBankAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankInstallmentDelayFinePeriod",
                table: "Bank");

            migrationBuilder.DropColumn(
                name: "LoanDelayFinePeriod",
                table: "Bank");

            migrationBuilder.AddColumn<int>(
                name: "BankInstallmentDelayFineTerm",
                table: "Bank",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LoanDelayFineTerm",
                table: "Bank",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserAccount",
                columns: table => new
                {
                    UserAccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNo = table.Column<string>(nullable: true),
                    ShareSubmitted = table.Column<double>(nullable: false),
                    FineSubmitted = table.Column<double>(nullable: false),
                    InterestSubmitted = table.Column<double>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    AmountOnLoan = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccount", x => x.UserAccountId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAccount");

            migrationBuilder.DropColumn(
                name: "BankInstallmentDelayFineTerm",
                table: "Bank");

            migrationBuilder.DropColumn(
                name: "LoanDelayFineTerm",
                table: "Bank");

            migrationBuilder.AddColumn<int>(
                name: "BankInstallmentDelayFinePeriod",
                table: "Bank",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LoanDelayFinePeriod",
                table: "Bank",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
