namespace PolkuForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArticleModify : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Article", "Description", c => c.String(nullable: false));
            AddColumn("dbo.Article", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Article", "Mark");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Article", "Mark", c => c.Int(nullable: false));
            DropColumn("dbo.Article", "Name");
            DropColumn("dbo.Article", "Description");
        }
    }
}
