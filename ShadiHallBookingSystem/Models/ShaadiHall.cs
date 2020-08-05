using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ShadiHallBookingSystem.Models
{
    public class ShaadiHall
    {/// <summary>
    /// Shadi Hall ID
    /// </summary>
        [Key]
        public int S_ID { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Local town name where the hall is located 
        /// </summary>
        public string Town { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        //public virtual ICollection<Service> Services { get; set; }
        public virtual ICollection<Shift> Shifts { get; set; }

    }
}