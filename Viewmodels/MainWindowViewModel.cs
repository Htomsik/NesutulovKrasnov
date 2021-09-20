using Praktika.Viewmodels.Base;
using System.Collections.ObjectModel;
using Praktika.Models;


namespace Praktika.Viewmodels
{
    class MainWindowViewModel: BaseViewModel
    {
        public ObservableCollection<Pages> Pages { get; set; }

        #region Title
        private string _Title = "Несутулов К.C";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        public MainWindowViewModel()
        {
            Pages = new ObservableCollection<Pages>
            {
                      new Pages{URLicon="Solid_Calculator",NamePage="Калькулятор"},
                      new Pages{URLicon="Solid_Calculator",NamePage="Калькулятор"},
                      new Pages{URLicon="Solid_Calculator",NamePage="Калькулятор"},
                      new Pages{URLicon="Solid_Calculator",NamePage="Калькулятор"},
                      new Pages{URLicon="Solid_Calculator",NamePage="Калькулятор"},
                      new Pages{URLicon="Solid_Calculator",NamePage="Калькулятор"},
                      new Pages{URLicon="Solid_Calculator",NamePage="Калькулятор"},
                      new Pages{URLicon="Solid_Calculator",NamePage="Калькулятор"},

            };
        }

    }
}
