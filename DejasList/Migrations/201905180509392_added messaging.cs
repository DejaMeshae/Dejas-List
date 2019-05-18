namespace DejasList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedmessaging : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        _Message = c.String(),
                        ContractorId = c.Int(nullable: false),
                        Jobs_JobsId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contractors", t => t.ContractorId, cascadeDelete: true)
                .ForeignKey("dbo.Jobs", t => t.Jobs_JobsId)
                .Index(t => t.ContractorId)
                .Index(t => t.Jobs_JobsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "Jobs_JobsId", "dbo.Jobs");
            DropForeignKey("dbo.Messages", "ContractorId", "dbo.Contractors");
            DropIndex("dbo.Messages", new[] { "Jobs_JobsId" });
            DropIndex("dbo.Messages", new[] { "ContractorId" });
            DropTable("dbo.Messages");
        }
    }
}
