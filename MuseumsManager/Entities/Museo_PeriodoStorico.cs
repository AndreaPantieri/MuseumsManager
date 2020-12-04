using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Museo_PeriodoStorico
    {
        public int idMuseo { get; set; }
        public int idPeriodoStorico { get; set; }

        public Museo_PeriodoStorico(int idMuseo, int idPeriodoStorico)
        {
            using (DBConnection dBConnection = new DBConnection())
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Museo_PeriodoStorico WHERE idMuseo = @idMuseo AND idPeriodoStorico = @idPeriodoStorico");
                sqlCommand.Parameters.AddWithValue("@idMuseo", idMuseo);
                sqlCommand.Parameters.AddWithValue("@idPeriodoStorico", idPeriodoStorico);

                using (SqlDataReader sqlDataReader = dBConnection.SelectQuery(sqlCommand))
                {
                    while (sqlDataReader.Read())
                    {
                        this.idMuseo = DBNull.Value.Equals(sqlDataReader["idMuseo"]) ? default(int) : (int)sqlDataReader["idMuseo"];
                        this.idPeriodoStorico = DBNull.Value.Equals(sqlDataReader["idPeriodoStorico"]) ? default(int) : (int)sqlDataReader["idPeriodoStorico"];
                    }
                }
            }
        }
    }
}
