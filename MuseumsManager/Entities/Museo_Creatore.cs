using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Museo_Creatore : DBRelationN2NOnlyIndexes
    {
        public int idMuseo { get; set; }
        public int idCreatore { get; set; }

        public Museo_Creatore(int idMuseo, int idCreatore) : base(idMuseo, idCreatore) { }
    }
}
