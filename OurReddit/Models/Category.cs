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
        [Required]
        public string Name { get; set; }
        //public DateTime CreationDate { get; set; } nu mi merge datetime pls help
        
        // o categorie are un creator
        //public virtual User user { get; set; }

        // o categorie are mai multe subiecte
        public virtual ICollection<Subject> Subjects { get; set; } 
    }


}