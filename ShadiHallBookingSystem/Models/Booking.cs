using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShadiHallBookingSystem.Models
{
    public class Booking
    {/// <summary>
    /// Booking ID
    /// </summary>
    [Key]
        public int B_ID { get; set; }

        [Column(TypeName = "Date")]
        public DateTime FromDate { get; set; }

        [Column(TypeName = "Date")]
        public DateTime ToDate { get; set; }

        [Required]
        public int Total { get; set; }

        public int Sh_ID { get; set; }
        public virtual Shift Shift { get; set; }
        public int U_ID { get; set; }
        public virtual User User { get; set; }

    }
}