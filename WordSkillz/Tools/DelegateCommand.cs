using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WordSkillz.Tools
{
    internal class DelegateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Action<object> command;

        public DelegateCommand(Action<object> command)
        {
            this.command = command;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            command?.Invoke(parameter);
        }
    }

    internal class DelegateCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Action<T> command;

        public DelegateCommand(Action<T> command)
        {
            this.command = command;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            command?.Invoke((T)parameter);
        }
    }
}
