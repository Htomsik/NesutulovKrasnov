using System.Windows;
using System.Windows.Input;
using Praktika.Services;
using Praktika.Infrastructures.Commands;

namespace Praktika.Viewmodels
{
    public class RegistContentControlViewModel:BaseViewModel
    {
        public RegistContentControlViewModel()
        {
            CreateNewUserCommand = new LambdaCommand(OnCreateNewUserCommandExecuted, CanCreateNewUserCommandExecute);

            OpenAuthCommand = new LambdaCommand(OnOpenAuthCommandExecuted, CanOpenAuthCommandExecute);

            

        }

        #region Данные с формы

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

        private string _Role;
        public string Role
        {
            get => _Role;
            set => Set(ref _Role, value);
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

        #endregion


        #region Commands

        #region создание нового пользователя

        public ICommand CreateNewUserCommand { get; }

        private bool CanCreateNewUserCommandExecute(object p) => true;

        private void OnCreateNewUserCommandExecuted(object p)
        {
            if (DataWorker.CreateUser(Login, Password, Fio, Role))
            {
                ModalStatus = true;
            }

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





    }
}
