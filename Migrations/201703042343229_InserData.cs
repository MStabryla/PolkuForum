namespace PolkuForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InserData : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Satire", "Link", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Satire", "Link", c => c.String(nullable: false));
        }
    }
}
