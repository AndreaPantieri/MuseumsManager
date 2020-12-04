using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class StatisticheFamigliaMusei
    {
        public int idStatistiche { get; set; }
        public int idFamiglia { get; set; }

        public StatisticheFamigliaMusei(int idStatistiche, int idFamiglia)
        {
            using (DBConnection dBConnection = new DBConnection())
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM StatisticheFamigliaMusei WHERE idStatistiche = @idStatistiche AND idFamiglia = @idFamiglia");
                sqlCommand.Parameters.AddWithValue("@idStatistiche", idStatistiche);
                sqlCommand.Parameters.AddWithValue("@idFamiglia", idFamiglia);

                using (SqlDataReader sqlDataReader = dBConnection.SelectQuery(sqlCommand))
                {
                    while (sqlDataReader.Read())
                    {
                        this.idStatistiche = DBNull.Value.Equals(sqlDataReader["idStatistiche"]) ? default(int) : (int)sqlDataReader["idStatistiche"];
                        this.idFamiglia = DBNull.Value.Equals(sqlDataReader["idFamiglia"]) ? default(int) : (int)sqlDataReader["idFamiglia"];
                    }
                }
            }
        }
    }
}
