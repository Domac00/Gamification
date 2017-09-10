namespace Gamification.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NumberOfCompletedTutorials : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserQuizDatas", "NumberOfCompletedTutorials", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserQuizDatas", "NumberOfCompletedTutorials");
        }
    }
}
