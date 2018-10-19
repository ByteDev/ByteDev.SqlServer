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

        [TestFixture]
        public class IsDataSourceLocal : SqlConnectionStringBuilderExtensionsTests
        {
            [TestCase("", false)]
            [TestCase("SomeServer", false)]
            [TestCase(".", true)]
            [TestCase("(localdb)\\MSSQLLocalDB", true)]
            [TestCase("(localdb)\\mssqllocaldb", true)]
            [TestCase("localhost", true)]
            [TestCase("LOCALHOST", true)]
            public void WhenDataSourceSet_ThenReturnExpected(string dataSource, bool expected)
            {
                var sut = new SqlConnectionStringBuilder
                {
                    DataSource = dataSource
                };

                var result = sut.IsDataSourceLocal();

                Assert.That(result, Is.EqualTo(expected));
            }
        }
    }
}