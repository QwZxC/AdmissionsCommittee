using AdmissionsCommittee.Commands.Base;
using System;

namespace AdmissionsCommittee.Commands
{
    public class LambdaCommand : Command
    {
        readonly Action<object> execute;
        readonly Func<object, bool> canExecute;

        public LambdaCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(Execute));
            this.canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
           return canExecute?.Invoke(parameter) ?? true;
        }

        public override void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
