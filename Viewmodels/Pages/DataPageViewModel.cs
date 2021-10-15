using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Praktika.Models;

namespace Praktika.Viewmodels
{
    public class DataPageViewModel:BaseViewModel
    {
        public DataPageViewModel()
        {
            Videocards = new ObservableCollection<Videocard>
            {
                new Videocard
                {
                    ID = 1, Company = "Nvidia", Name = "GeForce RTX 3090", Intarface = "PCI-E 4.0",
                    MemoryType = "GDDR6X", TechProcess = 8, RealeaseDate = new DateTime(2020, 09, 24)
                },
                new Videocard
                {
                    ID = 1, Company = "Nvidia", Name = "GeForce RTX 3090", Intarface = "PCI-E 4.0",
                    MemoryType = "GDDR6X", TechProcess = 8, RealeaseDate = new DateTime(2020, 09, 24)
                },
                new Videocard
                {
                    ID = 1, Company = "Nvidia", Name = "GeForce RTX 3090", Intarface = "PCI-E 4.0",
                    MemoryType = "GDDR6X", TechProcess = 8, RealeaseDate = new DateTime(2020, 09, 24)
                }, new Videocard
                {
                    ID = 1, Company = "Nvidia", Name = "GeForce RTX 3090", Intarface = "PCI-E 4.0",
                    MemoryType = "GDDR6X", TechProcess = 8, RealeaseDate = new DateTime(2020, 09, 24)
                },
                new Videocard
                {
                    ID = 1, Company = "Nvidia", Name = "GeForce RTX 3090", Intarface = "PCI-E 4.0",
                    MemoryType = "GDDR6X", TechProcess = 8, RealeaseDate = new DateTime(2020, 09, 24)
                },
                new Videocard
                {
                    ID = 1, Company = "Nvidia", Name = "GeForce RTX 3090", Intarface = "PCI-E 4.0",
                    MemoryType = "GDDR6X", TechProcess = 8, RealeaseDate = new DateTime(2020, 09, 24)
                }


            };
        }

        public ObservableCollection<Videocard> Videocards { get; set; }
    }
}
