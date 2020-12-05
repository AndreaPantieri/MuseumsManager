using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Creato : DBRelationN2NOnlyIndexes
    {
        public int idContenuto { get; set; }
        public int idCreatore { get; set; }

        public Creato(int idContenuto, int idCreatore) : base(idContenuto, idCreatore) { }
    }
}
