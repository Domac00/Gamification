namespace Gamification.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tutoriallink : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tutorials", "TutorialLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tutorials", "TutorialLink");
        }
    }
}
