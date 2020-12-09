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
        private static readonly string HostName = "LAPTOP-A2UM0TN5";
        private static readonly string ConnectionString = "Data Source=" + HostName + ";Initial Catalog=MuseumsManagerDB;Integrated Security=True";

        public SqlConnection Connection { get; } = new SqlConnection(ConnectionString);

        public DBConnection()
        {
            this.Connection.Open();
        }

        public int ScalarQuery(SqlCommand sqlCommand)
        {
            sqlCommand.Connection = this.Connection;
            return (int)sqlCommand.ExecuteScalar();
        }

        public int GenericQuery(SqlCommand sqlCommand)
        {
            sqlCommand.Connection = this.Connection;
            return (int)sqlCommand.ExecuteNonQuery();
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
