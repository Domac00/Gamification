namespace Gamification.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class achievementCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AchievementCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Achievements", "AchievementCategory_Id", c => c.Int());
            CreateIndex("dbo.Achievements", "AchievementCategory_Id");
            AddForeignKey("dbo.Achievements", "AchievementCategory_Id", "dbo.AchievementCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Achievements", "AchievementCategory_Id", "dbo.AchievementCategories");
            DropIndex("dbo.Achievements", new[] { "AchievementCategory_Id" });
            DropColumn("dbo.Achievements", "AchievementCategory_Id");
            DropTable("dbo.AchievementCategories");
        }
    }
}
