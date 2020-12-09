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
    public abstract class DBObject<T>
    {
        protected static bool IsDBNull(object DBValue) => DBNull.Value.Equals(DBValue);

        public static int Insert(params object[] list)
        {
            if (list.Length == 0 || list.Length % 2 != 0)
            {
                throw new Exception("Wrong number of params");
            }

            Type t = typeof(T);
            string sqlCommandString = "INSERT INTO " + t.Name + "(";
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

            if (t.IsSubclassOf(typeof(DBEntity)))
            {
                sqlCommandString += " SELECT CONVERT(int, SCOPE_IDENTITY());";
            }

            int ret;
            Debug.WriteLine(sqlCommandString);
            using (DBConnection dBConnection = new DBConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(sqlCommandString, dBConnection.Connection);
                ret = t.IsSubclassOf(typeof(DBEntity)) ? dBConnection.ScalarQuery(sqlCommand) : dBConnection.GenericQuery(sqlCommand);
            }
            return ret;
        }

        public static List<T> SelectAll()
        {
            Type t = typeof(T);
            List<T> dBObjects = new List<T>();
            using(DBConnection dBConnection = new DBConnection())
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM " + t.Name + ";");
                

                using (SqlDataReader sqlDataReader = dBConnection.SelectQuery(sqlCommand))
                {
                    while (sqlDataReader.Read())
                    {
                        T tmp = (T)Activator.CreateInstance(t);
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
                            pi.SetValue(tmp, value);
                        });

                        dBObjects.Add(tmp);
                    }
                }
            }

            return dBObjects;
        }
    }
}
