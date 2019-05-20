namespace DejasList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedprofileimagetoclient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "ProfilePhoto", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "ProfilePhoto");
        }
    }
}
