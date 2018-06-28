using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MT
{
    /// <summary>
    /// Can only handle un parameterised commands
    /// </summary>
    class DelegateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action ExecuteAction { get; }
        private Func<bool> CanExecuteFunction { get; } = null;

        public bool CanExecute(object parameter)
        {
            if (CanExecuteFunction != null)
            {
                return CanExecuteFunction();
            }
            else
            { 
                return true;
            }
        }

        public void Execute(object parameter)
        {
            ExecuteAction?.Invoke();
        }

        public DelegateCommand(Action executeAction, Func<bool> canExecuteFunction)
        {
            ExecuteAction = executeAction;
            CanExecuteFunction = canExecuteFunction;
        }

        public DelegateCommand(Action executeAction)
        {
            ExecuteAction = executeAction;
        }
    }
}
