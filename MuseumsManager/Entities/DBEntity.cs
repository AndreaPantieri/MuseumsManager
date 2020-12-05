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
    }
}
