using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Museo : DBEntity
    {
        public int idMuseo;
        public string Nome;
        public string Luogo;
        public TimeSpan OrarioAperturaGenerale;
        public TimeSpan OrarioChiusuraGenerale;
        public int NumBigliettiMaxGenerale;
        public int idFamiglia;

        public Museo(int idMuseo)
        {
            this.idMuseo = idMuseo;
            using(DBConnection dBConnection = new DBConnection())
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Museo WHERE idMuseo = @idMuseo");
                sqlCommand.Parameters.AddWithValue("@idMuseo", this.idMuseo);

                using (SqlDataReader sqlDataReader = dBConnection.SelectQuery(sqlCommand))
                {
                    while (sqlDataReader.Read())
                    {
                        this.Nome = isDBNull(sqlDataReader["Nome"]) ? "" : (string)sqlDataReader["Nome"];
                        this.Luogo = isDBNull(sqlDataReader["Luogo"]) ? "" : (string)sqlDataReader["Luogo"];
                        this.OrarioAperturaGenerale = isDBNull(sqlDataReader["OrarioAperturaGenerale"]) ? TimeSpan.Zero : (TimeSpan)sqlDataReader["OrarioAperturaGenerale"];
                        this.OrarioChiusuraGenerale = isDBNull(sqlDataReader["OrarioChiusuraGenerale"]) ? TimeSpan.Zero : (TimeSpan)sqlDataReader["OrarioChiusuraGenerale"];
                        this.NumBigliettiMaxGenerale = isDBNull(sqlDataReader["NumBigliettiMaxGenerale"]) ? int.MinValue : (int)sqlDataReader["NumBigliettiMaxGenerale"];
                        this.idFamiglia = isDBNull(sqlDataReader["idFamiglia"]) ? int.MinValue : (int)sqlDataReader["idFamiglia"];
                    }
                }
            }
        }
    }
}
