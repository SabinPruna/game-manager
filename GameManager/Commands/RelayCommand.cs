using System;
using System.Windows.Input;

namespace GameManager.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Func<object, bool> _canExecute;
        private readonly Action<object> _execute;

        #region Constructors

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion

        #region Interface Implementations

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        #endregion
    }
}