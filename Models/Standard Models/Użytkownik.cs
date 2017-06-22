using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Security;

namespace PolkuForum.Models
{
    public class ForumUser : IdentityUser
    {
        public ForumUser() : base()
        {
            Articles = new HashSet<Article>();
            LikesS = new HashSet<LikeS>();
            LikesD = new HashSet<LikeD>();
            LikesA = new HashSet<LikeA>();
            CommentsA = new HashSet<ComA>();
            CommentsD = new HashSet<ComD>();
            CommentsS = new HashSet<ComS>();
            Discussions = new HashSet<Discussion>();
            Satires = new HashSet<Satire>();
            Reports = new HashSet<Report>();
            Credibilities = new HashSet<Credibility>();
            Sources = new HashSet<Source>();
        }
        public async Task<ClaimsIdentity> GenUserIdentity(UserManager<ForumUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
        [Required]
        public string Nick
        {
            get;set;
        }
        [Required]
        public string Role
        { get; set; }
        public virtual Profile Profile
        { get; set; }
        public virtual ICollection<Article> Articles
        { get; set; }
        public virtual ICollection<LikeA> LikesA
        { get; set; }
        public virtual ICollection<LikeD> LikesD
        { get; set; }
        public virtual ICollection<LikeS> LikesS
        { get; set; }
        public virtual ICollection<ComA> CommentsA
        { get; set; }
        public virtual ICollection<ComD> CommentsD
        { get; set; }
        public virtual ICollection<ComS> CommentsS
        { get; set; }
        public virtual ICollection<Discussion> Discussions
        { get; set; }
        public virtual ICollection<Satire> Satires
        { get; set; }
        public virtual ICollection<Report> Reports
        { get; set; }
        public virtual ICollection<Credibility> Credibilities
        { get; set; }
        public virtual ICollection<Source> Sources
        { get; set; }
    }
}