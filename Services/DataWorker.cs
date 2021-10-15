using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Praktika.Models;
using Praktika.Models.Data;
using Praktika.Viewmodels;

namespace Praktika.Services
{
    public static class DataWorker
    {

        #region Пользователи

        #region Создание пользователя

        /// <summary>
        /// Создание пользователя true-пользователь успешно создан
        /// </summary>
        /// <param name="_Login"></param>
        /// <param name="_Password"></param>
        /// <param name="_Role"></param>
        /// <param name="_FIO"></param>
        public static bool CreateUser(string _Login, string _Password,string _FIO, string _Role )
        {

            using (AppDbContext db = new AppDbContext())
            {
                //check the user is exist
                bool checkIsExist = db.Users.Any(el => el.Login == _Login);
                if (!checkIsExist)
                {
                    User newUser = new User
                    {
                        Login = _Login,
                        Password = _Password,
                        Role = _Role,
                        FIO = _FIO
                    };
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    
                }
                else
                {
                    return false;
                }
                

            }

            return true;

        }

        #endregion

        #region Авторизация

        /// <summary>
        /// Метод авторизации с формы
        /// </summary>
        /// <param name="_Login"></param>
        /// <param name="_Password"></param>
        /// <returns></returns>
        public static bool Authorization(string _Login, string _Password)
        {

            using (AppDbContext db = new AppDbContext())
            {
                UserSingltonViewmodel s1 = UserSingltonViewmodel.Initialize;

                User _checkuser = db.Users.FirstOrDefault(el => el.Login == _Login && el.Password == _Password);

                if (_checkuser != default)
                {
                    s1.CurrentUser = _checkuser;
                    JsonWorker js = new JsonWorker();
                    js.SaveUser();
                    return true;
                }
                
            }
            return false;
        }

        /// <summary>
        /// Авторизация из файла
        /// </summary>
        /// <returns></returns>
        public static bool Authorization()
        {

            using (AppDbContext db = new AppDbContext())
            {
                UserSingltonViewmodel s1 = UserSingltonViewmodel.Initialize;

                return db.Users.Any(el =>
                    el.Login == s1.CurrentUser.Login && el.Password == s1.CurrentUser.Password);

            }
            return false;
        }


        #endregion

        #region Работа с тиблицами

        #region Вывод списка видеокарт (list)

        //получить все видеокарты
        public static List<Videocard> GetAllVideocards()
        {
            using (AppDbContext db = new AppDbContext())
            {
               return db.Videocards.ToList();
                
            }
        }

        #endregion

        #region Создать видеокарту

        public static bool CreateVideocard(string _Company, string _Name, byte _TechProcess, string _MemoryType, string _Interface, DateTime _RealeseDate)
        {

            using (AppDbContext db = new AppDbContext())
            {
                bool checkIsExist = db.Videocards.Any(el => el.Name == _Name);

                if (!checkIsExist)
                {
                    Videocard newVideocard = new Videocard()
                    {
                        Company = _Company,
                        Name = _Name,
                        TechProcess = _TechProcess,
                        MemoryType = _MemoryType,
                        Intarface = _Interface,
                        RealeaseDate = _RealeseDate
                    };

                    db.Videocards.Add(newVideocard);
                    db.SaveChanges();
                    
                }
                else
                {
                    return false;
                }

            }

            return true;

        }

        #endregion

        #endregion


        #endregion

    }
}
