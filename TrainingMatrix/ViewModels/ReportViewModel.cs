using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Treningelo.Commands;
using Excel = Microsoft.Office.Interop.Excel;

namespace Treningelo.ViewModels
{
    class ReportViewModel : ViewModelBase
    {
        public bool Tsz { get; set; } = true;
        public bool Nev { get; set; } = true;
        public bool Megjegyzes { get; set; } = true;
        public bool Terulet { get; set; } = true;
        public bool Sor { get; set; } = true;
        public bool Allomas { get; set; } = true;
        public bool Complete { get; set; } = true;
        public bool Start { get; set; } = true;
        public bool End { get; set; } = true;
        public bool Trainer { get; set; } = true;
        public bool Mentor { get; set; } = true;
        public bool Points { get; set; } = true;
        public bool Restriction { get; set; } = true;
        public bool Material { get; set; } = true;
        //
        public bool Pontertek { get; set; } = true;
        //

        private ICommand createReportCommand;
        public ICommand CreateReportCommand
        {
            get => createReportCommand ?? (createReportCommand = new CommandBase(() => Task.Factory.StartNew(() => CreateReport())));
        }

        private bool notCurrentlyCreatingReport = true;
        public bool NotCurrentlyCreatingReport
        {
            get { return notCurrentlyCreatingReport; }
            set
            {
                notCurrentlyCreatingReport = value;
                OnPropertyChanged();
            }
        }

        private string reportButtonText = "Riport készítése";
        public string ReportButtonText
        {
            get { return reportButtonText; }
            set
            {
                reportButtonText = value;
                OnPropertyChanged();
            }
        }

