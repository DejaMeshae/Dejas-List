namespace DejasList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contractors", "SocialNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contractors", "SocialNumber", c => c.Long(nullable: false));
        }
    }
}
