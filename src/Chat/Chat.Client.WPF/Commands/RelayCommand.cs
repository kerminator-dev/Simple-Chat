using System;
using System.Windows.Input;

namespace Chat.Client.WPF.Commands
{
    internal class RelayCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly Func<object?, bool>? _canExecute;
        private readonly Action<object?> _execute;

        public RelayCommand(Action<object?> execute) 
            : this(null, execute)
        {
           
        }

        public RelayCommand(Func<object?, bool>? canExecute, Action<object?> execute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        public bool CanExecute(object? parameter)
        {
            if (_canExecute == null)
                return true;

            return _canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute.Invoke(parameter);
        }
    }
}
