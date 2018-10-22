using System.IO;

namespace ByteDev.SqlServer.IntTests
{
    public static class TestFiles
    {
        public static string Dacpac
        {
            get
            {
                const string dacpacPath = @".\SqlServerTest\SqlServerTest.dacpac";

                if (!File.Exists(dacpacPath))
                {
                    throw new FileNotFoundException("Dacpac file not found.", dacpacPath);
                }

                return dacpacPath;
            }
        }
    }
}