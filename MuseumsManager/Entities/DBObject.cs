using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public abstract class DBObject
    {
        protected bool IsDBNull(object DBValue) => DBNull.Value.Equals(DBValue);

        public int Insert(params object[] list)
        {
            if (list.Length == 0 || list.Length % 2 != 0)
            {
                throw new Exception("Wrong number of params");
            }

            string sqlCommandString = "INSERT INTO '" + this.GetType().Name + "'(";
            for (int i = 0; i < list.Length; i += 2)
            {
                sqlCommandString += list[i];
                if (i < list.Length - 2)
                {
                    sqlCommandString += ", ";
                }
            }
            sqlCommandString += ") VALUES ('";
            for (int i = 1; i < list.Length; i += 2)
            {
                sqlCommandString += list[i];
                if (i < list.Length - 1)
                {
                    sqlCommandString += "', '";
                }
            }
            sqlCommandString += "');";
            int ret;
            using (DBConnection dBConnection = new DBConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(sqlCommandString);
                ret = dBConnection.InsertQuery(sqlCommand);
            }
            return ret;
        }
    }
}
