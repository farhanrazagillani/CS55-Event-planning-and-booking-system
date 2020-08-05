using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShadiHallBookingSystem.Models
{
    public class bookingViewModel
    {

        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public int Guests { get; set; }

        [Column(TypeName = "Date")]
      public DateTime FromDate { get; set; }

        [Column(TypeName = "Date")]
        public DateTime ToDate { get; set; }


    }
}