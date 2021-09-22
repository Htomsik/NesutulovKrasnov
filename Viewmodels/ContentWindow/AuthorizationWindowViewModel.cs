namespace Praktika.Viewmodels
{
    internal class AuthorizationWindowViewModel : BaseViewModel
    {
        #region переменные

        #region Nickname

        /// <summary>
        ///     Имя пользователя в базе данных
        /// </summary>
        private string _Nickname = "Никнейм";

        public string Nickname
        {
            get => _Nickname;
            set => Set(ref _Nickname, value);
        }

        #endregion

        #region password

        /// <summary>
        ///     Пароль пользователя в базе данных
        /// </summary>
        private string _Password;

        public string Password
        {
            get => _Password;
            set => Set(ref _Password, value);
        }

        #endregion

        #endregion
    }
}