using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OurReddit.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; } //cine a scris msg
        public string CreationDate { get; set; }
        [Required]
        public string Content { get; set; }
        public bool edited { get; set; }
    }
}