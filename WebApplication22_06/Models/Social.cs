using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace WebApplication22_06.Models
{
    public class Social : DbContext
    {
       public DbSet<User> Users { get; set; }
        public DbSet<Requests> Requests { get; set; }
        public DbSet<Friends> Friends { get; set; }
        public DbSet<Namak> Messenger { get; set; }

        public Social(DbContextOptions x) : base(x) { }
    }
}
