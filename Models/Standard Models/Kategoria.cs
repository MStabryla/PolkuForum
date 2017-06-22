using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PolkuForum.Models
{
    public class Category
    {
        public Category()
        {
            Articles = new HashSet<Article>();
            Discussion = new HashSet<Discussion>();
            Users = new HashSet<Profile>();
        }
        [Key]
        public int Id
        { get; set; }
        [Required]
        public string Name
        { get; set; }
        [Required]
        public string Description
        { get; set; }
        public string Color
        { get; set; }
        [Required]
        public string Icon
        { get; set; }
        public virtual ICollection<Article> Articles
        { get; set; }
        public virtual ICollection<Discussion> Discussion
        { get; set; }
        public virtual ICollection<Profile> Users
        { get; set; }
    }
}