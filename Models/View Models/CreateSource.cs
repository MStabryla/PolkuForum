using System.ComponentModel.DataAnnotations;

namespace PolkuForum.Models
{
    public class CreateSource
    {
        public int Id
        { get; set; }
        [Required]
        public string Name
        { get; set; }
        [Required]
        public string Domain
        { get; set; }
        [Required]
        public string Description
        { get; set; }
        [Required]
        public string Image
        { get; set; }
    }
}