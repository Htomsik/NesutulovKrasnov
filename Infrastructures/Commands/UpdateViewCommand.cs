using System;
using Praktika.Viewmodels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Praktika.Infrastructures.Commands.Base;

namespace Praktika.Infrastructures.Commands
{
    public class UpdateViewCommand: BaseCommand
    {
        private MainWindowViewModel viewModel;
        public UpdateViewCommand(MainWindowViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override bool CanExecute(object parameter) => true;

        Praktika.Viewmodels.BaseViewModel[] _ViewModelArray = new Praktika.Viewmodels.BaseViewModel[]
           {
                new HomapageViewmodel(),
                new CalcPageViewModel()
           };

        public override void Execute(object parameter)
        {
            var IntParametr = Convert.ToInt32(parameter);
            if (IntParametr != -1)
            {
                viewModel.SelectedViewModel = _ViewModelArray[IntParametr];
            }
            else
            {
                viewModel.SelectedViewModel = _ViewModelArray[0];
            }



        }
    }
}
