using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShadiHallBookingSystem.Models
{
    public class Service
    {/// <summary>
    /// Service ID
    /// </summary>
        [Key]
        public int Se_ID { get; set; }
        public string Wifi { get; set; }
        public string musicSystem { get; set; }
        public string Decoration { get; set; }
        public string speciaLights { get; set; }
        public string Lights { get; set; }
        public string Dj { get; set; }
        public string bikeParking { get; set; }
        public string carParking { get; set; }
        public string AirCondition { get; set; }
        public string socialMediaPages { get; set; }
        public string Seggretion { get; set; }
        public string Catering { get; set; }
        public string Projector { get; set; }
        public string stageDecoration { get; set; } 
        public string ladiesWaitress { get; set; }
        public string peopleCapacity { get; set; }
        //public string sitting { get; set; }
        //public string roundTableCapacity { get; set; }
        public string Electricity { get; set; }

        public int S_ID { get; set; }
        public virtual ShaadiHall ShaadiHall { get; set; }

   
}
}