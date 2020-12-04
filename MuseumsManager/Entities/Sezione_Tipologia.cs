using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Sezione_Tipologia
    {
        public int idSezione { get; set; }
        public int idTipoSezione { get; set; }

        public Sezione_Tipologia(int idSezione, int idTipoSezione)
        {
            using (DBConnection dBConnection = new DBConnection())
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Sezione_Tipologia WHERE idSezione = @idSezione AND idTipoSezione = @idTipoSezione");
                sqlCommand.Parameters.AddWithValue("@idSezione", idSezione);
                sqlCommand.Parameters.AddWithValue("@idTipoSezione", idTipoSezione);

                using (SqlDataReader sqlDataReader = dBConnection.SelectQuery(sqlCommand))
                {
                    while (sqlDataReader.Read())
                    {
                        this.idSezione = DBNull.Value.Equals(sqlDataReader["idSezione"]) ? default(int) : (int)sqlDataReader["idSezione"];
                        this.idTipoSezione = DBNull.Value.Equals(sqlDataReader["idTipoSezione"]) ? default(int) : (int)sqlDataReader["idTipoSezione"];
                    }
                }
            }

        }
    }
}
