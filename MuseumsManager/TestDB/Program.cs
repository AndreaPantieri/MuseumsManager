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
            TipoMuseo tmp = new TipoMuseo();
            List<TipoMuseo> ltmp = tmp.SelectAll().Select(x => (TipoMuseo) x).ToList();
            Console.WriteLine(ltmp.Count);
            Console.ReadLine();
        }
    }
}
