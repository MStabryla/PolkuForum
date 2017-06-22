using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PolkuForum.Models
{
    public class Credibility
    {
        [Key]
        public int Id
        { get; set; }
        [Required]
        public virtual Source Source
        { get; set; }
        [Required]
        public virtual ForumUser Author
        { get; set; }
        [Required]
        public int Mark
        { get; set; }
        [Required]
        public string Comment
        { get; set; }
    }
}