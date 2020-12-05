using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace TestDB
{
    class Program
    {
        static void Main(string[] args)
        {
            Museo museo = new Museo();
            museo.Insert("Nome", "Rocca Malatestiana", "Luogo", "Cesena", "OrarioAperturaGenerale", new TimeSpan(7, 0, 0), "OrarioChiusuraGenerale", new TimeSpan(18, 0, 0), "NumBigliettiMaxGenerale", 10, "idFamiglia", 1);
            Console.ReadLine();
        }
    }
}
