using System.Data;
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

        public static void DropDatabase(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString);

            const string sqlFormat = @"ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; " +
                                     @"DROP DATABASE [{0}] ;";

            var sql = string.Format(sqlFormat, builder.InitialCatalog);

            var masterBuilder = new SqlConnectionStringBuilder(connectionString);
            masterBuilder.SetToMasterDatabase();

            ExecuteNonQuery(masterBuilder.ConnectionString, sql);
        }

        public static void ExecuteQueryStoreOff(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString);

            var sql = $"ALTER DATABASE [{builder.InitialCatalog}] SET QUERY_STORE=OFF";

            ExecuteNonQuery(connectionString, sql);
        }

        private static int ExecuteNonQuery(string connectionString, string sql)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    return command.ExecuteNonQuery();
                }
            }
        }
    }
}