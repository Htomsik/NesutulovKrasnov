using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Praktika.Infrastructures.Commands;
using Praktika.Models;
using Praktika.Services;

namespace Praktika.Viewmodels
{
    public class DataPageViewModel:BaseViewModel
    {
        public DataPageViewModel()
        {
            Videocards = new ObservableCollection<Videocard>(DataWorker.GetAllVideocards());

            Companys = new List<Company>
            {
                new Company{Name = "Amd"},
                new Company{Name = "Nvidia"},
                new Company{Name = "Intel"}
            };

            MemoryTypes = new List<MemoryType>
            {
                new MemoryType {Name = "GDDR3"},
                new MemoryType {Name = "GDDR4"},
                new MemoryType {Name = "GDDR5"},
                new MemoryType {Name = "GDDR5X"},
                new MemoryType {Name = "GDDR6"},
                new MemoryType {Name = "GDDR6X"},
                new MemoryType {Name = "HBM2"},
                new MemoryType {Name = "HBM3"}

            };

            Interfaces = new List<Interface>
            {
                new Interface {Name = "AGP 4x"},
                new Interface {Name = "AGP 8x"},
                new Interface {Name = "PCI-e 3.0 x4"},
                new Interface {Name = "PCI-e 3.0 x16"},
                new Interface {Name = "PCI-e 4.0 x4"},
                new Interface {Name = "PCI-e 4.0 x16"},
                new Interface {Name = "PCI-e 5.0 x4"},
                new Interface {Name = "PCI-e 5.0 x16"},
            };
        }

        public ObservableCollection<Videocard> Videocards { get; set; }

        public List<Company> Companys { get; }

        public List<MemoryType> MemoryTypes { get; }

        public List<Interface> Interfaces { get; }


        #region Данные с формы


        private Videocard _SelectedCard;
        /// <summary>
        /// Текущая выбранная карта в датагриде
        /// </summary>
        public Videocard SelectedCard
        {
            get => _SelectedCard;
            set => Set(ref _SelectedCard, value);
        }

        private Company _SelectedCompany;
        /// <summary>
        /// Текущий выбранный производитель видеокарты
        /// </summary>
        public Company SelectedCompany
        {
            get => _SelectedCompany;
            set => Set(ref _SelectedCompany, value);
        }

        private MemoryType _SelectedMemoryType;
        /// <summary>
        /// Текущий выбранный тип памяти видеокарты
        /// </summary>
        public MemoryType SelectedMemoryType
        {
            get => _SelectedMemoryType;
            set => Set(ref _SelectedMemoryType, value);
        }


        private Interface _SelectedInterface;
        /// <summary>
        /// Текущий выбранный интерфейс видеокарты
        /// </summary>
        public Interface SelectedInterface
        {
            get => _SelectedInterface;
            set => Set(ref _SelectedInterface, value);
        }

        private string _VideocardName;
        /// <summary>
        /// Имя видеокарты для создания
        /// </summary>
        public string VideocardName
        {
            get => _VideocardName;
            set => Set(ref _VideocardName, value);
        }

        private string _VideocardCore;
        /// <summary>
        /// Ядро видеокарты для создания
        /// </summary>
        public string VideocardCore
        {
            get => _VideocardCore;
            set => Set(ref _VideocardCore, value);
        }

        private byte _VideocardTechProcess;
        /// <summary>
        /// Техпроцесс видеокарты для создания
        /// </summary>
        public byte VideocardTechProcess
        {
            get => _VideocardTechProcess;
            set => Set(ref _VideocardTechProcess, value);
        }

        #endregion
    }
}
