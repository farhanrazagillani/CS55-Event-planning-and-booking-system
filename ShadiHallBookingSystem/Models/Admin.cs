﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShadiHallBookingSystem.Models
{
    public class Admin
    {
        [Key]
        public int A_ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}