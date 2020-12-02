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

        private readonly SqlConnection connection = new SqlConnection(ConnectionString);
    }
}
