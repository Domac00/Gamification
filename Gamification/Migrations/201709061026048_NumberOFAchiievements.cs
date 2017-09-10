namespace Gamification.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NumberOFAchiievements : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserQuizDatas", "NumberOfAchievements", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserQuizDatas", "NumberOfAchievements");
        }
    }
}
