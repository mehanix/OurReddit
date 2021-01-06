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
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "Cannot create empty message")]
        public string Content { get; set; }
        public bool Edited { get; set; }
        [Required(ErrorMessage = "This field is mandatory")]
        public int SubjectId { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        //un mesaj apartine unui singur subiect
        public virtual Subject Subject { get; set; }
    }
}