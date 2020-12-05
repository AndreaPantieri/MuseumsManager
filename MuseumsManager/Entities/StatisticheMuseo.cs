using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class StatisticheMuseo : DBRelationN2NOnlyIndexes
    {
        public int idStatistiche { get; set; }
        public int idMuseo { get; set; }

        public StatisticheMuseo(int idStatistiche, int idMuseo) : base(idStatistiche, idMuseo) { }
    }
}
