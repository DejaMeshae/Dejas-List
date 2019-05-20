namespace DejasList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class job : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "Name");
        }
    }
}
