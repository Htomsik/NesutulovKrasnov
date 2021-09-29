using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using  Praktika.Services;
using Praktika.Infrastructures.Commands;


namespace Praktika.Viewmodels
{
    public class AuthorizationContentcontrolViewModel:BaseViewModel
    {

        public AuthorizationContentcontrolViewModel()
        {
            SendContentControlNumerCommand =
                new LambdaCommand(OnContentControlNumerExecuted, CanContentControlNumerExecute);
        }

        #region Отправка номера страницы

        public ICommand SendContentControlNumerCommand { get; }

        private bool CanContentControlNumerExecute(object p) => true;

        private void OnContentControlNumerExecuted(object p)
        {
            
            //Если есть такой пользователь то открыть главную страницу
            if (DataWorker.Authorization(_Login, _Password))
            {
                Thread.Sleep(150);
                MessageBus.Send(p);
            }
            else
            {
                //показ ошибки
                ErrorVisibility = Visibility.Visible;
            }
        }
        #endregion

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


        //Видимость 
        private Visibility _ErrorVisibility = Visibility.Hidden;

        public Visibility ErrorVisibility
        {
            get => _ErrorVisibility;
            set => Set(ref _ErrorVisibility, value);
        }

        #endregion

    }
}
