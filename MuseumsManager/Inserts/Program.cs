using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Inserts
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Museo> musei = DBObject<Museo>.SelectAll();
            List<DateTime> dateTimes = new List<DateTime>();
            for(int i = 1; i <= 12; i++)
            {
                for(int j = 1; j <= 28; j++)
                {
                    DateTime dateTime = new DateTime(2020, i, j);
                    dateTimes.Add(dateTime);
                }
            }
            dateTimes.ForEach(dt =>
            {
                if(dt.DayOfWeek == DayOfWeek.Sunday)
                {
                    try
                    {
                        musei.ForEach(m => DBObject<CalendarioChiusure>.Insert("Data", dt.Date.ToString("yyyy-MM-dd"), "idMuseo", m.idMuseo));
                        
                    }
                    catch
                    {

                    }
                }
            });

            List<Personale> personales = DBObject<Personale>.SelectAll();
            Random random = new Random();
            musei.ForEach(m =>
            {
                List<Personale> p = personales.Where(pr => pr.idMuseo == m.idMuseo).ToList();
                List<CalendarioChiusure> cc = DBObject<CalendarioChiusure>.Select("idMuseo", m.idMuseo);
                dateTimes.ForEach(dt =>
                {
                    if(!cc.Any(c => c.Data.Equals(dt)))
                    {
                        p.ForEach(tmp =>
                        {
                            RegistroPresenze registroPresenze = new RegistroPresenze();
                            registroPresenze.idPersonale = tmp.idPersonale;
                            DateTime entrata = new DateTime(dt.Year, dt.Month, dt.Day, 0,0,0);
                            registroPresenze.DataEntrata = entrata.AddHours(random.Next(8, 12));
                            DateTime uscita = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
                            registroPresenze.DataUscita = entrata.AddHours(random.Next(15, 18));

                            try
                            {
                                DBObject<RegistroPresenze>.Insert("idPersonale", registroPresenze.idPersonale, "DataEntrata", registroPresenze.DataEntrata, "DataUscita", registroPresenze.DataUscita);
                            }
                            catch
                            {

                            }
                        });
                    }
                });
            });

        }
    }
}
