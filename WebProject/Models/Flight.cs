using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    public class Flight
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string From { get; set; }
        [Required]
        public string To { get; set; }
        [Required]
        public DateTime DepTime { get; set; }
        [Required]
        public DateTime ArrTime { get; set; }
        [Required]
        [Range(0,100)]
        public int SeatCap { get; set; }
        [Required]
        public float Price { get; set; }

    }

    public class FlightDBContext : DbContext
    {
        public DbSet<Flight> Flights { get; set; }

    }
}