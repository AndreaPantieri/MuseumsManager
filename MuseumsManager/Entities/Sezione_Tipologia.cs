using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Sezione_Tipologia : DBRelationN2NOnlyIndexes
    {
        public int idSezione { get; set; }
        public int idTipoSezione { get; set; }

        public Sezione_Tipologia() : base() { }
        public Sezione_Tipologia(int idSezione, int idTipoSezione) : base(idSezione, idTipoSezione) { }
    }
}
