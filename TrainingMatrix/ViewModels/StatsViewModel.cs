using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Treningelo.Models;

namespace Treningelo.ViewModels
{
    class StatsViewModel : ViewModelBase
    {
        private SeriesCollection seriesCollection;
        public SeriesCollection SeriesCollection
        {
            get
            {
                if (seriesCollection == null)
                    SetupSeries();
                return seriesCollection;
            }
        }
        public List<string> Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
        
        private void SetupSeries()
        {
            Formatter = value => value.ToString();
            seriesCollection = new SeriesCollection();
            Labels = new List<string>();

            var treningeltekcs = new ColumnSeries
            {
                Values = new ChartValues<int>(),
                Title = "Tréningeltek (fő)"
            };
            var allomasokcs = new ColumnSeries
            {
                Values = new ChartValues<int>(),
                Title = "Állomások (db)"
            };
            var treningekcs = new ColumnSeries
            {
                Values = new ChartValues<int>(),
                Title = "Tréningek (db)"
            };

            foreach (dynamic item in GetLinesData())
            {
                string teruletNev = item.TeruletNev as string;
                string sorNev = item.SorNev as string;
                int stationCount = (int)item.StationCount;
                int totalTrainings = (int)item.TotalTrainings;
                int totalTrained = (int)item.TotalTrained;

                string fullname = teruletNev;
                if (sorNev != null) fullname += " / " + sorNev;

                Labels.Add(fullname);
                treningekcs.Values.Add(totalTrainings);
                treningeltekcs.Values.Add(totalTrained);
                allomasokcs.Values.Add(stationCount);
            }

            SeriesCollection.Add(treningekcs);
            SeriesCollection.Add(treningeltekcs);
            SeriesCollection.Add(allomasokcs);
        }

        public static string GetFullName(TpAllomas allomas)
        {
            string name = string.Empty;
            name += database.TpTerulet.First(x => x.Id == allomas.TeruletId).Nev;
            name += " / ";
            if (allomas.SorId != null)
            {
                name += database.TpSor.First(x => x.Id == allomas.SorId).Nev;
                name += " / ";
            }
            name += allomas.Nev;
            return name;
        }

        public static IEnumerable<dynamic> GetLinesData()
        {
            var sql =

@"select subquery3.TeruletId as TeruletId,

       subquery3.SorId as SorId,
	   subquery3.TeruletNev as TeruletNev,
	   subquery3.SorNev as SorNev,
	   subquery4.StationCount as StationCount,
	   subquery5.TotalTrainings as TotalTrainings,
	   subquery6.TotalIndividualsTrained as TotalTrained

       from(

/*START names*/
select TeruletId,
	   case when SorId IS NULL then - 1 else SorId end as SorId, 
	   TeruletNev, 
	   SorNev from
(
select TOP 100 PERCENT

       TpTerulet.Id as TeruletId, 
	   TpAllomas.SorId as SorId, 
	   TpTerulet.Nev as TeruletNev, 
	   TpSor.Nev as SorNev

       from TpAllomas
full outer
       join TpTerulet on TpAllomas.TeruletId = TpTerulet.Id
full outer join TpSor on TpAllomas.SorId = TpSor.Id
) subquery1
group by TeruletId, SorId, TeruletNev, SorNev
/*END names*/ 

) subquery3 join(

/*START station count*/
select TeruletId,
	   case when SorId IS NULL then -1 else SorId end as SorId, 
	   COUNT(TpAllomas.Id) as StationCount
from TpAllomas
group by TeruletId, SorId
/*END station count*/

) subquery4 on subquery3.TeruletId = subquery4.TeruletId and subquery3.SorId = subquery4.SorId join(

/*START total trainings*/
select TOP 100 PERCENT
       TpTrening.TeruletId,
       TpTrening.SorId,
       COUNT(TpAllomas.Id) as TotalTrainings

       from TpAllomas
full outer
       join TpTerulet on TpAllomas.TeruletId = TpTerulet.Id
full outer join TpSor on TpAllomas.SorId = TpSor.Id
join TpTrening on TpAllomas.Id = TpTrening.AllomasId
group by TpTrening.TeruletId, TpTrening.SorId
order by TpTrening.TeruletId, TpTrening.SorId
/*END total trainings*/

) subquery5 on subquery3.TeruletId = subquery5.TeruletId and subquery3.SorId = subquery5.SorId join(

/*START total trained*/
select TeruletId, SorId, COUNT(DolgozoTsz) as TotalIndividualsTrained from
(
select TOP 100 PERCENT

       TpTrening.TeruletId,
       TpTrening.SorId,
       TpTrening.DolgozoTsz

       from TpAllomas
full outer
       join TpTerulet on TpAllomas.TeruletId = TpTerulet.Id
full outer join TpSor on TpAllomas.SorId = TpSor.Id
join TpTrening on TpAllomas.Id = TpTrening.AllomasId
group by TpTrening.TeruletId, TpTrening.SorId, TpTrening.DolgozoTsz
) subquery2
group by TeruletId, SorId
/*END total trained*/

) subquery6 on subquery3.TeruletId = subquery6.TeruletId and subquery3.SorId = subquery6.SorId 
order by TeruletNev, SorNev";


            using (var cmd = database.Database.Connection.CreateCommand())
            {
                if (database.Database.Connection.State == System.Data.ConnectionState.Closed)
                    database.Database.Connection.Open();
                cmd.CommandText = sql;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dynamic d = new ExpandoObject();
                        d.TeruletNev = reader[2];
                        d.SorNev = reader[3];
                        d.StationCount = reader[4];
                        d.TotalTrainings = reader[5];
                        d.TotalTrained = reader[6];
                        yield return d;
                    }
                }
            }
        }
    }
}
