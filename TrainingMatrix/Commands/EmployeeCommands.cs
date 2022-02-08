using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Treningelo.ViewModels;
using Treningelo.Windows;

namespace Treningelo.Commands
{
    class NewEmployeeCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var employee = new Employee();
            var window = new EmployeeEditWindow(employee) { Owner = Application.Current.MainWindow };
            window.StateChanged += delegate
            {
                if (window.WindowState == WindowState.Minimized) window.Owner.WindowState = WindowState.Minimized;
                else if (window.WindowState == WindowState.Normal) window.Owner.WindowState = WindowState.Normal;
            };

            window.ShowDialog();

            if (employee.IsUnderEdit) employee.CancelEdit();
        }
    }

    class EditEmployeeCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return parameter is Employee;
        }

        public override void Execute(object parameter)
        {
            Employee employee = parameter as Employee;
            Window window = new EmployeeEditWindow(employee) { Owner = Application.Current.MainWindow };
            window.StateChanged += delegate
            {
                if (window.WindowState == WindowState.Minimized) window.Owner.WindowState = WindowState.Minimized;
                else if (window.WindowState == WindowState.Normal) window.Owner.WindowState = WindowState.Normal;
            };

            window.ShowDialog();

            if (employee.IsUnderEdit) employee.CancelEdit();
        }
    }

    class DeleteEmployeeCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return parameter is Employee;
        }

        public override void Execute(object parameter)
        {
            var e = parameter as Employee;

            var result = MessageBox.Show(
                "Biztosan törlöd '" + e.Nev + "' nevű dolgozót és tréningeit?",
                "Dolgozó törlése",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                e.Megjegyzes = "archiv";
                ViewModelBase.SaveChangesToDatabase();
            }
        }
    }
}
