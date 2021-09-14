using System;
using System.Windows.Input;

namespace SandBox
{
    public class SimpleCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        Func<object, bool> CanExecuteDelegate { get; set; }
        Action<object> ExecuteDelegate { get; set; }

        public bool CanExecute(object parameter)
        {
            var canExecute = CanExecuteDelegate;
            return canExecute == null || canExecute.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            ExecuteDelegate?.Invoke(parameter);
        }

        public SimpleCommand(Func<object, bool> canExcute = null, Action<object> execute = null)
        {
            CanExecuteDelegate = canExcute;
            ExecuteDelegate = execute;
        }
    }
}
