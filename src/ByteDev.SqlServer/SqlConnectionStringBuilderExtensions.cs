using System;
using System.Data.SqlClient;

namespace ByteDev.SqlServer
{
    public static class SqlConnectionStringBuilderExtensions
    {
        private const string MasterDatabaseName = "master";

        public static void SetToMasterDatabase(this SqlConnectionStringBuilder source)
        {
            source.InitialCatalog = MasterDatabaseName;
        }

        public static bool IsDataSourceLocal(this SqlConnectionStringBuilder source)
        {
            return source.DataSource.Equals("(localdb)\\MSSQLLocalDB", StringComparison.InvariantCultureIgnoreCase) ||
                   source.DataSource.Equals(".") ||
                   source.DataSource.Equals("localhost", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}