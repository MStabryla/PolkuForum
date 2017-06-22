using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolkuForum.Models
{
    public class ComD
    {
        public ComD()
        {
            Likes = new HashSet<LikeD>();
        }
        [Key]
        public int Id
        { get; set; }
        public virtual ForumUser Author
        { get; set; }
        public virtual Discussion Discussion
        { get; set; }
        [Required]
        public bool Kind
        { get; set; }
        [Required]
        public string Content
        { get; set; }
        [Required]
        public DateTime Date
        { get; set; }
        public virtual ICollection<LikeD> Likes
        { get; set; }
        [NotMapped]
        public int CountOfSupprotedLikes
        {
            get
            {
                return Likes.Where(x => x.Side).Count();
            }
        }
        [NotMapped]
        public int CountOfOpposedLikes
        {
            get
            {
                return Likes.Where(x => !x.Side).Count();
            }
        }
    }
}