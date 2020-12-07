using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DBRelationN2NOnlyIndexes : DBObject
    {
        public DBRelationN2NOnlyIndexes(int id1, int id2)
        {
            Type t = this.GetType();
            List<PropertyInfo> lpi = new List<PropertyInfo>(t.GetProperties());
            if(lpi.Count == 2)
            {
                PropertyInfo pi1 = lpi[0], pi2 = lpi[1];

                using (DBConnection dBConnection = new DBConnection())
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT * FROM " + t.Name + " WHERE " + pi1.Name + " = @" + pi1.Name + " AND " + pi2.Name + " = @" + pi2.Name + "");
                    sqlCommand.Parameters.AddWithValue("@" + pi1.Name + "", id1);
                    sqlCommand.Parameters.AddWithValue("@" + pi2.Name + "", id2);


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

        public int Delete(string idName1, int idValue1, string idName2, int idValue2)
        {
            string sqlCommandString = "DELETE FROM '" + this.GetType().Name + "' WHERE '" + idName1 + "' = '" + idValue1 + "' AND '" + idName2 + "' = '" + idValue2 + "';";
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
