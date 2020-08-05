using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShadiHallBookingSystem.Models
{
    public class User
    {
        [Key]
        public int U_ID { get; set; }
        public string Name { get; set; }
        public string Email_ID { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }

    }
}