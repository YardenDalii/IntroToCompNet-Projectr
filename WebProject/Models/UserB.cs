using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebProject.Models
{
    public class UserB
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        [StringLength(9)]
        public string id_number { get; set; }
        public virtual ICollection<CreditCard> PayMethods { get; set; }
    }
}