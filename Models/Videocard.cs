using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktika.Models
{
    public class Videocard
    {

        /// <summary>
        /// Id видеокарты в базе данных
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Имя компании производителя
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// Название видеокарты
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Техпроцесс видеокарты
        /// </summary>
        public byte TechProcess { get; set; }

        /// <summary>
        /// Тип памяти
        /// </summary>
        public string MemoryType { get; set; }

        /// <summary>
        /// Интерйфес подключения видеокарты
        /// </summary>
        public string Intarface { get; set; }

        /// <summary>
        /// Дата релиза
        /// </summary>
        public DateTime RealeaseDate { get; set; }


    }
}
