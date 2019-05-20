namespace DejasList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Jobs", "EmailId", "dbo.Emails");
            DropIndex("dbo.Jobs", new[] { "EmailId" });
            DropColumn("dbo.Jobs", "EmailId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "EmailId", c => c.Int(nullable: false));
            CreateIndex("dbo.Jobs", "EmailId");
            AddForeignKey("dbo.Jobs", "EmailId", "dbo.Emails", "EmailId", cascadeDelete: true);
        }
    }
}
