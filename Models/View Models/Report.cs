using System.ComponentModel.DataAnnotations;

namespace PolkuForum.Models
{
    public class CreateReport
    {
        [Required]
        public string Substantiation
        { get; set; }
        [Required]
        public int DisId
        { get; set; }
    }
}