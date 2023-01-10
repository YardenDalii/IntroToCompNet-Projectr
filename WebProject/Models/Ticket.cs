using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebProject.Models
{
    public class Ticket
    {
        /*[Key]
        public int ID { get; set; }
        */
        [Required]
        public string Email { get; set; }
        [Required]
        public string FullName { get; set; }
        public string id { get; set; }

    }
    /*
    public class TicketsDBContext : DbContext
    {
        public DbSet<Ticket> Tickets { get; set; }
    }
    */
}