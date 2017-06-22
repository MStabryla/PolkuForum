namespace PolkuForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstStart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Article",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(nullable: false),
                        Mark = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Author_Id = c.String(nullable: false, maxLength: 128),
                        Category_Id = c.Int(nullable: false),
                        Source_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id)
                .ForeignKey("dbo.Category", t => t.Category_Id)
                .ForeignKey("dbo.Source", t => t.Source_Id)
                .Index(t => t.Author_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.Source_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Nick = c.String(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ComA",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        Mark = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Article_Id = c.Int(nullable: false),
                        Author_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Article", t => t.Article_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id)
                .Index(t => t.Article_Id)
                .Index(t => t.Author_Id);
            
            CreateTable(
                "dbo.LikeA",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Author_Id = c.String(nullable: false, maxLength: 128),
                        Comment_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id)
                .ForeignKey("dbo.ComA", t => t.Comment_Id)
                .Index(t => t.Author_Id)
                .Index(t => t.Comment_Id);
            
            CreateTable(
                "dbo.ComD",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Kind = c.Boolean(nullable: false),
                        Content = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Author_Id = c.String(nullable: false, maxLength: 128),
                        Discussion_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id)
                .ForeignKey("dbo.Discussion", t => t.Discussion_Id)
                .Index(t => t.Author_Id)
                .Index(t => t.Discussion_Id);
            
            CreateTable(
                "dbo.Discussion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Decription = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Author_Id = c.String(nullable: false, maxLength: 128),
                        Kategoria_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id)
                .ForeignKey("dbo.Category", t => t.Kategoria_Id)
                .Index(t => t.Author_Id)
                .Index(t => t.Kategoria_Id);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Color = c.String(),
                        Icon = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Profile",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Obraz = c.String(),
                        Decription = c.String(),
                        Education = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.LikeD",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Side = c.Boolean(nullable: false),
                        Author_Id = c.String(nullable: false, maxLength: 128),
                        Comment_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id)
                .ForeignKey("dbo.ComD", t => t.Comment_Id)
                .Index(t => t.Author_Id)
                .Index(t => t.Comment_Id);
            
            CreateTable(
                "dbo.ComS",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        Mark = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Author_Id = c.String(nullable: false, maxLength: 128),
                        Satire_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id)
                .ForeignKey("dbo.Satire", t => t.Satire_Id)
                .Index(t => t.Author_Id)
                .Index(t => t.Satire_Id);
            
            CreateTable(
                "dbo.LikeS",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Author_Id = c.String(nullable: false, maxLength: 128),
                        Comment_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id)
                .ForeignKey("dbo.ComS", t => t.Comment_Id)
                .Index(t => t.Author_Id)
                .Index(t => t.Comment_Id);
            
            CreateTable(
                "dbo.Satire",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Kind = c.String(nullable: false),
                        Link = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Author_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id)
                .Index(t => t.Author_Id);
            
            CreateTable(
                "dbo.Credibility",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Mark = c.Int(nullable: false),
                        Comment = c.String(nullable: false),
                        Author_Id = c.String(nullable: false, maxLength: 128),
                        Source_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id)
                .ForeignKey("dbo.Source", t => t.Source_Id)
                .Index(t => t.Author_Id)
                .Index(t => t.Source_Id);
            
            CreateTable(
                "dbo.Source",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Domain = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Image = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Report",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Substantiation = c.String(nullable: false),
                        Author_Id = c.String(nullable: false, maxLength: 128),
                        Discussion_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id)
                .ForeignKey("dbo.Discussion", t => t.Discussion_Id)
                .Index(t => t.Author_Id)
                .Index(t => t.Discussion_Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.ProfileCategory",
                c => new
                    {
                        Profile_Id = c.Int(nullable: false),
                        Category_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Profile_Id, t.Category_Id })
                .ForeignKey("dbo.Profile", t => t.Profile_Id, cascadeDelete: true)
                .ForeignKey("dbo.Category", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.Profile_Id)
                .Index(t => t.Category_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Article", "Source_Id", "dbo.Source");
            DropForeignKey("dbo.Article", "Category_Id", "dbo.Category");
            DropForeignKey("dbo.Article", "Author_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Report", "Discussion_Id", "dbo.Discussion");
            DropForeignKey("dbo.Report", "Author_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Credibility", "Source_Id", "dbo.Source");
            DropForeignKey("dbo.Credibility", "Author_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ComS", "Satire_Id", "dbo.Satire");
            DropForeignKey("dbo.Satire", "Author_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.LikeS", "Comment_Id", "dbo.ComS");
            DropForeignKey("dbo.LikeS", "Author_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ComS", "Author_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.LikeD", "Comment_Id", "dbo.ComD");
            DropForeignKey("dbo.LikeD", "Author_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ComD", "Discussion_Id", "dbo.Discussion");
            DropForeignKey("dbo.Profile", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProfileCategory", "Category_Id", "dbo.Category");
            DropForeignKey("dbo.ProfileCategory", "Profile_Id", "dbo.Profile");
            DropForeignKey("dbo.Discussion", "Kategoria_Id", "dbo.Category");
            DropForeignKey("dbo.Discussion", "Author_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ComD", "Author_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.LikeA", "Comment_Id", "dbo.ComA");
            DropForeignKey("dbo.LikeA", "Author_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ComA", "Author_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ComA", "Article_Id", "dbo.Article");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ProfileCategory", new[] { "Category_Id" });
            DropIndex("dbo.ProfileCategory", new[] { "Profile_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Report", new[] { "Discussion_Id" });
            DropIndex("dbo.Report", new[] { "Author_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Credibility", new[] { "Source_Id" });
            DropIndex("dbo.Credibility", new[] { "Author_Id" });
            DropIndex("dbo.Satire", new[] { "Author_Id" });
            DropIndex("dbo.LikeS", new[] { "Comment_Id" });
            DropIndex("dbo.LikeS", new[] { "Author_Id" });
            DropIndex("dbo.ComS", new[] { "Satire_Id" });
            DropIndex("dbo.ComS", new[] { "Author_Id" });
            DropIndex("dbo.LikeD", new[] { "Comment_Id" });
            DropIndex("dbo.LikeD", new[] { "Author_Id" });
            DropIndex("dbo.Profile", new[] { "User_Id" });
            DropIndex("dbo.Discussion", new[] { "Kategoria_Id" });
            DropIndex("dbo.Discussion", new[] { "Author_Id" });
            DropIndex("dbo.ComD", new[] { "Discussion_Id" });
            DropIndex("dbo.ComD", new[] { "Author_Id" });
            DropIndex("dbo.LikeA", new[] { "Comment_Id" });
            DropIndex("dbo.LikeA", new[] { "Author_Id" });
            DropIndex("dbo.ComA", new[] { "Author_Id" });
            DropIndex("dbo.ComA", new[] { "Article_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Article", new[] { "Source_Id" });
            DropIndex("dbo.Article", new[] { "Category_Id" });
            DropIndex("dbo.Article", new[] { "Author_Id" });
            DropTable("dbo.ProfileCategory");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Report");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Source");
            DropTable("dbo.Credibility");
            DropTable("dbo.Satire");
            DropTable("dbo.LikeS");
            DropTable("dbo.ComS");
            DropTable("dbo.LikeD");
            DropTable("dbo.Profile");
            DropTable("dbo.Category");
            DropTable("dbo.Discussion");
            DropTable("dbo.ComD");
            DropTable("dbo.LikeA");
            DropTable("dbo.ComA");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Article");
        }
    }
}
