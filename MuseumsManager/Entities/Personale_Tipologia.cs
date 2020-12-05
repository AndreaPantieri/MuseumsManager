using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Personale_Tipologia : DBRelationN2NOnlyIndexes
    {
        public int idPersonale { get; set; }
        public int idTipoPersonale { get; set; }

        public Personale_Tipologia(int idPersonale, int idTipoPersonale) : base(idPersonale, idTipoPersonale) { }
    }
}
