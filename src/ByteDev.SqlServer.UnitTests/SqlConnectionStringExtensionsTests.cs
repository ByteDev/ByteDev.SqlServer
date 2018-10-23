using NUnit.Framework;

namespace ByteDev.SqlServer.UnitTests
{
    [TestFixture]
    public class SqlConnectionStringExtensionsTests
    {
        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase("Server=tcp:My-Server.database.windows.net,1433;Initial Catalog=MyDb;", true)]
        [TestCase("server=tcp:My-Server.database.windows.net,1433;Initial Catalog=MyDb;", true)]
        [TestCase("Data Source=tcp:My-Server.database.windows.net,1433;Initial Catalog=MyDb;", true)]
        [TestCase("Initial Catalog=MyDb;Data Source=tcp:My-Server.database.windows.net,1433", true)]
        public void WhenConnectionStringProvided_ThenReturnExpected(string value, bool expected)
        {
            var result = value.IsAzureConnectionString();

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}