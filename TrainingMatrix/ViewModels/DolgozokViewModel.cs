using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Treningelo.Models;
using Treningelo.Commands;
using Treningelo.Windows;
using System.Windows;
using System.Collections.Specialized;
using System.Windows.Controls;

namespace Treningelo.ViewModels
{
    class DolgozokViewModel : ViewModelBase
    {
        public UserControl View { get; set; }

        private string searchText;
        public string SearchText
        {
            get => searchText ?? (searchText = string.Empty);
            set
            {
                searchText = value;
                OnPropertyChanged();
                EmployeesView.Refresh();
            }
        }

        private Employee selectedEmployee;
        public Employee SelectedEmployee
        {
            get => selectedEmployee;
            set
            {
                    selectedEmployee = value;
                    OnPropertyChanged();
                    TrainingsView.Refresh();
            }
        }
        private Training selectedTraining;
        public Training SelectedTraining
        {
            get => selectedTraining;
            set
            {
                selectedTraining = value;
                OnPropertyChanged();
            }
        }

        private ICommand editEmployeeCommand;
        public ICommand EditEmployeeCommand
        {
            get => editEmployeeCommand ?? (editEmployeeCommand = new EditEmployeeCommand());
        }
        private ICommand deleteEmployeeCommand;
        public ICommand DeleteEmployeeCommand
        {
            get => deleteEmployeeCommand ?? (deleteEmployeeCommand = new DeleteEmployeeCommand());
        }
        private ICommand newEmployeeCommand;
        public ICommand NewEmployeeCommand
        {
            get => newEmployeeCommand ?? (newEmployeeCommand = new NewEmployeeCommand());
        }

        private ICommand editTrainingCommand;
        public ICommand EditTrainingCommand
        {
            get => editTrainingCommand ?? (editTrainingCommand = new EditTrainingCommand());
        }
        private ICommand deleteTrainingCommand;
        public ICommand DeleteTrainingCommand
        {
            get => deleteTrainingCommand ?? (deleteTrainingCommand = new DeleteTrainingCommand());
        }
        private ICommand newTrainingCommand;
        public ICommand NewTrainingCommand
        {
            get => newTrainingCommand ?? (newTrainingCommand = new NewTrainingCommand());
        }

        public bool EmployeesFilter(object o)
        {
            var e = o as Employee;

            if (int.TryParse(SearchText, out int number))
            {
                if (e.Torzsszam == number.ToString()) return true;
                else return false;
            }

            if (e.Nev.ToLower().Contains(SearchText.ToLower()) |
                e.Torzsszam.ToLower().Contains(SearchText.ToLower())) return true;
            
            return false;
        }
        public bool TrainingsFilter(object o)
        {
            var t = o as Training;
            if (t.DolgozoTsz == SelectedEmployee?.Torzsszam /*&& SelectedEmployee ?.Megjegyzes != "archiv"*/) return true;
            else return false;
        }

        private ListCollectionView employeesView;
        public ListCollectionView EmployeesView
        {
            get => employeesView ?? (employeesView = new ListCollectionView(Employees) { Filter = EmployeesFilter });
        }
        private ListCollectionView trainingsView;
        public ListCollectionView TrainingsView
        {
            get => trainingsView ?? (trainingsView = new ListCollectionView(Trainings) { Filter = TrainingsFilter });
        }
    }
}
