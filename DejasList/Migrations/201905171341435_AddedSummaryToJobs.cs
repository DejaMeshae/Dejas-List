namespace DejasList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSummaryToJobs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "Summary", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "Summary");
        }
    }
}
