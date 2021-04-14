using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Treningelo.ViewModels;

namespace Treningelo.Commands
{
    class AddToTrainingListCommand : CommandBase
    {
        private ObservableCollection<Employee> employees;

        public AddToTrainingListCommand(ObservableCollection<Employee> employees)
        {
            this.employees = employees;
        }

        public override bool CanExecute(object parameter)
        {
            return parameter as IList<object> != null;
        }

        public override void Execute(object parameter)
        {
            foreach (var o in parameter as IList<object>)
            {
                var t = o as Training;
                var e = ViewModelBase.Employees.First(x => x.Torzsszam == t.DolgozoTsz);
                if (!employees.Contains(e)) employees.Add(e);
            }
        }
    }

    class RemoveFromTrainingListCommand : CommandBase
    {
        private ObservableCollection<Employee> employees;

        public RemoveFromTrainingListCommand(ObservableCollection<Employee> employees)
        {
            this.employees = employees;
        }

        public override bool CanExecute(object parameter)
        {
            return parameter as Employee != null;
        }

        public override void Execute(object parameter)
        {
            employees.Remove(parameter as Employee);
        }
    }
}
