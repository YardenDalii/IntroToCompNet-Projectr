using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProject.Models
{
    public class TwoWayF
    {
        public List<Flight> FromList { get; set; }
        public List<Flight> ToList { get; set; }

        public Flight f { get; set; }
    }
}