namespace TheBankMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EMIHeaders", "LoanStatus", c => c.Int(nullable: false));
            AddColumn("dbo.Installments", "DueDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Installments", "PaymentDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Installments", "InstallmentStatus", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "DrivingLicense", c => c.String(maxLength: 255));
            AlterColumn("dbo.AspNetUsers", "PhoneNo", c => c.String(maxLength: 50));
            DropColumn("dbo.EMIHeaders", "DateFormat");
            DropColumn("dbo.Installments", "DateOfInstallment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Installments", "DateOfInstallment", c => c.DateTime(nullable: false));
            AddColumn("dbo.EMIHeaders", "DateFormat", c => c.String());
            AlterColumn("dbo.AspNetUsers", "PhoneNo", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.AspNetUsers", "DrivingLicense", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.Installments", "InstallmentStatus");
            DropColumn("dbo.Installments", "PaymentDate");
            DropColumn("dbo.Installments", "DueDate");
            DropColumn("dbo.EMIHeaders", "LoanStatus");
        }
    }
}
