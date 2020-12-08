using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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

            string sqlCommandString = "INSERT INTO " + this.GetType().Name + "(";
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
            Debug.WriteLine(sqlCommandString);
            using (DBConnection dBConnection = new DBConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(sqlCommandString, dBConnection.Connection);
                ret = dBConnection.InsertQuery(sqlCommand);
            }
            return ret;
        }

        public List<DBObject> SelectAll()
        {
            Type t = this.GetType();
            List<DBObject> dBObjects = new List<DBObject>();
            using(DBConnection dBConnection = new DBConnection())
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM " + t.Name + ";", dBConnection.Connection);
                

                using (SqlDataReader sqlDataReader = dBConnection.SelectQuery(sqlCommand))
                {
                    while (sqlDataReader.Read())
                    {
                        DBObject tmp = (DBObject)Activator.CreateInstance(t);
                        List<PropertyInfo> lpi = new List<PropertyInfo>(tmp.GetType().GetProperties());
                        lpi.ForEach(pi =>
                        {
                            object value;
                            if (IsDBNull(sqlDataReader[pi.Name]))
                            {
                                value = Activator.CreateInstance(pi.PropertyType);
                            }
                            else
                            {
                                value = sqlDataReader[pi.Name];
                            }
                            pi.SetValue(this, value);
                        });

                        dBObjects.Add(tmp);
                    }
                }
            }

            return dBObjects;
        }
    }
}
