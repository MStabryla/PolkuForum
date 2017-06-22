namespace PolkuForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SourceUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SourceForumUser",
                c => new
                    {
                        Source_Id = c.Int(nullable: false),
                        ForumUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Source_Id, t.ForumUser_Id })
                .ForeignKey("dbo.Source", t => t.Source_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ForumUser_Id, cascadeDelete: true)
                .Index(t => t.Source_Id)
                .Index(t => t.ForumUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SourceForumUser", "ForumUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.SourceForumUser", "Source_Id", "dbo.Source");
            DropIndex("dbo.SourceForumUser", new[] { "ForumUser_Id" });
            DropIndex("dbo.SourceForumUser", new[] { "Source_Id" });
            DropTable("dbo.SourceForumUser");
        }
    }
}
