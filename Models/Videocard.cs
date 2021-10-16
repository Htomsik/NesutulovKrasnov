using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktika.Models
{
    public sealed class Videocard
    {
        public int ID { get; set; }

        public string Company { get; set; }

        public string Name { get; set; }

        public string Core { get; set; }

        public byte TechProcess { get; set; }

        public string MemoryType { get; set; }

        public string Interface { get; set; }

        
    }
}
