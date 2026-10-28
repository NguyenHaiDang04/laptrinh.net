using System;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace BaiTap4.Shared
{
    public sealed class DatabaseOptions
    {
        public string Server { get; }
        public string DatabaseName { get; }
        public string DataDirectory { get; }
        public string DataFile => Path.Combine(DataDirectory, DatabaseName + ".mdf");

        public DatabaseOptions(string server, string databaseName, string dataDirectory)
        {
            if (string.IsNullOrWhiteSpace(server)) throw new ArgumentException("Thiếu tên SQL Server.");
            if (!Regex.IsMatch(databaseName ?? "", @"^[A-Za-z][A-Za-z0-9_]{0,79}$")) throw new ArgumentException("Tên CSDL không hợp lệ.");
            Server = server; DatabaseName = databaseName; DataDirectory = Path.GetFullPath(dataDirectory);
        }

        public static DatabaseOptions Default()
        {
            return new DatabaseOptions(
                Environment.GetEnvironmentVariable("LTNET_SQL_SERVER") ?? @"(localdb)\MSSQLLocalDB",
                Environment.GetEnvironmentVariable("LTNET_DB_NAME") ?? "LTNET_Bai4_CSDL",
                Environment.GetEnvironmentVariable("LTNET_DB_DIRECTORY") ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "LTNET", "BaiTap4_CSDL"));
        }

        public string ConnectionString(bool master = false)
        {
            return new SqlConnectionStringBuilder { DataSource = Server, InitialCatalog = master ? "master" : DatabaseName,
                IntegratedSecurity = true, ConnectTimeout = 30, ApplicationName = "LTNET Bai 4 CSDL" }.ConnectionString;
        }
    }
}
