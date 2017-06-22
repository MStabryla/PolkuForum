using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolkuForum.Models
{
    public class Article
    {
        public Article()
        {
            this.Comments = new HashSet<ComA>();
        }
        [Key]
        public int Id
        { get; set; }
        [Required]
        public virtual Source Source
        { get; set; }
        [Required]
        public string Address
        { get; set; }
        [Required]
        public string Description
        { get; set; }
        [Required]
        public string Title
        { get; set; }
        [NotMapped]
        public string ObjectName
        { get { return "Article"; } }
        [NotMapped]
        public string Image
        {
            get
            {
                return Category.Icon;
            }
        }
        [NotMapped]
        public int Mark
        {
            get
            {
                return Comments.ToList().Sum(x => x.Mark) / Comments.ToList().Count;
            }
        }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date
        { get; set; }
        [Required]
        public virtual ForumUser Author
        { get; set; }
        [Required]
        public virtual Category Category
        { get; set; }
        public virtual ICollection<ComA> Comments
        { get; set; }
    }
}