using System.ComponentModel.DataAnnotations;

namespace PolkuForum.Models
{
    public class CreateArticle
    {
        [Required]
        public string Address
        { get; set; }
        [Required]
        public string Description
        { get; set; }
        [Required]
        public string Name
        { get; set; }
        [Required]
        public bool NewSource
        { get; set; }
        public int SourceId
        { get; set; }
    }
}