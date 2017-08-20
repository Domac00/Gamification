namespace Gamification.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tutorial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tutorials",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        isCompleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.UserQuizDataTutorials",
                c => new
                    {
                        UserQuizData_UserId = c.String(nullable: false, maxLength: 128),
                        Tutorials_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserQuizData_UserId, t.Tutorials_id })
                .ForeignKey("dbo.UserQuizDatas", t => t.UserQuizData_UserId, cascadeDelete: true)
                .ForeignKey("dbo.Tutorials", t => t.Tutorials_id, cascadeDelete: true)
                .Index(t => t.UserQuizData_UserId)
                .Index(t => t.Tutorials_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserQuizDataTutorials", "Tutorials_id", "dbo.Tutorials");
            DropForeignKey("dbo.UserQuizDataTutorials", "UserQuizData_UserId", "dbo.UserQuizDatas");
            DropIndex("dbo.UserQuizDataTutorials", new[] { "Tutorials_id" });
            DropIndex("dbo.UserQuizDataTutorials", new[] { "UserQuizData_UserId" });
            DropTable("dbo.UserQuizDataTutorials");
            DropTable("dbo.Tutorials");
        }
    }
}
