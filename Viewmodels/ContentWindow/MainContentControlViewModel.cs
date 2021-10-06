using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Praktika.Infrastructures.Commands;
using Praktika.Models;
using Praktika.Services;

namespace Praktika.Viewmodels
{
    public class MainContentControlViewModel : BaseViewModel
    {
        public MainContentControlViewModel()
        {
            UpdatePagesViewCommand = new UpdatePagesViewCommand(this);

            Pages = new ObservableCollection<Pages>
            {
                new Pages {URLicon = "Solid_home", NamePage = "Главная страница", Number = 0},
                new Pages {URLicon = "Solid_calculator", NamePage = "Калькулятор", Number = 1}
            };

            SendContentControlNumerCommand =
                new LambdaCommand(OnContentControlNumerExecuted, CanContentControlNumerExecute);
        }

        public ObservableCollection<Pages> Pages { get; }

        #region Отправка номера страницы

        public ICommand SendContentControlNumerCommand { get; }

        private bool CanContentControlNumerExecute(object p) => true;

        private async void OnContentControlNumerExecuted(object p)
        {
            MessageBus.Send(p);
        }
        #endregion

        #region Title

        private string _Title = "Несутулов К.C";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

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

        public ICommand UpdatePagesViewCommand { get; set; }

        #endregion
    }
}