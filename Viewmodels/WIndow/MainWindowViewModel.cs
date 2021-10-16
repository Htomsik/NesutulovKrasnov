using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Praktika.Infrastructures.Commands;
using Praktika.Models;
using Praktika.Models.Data;
using Praktika.Services;

namespace Praktika.Viewmodels
{
    public sealed class MainWindowViewModel:BaseViewModel
    {
        public MainWindowViewModel()
        {
            UpdateWindowViewCommand = new UpdateWindowViewCommand(this);

            WindowLoadedMessabeBusCommand =
                new LambdaCommand(OnWindowLoadedMessabeBusExecuted, CanWindowLoadedMessabeBusExecute);

        }

        #region Выбор страниц

        private BaseViewModel _selectedViewModel;

        public BaseViewModel SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public ICommand UpdateWindowViewCommand { get; set; }

        #endregion

        #region привязка шины сообщений

        public ICommand WindowLoadedMessabeBusCommand { get; }

        private bool CanWindowLoadedMessabeBusExecute(object p) => true;

        private void OnWindowLoadedMessabeBusExecuted(object p)
        {
            MessageBus.Bus += Receive;
        }

        private void Receive(object p)
        {
            UpdateWindowViewCommand.Execute(p);
        }

        #endregion

       


    }
}
