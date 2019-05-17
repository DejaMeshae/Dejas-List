namespace DejasList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class latLng : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "Lat", c => c.String());
            AddColumn("dbo.Jobs", "Lng", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "Lng");
            DropColumn("dbo.Jobs", "Lat");
        }
    }
}
