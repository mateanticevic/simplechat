using System.Configuration;
using System.Data.SqlClient;

namespace SimpleChat.DataLayer
{
    public class DbHelper
    {
        public static SqlConnection GetConnection()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["chatSql"].ConnectionString;

            return new SqlConnection(connectionString);
        }
    }
}