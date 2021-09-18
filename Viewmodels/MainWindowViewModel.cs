using Praktika.Viewmodels.Base;
using System.Collections.ObjectModel;


namespace Praktika.Viewmodels
{
    class MainWindowViewModel: BaseViewModel
    {
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

        }

    }
}
