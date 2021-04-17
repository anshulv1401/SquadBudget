namespace TheBankMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EMIHeaders",
                c => new
                    {
                        EMIHeaderId = c.Int(nullable: false, identity: true),
                        EMIAmount = c.Double(nullable: false),
                        LoanAmount = c.Double(nullable: false),
                        MonthlyRateOfInterest = c.Double(nullable: false),
                        NoOfInstallment = c.Int(nullable: false),
                        LockInPeriod = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        DateFormat = c.String(),
                    })
                .PrimaryKey(t => t.EMIHeaderId);
            
            CreateTable(
                "dbo.Installments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EMIHeaderId = c.Int(nullable: false),
                        InstallmentNo = c.Int(nullable: false),
                        DateOfInstallment = c.DateTime(nullable: false),
                        Opening = c.Double(nullable: false),
                        PrincipalAmount = c.Double(nullable: false),
                        InterestAmount = c.Double(nullable: false),
                        EMIAmount = c.Double(nullable: false),
                        Closing = c.Double(nullable: false),
                        Difference = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Installments");
            DropTable("dbo.EMIHeaders");
        }
    }
}
