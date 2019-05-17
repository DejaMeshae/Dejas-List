namespace DejasList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newregistar : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Contractors", "Background");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contractors", "Background", c => c.Boolean(nullable: false));
        }
    }
}
