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
    }
}