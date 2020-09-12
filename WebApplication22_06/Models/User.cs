﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication22_06.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string photo { get; set; }
        public int type { get; set; }

    }
}
