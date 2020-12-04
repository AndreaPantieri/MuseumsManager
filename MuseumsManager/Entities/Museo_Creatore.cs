using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Museo_Creatore
    {
        public int idMuseo { get; set; }
        public int idCreatore { get; set; }

        public Museo_Creatore(int idMuseo, int idCreatore)
        {
            using (DBConnection dBConnection = new DBConnection())
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Museo_Creatore WHERE idMuseo = @idMuseo AND idCreatore = @idCreatore");
                sqlCommand.Parameters.AddWithValue("@idMuseo", idMuseo);
                sqlCommand.Parameters.AddWithValue("@idCreatore", idCreatore);

                using (SqlDataReader sqlDataReader = dBConnection.SelectQuery(sqlCommand))
                {
                    while (sqlDataReader.Read())
                    {
                        this.idMuseo = DBNull.Value.Equals(sqlDataReader["idMuseo"]) ? default(int) : (int)sqlDataReader["idMuseo"];
                        this.idCreatore = DBNull.Value.Equals(sqlDataReader["idCreatore"]) ? default(int) : (int)sqlDataReader["idCreatore"];
                    }
                }
            }
        }
    }
}
