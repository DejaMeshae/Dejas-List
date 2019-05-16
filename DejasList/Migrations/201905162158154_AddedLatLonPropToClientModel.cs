namespace DejasList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLatLonPropToClientModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "Lat", c => c.String());
            AddColumn("dbo.Clients", "Lng", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "Lng");
            DropColumn("dbo.Clients", "Lat");
        }
    }
}
