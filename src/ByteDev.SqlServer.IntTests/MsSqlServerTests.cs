using System.Data.SqlClient;
using NUnit.Framework;

namespace ByteDev.SqlServer.IntTests
{
    [TestFixture]
    public class MsSqlServerTests
    {
        [TestFixture]
        public class Exists : MsSqlServerTests
        {
            [Test]
            public void WhenServerExists_ThenReturnTrue()
            {
                const string connString = "Data Source=.;Integrated Security=true;";

                var result = MsSqlServer.Exists(connString);

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenServerDoesNotExist_ThenReturnFalse()
            {
                const string connString = "Data Source=ServerNotExist;Integrated Security=true;";

                var result = MsSqlServer.Exists(connString);

                Assert.That(result, Is.False);
            }

            [Test]
            public void WhenDbExists_ThenReturnTrue()
            {
                const string connString = "Data Source=.;Integrated Security=true;Initial Catalog=SqlServerTest";

                var result = MsSqlServer.Exists(connString);

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenDbDoesNotExist_ThenReturnFalse()
            {
                const string connString = "Data Source=.;Integrated Security=true;Initial Catalog=DbNotExist";

                var result = MsSqlServer.Exists(connString);

                Assert.That(result, Is.False);
            }
        }

        [TestFixture]
        public class ExecuteQueryStoreOff : MsSqlServerTests
        {
            [Test]
            public void WhenServerExists_ThenSetQueryStoreOff()
            {
                // TODO: create DB

                const string connString = "Data Source=.;Integrated Security=true;Initial Catalog=SqlServerTest";

                MsSqlServer.ExecuteQueryStoreOff(connString);
            }
        }

        [TestFixture]
        public class DropDatabase : MsSqlServerTests
        {
            [Test]
            public void WhenDbExists_ThenDropDb()
            {
                // TODO: create DB

                const string connString = "Data Source=.;Integrated Security=true;Initial Catalog=SqlServerTest";

                MsSqlServer.DropDatabase(connString);

                var exists = MsSqlServer.Exists(connString);

                Assert.That(exists, Is.False);
            }

            [Test]
            public void WhenDbDoesNotExist_ThenThrowException()
            {
                const string connString = "Data Source=.;Integrated Security=true;Initial Catalog=DbNotExist";

                Assert.Throws<SqlException>(() => MsSqlServer.DropDatabase(connString));
            }
        }
    }
}