namespace Gamification.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userTitle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserQuizDatas", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserQuizDatas", "Title");
        }
    }
}
