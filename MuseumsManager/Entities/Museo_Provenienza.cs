using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Museo_Provenienza : DBRelationN2NOnlyIndexes
    {
        public int idMuseo { get; set; }
        public int idProvenienza { get; set; }

        public Museo_Provenienza(int idMuseo, int idProvenienza) : base(idMuseo, idProvenienza) { }
    }
}
