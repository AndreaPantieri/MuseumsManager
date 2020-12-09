using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DBEntity : DBObject<DBEntity>
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

        public int Update(string idName, int idValue, params object[] list)
        {
            if (list.Length == 0 || list.Length % 2 != 0)
            {
                throw new Exception("Wrong number of params");
            }

            string sqlCommandString = "UPDATE " + this.GetType().Name + " SET ";
            for (int i = 0; i < list.Length; i += 2)
            {
                sqlCommandString += list[i];
                sqlCommandString += "= '";
                sqlCommandString += list[i + 1];
                if (i < list.Length - 2)
                {
                    sqlCommandString += "', ";
                }
            }
            sqlCommandString += "' WHERE " + idName + " = '" + idValue + "';";
            int ret;
            using (DBConnection dBConnection = new DBConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(sqlCommandString);
                ret = dBConnection.GenericQuery(sqlCommand);
            }
            return ret;
        }

        public int Delete(string idName, int idValue)
        {
            string sqlCommandString = "DELETE FROM " + this.GetType().Name + " WHERE " + idName + " = '" + idValue + "';";
            int ret;
            using (DBConnection dBConnection = new DBConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(sqlCommandString);
                ret = dBConnection.GenericQuery(sqlCommand);
            }
            return ret;
        }
    }
}
