using System.Windows;
using Praktika.Infrastructures.Commands.Base;

namespace Praktika.Infrastructures.Commands
{
    internal class CloseApplicationCommand : BaseCommand
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }


        public override void Execute(object parameter)
        {
            Application.Current.Shutdown();
        }
    }
}