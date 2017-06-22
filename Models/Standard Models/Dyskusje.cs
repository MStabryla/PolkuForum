using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolkuForum.Models
{
    public class Discussion
    {
        public Discussion()
        {
            Comments = new HashSet<ComD>();
        }
        [Key]
        public int Id
        { get; set; }
        [Required]
        public string Title
        { get; set; }
        [Required]
        public string Decription
        { get; set; }
        [NotMapped]
        public string ObjectName
        { get { return "Discussion"; } }
        [NotMapped]
        public string Image
        {
            get
            {
                return Category.Icon;
            }
        }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date
        { get; set; }
        public virtual Category Category
        { get; set; }
        [Required]
        public virtual ForumUser Author
        { get; set; }
        public virtual ICollection<ComD> Comments
        { get; set; }
    }
}