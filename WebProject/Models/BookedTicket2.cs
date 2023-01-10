using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProject.Models
{
    public class BookedTicket2
    {
        public Ticket Person { get; set; }
        public Flight Flight1 { get; set; }
        public Flight Flight2 { get; set; }
    }
}