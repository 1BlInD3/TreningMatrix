using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Treningelo.Models
{
    public class ModelBase
    {
        protected static FusetechEntities database = new FusetechEntities();
    }

    public partial class TpTrening : ModelBase
    {
        public string DolgozoNev => database.TpDolgozo.FirstOrDefault(x => x.Torzsszam == this.DolgozoTsz)?.Nev;

        public bool IsComplete => this.TreningEnd != null;

        public string AllomasNev => database.TpAllomas.FirstOrDefault(x => x.Id == this.AllomasId)?.Nev;

        public string SorNev => database.TpSor.FirstOrDefault(x => x.Id == this.SorId)?.Nev;

        public string TeruletNev => database.TpTerulet.FirstOrDefault(x => x.Id == this.TeruletId)?.Nev;
    }

    public partial class TpAllomas : ModelBase
    {
        public int Trained =>  database.TpTrening.Where(x => x.AllomasId == this.Id).Count();

        public int PontertekInt => (int)this.Pontertek;

        public override string ToString()
        {
            return this.Id.ToString() + " - " + this.Nev;
        }
    }

    public partial class TpSor : ModelBase
    {
        public int Trained => database.TpTrening.Where(x => x.SorId == this.Id).Count();

        public override string ToString()
        {
            return this.Id.ToString() + " - " + this.Nev;
        }
    }

    public partial class TpTerulet : ModelBase
    {
        public int Trained => database.TpTrening.Where(x => x.TeruletId == this.Id).Count();

        public override string ToString()
        {
            return this.Id.ToString() + " - " + this.Nev;
        }
    }

    public partial class TpDolgozo : ModelBase
    {
        public TpDolgozo()
        {
            this.ID = 0;
            this.Nev = string.Empty;
            this.OrvKorlat = string.Empty;
            this.Torzsszam = string.Empty;
        }
    }
}
