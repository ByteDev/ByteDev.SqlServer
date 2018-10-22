using System.Diagnostics;
using System.IO;

namespace ByteDev.SqlServer
{
    public class SqlPackageExe
    {
        private string _exePath;

        public SqlPackageExe(string connectionString, string dacpacFilePath)
        {
            ConnectionString = connectionString;
            DacpacFilePath = dacpacFilePath;
        }

        public string ConnectionString { get; }

        public string DacpacFilePath { get; }

        public string PublishArgs
        {
            get { return $"/Action:Publish /SourceFile:{DacpacFilePath} /TargetConnectionString:\"{ConnectionString}\""; }
        }

        public string ExePath
        {
            get { return _exePath ?? (_exePath = GetPath()); }
        }

        public Process CreatePublishProcess()
        {
            var processStartInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = ExePath,
                Arguments = PublishArgs,
                Verb = "runas",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            return new Process
            {
                StartInfo = processStartInfo
            };
        }

        private static string GetPath()
        {
            var sqlPackagePaths = new[]
            {
                @"C:\Program Files (x86)\Microsoft SQL Server\140\DAC\bin\SqlPackage.exe",
                @"C:\Program Files\Microsoft SQL Server\140\DAC\bin\SqlPackage.exe",
                @"C:\Microsoft.Data.Tools.Msbuild.10.0.61804.210\lib\net46\SqlPackage.exe"
            };

            foreach (var path in sqlPackagePaths)
            {
                if (File.Exists(path))
                    return path;
            }

            throw new FileNotFoundException("SqlPackage.exe could not be found.");
        }
    }

}