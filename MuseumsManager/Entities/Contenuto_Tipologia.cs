using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Contenuto_Tipologia
    {
        public int idContenuto { get; set; }
        public int idTipoContenuto { get; set; }

        public Contenuto_Tipologia(int idContenuto, int idTipoContenuto)
        {
            using (DBConnection dBConnection = new DBConnection())
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Contenuto_Tipologia WHERE idContenuto = @idContenuto AND idTipoContenuto = @idTipoContenuto");
                sqlCommand.Parameters.AddWithValue("@idContenuto", idContenuto);
                sqlCommand.Parameters.AddWithValue("@idTipoContenuto", idTipoContenuto);

                using (SqlDataReader sqlDataReader = dBConnection.SelectQuery(sqlCommand))
                {
                    while (sqlDataReader.Read())
                    {
                        this.idContenuto = DBNull.Value.Equals(sqlDataReader["idContenuto"]) ? default(int) : (int)sqlDataReader["idContenuto"];
                        this.idTipoContenuto = DBNull.Value.Equals(sqlDataReader["idTipoContenuto"]) ? default(int) : (int)sqlDataReader["idTipoContenuto"];
                    }
                }
            }
        }
    }
}
