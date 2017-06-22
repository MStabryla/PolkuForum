using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PolkuForum.Models
{
    public class ComA
    {
        public ComA()
        {
            Likes = new HashSet<LikeA>();
        }
        [Key]
        public int Id
        { get; set; }
        public virtual ForumUser Author
        { get; set; }
        [Required]
        public virtual Article Article
        { get; set; }
        [Required]
        public string Content
        { get; set; }
        public int Mark
        { get; set; }
        [Required]
        public DateTime Date
        { get; set; }
        public virtual ICollection<LikeA> Likes
        { get; set; }
    }
}