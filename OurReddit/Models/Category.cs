using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OurReddit.Models
{
    public class Category
    {
        [Key]
        public int Id{ get; set; }
        [MinLength(4, ErrorMessage = "Name too short")]
        [MaxLength(27, ErrorMessage = "Name too long")]
        [Required(ErrorMessage = "This field is mandatory")]
        public string Name { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        
        // o categorie are un creator
        //public virtual User user { get; set; }

        // o categorie are mai multe subiecte
        public virtual ICollection<Subject> Subjects { get; set; } 
    }
}