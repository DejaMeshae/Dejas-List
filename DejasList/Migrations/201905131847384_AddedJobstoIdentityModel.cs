namespace DejasList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedJobstoIdentityModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        JobsId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.JobsId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Jobs");
        }
    }
}
