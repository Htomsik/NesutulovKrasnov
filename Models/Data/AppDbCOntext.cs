using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktika.Models.Data
{
    public class AppDbCOntext:DbContext
    {

        public AppDbCOntext() : base("DBConnect"){}
        
        public DbSet<User> Users { get; set; }

        
    }
}
