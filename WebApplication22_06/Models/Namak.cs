using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication22_06.Models
{
    public class Namak
    {
        [Key]
        public int id { get; set; }
        public int User1Id { get; set; }
        public int User2Id { get; set; }
        public User User1 { get; set; }
        public User User2 { get; set; }
        public string content { get; set; }
        public int status { get; set; }
        public int time { get; set; }
    }
}
