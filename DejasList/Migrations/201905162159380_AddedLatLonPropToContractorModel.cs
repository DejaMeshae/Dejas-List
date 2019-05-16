namespace DejasList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLatLonPropToContractorModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contractors", "Lat", c => c.String());
            AddColumn("dbo.Contractors", "Lng", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contractors", "Lng");
            DropColumn("dbo.Contractors", "Lat");
        }
    }
}
