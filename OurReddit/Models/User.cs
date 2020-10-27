using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OurReddit.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // hased :D
        public string Description { get; set; }
        public DateTime Birthday { get; set; }
        public int RoleID { get; set; }
    }

    public class UserDBContext : DbContext
    {
        public UserDBContext() : base("DBConnectionString") { }
        public DbSet<User> Users { get; set; }
    }
}