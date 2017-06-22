using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PolkuForum.Models
{
    public class Source
    {
        public Source()
        {
            Credibility = new HashSet<Credibility>();
            Articles = new HashSet<Article>();
        }
        [Key]
        public int Id
        { get; set; }
        [Required]
        public string Name
        { get; set; }
        [Required]
        public string Domain
        { get; set; }
        [Required]
        public string Description
        { get; set; }
        [Required]
        public string Image
        { get; set; }
        public virtual ICollection<Credibility> Credibility
        { get; set; }
        public virtual ICollection<Article> Articles
        { get; set; }
        public virtual ICollection<ForumUser> Users
        { get; set; }
    }
}