using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Treningelo.Commands;
using Treningelo.Views;

namespace Treningelo.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private ICommand navigateToAllomasok;
        public ICommand NavigateToAllomasok
        {
            get => navigateToAllomasok ?? (navigateToAllomasok = new NavigateMenuCommand(new AllomasokView()));
        }

        private ICommand navigateToDolgozok;
        public ICommand NavigateToDolgozok
        {
            get => navigateToDolgozok ?? (navigateToDolgozok = new NavigateMenuCommand(new DolgozokView()));
        }

        private ICommand navigateToStats;
        public ICommand NavigateToStats
        {
            get => navigateToStats ?? (navigateToStats = new NavigateMenuCommand(new StatsView()));
        }

        private ICommand navigateToReports;
        public ICommand NavigateToReports
        {
            get => navigateToReports ?? (navigateToReports = new NavigateMenuCommand(new ReportView()));
        }
    }
}
