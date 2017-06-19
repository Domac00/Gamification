namespace Gamification.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodavanjeUserNameuUserQuizData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserQuizDatas", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserQuizDatas", "UserName");
        }
    }
}
