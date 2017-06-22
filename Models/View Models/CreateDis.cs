using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PolkuForum.Models
{
    public class CreateDis
    {
        [Required]
        public string Title
        { get; set; }
        [Required]
        public string Description
        { get; set; }
        [Required]
        public int CategoryId
        { get; set; }
    }
}