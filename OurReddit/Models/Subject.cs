using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OurReddit.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreationDate { get; set; }

        public virtual ICollection<Message> Messages { get; set; } //un subiect are mai multe mesaje
    }
}