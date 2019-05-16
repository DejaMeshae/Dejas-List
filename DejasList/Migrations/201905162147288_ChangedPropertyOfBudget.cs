namespace DejasList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedPropertyOfBudget : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Jobs", "Budget", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jobs", "Budget", c => c.Double(nullable: false));
        }
    }
}
