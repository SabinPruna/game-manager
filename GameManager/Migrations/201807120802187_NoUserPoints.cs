namespace GameManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoUserPoints : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Players", "Points");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Players", "Points", c => c.Int(nullable: false));
        }
    }
}
