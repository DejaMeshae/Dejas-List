namespace DejasList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TheModelBackingTheApplicationDbContextContextHasChangedSinceTheDatabasWasCreated : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Clients", "Email");
            DropColumn("dbo.Contractors", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contractors", "Email", c => c.String());
            AddColumn("dbo.Clients", "Email", c => c.String());
        }
    }
}
