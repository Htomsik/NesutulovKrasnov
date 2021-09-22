using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Praktika.Infrastructures.Commands;

namespace Praktika.Viewmodels
{
    public class MainWindowViewModel:BaseViewModel
    {
        public MainWindowViewModel()
        {
            UpdateWindowViewCommand = new UpdateWindowViewCommand(this);
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
    }
}
