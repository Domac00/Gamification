namespace Gamification.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class achCategoryId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Achievements", "AchievementCategory_Id", "dbo.AchievementCategories");
            DropIndex("dbo.Achievements", new[] { "AchievementCategory_Id" });
            RenameColumn(table: "dbo.Achievements", name: "AchievementCategory_Id", newName: "AchievementCategoryId");
            AlterColumn("dbo.Achievements", "AchievementCategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Achievements", "AchievementCategoryId");
            AddForeignKey("dbo.Achievements", "AchievementCategoryId", "dbo.AchievementCategories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Achievements", "AchievementCategoryId", "dbo.AchievementCategories");
            DropIndex("dbo.Achievements", new[] { "AchievementCategoryId" });
            AlterColumn("dbo.Achievements", "AchievementCategoryId", c => c.Int());
            RenameColumn(table: "dbo.Achievements", name: "AchievementCategoryId", newName: "AchievementCategory_Id");
            CreateIndex("dbo.Achievements", "AchievementCategory_Id");
            AddForeignKey("dbo.Achievements", "AchievementCategory_Id", "dbo.AchievementCategories", "Id");
        }
    }
}
