using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PolkuForum.Models
{
    public class CreateLike
    {
        [Required]
        public int DisId
        { get; set; }
        [Required]
        public int ComId
        { get; set; }
    }
}