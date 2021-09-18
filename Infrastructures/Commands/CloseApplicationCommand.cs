using Praktika.Infrastructures.Commands.Base;
using System.Windows;

namespace Praktika.Infrastructures.Commands
{
    class CloseApplicationCommand : BaseCommand
    {
        public override bool CanExecute(object parameter) => true;


        public override void Execute(object parameter)
        {
            Application.Current.Shutdown();
        }
    }
}
