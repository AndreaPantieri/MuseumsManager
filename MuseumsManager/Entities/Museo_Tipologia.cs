using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Museo_Tipologia : DBRelationN2NOnlyIndexes
    {
        public int idMuseo { get; set; }
        public int idTipoMuseo { get; set; }

        public Museo_Tipologia(int idMuseo, int idTipoMuseo) : base(idMuseo, idTipoMuseo) { }
    }
}
