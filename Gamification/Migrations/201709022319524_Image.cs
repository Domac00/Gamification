namespace Gamification.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Image : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserQuizDatas", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserQuizDatas", "ImageUrl");
        }
    }
}
