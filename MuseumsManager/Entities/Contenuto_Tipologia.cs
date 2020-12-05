using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Contenuto_Tipologia : DBRelationN2NOnlyIndexes
    {
        public int idContenuto { get; set; }
        public int idTipoContenuto { get; set; }

        public Contenuto_Tipologia(int idContenuto, int idTipoContenuto) : base(idContenuto, idTipoContenuto) { }
    }
}
