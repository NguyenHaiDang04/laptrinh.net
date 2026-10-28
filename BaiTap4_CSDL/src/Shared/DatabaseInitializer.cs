using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace BaiTap4.Shared
{
    public sealed class DatabaseInitializer
    {
        private readonly DatabaseOptions options;
        public DatabaseInitializer(DatabaseOptions options) { this.options = options; }
        public Task EnsureCreatedAsync() => Task.Run(() => EnsureCreated());

        private void EnsureCreated()
        {
            Directory.CreateDirectory(options.DataDirectory);
            using (var master = new SqlConnection(options.ConnectionString(true)))
            {
                master.Open();
                using (var check = new SqlCommand("SELECT DB_ID(@name)", master))
                {
                    check.Parameters.Add("@name", SqlDbType.NVarChar, 128).Value = options.DatabaseName;
                    if (check.ExecuteScalar() == DBNull.Value)
                    {
                        // Tên CSDL đã được giới hạn ký tự; đường dẫn được escape như SQL string literal.
                        string file = options.DataFile.Replace("'", "''");
                        string log = Path.Combine(options.DataDirectory, options.DatabaseName + "_log.ldf").Replace("'", "''");
                        string sql = File.Exists(options.DataFile)
                            ? "CREATE DATABASE [" + options.DatabaseName + "] ON (FILENAME=N'" + file + "') FOR ATTACH"
                            : "CREATE DATABASE [" + options.DatabaseName + "] ON PRIMARY (NAME=N'" + options.DatabaseName + "', FILENAME=N'" + file + "') LOG ON (NAME=N'" + options.DatabaseName + "_log', FILENAME=N'" + log + "')";
                        try { using (var create = new SqlCommand(sql, master) { CommandTimeout = 60 }) create.ExecuteNonQuery(); }
                        catch (SqlException ex) when (ex.Number == 1801) { /* Tiến trình khác vừa tạo cùng CSDL. */ }
                    }
                }
            }
            using (var connection = new SqlConnection(options.ConnectionString()))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    using (var gate = new SqlCommand("DECLARE @r int; EXEC @r = sys.sp_getapplock @Resource=N'LTNET_Bai4_Schema', @LockMode='Exclusive', @LockOwner='Transaction', @LockTimeout=30000; IF @r < 0 THROW 50001, 'Cannot acquire schema lock', 1;", connection, transaction)) gate.ExecuteNonQuery();
                    string schema;
                    using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("BaiTap4.Shared.Database.Schema.sql"))
                    using (var reader = new StreamReader(stream)) schema = reader.ReadToEnd();
                    using (var command = new SqlCommand(schema, connection, transaction)) command.ExecuteNonQuery();
                    using (var command = new SqlCommand("IF NOT EXISTS (SELECT 1 FROM dbo.AppUsers WHERE Username=@username) INSERT dbo.AppUsers(Username,FullName,PasswordHash,PasswordSalt,PasswordIterations) VALUES(@username,@name,@hash,@salt,@iterations)", connection, transaction))
                    {
                        byte[] salt = PasswordHasher.NewSalt();
                        command.Parameters.Add("@username", SqlDbType.NVarChar, 50).Value = "admin";
                        command.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = "Quản trị viên mẫu";
                        command.Parameters.Add("@hash", SqlDbType.VarBinary, 32).Value = PasswordHasher.Hash("Admin@123", salt, PasswordHasher.Iterations);
                        command.Parameters.Add("@salt", SqlDbType.VarBinary, 16).Value = salt;
                        command.Parameters.Add("@iterations", SqlDbType.Int).Value = PasswordHasher.Iterations;
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
            }
        }
    }
}
