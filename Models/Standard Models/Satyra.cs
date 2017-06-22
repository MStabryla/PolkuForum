using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolkuForum.Models
{
    public class Satire
    {
        public Satire()
        {
            Comments = new HashSet<ComS>();
        }
        [Key]
        public int Id
        { get; set; }
        [Required]
        public virtual ForumUser Author
        { get; set; }
        [Required]
        public string Kind
        { get; set; }
        public string Link
        { get; set; }
        public string Tags
        { get; set; }
        [Required]
        public string Title
        { get; set; }
        [NotMapped]
        public string ObjectName
        { get { return "Satire"; } }
        [NotMapped]
        public string Image
        {
            get
            {
                switch(Link)
                {
                    case "Picture":
                        return Link;
                    case "Poem":
                        return "~/grf/poem.png";
                    case "Movie":
                        return "~/grf/movie.png";
                }
                return Link;
            }
        }
        [NotMapped]
        public int Mark
        {
            get
            {
                return Comments.Sum(x => x.Mark) / Comments.Count;
            }
        }
        [Required]
        public string Description
        { get; set; }
        [Required]
        public DateTime Date
        { get; set; }
        public virtual ICollection<ComS> Comments
        { get; set; }
    }
}