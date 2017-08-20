namespace Gamification.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tutorialchangenameoftable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.UserQuizDataTutorials", name: "Tutorials_id", newName: "Tutorial_id");
            RenameIndex(table: "dbo.UserQuizDataTutorials", name: "IX_Tutorials_id", newName: "IX_Tutorial_id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.UserQuizDataTutorials", name: "IX_Tutorial_id", newName: "IX_Tutorials_id");
            RenameColumn(table: "dbo.UserQuizDataTutorials", name: "Tutorial_id", newName: "Tutorials_id");
        }
    }
}
