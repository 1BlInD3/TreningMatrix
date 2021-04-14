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
    class NewTrainingCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return parameter is Employee;
        }

        public override void Execute(object parameter)
        {
            var employee = parameter as Employee;
            var training = new Training() { DolgozoTsz = employee.Torzsszam };
            var window = new TrainingEditWindow(training) { Owner = Application.Current.MainWindow };
            window.StateChanged += delegate
            {
                if (window.WindowState == WindowState.Minimized) window.Owner.WindowState = WindowState.Minimized;
                else if (window.WindowState == WindowState.Normal) window.Owner.WindowState = WindowState.Normal;
            };

            window.ShowDialog();

            if (training.IsUnderEdit) training.CancelEdit();
        }
    }

    class EditTrainingCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return parameter is Training;
        }

        public override void Execute(object parameter)
        {
            Training training = parameter as Training;
            Window window = new TrainingEditWindow(training) { Owner = Application.Current.MainWindow };
            window.StateChanged += delegate
            {
                if (window.WindowState == WindowState.Minimized) window.Owner.WindowState = WindowState.Minimized;
                else if (window.WindowState == WindowState.Normal) window.Owner.WindowState = WindowState.Normal;
            };

            window.ShowDialog();

            if (training.IsUnderEdit) training.CancelEdit();
        }
    }

    class DeleteTrainingCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return parameter is Training;
        }

        public override void Execute(object parameter)
        {
            var t = parameter as Training;

            var result = MessageBox.Show(
                "Biztosan törlöd '" + t.DolgozoNev + "' nevű dolgozó tréningét?",
                "Tréning törlése",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                t.Remove();
                ViewModelBase.SaveChangesToDatabase();
            }
        }
    }
}
