using System;
using Praktika.Infrastructures.Commands.Base;

namespace Praktika.Infrastructures.Commands
{
    public class LambdaCommand : BaseCommand
    {
        private readonly Func<object, bool> _CanExecute;
        private readonly Action<object> _Execute;

        public LambdaCommand(Action<object> Execute, Func<object, bool> CanExecute = null)
        {
            _Execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
            _CanExecute = CanExecute;
        }

        public override bool CanExecute(object parameter)
        {
            return _CanExecute?.Invoke(parameter) ?? true;
        }

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter)) return;
            _Execute(parameter);
        }
    }
}