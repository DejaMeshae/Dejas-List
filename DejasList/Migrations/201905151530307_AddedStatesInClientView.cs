namespace DejasList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedStatesInClientView : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "State", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "State");
        }
    }
}
