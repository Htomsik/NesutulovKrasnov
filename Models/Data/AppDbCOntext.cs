using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktika.Models.Data
{
    public class AppDbContext:DbContext
    {

        public AppDbContext() : base("DBConnect"){}
        
        public DbSet<User> Users { get; set; }

        public DbSet<Videocard> Videocards { get; set; }

    }
}
