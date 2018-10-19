using NUnit.Framework;

namespace ByteDev.SqlServer.IntTests
{
    [TestFixture]
    public class MsSqlServerTests
    {
        [Test]
        public void WhenServerExists_ThenReturnTrue()
        {
            const string connString = "Data Source=.;Integrated Security=true;";

            var result = MsSqlServer.Exists(connString);

            Assert.That(result, Is.True);
        }
    }
}