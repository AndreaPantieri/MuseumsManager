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
            List<Museo> lm = DBObject<Museo>.SelectAll();
            List<FamigliaMusei> lfm = DBObject<FamigliaMusei>.SelectAll();
            List<DateTime> lma = new List<DateTime>();

            for (int i = 0; i < 12; i++)
            {
                DateTime tmp = new DateTime(2020, i+1, 1);
                lm.Add(tmp);
            }

        }
    }
}
