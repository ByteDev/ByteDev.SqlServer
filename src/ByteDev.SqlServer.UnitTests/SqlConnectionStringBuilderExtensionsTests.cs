using System.Data.SqlClient;
using NUnit.Framework;

namespace ByteDev.SqlServer.UnitTests
{
    [TestFixture]
    public class SqlConnectionStringBuilderExtensionsTests
    {
        [TestFixture]
        public class SetToMasterDatabase : SqlConnectionStringBuilderExtensionsTests
        {
            [Test]
            public void WhenInitialCatalogAlreadySet_ThenOverwriteInitialCatalog()
            {
                const string connString = "Data Source=.;Integrated Security=true;Initial Catalog=SqlServerTest";

                var sut = new SqlConnectionStringBuilder(connString);

                sut.SetToMasterDatabase();

                Assert.That(sut.InitialCatalog, Is.EqualTo("master"));
            }
        }
    }
}