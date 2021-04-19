using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheBankMVC.Data.Migrations
{
    public partial class initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EMIHeaders",
                columns: table => new
                {
                    EMIHeaderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountID = table.Column<int>(nullable: false),
                    EMIAmount = table.Column<double>(nullable: false),
                    LoanAmount = table.Column<double>(nullable: false),
                    MonthlyRateOfInterest = table.Column<double>(nullable: false),
                    NoOfInstallment = table.Column<int>(nullable: false),
                    LockInPeriod = table.Column<int>(nullable: false),
                    LoanStatus = table.Column<int>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    DelayFine = table.Column<double>(nullable: false),
                    DelayFineType = table.Column<int>(nullable: false),
                    DelayFinePeriod = table.Column<int>(nullable: false),
                    InterestTermId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMIHeaders", x => x.EMIHeaderId);
                });

            migrationBuilder.CreateTable(
                name: "Installments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EMIHeaderId = table.Column<int>(nullable: false),
                    InstallmentNo = table.Column<int>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: true),
                    Opening = table.Column<double>(nullable: false),
                    PrincipalAmount = table.Column<double>(nullable: false),
                    InterestAmount = table.Column<double>(nullable: false),
                    EMIAmount = table.Column<double>(nullable: false),
                    Closing = table.Column<double>(nullable: false),
                    Difference = table.Column<double>(nullable: false),
                    InstallmentStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Installments", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EMIHeaders");

            migrationBuilder.DropTable(
                name: "Installments");
        }
    }
}
