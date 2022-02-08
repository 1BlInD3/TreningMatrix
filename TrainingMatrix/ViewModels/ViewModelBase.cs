using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Treningelo.Models;

namespace Treningelo.ViewModels
{
    public abstract class ViewModelBase : ModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private static ObservableCollection<Employee> employees;
        private static ArrayList archive= new ArrayList();
        public static ObservableCollection<Employee> Employees
        {
            get
            {
                if (employees != null) return employees;
                employees = new ObservableCollection<Employee>();
                foreach (var d in database.TpDolgozo) {
                    if (d.Megjegyzes != "archiv")
                    {
                        employees.Add(new Employee(d));
                    }
                    else {
                        archive.Add(d.Torzsszam.ToString());
                    }
                }
                return employees;
            }
        }

        private static ObservableCollection<Training> trainings;
        public static ObservableCollection<Training> Trainings
        {
            get
            {
                if (trainings != null) return trainings;
                trainings = new ObservableCollection<Training>();
                foreach (var e in database.TpDolgozo) {
                    if (e.Megjegyzes == "archiv") {
                        archive.Add(e.Torzsszam.ToString());
                    }
                }
                foreach (var t in database.TpTrening)
                {
                    if (!archive.Contains(t.DolgozoTsz))
                    {
                        trainings.Add(new Training(t));
                    }
                }
                return trainings;
            }
        }

        private static readonly string[] usersWithEditRights =
        {
            "szanyigabriella",
            "balindattila",
            "gazdagviktoria",
            "mioveczagnes"
        };
        public static bool HasEditRights
        {
            get => Environment.UserDomainName.ToLower() == "fusetech" &&
                usersWithEditRights.Contains(Environment.UserName.ToLower());
        }

        public static void SaveChangesToDatabase()
        {
            try
            {
                database.SaveChanges();
                System.Diagnostics.Debug.WriteLine("Changes saved to database");
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    "Hiba az adatbázisba való mentéskor!\nA program most kilép.\n\n" + e.Message,
                    "Hiba",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                Environment.Exit(-1);
            }
        }
    }
}
