namespace TheBankMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BankConfig_Transaction_UserAccount_Enum_ModelsAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EMIHeaders", "AccountID", c => c.Int(nullable: false));
            AddColumn("dbo.EMIHeaders", "DelayFine", c => c.Double(nullable: false));
            AddColumn("dbo.EMIHeaders", "DelayFineType", c => c.Int(nullable: false));
            AddColumn("dbo.EMIHeaders", "DelayFinePeriod", c => c.Int(nullable: false));
            AddColumn("dbo.EMIHeaders", "InterestTermId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EMIHeaders", "InterestTermId");
            DropColumn("dbo.EMIHeaders", "DelayFinePeriod");
            DropColumn("dbo.EMIHeaders", "DelayFineType");
            DropColumn("dbo.EMIHeaders", "DelayFine");
            DropColumn("dbo.EMIHeaders", "AccountID");
        }
    }
}
