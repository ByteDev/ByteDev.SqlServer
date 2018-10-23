using System.Text.RegularExpressions;

namespace ByteDev.SqlServer
{
    public static class SqlConnectionStringExtensions
    {
        public static bool IsAzureConnectionString(this string source)
        {
            if(string.IsNullOrEmpty(source))
                return false;

            var regEx = new Regex(@"(Server|Data Source)=tcp:[a-zA-Z0-9-]*.database.windows.net", RegexOptions.IgnoreCase);

            return regEx.IsMatch(source);
        }
    }
}