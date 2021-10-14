using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktika.Models
{
    public class User
    {
        /// <summary>
        /// Хранит id пользователя в byte формате (Не предпологается наличие более 200 юзеров)
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Логин в базе данных (string)
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя (string)
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Роль пользователя в системе (string)
        /// </summary>
        public string Role { get; set;}

        /// <summary>
        /// Фамилия,Имя,Отчество пользователя в системе (string)
        /// </summary>
        public string FIO { get; set; }
    }

    public static class CurrentUser
    {
        public static User _CurrentUser;
    }
}
