using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Personale_Tipologia
    {
        public int idPersonale { get; set; }
        public int idTipoPersonale { get; set; }

        public Personale_Tipologia(int idPersonale, int idTipoPersonale)
        {
            using (DBConnection dBConnection = new DBConnection())
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Personale_Tipologia WHERE idPersonale = @idPersonale AND idTipoPersonale = @idTipoPersonale");
                sqlCommand.Parameters.AddWithValue("@idPersonale", idPersonale);
                sqlCommand.Parameters.AddWithValue("@idTipoPersonale", idTipoPersonale);

                using (SqlDataReader sqlDataReader = dBConnection.SelectQuery(sqlCommand))
                {
                    while (sqlDataReader.Read())
                    {
                        this.idPersonale = DBNull.Value.Equals(sqlDataReader["idPersonale"]) ? default(int) : (int)sqlDataReader["idPersonale"];
                        this.idTipoPersonale = DBNull.Value.Equals(sqlDataReader["idTipoPersonale"]) ? default(int) : (int)sqlDataReader["idTipoPersonale"];
                    }
                }
            }

        }
    }
}
