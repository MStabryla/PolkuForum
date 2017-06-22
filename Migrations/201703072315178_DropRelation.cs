namespace PolkuForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropRelation : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ComD", new[] { "Discussion_Id" });
            AlterColumn("dbo.ComD", "Discussion_Id", c => c.Int());
            CreateIndex("dbo.ComD", "Discussion_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ComD", new[] { "Discussion_Id" });
            AlterColumn("dbo.ComD", "Discussion_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.ComD", "Discussion_Id");
        }
    }
}
