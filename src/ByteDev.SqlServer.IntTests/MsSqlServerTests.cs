using System;
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

                var result = Act(connString);

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenServerDoesNotExist_ThenReturnFalse()
            {
                const string connString = "Data Source=ServerNotExist;Integrated Security=true;";

                var result = Act(connString);

                Assert.That(result, Is.False);
            }

            [Test]
            public void WhenDbExists_ThenReturnTrue()
            {
                var connString = GetConnectionString(GetDbName());

                MsSqlServer.DeployDacpac(connString, TestFiles.Dacpac);

                var result = Act(connString);

                Assert.That(result, Is.True);

                MsSqlServer.DropDatabase(connString);
            }

            [Test]
            public void WhenDbDoesNotExist_ThenReturnFalse()
            {
                var connString = GetConnectionString("DbNotExist");

                var result = Act(connString);

                Assert.That(result, Is.False);
            }

            private static bool Act(string connString)
            {
                return MsSqlServer.Exists(connString);
            }
        }

        [TestFixture]
        public class ExecuteQueryStoreOff : MsSqlServerTests
        {
            [Test]
            public void WhenDbExists_ThenSetQueryStoreOff()
            {
                var connString = GetConnectionString(GetDbName());

                MsSqlServer.DeployDacpac(connString, TestFiles.Dacpac);

                Act(connString);

                MsSqlServer.DropDatabase(connString);
            }

            [Test]
            public void WhenDbDoesNotExist_ThenThrowException()
            {
                var connString = GetConnectionString("DbNotExist");

                Assert.Throws<SqlException>(() => Act(connString));
            }

            private static void Act(string connString)
            {
                MsSqlServer.ExecuteQueryStoreOff(connString);
            }
        }

        [TestFixture]
        public class DropDatabase : MsSqlServerTests
        {
            [Test]
            public void WhenDbExists_ThenDropDb()
            {
                var connString = GetConnectionString(GetDbName());

                MsSqlServer.DeployDacpac(connString, TestFiles.Dacpac);

                Act(connString);

                var exists = MsSqlServer.Exists(connString);

                Assert.That(exists, Is.False);
            }

            [Test]
            public void WhenDbDoesNotExist_ThenThrowException()
            {
                var connString = GetConnectionString("DbNotExist");

                Assert.Throws<SqlException>(() => Act(connString));
            }

            private static void Act(string connString)
            {
                MsSqlServer.DropDatabase(connString);
            }
        }

        [TestFixture]
        public class DeployDacpac : MsSqlServerTests
        {
            [Test]
            public void WhenDbDoesNotExist_ThenCreateDb()
            {
                var connString = GetConnectionString(GetDbName());

                MsSqlServer.DeployDacpac(connString, TestFiles.Dacpac);

                var exists = MsSqlServer.Exists(connString);

                Assert.That(exists, Is.True);

                MsSqlServer.DropDatabase(connString);
            }
        }

        private static string GetDbName()
        {
            return "SqlServerTest-" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
        }

        private static string GetConnectionString(string databaseName)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = ".",
                InitialCatalog = databaseName,
                IntegratedSecurity = true
            };

            return builder.ConnectionString;
        }
    }
}