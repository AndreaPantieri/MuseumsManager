using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public abstract class DBObject
    {
        protected bool IsDBNull(object DBValue) => DBNull.Value.Equals(DBValue);
    }
}
