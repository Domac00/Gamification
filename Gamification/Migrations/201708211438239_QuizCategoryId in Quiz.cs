namespace Gamification.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuizCategoryIdinQuiz : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Quizs", "QuizCategory_Id", "dbo.QuizCategories");
            DropIndex("dbo.Quizs", new[] { "QuizCategory_Id" });
            RenameColumn(table: "dbo.Quizs", name: "QuizCategory_Id", newName: "QuizCategoryId");
            AlterColumn("dbo.Quizs", "QuizCategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Quizs", "QuizCategoryId");
            AddForeignKey("dbo.Quizs", "QuizCategoryId", "dbo.QuizCategories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Quizs", "QuizCategoryId", "dbo.QuizCategories");
            DropIndex("dbo.Quizs", new[] { "QuizCategoryId" });
            AlterColumn("dbo.Quizs", "QuizCategoryId", c => c.Int());
            RenameColumn(table: "dbo.Quizs", name: "QuizCategoryId", newName: "QuizCategory_Id");
            CreateIndex("dbo.Quizs", "QuizCategory_Id");
            AddForeignKey("dbo.Quizs", "QuizCategory_Id", "dbo.QuizCategories", "Id");
        }
    }
}
