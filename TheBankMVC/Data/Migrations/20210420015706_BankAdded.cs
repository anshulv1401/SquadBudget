using Microsoft.EntityFrameworkCore.Migrations;

namespace TheBankMVC.Data.Migrations
{
    public partial class BankAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bank",
                columns: table => new
                {
                    BankId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankName = table.Column<string>(nullable: true),
                    BankInstallmentAmount = table.Column<double>(nullable: false),
                    DefaultLoanInterest = table.Column<double>(nullable: false),
                    DefaultNoOfInstallment = table.Column<int>(nullable: false),
                    BankInstallmentDelayFine = table.Column<double>(nullable: false),
                    BankInstallmentDelayFineType = table.Column<int>(nullable: false),
                    BankInstallmentDelayFinePeriod = table.Column<int>(nullable: false),
                    LoanDelayFine = table.Column<double>(nullable: false),
                    LoanDelayFineType = table.Column<int>(nullable: false),
                    LoanDelayFinePeriod = table.Column<int>(nullable: false),
                    InterestTermID = table.Column<int>(nullable: false),
                    DateFormat = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bank", x => x.BankId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bank");
        }
    }
}
