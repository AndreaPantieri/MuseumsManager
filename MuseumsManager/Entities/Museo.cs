using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Museo : DBEntity
    {
        public int idMuseo { get; set; }
        public string Nome { get; set; }
        public string Luogo { get; set; }
        public TimeSpan OrarioAperturaGenerale { get; set; }
        public TimeSpan OrarioChiusuraGenerale { get; set; }
        public int NumBigliettiMaxGenerale { get; set; }
        public int idFamiglia { get; set; }

        public Museo(int idMuseo) : base(idMuseo) { }
    }
}
