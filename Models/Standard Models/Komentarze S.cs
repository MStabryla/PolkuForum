﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PolkuForum.Models
{
    public class ComS
    {
        public ComS()
        {
            Likes = new HashSet<LikeS>();
        }
        [Key]
        public int Id
        { get; set; }
        [Required]
        public virtual ForumUser Author
        { get; set; }
        [Required]
        public virtual Satire Satire
        { get; set; }
        [Required]
        public string Content
        { get; set; }
        public int Mark
        { get; set; }
        [Required]
        public DateTime Date
        { get; set; }
        public virtual ICollection<LikeS> Likes
        { get; set; }
    }
}