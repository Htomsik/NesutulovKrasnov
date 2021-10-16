using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Praktika.Viewmodels;

namespace Praktika.Models
{
    public class User
    {
        
        public int ID { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Role { get; set;}

        public string FIO { get; set; }
    }

}
