using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DBConnection : IDisposable
    {
        private static readonly string ConnectionString = "Data Source=LAPTOP-A2UM0TN5;Initial Catalog=MuseumsManagerDB;Integrated Security=True";

        public SqlConnection Connection { get; } = new SqlConnection(ConnectionString);

        public DBConnection()
        {
            this.Connection.Open();
        }

        public void InsertQuery(SqlCommand sqlCommand)
        {
            sqlCommand.Connection = this.Connection;
            sqlCommand.ExecuteNonQuery();
        }

        public SqlDataReader SelectQuery(SqlCommand sqlCommand)
        {
            sqlCommand.Connection = this.Connection;
            return sqlCommand.ExecuteReader();
        }

        public void Close()
        {
            this.Connection.Close();
        }

        public void Dispose()
        {
            Close();
        }

        ~DBConnection()
        {
            Close();
        }
    }
}
