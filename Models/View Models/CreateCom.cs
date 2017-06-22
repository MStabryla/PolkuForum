using System.ComponentModel.DataAnnotations;

namespace PolkuForum.Models
{
    public class CreateComment
    {
        [Required]
        public string Content
        { get; set; }
        [Required]
        public int PostID
        { get; set; }
        public bool Side
        { get; set; }
    }
}