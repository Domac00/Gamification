namespace Gamification.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dodavanjeocjenekviza : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quizs", "SumOfGrades", c => c.Single(nullable: false));
            AddColumn("dbo.Quizs", "NumberOfRateings", c => c.Int(nullable: false));
            AddColumn("dbo.Quizs", "Rating", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quizs", "Rating");
            DropColumn("dbo.Quizs", "NumberOfRateings");
            DropColumn("dbo.Quizs", "SumOfGrades");
        }
    }
}
