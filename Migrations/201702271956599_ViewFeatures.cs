namespace PolkuForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ViewFeatures : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Discussion", name: "Kategoria_Id", newName: "Category_Id");
            RenameIndex(table: "dbo.Discussion", name: "IX_Kategoria_Id", newName: "IX_Category_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Discussion", name: "IX_Category_Id", newName: "IX_Kategoria_Id");
            RenameColumn(table: "dbo.Discussion", name: "Category_Id", newName: "Kategoria_Id");
        }
    }
}
