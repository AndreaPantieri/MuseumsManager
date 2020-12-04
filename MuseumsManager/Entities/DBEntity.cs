using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DBEntity
    {
        protected bool isDBNull(object DBValue)
        {
            return DBNull.Value.Equals(DBValue);
        }
    }
}
