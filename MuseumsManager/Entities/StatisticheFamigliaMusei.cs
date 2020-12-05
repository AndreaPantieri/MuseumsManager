using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class StatisticheFamigliaMusei : DBRelationN2NOnlyIndexes
    {
        public int idStatistiche { get; set; }
        public int idFamiglia { get; set; }

        public StatisticheFamigliaMusei(int idStatistiche, int idFamiglia) : base(idStatistiche, idFamiglia) { }
    }
}
