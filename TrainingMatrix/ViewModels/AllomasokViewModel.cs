using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Treningelo.Commands;
using Treningelo.Models;
using Excel = Microsoft.Office.Interop.Excel;

namespace Treningelo.ViewModels
{
    class AllomasokViewModel : ViewModelBase
    {
        private TpTerulet selectedTerulet;
        public TpTerulet SelectedTerulet
        {
            get => selectedTerulet;
            set
            {
                selectedTerulet = value;
                IsSoleLine = Sorok.Count() == 1;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Sorok));
            }
        }
        public IEnumerable<TpTerulet> Teruletek
        {
            get
            {
                yield return new TpTerulet() { Id = -1, Nev = "[Összes]" };
                var teruletek = from t in database.TpTerulet select t;
                foreach (var t in teruletek) yield return t;
            }
        }

        private bool isSoleLine;
        public bool IsSoleLine
        {
            get => isSoleLine;
            set
            {
                isSoleLine = value;
                OnPropertyChanged();
            }
        }

        private TpSor selectedSor;
        public TpSor SelectedSor
        {
            get => selectedSor;
            set
            {
                selectedSor = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Allomasok));
            }
        }
        public IEnumerable<TpSor> Sorok
        {
            get
            {
                yield return new TpSor() { Id = -1, Nev = "[Összes]" };
                if (SelectedTerulet.Id == -1) yield break;
                var sorok = from s in database.TpSor where s.TeruletId == SelectedTerulet.Id select s;
                foreach (var s in sorok) yield return s;
            }
        }

        private TpAllomas selectedAllomas;
        public TpAllomas SelectedAllomas
        {
            get => selectedAllomas;
            set
            {
                selectedAllomas = value;
                OnPropertyChanged();
                TrainingsView.Refresh();
            }
        }
        public IEnumerable<TpAllomas> Allomasok
        {
            get
            {
                yield return new TpAllomas() { Id = -1, Nev = "[Összes]" };
                if (SelectedTerulet.Id == -1) yield break;
                if (SelectedSor.Id == -1 && Sorok.Count() > 1) yield break;
                var allomasok = from a in database.TpAllomas where a.TeruletId == SelectedTerulet.Id select a;
                if (SelectedSor.Id != -1) allomasok = allomasok.Where(x => x.SorId == SelectedSor.Id);
                foreach (var a in allomasok) yield return a;
            }
        }

        private bool TrainingsFilter(object o)
        {
            var t = o as Training;

            if (SelectedTerulet == null | SelectedSor == null | SelectedAllomas == null) return true;

            if (SelectedTerulet.Id == -1) return true;

            if (SelectedSor.Id == -1 && !isSoleLine) return t.TeruletId == SelectedTerulet.Id;

            if (SelectedAllomas.Id == -1) return t.TeruletId == SelectedTerulet.Id && t.SorId == SelectedSor.Id;

            return t.TeruletId == SelectedTerulet.Id && t.SorId == SelectedSor.Id && t.AllomasId == SelectedAllomas.Id;
        }

        private ListCollectionView employeesView;
        public ListCollectionView EmployeesView
        {
            get => employeesView ?? (employeesView = new ListCollectionView(Employees));
        }
        private ListCollectionView trainingsView;

        public ListCollectionView TrainingsView
        {
            get => trainingsView ?? (trainingsView = new ListCollectionView(Trainings) { Filter = TrainingsFilter });
        }

        private TrainingList employeesToTrain;
        public TrainingList EmployeesToTrain
        {
            get => employeesToTrain ?? (employeesToTrain = new TrainingList());
        }

        private ICommand addToEmployeesToTrainCommand;
        public ICommand AddToEmployeesToTrainCommand
        {
            get => addToEmployeesToTrainCommand ?? (addToEmployeesToTrainCommand = new AddToTrainingListCommand(EmployeesToTrain));
        }

        private ICommand removeFromEmployeesToTrainCommand;
        public ICommand RemoveFromEmployeesToTrainCommand
        {
            get => removeFromEmployeesToTrainCommand ?? (removeFromEmployeesToTrainCommand = new RemoveFromTrainingListCommand(EmployeesToTrain));
        }

        private ICommand printTrainingSheetCommand;
        public ICommand PrintTrainingSheetCommand
        {
            get => printTrainingSheetCommand ?? (printTrainingSheetCommand = new CommandBase(() => new TrainingSheetPrinter().PrintTrainingSheet(EmployeesToTrain)));
        }

        private class TrainingSheetPrinter
        {
            private static readonly int TorzsszamColumn = 2;
            private static readonly int NevColumn = 3;
            private static readonly int SorszamColumn = 1;

            private Excel.Application xl;
            private Excel.Workbook wb;
            private Excel.Worksheet ws;

            public void PrintTrainingSheet(IList<Employee> dolgozok)
            {
                int sorszam = 1;
                int currentRow = 18;
                int lastRow = 45;

                Open(true);

                foreach (var d in dolgozok)
                {
                    if (currentRow > lastRow)
                    {
                        Print();
                        Dispose();
                        Open(false);
                        currentRow = 3;
                        lastRow = 47;
                    }
                    ws.Cells[currentRow, SorszamColumn].Value = sorszam++.ToString();
                    ws.Cells[currentRow, TorzsszamColumn].Value = d.Torzsszam;
                    ws.Cells[currentRow, NevColumn].Value = d.Nev;
                    currentRow++;
                }

                Print();
                Dispose();
            }

            private void Open(bool isFirstPage)
            {

                if (isFirstPage)
                {
                    xl = new Excel.Application();
                    xl.Visible = false;
                    wb = xl.Workbooks.Open(Environment.CurrentDirectory + @"\kepzesilap_ures.xlsx");
                }
                else
                {
                    xl = new Excel.Application();
                    xl.Visible = false;
                    wb = xl.Workbooks.Open(Environment.CurrentDirectory + @"\kepzesilap_ures_p2+.xlsx");
                }
                ws = wb.Sheets[1];
            }

            private void Print()
            {
                ws.PrintOutEx(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }

            private void Dispose()
            {
                wb.Close(false, Missing.Value, Missing.Value);
                xl.Quit();
            }
        }
    }
}
