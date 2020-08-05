using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShadiHallBookingSystem.Models
{
    public class Shift
    {/// <summary>
     /// Shift ID
     /// </summary>
     [Key]
        public int Sh_ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int S_ID { get; set; }
        public virtual ShaadiHall ShaadiHall { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }

    }
}