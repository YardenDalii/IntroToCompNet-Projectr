using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace WebProject.Models
{
    public class CreditCard
    {
        public int ID { get; set; }
        public int UserBID { get; set; }

        [Required]
        [StringLength(16)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "must be numeric")]
        public string CardNumber { get; set; }


        [Required]
        [StringLength(4)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "must be numeric")]
        public string ExpDate { get; set; }


        [Required]
        [StringLength(3)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "must be numeric")]
        public string CVV { get; set; }


        public virtual UserB User { get; set; }
    }
}