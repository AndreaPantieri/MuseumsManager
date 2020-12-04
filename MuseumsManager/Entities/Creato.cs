using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Creato
    {
        public int idContenuto { get; set; }
        public int idCreatore { get; set; }

        public Creato(int idContenuto, int idCreatore)
        {
            using (DBConnection dBConnection = new DBConnection())
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Creato WHERE idContenuto = @idContenuto AND idCreatore = @idCreatore");
                sqlCommand.Parameters.AddWithValue("@idContenuto", idContenuto);
                sqlCommand.Parameters.AddWithValue("@idCreatore", idCreatore);

                using (SqlDataReader sqlDataReader = dBConnection.SelectQuery(sqlCommand))
                {
                    while (sqlDataReader.Read())
                    {
                        this.idContenuto = DBNull.Value.Equals(sqlDataReader["idContenuto"]) ? default(int) : (int)sqlDataReader["idContenuto"];
                        this.idCreatore = DBNull.Value.Equals(sqlDataReader["idCreatore"]) ? default(int) : (int)sqlDataReader["idCreatore"];
                    }
                }
            }
        }
    }
}
