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
            Museo_Tipologia museo_Tipologia = new Museo_Tipologia(1, 1);
            Console.WriteLine("idMuseo = " + museo_Tipologia.idMuseo);
            Console.WriteLine("idTipoMuseo = " + museo_Tipologia.idTipoMuseo);
            Console.ReadLine();
        }
    }
}
