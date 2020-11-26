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
        [StringLength(27, MinimumLength = 4, ErrorMessage = "Title whould have between 4 and 27 characters")]
        [Required(ErrorMessage = "This field is mandatory")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required(ErrorMessage = "This field is mandatory")]
        public int CategoryId { get; set; }
        
        //un subiect apartine unei categorii 
        public virtual Category Category { get; set; }
        //o categorie are mai multe mesaje
        public virtual ICollection<Message> Messages { get; set; } //un subiect are mai multe mesaje
    }
}