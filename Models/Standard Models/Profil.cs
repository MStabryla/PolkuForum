using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolkuForum.Models
{
    public class Profile
    {
        public Profile()
        {
            Categories = new HashSet<Category>();
        }
        [Key]
        public int Id
        { get; set; }
        public string Obraz
        { get; set; }
        public string Decription
        { get; set; }
        public string Education
        { get; set; }
        public virtual ICollection<Category> Categories
        { get; set; }
        [Required]
        public virtual ForumUser User
        { get; set; }
    }
}