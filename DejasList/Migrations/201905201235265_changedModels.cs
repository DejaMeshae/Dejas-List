namespace DejasList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Clients", "EmailId", "dbo.Emails");
            DropForeignKey("dbo.Contractors", "EmailId", "dbo.Emails");
            DropIndex("dbo.Clients", new[] { "EmailId" });
            DropIndex("dbo.Contractors", new[] { "EmailId" });
            AddColumn("dbo.Jobs", "EmailId", c => c.Int(nullable: false));
            CreateIndex("dbo.Jobs", "EmailId");
            AddForeignKey("dbo.Jobs", "EmailId", "dbo.Emails", "EmailId", cascadeDelete: true);
            DropColumn("dbo.Clients", "EmailId");
            DropColumn("dbo.Contractors", "EmailId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contractors", "EmailId", c => c.Int(nullable: false));
            AddColumn("dbo.Clients", "EmailId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Jobs", "EmailId", "dbo.Emails");
            DropIndex("dbo.Jobs", new[] { "EmailId" });
            DropColumn("dbo.Jobs", "EmailId");
            CreateIndex("dbo.Contractors", "EmailId");
            CreateIndex("dbo.Clients", "EmailId");
            AddForeignKey("dbo.Contractors", "EmailId", "dbo.Emails", "EmailId", cascadeDelete: true);
            AddForeignKey("dbo.Clients", "EmailId", "dbo.Emails", "EmailId", cascadeDelete: true);
        }
    }
}
