using Microsoft.EntityFrameworkCore.Migrations;

namespace TheBankMVC.Data.Migrations
{
    public partial class SeedEnumerations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"INSERT INTO[dbo].[Enumerations]([EnumerationType],[EnumerationName],[EnumerationValue]) VALUES
                ('TransactionType', 'Credit', 1)
                ,('TransactionType', 'Debit', 2)
                ,('CreditType', 'LoanEMI', 1)
                ,('CreditType', 'BankEMI', 2)
                ,('DebitType', 'Loan', 1)
                ,('DebitType', 'Bank', 2)
                ,('UserType', 'Admin', 1)
                ,('UserType', 'Member', 2)
                ,('DelayFineType', 'Amount', 1)
                ,('DelayFineType', 'Percentage', 2)
                ,('DelayFineTerm', 'PerDay', 1)
                ,('DelayFineTerm', 'PerMonth', 2)
                ,('InterestTerm', 'FixedEMI', 1)
                ,('InterestTerm', 'FixedPrincipal', 2)
                ,('InterestTerm', 'BeforeLoan', 3)
                ,('InstallmentStatus', 'Due', 1)
                ,('InstallmentStatus', 'Paid', 2)
                ,('InstallmentStatus', 'Late', 3)
                ,('LoanStatus', 'Pending', 1)
                ,('LoanStatus', 'Completed', 2)"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
