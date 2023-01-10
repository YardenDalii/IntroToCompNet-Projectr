using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    public class Admin
    {
        public int ID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }

    public class AdminDBContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
    }
}