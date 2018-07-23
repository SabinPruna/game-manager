namespace GameManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserPicture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "UserPicture", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Players", "UserPicture");
        }
    }
}
