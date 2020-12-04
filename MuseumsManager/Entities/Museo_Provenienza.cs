using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Museo_Provenienza
    {
        public int idMuseo { get; set; }
        public int idProvenienza { get; set; }

        public Museo_Provenienza(int idMuseo, int idProvenienza)
        {
            using (DBConnection dBConnection = new DBConnection())
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Museo_Provenienza WHERE idMuseo = @idMuseo AND idProvenienza = @idProvenienza");
                sqlCommand.Parameters.AddWithValue("@idMuseo", idMuseo);
                sqlCommand.Parameters.AddWithValue("@idProvenienza", idProvenienza);

                using (SqlDataReader sqlDataReader = dBConnection.SelectQuery(sqlCommand))
                {
                    while (sqlDataReader.Read())
                    {
                        this.idMuseo = DBNull.Value.Equals(sqlDataReader["idMuseo"]) ? default(int) : (int)sqlDataReader["idMuseo"];
                        this.idProvenienza = DBNull.Value.Equals(sqlDataReader["idProvenienza"]) ? default(int) : (int)sqlDataReader["idProvenienza"];
                    }
                }
            }
        }
    }
}
