using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Museo
    {
        public int idMuseo;
        public string Nome;
        public string Luogo;
        public TimeSpan OrarioAperturaGenerale;
        public TimeSpan OrarioChiusuraGenerale;
        public int NumBigliettiMaxGenerale;
        public int idFamiglia;
    }
}
