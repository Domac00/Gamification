namespace Gamification.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodavanjeUserLevela : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserQuizDatas", "UserLevel", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserQuizDatas", "UserLevel");
        }
    }
}
