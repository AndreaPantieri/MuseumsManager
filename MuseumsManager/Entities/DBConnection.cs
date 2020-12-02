using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DBConnection
    {
        private static readonly string ConnectionString = "Data Source=LAPTOP-A2UM0TN5;Initial Catalog=MuseumsManagerDB;Integrated Security=True";

        public SqlConnection Connection { get; } = new SqlConnection(ConnectionString);

        public DBConnection()
        {
            this.Connection.Open();
        }

        public void InsertQuery(SqlCommand sqlCommand)
        {
            sqlCommand.ExecuteNonQuery();
        }

        public SqlDataReader SelectQuery(SqlCommand sqlCommand)
        {
            return sqlCommand.ExecuteReader();
        }
    }
}
