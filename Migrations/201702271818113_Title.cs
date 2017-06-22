namespace PolkuForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Title : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Article", "Title", c => c.String(nullable: false));
            AddColumn("dbo.Satire", "Title", c => c.String(nullable: false));
            DropColumn("dbo.Article", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Article", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Satire", "Title");
            DropColumn("dbo.Article", "Title");
        }
    }
}
