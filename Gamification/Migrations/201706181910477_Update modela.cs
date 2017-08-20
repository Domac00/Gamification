namespace Gamification.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatemodela : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Achievements", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Achievements", "UserId", c => c.String());
        }
    }
}
