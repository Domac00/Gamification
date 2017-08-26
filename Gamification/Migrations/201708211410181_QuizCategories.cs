namespace Gamification.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuizCategories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuizCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Quizs", "QuizCategory_Id", c => c.Int());
            CreateIndex("dbo.Quizs", "QuizCategory_Id");
            AddForeignKey("dbo.Quizs", "QuizCategory_Id", "dbo.QuizCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Quizs", "QuizCategory_Id", "dbo.QuizCategories");
            DropIndex("dbo.Quizs", new[] { "QuizCategory_Id" });
            DropColumn("dbo.Quizs", "QuizCategory_Id");
            DropTable("dbo.QuizCategories");
        }
    }
}
