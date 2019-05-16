namespace DejasList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TookOffConstructorFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Jobs", "ContractorId", "dbo.Contractors");
            DropIndex("dbo.Jobs", new[] { "ContractorId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Jobs", "ContractorId");
            AddForeignKey("dbo.Jobs", "ContractorId", "dbo.Contractors", "ContractorId", cascadeDelete: true);
        }
    }
}
