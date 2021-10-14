using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Praktika.Models;
using Praktika.Viewmodels;


namespace Praktika.Services
{
    public class JsonWorker
    {

        

        private string UserPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "Praktika4Kurs/User.js");

        private string SettingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "Praktika4Kurs/Settings.js");

        private string DirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "Praktika4Kurs");


        bool CheckDirectory()
        {
            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
                return false;
            }

            return true;
        }


        /// <summary>
        /// Сохранение юзера в файле
        /// </summary>
        public  void SaveUser()
        {

            var save = JsonConvert.SerializeObject(UserSingltonViewmodel.Initialize.CurrentUser);

            File.WriteAllText(UserPath, save);
        }

        /// <summary>
        /// Получение текущего пользователя
        /// </summary>
        public void GetUser()
        {
            if (File.Exists(UserPath)&&CheckDirectory())
            {
                UserSingltonViewmodel.Initialize.CurrentUser =
                    JsonConvert.DeserializeObject<User>(File.ReadAllText(UserPath));
            }
           
        }

        /// <summary>
        /// Сохранение настроек в файле
        /// </summary>
        public void SaveSettings(bool checkstatus)
        {

            var save = JsonConvert.SerializeObject(checkstatus);

            File.WriteAllText(SettingsPath, save);
        }

        /// <summary>
        /// Получение текущих настроек
        /// </summary>
        public bool GetSettings()
        {
            if (File.Exists(SettingsPath) && CheckDirectory())
            {
               
                  return JsonConvert.DeserializeObject<bool>(File.ReadAllText(SettingsPath));
            }

            return false;
        }


    }
}
