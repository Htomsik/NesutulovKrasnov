using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Praktika.Models;

namespace Praktika.Viewmodels
{
    public class UserSingltonViewmodel : BaseViewModel
    {

        private User _CurrentUser;
        /// <summary>
        /// Текущий пользователь в приложении
        /// </summary>
        public User CurrentUser
        {
            get => _CurrentUser;
            set => Set(ref _CurrentUser, value);
        }

        private static UserSingltonViewmodel initialize;

        public UserSingltonViewmodel()
        {

        }

        public static UserSingltonViewmodel Initialize
        {
            get => initialize ?? (initialize = new UserSingltonViewmodel());
        }
       
    }
}
