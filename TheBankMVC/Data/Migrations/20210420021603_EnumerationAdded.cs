using Microsoft.EntityFrameworkCore.Migrations;

namespace TheBankMVC.Data.Migrations
{
    public partial class EnumerationAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountID",
                table: "EMIHeaders",
                newName: "AccountId");

            migrationBuilder.AddColumn<int>(
                name: "BankId",
                table: "EMIHeaders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Enumerations",
                columns: table => new
                {
                    EnumerationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnumerationType = table.Column<string>(nullable: true),
                    EnumerationName = table.Column<string>(nullable: true),
                    EnumerationValue = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enumerations", x => x.EnumerationId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enumerations");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "EMIHeaders");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "EMIHeaders",
                newName: "AccountID");
        }
    }
}
