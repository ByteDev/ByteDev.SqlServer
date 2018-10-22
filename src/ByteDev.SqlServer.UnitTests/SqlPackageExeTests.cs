using NUnit.Framework;

namespace ByteDev.SqlServer.UnitTests
{
    public class SqlPackageExeTests
    {
        private const string ConnectionString = "Data Source=.;";
        private const string DacpacFilePath = @"C:\Promotions.dacpac";

        public class Constructor : SqlPackageExeTests
        {
            [Test]
            public void WhenCreated_ThenSetConnectionAndDacpacPath()
            {
                var sut = new SqlPackageExe(ConnectionString, DacpacFilePath);

                Assert.That(sut.ConnectionString, Is.EqualTo(ConnectionString));
                Assert.That(sut.DacpacFilePath, Is.EqualTo(DacpacFilePath));
            }
        }

        public class PublishArgs : SqlPackageExeTests
        {
            [Test]
            public void WhenCreated_ThenSetPublishArgs()
            {
                var sut = new SqlPackageExe(ConnectionString, DacpacFilePath);

                var result = sut.PublishArgs;

                Assert.That(result, Is.EqualTo($"/Action:Publish /SourceFile:{DacpacFilePath} /TargetConnectionString:\"{ConnectionString}\""));
            }
        }

        public class CreatePublishProcess : SqlPackageExeTests
        {
            [Test]
            public void WhenCalled_ThenSetsProcessStartInfo()
            {
                var sut = new SqlPackageExe(ConnectionString, DacpacFilePath);

                var result = sut.CreatePublishProcess();

                Assert.That(result.StartInfo.FileName.Length, Is.GreaterThan(0));
                Assert.That(result.StartInfo.Arguments, Is.EqualTo(sut.PublishArgs));
            }
        }
    }
}