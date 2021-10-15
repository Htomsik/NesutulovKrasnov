using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Praktika.Infrastructures.Commands;
using Praktika.Models;
using Praktika.Services;

namespace Praktika.Viewmodels
{
    public class DataPageViewModel:BaseViewModel
    {
        public DataPageViewModel()
        {
            Videocards = new ObservableCollection<Videocard>(DataWorker.GetAllVideocards());
            AddNewCardCommand = new LambdaCommand(OnAddNewCardExecuted,CanAddNewCardExecute);
        }

        public ObservableCollection<Videocard> Videocards { get; set; }


        #region Команды


        public ICommand AddNewCardCommand { get; }

        private bool CanAddNewCardExecute(object p) => true;

        private void OnAddNewCardExecuted(object p)
        {
           
        }

        #endregion
    }
}
