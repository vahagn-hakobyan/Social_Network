using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication22_06.Models
{
    public class Friends
    {
        [Key]
        public int id { get; set; }
        public int User1Id { get; set; }
        public int User2Id { get; set; }
        public User User1 { get; set; }
        public User User2 { get; set; }
    }
}
