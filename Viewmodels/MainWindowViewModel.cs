using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Praktika.Infrastructures.Commands;
using Praktika.Models;

namespace Praktika.Viewmodels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
            UpdateViewCommand = new UpdateViewCommand(this);

            Pages = new ObservableCollection<Pages>
            {
                new Pages {URLicon = "Solid_home", NamePage = "Главная страница", Number = 0},
                new Pages {URLicon = "Solid_calculator", NamePage = "Калькулятор", Number = 1}
            };
        }

        public ObservableCollection<Pages> Pages { get; set; }

        #region Title

        private string _Title = "Несутулов К.C";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion

        #region ContetMenuVisibility

        

        #endregion

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

        public ICommand UpdateViewCommand { get; set; }

        #endregion
    }
}