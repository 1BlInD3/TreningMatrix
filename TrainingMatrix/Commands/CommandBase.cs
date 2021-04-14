using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Treningelo.Models;

namespace Treningelo.Commands
{
    class CommandBase : ModelBase, ICommand
    {
        private readonly Action action;

        public CommandBase() { }
        public CommandBase(Action action)
        {
            this.action = action;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public virtual void Execute(object parameter)
        {
            action?.Invoke();
            if (parameter is Window w) w.Close();
        }
    }
}