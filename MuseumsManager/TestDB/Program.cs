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
            Museo museo = new Museo(1);
            Console.WriteLine("Nome = " + museo.Nome);
            Console.WriteLine("Luogo = " + museo.Luogo);
            Console.WriteLine("OrarioAperturaGenerale = " + museo.OrarioAperturaGenerale);
            Console.WriteLine("OrarioChiusuraGenerale = " + museo.OrarioChiusuraGenerale);
            Console.WriteLine("NumBigliettiMaxGenerale = " + museo.NumBigliettiMaxGenerale);
            Console.WriteLine("idFamiglia = " + museo.idFamiglia);
            Console.ReadKey();
        }
    }
}
