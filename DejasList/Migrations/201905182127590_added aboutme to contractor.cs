namespace DejasList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedaboutmetocontractor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contractors", "AboutMe", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contractors", "AboutMe");
        }
    }
}