        private void CreateReport()
        {
            NotCurrentlyCreatingReport = false;
            ReportButtonText = "A riport készítése folyamatban...";

            var xl = new Excel.Application();
            xl.Visible = false; //false
            Excel.Workbook wb = xl.Workbooks.Add("");
            Excel.Worksheet ws = wb.ActiveSheet;

            var currCol = 1;
            if (Tsz) ws.Cells[currCol++][1] = "Törzsszám";
            if (Nev) ws.Cells[currCol++][1] = "Név";
            if (Megjegyzes) ws.Cells[currCol++][1] = "Dolgozói megjegyzés";
            if (Terulet) ws.Cells[currCol++][1] = "Terület";
            if (Sor) ws.Cells[currCol++][1] = "Gyártósor";
            if (Allomas) ws.Cells[currCol++][1] = "Munkaállomás";
            if (Pontertek) ws.Cells[currCol++][1] = "Pontérték";
            if (Complete) ws.Cells[currCol++][1] = "Tréning befejezve";
            if (Start) ws.Cells[currCol++][1] = "Tréning kezdete";
            if (End) ws.Cells[currCol++][1] = "Tréning vége";
            if (Megjegyzes) ws.Cells[currCol++][1] = "Tréning megjegyzése";
            if (Trainer) ws.Cells[currCol++][1] = "Oktathat";
            if (Mentor) ws.Cells[currCol++][1] = "Mentorál";
            if (Points) ws.Cells[currCol++][1] = "Pontszám";
            if (Restriction) ws.Cells[currCol++][1] = "Orvosi korlátozás";
            if (Material) ws.Cells[currCol++][1] = "Anyagok";
            

            int currRow = 2;
            foreach (var t in Trainings)
            {
                var e = Employees.FirstOrDefault(x => x.Torzsszam == t.DolgozoTsz);
                var a = database.TpAllomas.FirstOrDefault(x => x.Id == t.AllomasId);

                currCol = 1;
                if (Tsz) ws.Cells[currCol++][currRow] = t.DolgozoTsz;
                if (Nev) ws.Cells[currCol++][currRow] = t.DolgozoNev;
                if (Megjegyzes) ws.Cells[currCol++][currRow] = e.Megjegyzes;
                if (Terulet) ws.Cells[currCol++][currRow] = t.TeruletNev;
                if (Sor) ws.Cells[currCol++][currRow] = t.SorNev;
                if (Allomas) ws.Cells[currCol++][currRow] = t.AllomasNev;
                if (Pontertek) ws.Cells[currCol++][currRow] = a.PontertekInt;
                if (Complete) ws.Cells[currCol++][currRow] = t.IsComplete ? "Igen" : "Nem";
                if (Start) ws.Cells[currCol++][currRow] = t.TreningStart.ToShortDateString();
                if (End) ws.Cells[currCol++][currRow] = t.TreningEnd == null ? "" : ((DateTime)t.TreningEnd).ToShortDateString();
                if (Megjegyzes) ws.Cells[currCol++][currRow] = t.Megjegyzes;
                if (Trainer) ws.Cells[currCol++][currRow] = t.Oktathat ? "Igen" : "Nem";
                if (Mentor) ws.Cells[currCol++][currRow] = t.Mentoral ? "Igen" : "Nem";
                if (Points) ws.Cells[currCol++][currRow] = e?.Pontszam.ToString("N2");
                if (Restriction) ws.Cells[currCol++][currRow] = e?.OrvKorlat;
                if (Material) ws.Cells[currCol++][currRow] = a?.Anyag;
                
                currRow++;
                //if (currRow > 100) break;
            }

            xl.Visible = true;

            NotCurrentlyCreatingReport = true;
            ReportButtonText = "Riport készítése";
            MessageBox.Show("A riport elkészült", "Riport", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //-------------------------------------------------------

        private string filePath;
        public string FilePath
        {
            get { return filePath; }
            set 
            {
                filePath = value;
                OnPropertyChanged();
            }
        }

        private ICommand browseForFileCommand;
        public ICommand BrowseForFileCommand
        {
            get => browseForFileCommand ?? (browseForFileCommand = new CommandBase(() =>
            {
                var dialog = new OpenFileDialog()
                {
                    Multiselect = false
                };
                var result = dialog.ShowDialog();
                if (result == true)
                {
                    FilePath = dialog.FileName;
                }
            }));
        }

        private ICommand uploadRestrictionsCommand;
        public ICommand UploadRestrictionsCommand
        {
            get => uploadRestrictionsCommand ?? (uploadRestrictionsCommand = new CommandBase(() =>
            {
                Task.Run(() => UploadRestrictionsFromExcel(FilePath, TorzsszamColumnNumber, RestrictionColumnNumber, HasHeader));
            }));
        }

        private int torzsszamColumnNumber = 1;
        public int TorzsszamColumnNumber
        {
            get { return torzsszamColumnNumber; }
            set 
            { 
                torzsszamColumnNumber = value;
                OnPropertyChanged();
            }
        }

        private int restrictionColumnNumber = 6;
        public int RestrictionColumnNumber
        {
            get { return restrictionColumnNumber; }
            set 
            { 
                restrictionColumnNumber = value;
                OnPropertyChanged();
            }
        }

        private bool hasHeader = true;
        public bool HasHeader
        {
            get { return hasHeader; }
            set 
            { 
                hasHeader = value;
                OnPropertyChanged();
            }
        }

        private void UploadRestrictionsFromExcel(string filePath, int torzsszamColumnNumber, int restrictionColumnNumber, bool hasHeader)
        {
            NotCurrentlyCreatingReport = false;
            UploadButtonText = "A korlátozások feltöltése folyamatban...";
            Excel.Application excel = null;
            Excel.Workbook wb = null;
            Excel.Worksheet ws = null;

            try
            {
                excel = new Excel.Application();
                wb = excel.Workbooks.Open(filePath);
                ws = wb.Worksheets[1];
            }
            catch (Exception e)
            {
                wb?.Close();
                excel?.Quit();
                NotCurrentlyCreatingReport = true;
                UploadButtonText = "Korlátozások feltöltése";
                MessageBox.Show("Hiba történt az excel betöltésekor.\n" + e.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int currentRow = hasHeader ? 2 : 1;
            while (ws.Cells[currentRow, torzsszamColumnNumber].Value != null)
            {
                string tsz = ws.Cells[currentRow, torzsszamColumnNumber].Value.ToString();
                string korlat = ws.Cells[currentRow, restrictionColumnNumber].Value.ToString().Replace(Environment.NewLine, " ");
                if (korlat.Length > 100) korlat = korlat.Substring(0, 100).TrimEnd('\r', '\n');
                var employee = database.TpDolgozo.FirstOrDefault(x => x.Torzsszam == tsz);
                if (employee != null) employee.OrvKorlat = korlat;
                currentRow++;
            }

            wb?.Close();
            excel?.Quit();

            try
            {
                database.SaveChanges();
            }
            catch (Exception e)
            {
                NotCurrentlyCreatingReport = true;
                UploadButtonText = "Korlátozások feltöltése";
                MessageBox.Show("Hiba történt az adatok feltöltésekor.\n" + e.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NotCurrentlyCreatingReport = true;
            UploadButtonText = "Korlátozások feltöltése";
            MessageBox.Show("Az excel adatbázisba való feltöltése sikeres!", "Feltöltés", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private string uploadButtonText = "Korlátozások feltöltése";
        public string UploadButtonText
        {
            get { return uploadButtonText; }
            set
            {
                uploadButtonText = value;
                OnPropertyChanged();
            }
        }

    }
}
