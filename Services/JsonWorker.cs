using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Praktika.Models;
using Praktika.Viewmodels;


namespace Praktika.Services
{
    public class JsonWorker
    {
        private string UserPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "Praktika4Kurs/User.js");

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
        public void SaveUser()
        {

            var save = JsonConvert.SerializeObject(UserSingltonViewmodel.Initialize.CurrentUser);

            File.WriteAllText(UserPath, save);
        }

        /// <summary>
        /// Сохранение текущего пользователя
        /// </summary>
        public void GetUser()
        {
            
            if (File.Exists(UserPath))
            {
                UserSingltonViewmodel.Initialize.CurrentUser =
                    JsonConvert.DeserializeObject<User>(File.ReadAllText(UserPath));
            }
           
        }
    }
}
