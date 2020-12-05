using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DBEntity : DBObject
    {
        public DBEntity() { }

        public DBEntity(int id)
        {
            Type t = this.GetType();
            using (DBConnection dBConnection = new DBConnection())
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM " + t.Name + " WHERE id" + t.Name + " = @id" + t.Name + "");
                sqlCommand.Parameters.AddWithValue("@id" + t.Name + "", id);
                List<PropertyInfo> lpi = new List<PropertyInfo>(t.GetProperties());

                using (SqlDataReader sqlDataReader = dBConnection.SelectQuery(sqlCommand))
                {
                    while (sqlDataReader.Read())
                    {
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
                    }
                }
            }
            
        }

        public int Insert(params object[] list)
        {
            if(list.Length == 0 || list.Length % 2 != 0)
            {
                throw new Exception("Wrong number of params");
            }

            string sqlCommandString = "INSERT INTO " + this.GetType().Name + "(";
            for(int i = 0; i < list.Length; i += 2)
            {
                sqlCommandString += list[i];
                if(i < list.Length - 2)
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
            using(DBConnection dBConnection = new DBConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(sqlCommandString);
                ret = dBConnection.InsertQuery(sqlCommand);
            }
            return ret;
        }
    }
}
