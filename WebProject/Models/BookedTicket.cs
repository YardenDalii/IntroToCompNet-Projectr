using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProject.Models
{
    public class BookedTicket
    {
        public Ticket Person { get; set; }
        public Flight Flight { get; set; }
    }
}