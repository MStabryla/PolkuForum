using System.ComponentModel.DataAnnotations;

namespace PolkuForum.Models
{
    public class CreateSatire
    {
        [Required]
        public string Kind
        { get; set; }
        [Required]
        public string Link
        { get; set; }
        public string Tags
        { get; set; }
        [Required]
        public string Description
        { get; set; }
    }
}