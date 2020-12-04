using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Museo_Tipologia
    {
        public int idMuseo { get; set; }
        public int idTipoMuseo { get; set; }

        public Museo_Tipologia(int idMuseo, int idTipoMuseo)
        {
            using (DBConnection dBConnection = new DBConnection())
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Museo_Tipologia WHERE idMuseo = @idMuseo AND idTipoMuseo = @idTipoMuseo");
                sqlCommand.Parameters.AddWithValue("@idMuseo", idMuseo);
                sqlCommand.Parameters.AddWithValue("@idTipoMuseo", idTipoMuseo);

                using (SqlDataReader sqlDataReader = dBConnection.SelectQuery(sqlCommand))
                {
                    while (sqlDataReader.Read())
                    {
                        this.idMuseo = DBNull.Value.Equals(sqlDataReader["idMuseo"]) ? default(int) : (int)sqlDataReader["idMuseo"];
                        this.idTipoMuseo = DBNull.Value.Equals(sqlDataReader["idTipoMuseo"]) ? default(int) : (int)sqlDataReader["idTipoMuseo"];
                    }
                }
            }

        }
    }
}
