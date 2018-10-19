using System.Data.SqlClient;

namespace ByteDev.SqlServer
{
    public class MsSqlServer
    {
        public static bool Exists(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString)
            {
                ConnectTimeout = 1
            };

            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                try
                {
                    connection.Open();
                    connection.Close();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}