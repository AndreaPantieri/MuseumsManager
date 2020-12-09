using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Museo_PeriodoStorico : DBRelationN2NOnlyIndexes<Museo_PeriodoStorico>
    {
        public int idMuseo { get; set; }
        public int idPeriodoStorico { get; set; }

        public Museo_PeriodoStorico() : base() { }
        public Museo_PeriodoStorico(int idMuseo, int idPeriodoStorico) : base(idMuseo, idPeriodoStorico) { }
    }
}
