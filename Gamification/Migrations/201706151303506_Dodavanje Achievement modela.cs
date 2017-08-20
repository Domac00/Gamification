namespace Gamification.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodavanjeAchievementmodela : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Achievements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserQuizDataAchievements",
                c => new
                    {
                        UserQuizData_UserId = c.String(nullable: false, maxLength: 128),
                        Achievement_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserQuizData_UserId, t.Achievement_Id })
                .ForeignKey("dbo.UserQuizDatas", t => t.UserQuizData_UserId, cascadeDelete: true)
                .ForeignKey("dbo.Achievements", t => t.Achievement_Id, cascadeDelete: true)
                .Index(t => t.UserQuizData_UserId)
                .Index(t => t.Achievement_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserQuizDataAchievements", "Achievement_Id", "dbo.Achievements");
            DropForeignKey("dbo.UserQuizDataAchievements", "UserQuizData_UserId", "dbo.UserQuizDatas");
            DropIndex("dbo.UserQuizDataAchievements", new[] { "Achievement_Id" });
            DropIndex("dbo.UserQuizDataAchievements", new[] { "UserQuizData_UserId" });
            DropTable("dbo.UserQuizDataAchievements");
            DropTable("dbo.Achievements");
        }
    }
}
