using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PolkuForum.Models
{
    public class LikeD
    {
        [Key]
        public int Id
        { get; set; }
        [Required]
        public virtual ForumUser Author
        { get; set; }
        [Required]
        public virtual ComD Comment
        { get; set; }
        [Required]
        public bool Side
        { get; set; }
    }
}