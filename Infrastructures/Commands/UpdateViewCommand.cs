using System;
using Praktika.Infrastructures.Commands.Base;
using Praktika.Viewmodels;

namespace Praktika.Infrastructures.Commands
{
    public class UpdatePagesViewCommand : BaseCommand
    {
        private readonly BaseViewModel[] _ViewModelArray =
        {
            new HomapageViewmodel(),
            new CalcPageViewModel()
        };

        private readonly MainContentControlViewModel viewModel;

        public UpdatePagesViewCommand(MainContentControlViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var IntParametr = Convert.ToInt32(parameter);
            if (IntParametr != -1)
                viewModel.SelectedViewModel = _ViewModelArray[IntParametr];
            else
                viewModel.SelectedViewModel = _ViewModelArray[0];
        }
    }



    public class UpdateWindowViewCommand : BaseCommand
    {
        private readonly BaseViewModel[] _ViewModelArray =
        {
            new MainContentControlViewModel()

        };

        private  readonly  MainWindowViewModel viewModel;

        public UpdateWindowViewCommand(MainWindowViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var IntParametr = Convert.ToInt32(parameter);
            if (IntParametr != -1)
                viewModel.SelectedViewModel = _ViewModelArray[IntParametr];
            else
                viewModel.SelectedViewModel = _ViewModelArray[0];
        }
    }

    
}