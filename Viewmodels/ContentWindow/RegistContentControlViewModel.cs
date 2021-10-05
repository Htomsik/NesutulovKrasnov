using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Praktika.Services;
using Praktika.Infrastructures.Commands;
using Praktika.Models;

namespace Praktika.Viewmodels
{
    public class RegistContentControlViewModel:BaseViewModel
    {
        public RegistContentControlViewModel()
        {
            CreateNewUserCommand = new LambdaCommand(OnCreateNewUserCommandExecuted, CanCreateNewUserCommandExecute); 

            OpenAuthCommand = new LambdaCommand(OnOpenAuthCommandExecuted, CanOpenAuthCommandExecute);

            Roles = new ObservableCollection<Role>
            {
                new Role {Name = "Customer"},
                new Role {Name = "Director"}
            };

        }

        #region Commands

        #region создание нового пользователя

        public ICommand CreateNewUserCommand { get; }

        private bool CanCreateNewUserCommandExecute(object p) => CheckParametrs();

        private async void OnCreateNewUserCommandExecuted(object p)
        {
            
            StartLoading();
            await Task.Run(() => Registrarion(p));

        }

        #endregion

        #region Переход на страницу Авторизации

        public ICommand OpenAuthCommand { get; }

        private bool CanOpenAuthCommandExecute(object p) => true;

        private void OnOpenAuthCommandExecuted(object p)
        {
            MessageBus.Send(p);
        }
        #endregion

        #endregion

        #region Данные с формы

        /// <summary>
        /// Колекция (список) категорий
        /// </summary>
        public ObservableCollection<Role> Roles { get; }

        private string _Login;
        public string Login
        {
            get => _Login;
            set => Set(ref _Login, value);
        }

        private string _Password;
        public string Password
        {
            get => _Password;
            set => Set(ref _Password, value);
        }

        private string _Fio;
        public string Fio
        {
            get => _Fio;
            set => Set(ref _Fio, value);
        }

        /// <summary>
        /// Статус модального окна (открыт/заркыт)
        /// </summary>
        private bool _ModalStatus;
        public bool ModalStatus
        {
            get => _ModalStatus;
            set => Set(ref _ModalStatus, value);
        }

        /// <summary>
        /// Видимость грида с информацией
        /// </summary>
        private Visibility _MainGridVisibility = Visibility.Visible;
        public Visibility MainGridVisibility
        {
            get => _MainGridVisibility;
            set => Set(ref _MainGridVisibility, value);
        }

        /// <summary>
        /// Статус видимости загрузки
        /// </summary>
        private bool _LoadingStatus = false;
        public bool LoadingStatus
        {
            get => _LoadingStatus;
            set => Set(ref _LoadingStatus, value);
        }

        /// <summary>
        /// Видимость ошибки
        /// </summary>
        private Visibility _ErrorVisibility = Visibility.Hidden;
        public Visibility ErrorVisibility
        {
            get => _ErrorVisibility;
            set => Set(ref _ErrorVisibility, value);
        }

        /// <summary>
        /// Выбранная в данный момент роль
        /// </summary>
        private Role _SelectedRole;
        public Role SelectedRole
        {
            get => _SelectedRole;
            set => Set(ref _SelectedRole, value);
        }

        #endregion

        #region Методы

        #region Проверка заполненности параметров

        /// <summary>
        /// Проверка заполненности всех параметров
        /// </summary>
        /// <returns></returns>
        private bool CheckParametrs() => !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Fio) && !(SelectedRole==default);

        #endregion

        #region методы переключения видимости

        private void StartLoading()
        {

            MainGridVisibility = Visibility.Collapsed; //выключаю видимость грида с данными
            LoadingStatus = true; //включает анимацию
        }

        private void EndLoading(bool modalstatus=false)
        {
            Thread.Sleep(700);
            MainGridVisibility = Visibility.Visible; //включает видимость грида с данными
            LoadingStatus = false; //выключает анимацию
            
            if(modalstatus)
                ModalStatus = true;
        }

        #endregion

        #region регистрация

        private void Registrarion(object p)
        {
            Role role = SelectedRole;
            //если нет такого пользователя то создать
            if (DataWorker.CreateUser(Login, Password, Fio, role.Name))
            {
                Login = default;
                Password = default;
                Fio = default;
                SelectedRole = default;
                EndLoading(true);
            }
            else
            {
                ErrorVisibility = Visibility.Visible;
                EndLoading();
            }
            
        }

        #endregion

        #endregion

    }
}
