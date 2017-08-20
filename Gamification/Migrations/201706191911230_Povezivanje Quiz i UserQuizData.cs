namespace Gamification.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PovezivanjeQuiziUserQuizData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserQuizDataQuizs",
                c => new
                    {
                        UserQuizData_UserId = c.String(nullable: false, maxLength: 128),
                        Quiz_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserQuizData_UserId, t.Quiz_Id })
                .ForeignKey("dbo.UserQuizDatas", t => t.UserQuizData_UserId, cascadeDelete: true)
                .ForeignKey("dbo.Quizs", t => t.Quiz_Id, cascadeDelete: true)
                .Index(t => t.UserQuizData_UserId)
                .Index(t => t.Quiz_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserQuizDataQuizs", "Quiz_Id", "dbo.Quizs");
            DropForeignKey("dbo.UserQuizDataQuizs", "UserQuizData_UserId", "dbo.UserQuizDatas");
            DropIndex("dbo.UserQuizDataQuizs", new[] { "Quiz_Id" });
            DropIndex("dbo.UserQuizDataQuizs", new[] { "UserQuizData_UserId" });
            DropTable("dbo.UserQuizDataQuizs");
        }
    }
}
