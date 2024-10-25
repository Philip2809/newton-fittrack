using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FitTrack.MVVM
{
    // Generic implementation of ICommand to easily create commands for buttons

    class RelayCommand<Type> : ICommand
    {
        private Action<Type> execute;
        private Func<object, bool>? canExecute;

        public RelayCommand(Action<Type> execute, Func<object, bool>? canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute((Type)parameter);
        }
    }
}
