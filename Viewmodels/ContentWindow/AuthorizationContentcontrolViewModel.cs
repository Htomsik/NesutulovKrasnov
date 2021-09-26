using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            MessageBus.Send(p);
        }
        #endregion

        

    }
}
