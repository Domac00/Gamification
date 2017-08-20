namespace Gamification.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodavanjeQuizLeveluQuizklasu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quizs", "QuizLevel", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quizs", "QuizLevel");
        }
    }
}
