namespace PolkuForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoleToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Role", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Role");
        }
    }
}
