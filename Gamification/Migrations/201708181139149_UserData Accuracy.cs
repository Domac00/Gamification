namespace Gamification.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserDataAccuracy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserQuizDatas", "Accuracy", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserQuizDatas", "Accuracy");
        }
    }
}
