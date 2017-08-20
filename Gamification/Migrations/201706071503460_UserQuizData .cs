namespace Gamification.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserQuizData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserQuizDatas",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        xp = c.Int(nullable: false),
                        NumberOfSolvedQuizes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserQuizDatas");
        }
    }
}
