using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace PolkuForum.Models
{
    public class MainContext : IdentityDbContext<ForumUser>
    {
        public MainContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public static MainContext Create()
        {
            return new MainContext();
        }
        public bool OnModelCompleted
        { get; set; }
        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Conventions.Remove<PluralizingTableNameConvention>();
            builder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            OnModelCompleted = true;
            //builder.Entity<ComD>().HasRequired(x => x.Author);
        }
        public DbSet<Article> Articles
        { get; set; }
        public DbSet<Category> Categories
        { get; set; }
        public DbSet<ComA> CommentsOfArticle
        { get; set; }
        public DbSet<ComD> CommentsOfDiscussion
        { get; set; }
        public DbSet<ComS> CommentsOfSatire
        { get; set; }
        public DbSet<Credibility> Credibilities
        { get; set; }
        public DbSet<Discussion> Discussions
        { get; set; }
        public DbSet<LikeA> LikesOfArticle
        { get; set; }
        public DbSet<LikeD> LikesOfDiscussion
        { get; set; }
        public DbSet<LikeS> LikesOfSatire
        { get; set; }
        public DbSet<Profile> Profiles
        { get; set; }
        public DbSet<Report> Reports
        { get; set; }
        public DbSet<Satire> Satires
        { get; set; }
        public DbSet<Source> Sources
        { get; set; }
    }
}