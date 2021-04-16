using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Treningelo.Commands;
using Treningelo.Models;

namespace Treningelo.ViewModels
{
    public class Employee : ViewModelBase, IEditableObject
    {
        // model being wrapped
        private readonly TpDolgozo model;

        #region ModelProperties
        public int ID
        {
            get => model.ID;
            set
            {
                model.ID = value;
                OnPropertyChanged();
            }
        }
        public string Torzsszam
        {
            get => model.Torzsszam;
            set
            {
                model.Torzsszam = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsTorzsszamValid));
            }
        }
        public string Nev
        {
            get => model.Nev;
            set
            {
                model.Nev = value;
                OnPropertyChanged();
            }
        }
        public string OrvKorlat
        {
            get => model.OrvKorlat;
            set
            {
                model.OrvKorlat = value;
                OnPropertyChanged();
            }
        }
        public string Megjegyzes
        {
            get => model.Megjegyzes;
            set
            {
                model.Megjegyzes = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasComment));
                OnPropertyChanged(nameof(StarWhenCommented));
            }
        }
        public bool Mentor
        {
            get => model.Mentor;
            set
            {
                model.Mentor = value;
                OnPropertyChanged();
            }
        }
        #endregion ModelProperties

        #region ReadonlyProperties
        public bool HasComment => !string.IsNullOrEmpty(Megjegyzes);
        public string StarWhenCommented => (HasComment ? Megjegyzes : string.Empty);
        #endregion ReadonlyProperties

        public Employee(TpDolgozo model = null)
        {
            if (model is null)
                this.model = new TpDolgozo
                {
                    ID = 0,
                    Torzsszam = string.Empty,
                    Nev = string.Empty,
                    OrvKorlat = string.Empty,
                };
            else
            {
                this.model = model;
                this.UpdatePontszamFromDatabase();
            }
        }

        #region Collections
        private void Add()
        {
            database.TpDolgozo.Add(model);
            Employees.Add(this);
        }
        public void Remove()
        {
            Employees.Remove(this);
            database.TpDolgozo.Remove(model);
            var trainings = (from t in Trainings where t.DolgozoTsz == this.Torzsszam select t).ToArray();
            foreach (var t in trainings) t.Remove();
        }
        #endregion Collections

        #region Points
        private decimal? pontszam;
        public decimal Pontszam
        {
            get
            {
                if (pontszam == null)
                {
                    UpdatePontszamFromDatabase();
                }
                return (decimal)pontszam;
            }
            private set
            {
                pontszam = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasAboveEightyPoints));
                OnPropertyChanged(nameof(HasAboveHundredAndSixtyPoints));
            }
        }
        public bool HasAboveEightyPoints
        {
            get => Pontszam >= 79.5M;
        }
        public bool HasAboveHundredAndSixtyPoints
        {
            get => Pontszam >= 159.5M;
        }
        public void UpdatePontszamFromDatabase()
        {
            this.Pontszam = database.Database.SqlQuery<decimal?>(@"select sum(case when TreningEnd is null then 0 
				                                                                    when Oktathat = 1 and TreningEnd < '2020-05-01' then Pontertek * 1.4
                                                                                    else Pontertek end) 
                                                                    from TpAllomas
                                                                    join TpTrening on TpAllomas.Id = TpTrening.AllomasId
                                                                    where DolgozoTsz = '" + this.Torzsszam + "'").FirstOrDefault() ?? 0;
        }
        #endregion Points

        #region Editing
        private TpDolgozo temp;
        public bool IsUnderEdit { get; private set; }
        public bool IsTorzsszamValid
        {
            get
            {
                if (model.Torzsszam == string.Empty) return false;

                if (model.ID == 0)
                {
                    return !database.TpDolgozo.Any(x => x.Torzsszam == model.Torzsszam);
                }
                else
                {
                    if (temp?.Torzsszam == model.Torzsszam) return true;
                    else return !database.TpDolgozo.Any(x => x.Torzsszam == model.Torzsszam);
                }
            }
        }
        public ICommand CancelEditCommand
        {
            get => new CommandBase(() => this.CancelEdit());
        }
        public ICommand EndEditCommand
        {
            get => new CommandBase(() => this.EndEdit());
        }

        public void BeginEdit()
        {
            if (temp == null)
            {
                temp = new TpDolgozo
                {
                    ID = model.ID,
                    Torzsszam = model.Torzsszam,
                    Nev = model.Nev,
                    OrvKorlat = model.OrvKorlat,
                    Megjegyzes = model.Megjegyzes
                };
                IsUnderEdit = true;
            }
        }
        public void EndEdit()
        {
            if (temp != null)
            {
                if (this.ID == 0) this.Add();

                if (this.Torzsszam != temp.Torzsszam)
                {
                    var trainings = from t in Trainings where t.DolgozoTsz == temp.Torzsszam select t;
                    foreach (var t in trainings) t.DolgozoTsz = this.Torzsszam;
                }

                if (string.IsNullOrWhiteSpace(OrvKorlat)) OrvKorlat = null;
                if (string.IsNullOrWhiteSpace(Megjegyzes)) Megjegyzes = null;

                SaveChangesToDatabase();

                temp = null;
                this.IsUnderEdit = false;
            }
        }
        public void CancelEdit()
        {
            if (temp != null)
            {
                this.Torzsszam = temp.Torzsszam;
                this.Nev = temp.Nev;
                this.OrvKorlat = temp.OrvKorlat;
                this.Megjegyzes = temp.Megjegyzes;
   
                temp = null;
                this.IsUnderEdit = false;
            }
        }
        #endregion Editing

        public override string ToString()
        {
            return this.Torzsszam + " - " + this.Nev;
        }
    }

    public class Training : ViewModelBase, IEditableObject
    {
        // model being wrapped
        private readonly TpTrening model;

        #region ModelProperties
        public int ID
        {
            get => model.ID;
            set
            {
                model.ID = value;
                OnPropertyChanged();
            }
        }
        public string DolgozoTsz
        {
            get => model.DolgozoTsz;
            set
            {
                model.DolgozoTsz = value;
                OnPropertyChanged();
            }
        }
        public int TeruletId
        {
            get => model.TeruletId;
            set
            {
                model.TeruletId = value;
                OnPropertyChanged();
            }
        }
        public int? SorId
        {
            get => model.SorId;
            set
            {
                model.SorId = value ?? -1;
                OnPropertyChanged();
            }
        }
        public int AllomasId
        {
            get => model.AllomasId;
            set
            {
                model.AllomasId = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TeruletNev));
                OnPropertyChanged(nameof(SorNev));
                OnPropertyChanged(nameof(AllomasNev));
            }
        }
        public DateTime TreningStart
        {
            get => model.TreningStart;
            set
            {
                model.TreningStart = value;
                OnPropertyChanged();
            }
        }
        public DateTime? TreningEnd
        {
            get => model.TreningEnd;
            set
            {
                if (value != null && (DateTime)value < TreningStart) model.TreningEnd = TreningStart;
                else model.TreningEnd = value;
                if (value == null) Oktathat = false;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsComplete));
            }
        }
        public bool Oktathat
        {
            get => model.Oktathat;
            set
            {
                model.Oktathat = value;
                OnPropertyChanged();
            }
        }
        public string Megjegyzes
        {
            get => model.Megjegyzes;
            set
            {
                model.Megjegyzes = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasComment));
                OnPropertyChanged(nameof(StarWhenCommented));
            }
        }
        public bool Mentoral
        {
            get => model.Mentoral;
            set
            {
                model.Mentoral = value;
                OnPropertyChanged();
            }
        }
        public string Mentor {
            get => model.Mentor;
            set {
                model.Mentor = value;
                OnPropertyChanged();
            }
        }
        #endregion ModelProperties

        #region ReadonlyProperties
        public string AllomasNev => database.TpAllomas.FirstOrDefault(x => x.Id == this.AllomasId)?.Nev;
        public string SorNev => database.TpSor.FirstOrDefault(x => x.Id == this.SorId)?.Nev;
        public string TeruletNev => database.TpTerulet.FirstOrDefault(x => x.Id == this.TeruletId)?.Nev;
        public string DolgozoNev => database.TpDolgozo.FirstOrDefault(x => x.Torzsszam == model.DolgozoTsz)?.Nev;
        public bool IsComplete => this.TreningEnd != null;
        public bool HasComment => !string.IsNullOrWhiteSpace(Megjegyzes);
        public string StarWhenCommented => (HasComment ? Megjegyzes : string.Empty);
        #endregion ReadonlyProperties

        #region Selection
        public TpTerulet SelectedTerulet
        {
            get => database.TpTerulet.FirstOrDefault(x => x.Id == model.TeruletId);
            set
            {
                model.TeruletId = value.Id;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Sorok));
                OnPropertyChanged(nameof(Allomasok));
            }
        }
        public IEnumerable<TpTerulet> Teruletek
        {
            get
            {
                var teruletek = from t in database.TpTerulet select t;
                foreach (var t in teruletek) yield return t;
            }
        }
        public TpSor SelectedSor
        {
            get => database.TpSor.FirstOrDefault(x => x.Id == model.SorId);
            set
            {
                if (value is null) model.SorId = -1;
                else model.SorId = value.Id;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Allomasok));
            }
        }
        public IEnumerable<TpSor> Sorok
        {
            get
            {
                var sorok = from s in database.TpSor where s.TeruletId == SelectedTerulet.Id select s;
                foreach (var s in sorok) yield return s;
            }
        }
        public TpAllomas SelectedAllomas
        {
            get => database.TpAllomas.FirstOrDefault(x => x.Id == model.AllomasId);
            set
            {
                this.AllomasId = value.Id;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsTrainingValid));
            }
        }
        public IEnumerable<TpAllomas> Allomasok
        {
            get
            {
                var allomasok = from a in database.TpAllomas where a.TeruletId == SelectedTerulet.Id select a;
                if (SelectedSor != null) allomasok = allomasok.Where(x => x.SorId == SelectedSor.Id);
                foreach (var a in allomasok) yield return a;
            }
        }
        #endregion Selection

        #region Points
        private void UpdateEmployeePontszam()
        {
            var e = Employees.FirstOrDefault(x => x.Torzsszam == this.DolgozoTsz);
            e?.UpdatePontszamFromDatabase();
        }
        #endregion Points

        public Training(TpTrening model = null)
        {
            if (model is null)
                this.model = new TpTrening
                {
                    ID = 0,
                    TeruletId = 1,
                    SorId = -1,
                    AllomasId = 1,
                    TreningStart = DateTime.Today,
                    TreningEnd = null,
                    Oktathat = false
                };
            else
            {
                this.model = model;
            }
        }

        #region Collections
        private void Add()
        {
            Trainings.Add(this);
            database.TpTrening.Add(model);
            UpdateEmployeePontszam();
        }
        public void Remove()
        {
            Trainings.Remove(this);
            database.TpTrening.Remove(model);
            UpdateEmployeePontszam();
        }
        #endregion Collections

        #region Editing
        private TpTrening temp;
        public bool IsUnderEdit { get; private set; }
        public bool IsTrainingValid
        {
            get
            {
                if (ID == 0)
                {
                    return !database.TpTrening.Any(x => x.DolgozoTsz == this.DolgozoTsz && x.AllomasId == this.AllomasId);
                }
                else
                {
                    if (temp?.AllomasId == this.AllomasId) return true;
                    else return !database.TpTrening.Any(x => x.DolgozoTsz == this.DolgozoTsz && x.AllomasId == this.AllomasId);
                }
            }
        }
        public ICommand CancelEditCommand
        {
            get => new CommandBase(() => this.CancelEdit());
        }
        public ICommand EndEditCommand
        {
            get => new CommandBase(() => this.EndEdit());
        }

        public void BeginEdit()
        {
            if (temp == null)
            {
                temp = new TpTrening
                {
                    ID = model.ID,
                    DolgozoTsz = model.DolgozoTsz,
                    Oktathat = model.Oktathat,
                    TreningStart = model.TreningStart,
                    TreningEnd = model.TreningEnd,
                    TeruletId = model.TeruletId,
                    AllomasId = model.AllomasId,
                    SorId = model.SorId,
                    Megjegyzes = model.Megjegyzes,
                    Mentor = model.Mentor
                   
                };
                IsUnderEdit = true;
            }
        }
        public void EndEdit()
        {
            if (temp != null)
            {
                if (this.ID == 0) this.Add();

                if (string.IsNullOrWhiteSpace(Megjegyzes)) Megjegyzes = null;

                SaveChangesToDatabase();
                UpdateEmployeePontszam();

                temp = null;
                this.IsUnderEdit = false;
            }
        }
        public void CancelEdit()
        {
            if (temp != null)
            {
                this.TeruletId = temp.TeruletId;
                this.SorId = temp.SorId;
                this.AllomasId = temp.AllomasId;
                this.TreningStart = temp.TreningStart;
                this.TreningEnd = temp.TreningEnd;
                this.Oktathat = temp.Oktathat;
                this.Megjegyzes = temp.Megjegyzes;
                this.Mentor = temp.Mentor;

                temp = null;
                this.IsUnderEdit = false;
            }
        }
        #endregion Editing
    }
}
