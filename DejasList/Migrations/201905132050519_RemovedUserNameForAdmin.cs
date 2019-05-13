namespace DejasList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedUserNameForAdmin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "ApplicationUserId", c => c.String(maxLength: 128));
            AddColumn("dbo.Contractors", "ApplicationUserId", c => c.String(maxLength: 128));
            AddColumn("dbo.Jobs", "ClientId", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "ContractorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Clients", "ApplicationUserId");
            CreateIndex("dbo.Contractors", "ApplicationUserId");
            CreateIndex("dbo.Jobs", "ClientId");
            CreateIndex("dbo.Jobs", "ContractorId");
            AddForeignKey("dbo.Clients", "ApplicationUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Contractors", "ApplicationUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Jobs", "ClientId", "dbo.Clients", "ClientId", cascadeDelete: true);
            AddForeignKey("dbo.Jobs", "ContractorId", "dbo.Contractors", "ContractorId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "ContractorId", "dbo.Contractors");
            DropForeignKey("dbo.Jobs", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Contractors", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Clients", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Jobs", new[] { "ContractorId" });
            DropIndex("dbo.Jobs", new[] { "ClientId" });
            DropIndex("dbo.Contractors", new[] { "ApplicationUserId" });
            DropIndex("dbo.Clients", new[] { "ApplicationUserId" });
            DropColumn("dbo.Jobs", "ContractorId");
            DropColumn("dbo.Jobs", "ClientId");
            DropColumn("dbo.Contractors", "ApplicationUserId");
            DropColumn("dbo.Clients", "ApplicationUserId");
        }
    }
}
