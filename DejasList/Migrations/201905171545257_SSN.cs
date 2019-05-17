namespace DejasList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SSN : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contractors", "SocialNumber", c => c.Long(nullable: false));
            AddColumn("dbo.Contractors", "DateOfBirth", c => c.DateTime(nullable: false));
            AddColumn("dbo.Contractors", "Background", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contractors", "Background");
            DropColumn("dbo.Contractors", "DateOfBirth");
            DropColumn("dbo.Contractors", "SocialNumber");
        }
    }
}
