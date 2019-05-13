namespace DejasList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedContractorsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contractors",
                c => new
                    {
                        ContractorId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        Zipcode = c.String(),
                        State = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.ContractorId);
            
            AddColumn("dbo.Jobs", "TypeOfProject", c => c.String());
            AddColumn("dbo.Jobs", "SizeOfProject", c => c.String());
            AddColumn("dbo.Jobs", "Budget", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "Budget");
            DropColumn("dbo.Jobs", "SizeOfProject");
            DropColumn("dbo.Jobs", "TypeOfProject");
            DropTable("dbo.Contractors");
        }
    }
}
