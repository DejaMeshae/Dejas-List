namespace DejasList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedAboutMepropertytoclient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "AboutMe", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "AboutMe");
        }
    }
}
