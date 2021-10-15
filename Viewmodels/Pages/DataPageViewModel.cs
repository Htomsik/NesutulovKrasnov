using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Praktika.Models;
using Praktika.Services;

namespace Praktika.Viewmodels
{
    public class DataPageViewModel:BaseViewModel
    {
        public DataPageViewModel()
        {
            Videocards = new ObservableCollection<Videocard>(DataWorker.GetAllVideocards());

        }

        public ObservableCollection<Videocard> Videocards { get; set; }
    }
}
