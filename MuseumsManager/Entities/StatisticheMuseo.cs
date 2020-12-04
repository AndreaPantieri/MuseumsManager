using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class StatisticheMuseo
    {
        public int idStatistiche { get; set; }
        public int idMuseo { get; set; }

        public StatisticheMuseo(int idStatistiche, int idMuseo)
        {
            using (DBConnection dBConnection = new DBConnection())
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM StatisticheMuseo WHERE idStatistiche = @idStatistiche AND idMuseo = @idMuseo");
                sqlCommand.Parameters.AddWithValue("@idStatistiche", idStatistiche);
                sqlCommand.Parameters.AddWithValue("@idMuseo", idMuseo);

                using (SqlDataReader sqlDataReader = dBConnection.SelectQuery(sqlCommand))
                {
                    while (sqlDataReader.Read())
                    {
                        this.idStatistiche = DBNull.Value.Equals(sqlDataReader["idStatistiche"]) ? default(int) : (int)sqlDataReader["idStatistiche"];
                        this.idMuseo = DBNull.Value.Equals(sqlDataReader["idMuseo"]) ? default(int) : (int)sqlDataReader["idMuseo"];
                    }
                }
            }
        }
    }
}
