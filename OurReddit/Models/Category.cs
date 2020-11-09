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
        //public virtual User user;// cine l-a creat
       public virtual ICollection<Subject> Subjects { get; set; } // o categorie are mai multe subiecte
    }


}