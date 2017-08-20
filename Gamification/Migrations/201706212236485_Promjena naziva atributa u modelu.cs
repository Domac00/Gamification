namespace Gamification.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Promjenanazivaatributaumodelu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quizs", "NumberOfRatings", c => c.Int(nullable: false));
            DropColumn("dbo.Quizs", "NumberOfRateings");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Quizs", "NumberOfRateings", c => c.Int(nullable: false));
            DropColumn("dbo.Quizs", "NumberOfRatings");
        }
    }
}
