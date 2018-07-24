namespace GameManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoneyFunctionality : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "Money", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Players", "Money");
        }
    }
}
