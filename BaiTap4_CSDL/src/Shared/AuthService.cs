using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BaiTap4.Shared
{
    public sealed class AuthService
    {
        private readonly DatabaseOptions options;
        public AuthService(DatabaseOptions options) { this.options = options; }
        public Task<LoginResult> LoginAsync(string username, string password) => Task.Run(() => Login(username, password));

        private LoginResult Login(string username, string password)
        {
            username = (username ?? "").Trim();
            if (username.Length == 0 || username.Length > 50 || string.IsNullOrWhiteSpace(password) || password.Length > 128)
                throw new ArgumentException("Vui lòng nhập tên đăng nhập và mật khẩu hợp lệ.");
            using (var connection = new SqlConnection(options.ConnectionString()))
            {
                connection.Open(); int? userId = null; string name = null; bool active = false; byte[] hash = null, salt = null; int iterations = 0;
                using (var command = new SqlCommand("SELECT UserId,FullName,PasswordHash,PasswordSalt,PasswordIterations,IsActive FROM dbo.AppUsers WHERE Username=@username", connection))
                {
                    command.Parameters.Add("@username", SqlDbType.NVarChar, 50).Value = username;
                    using (var reader = command.ExecuteReader())
                        if (reader.Read()) { userId = reader.GetInt32(0); name = reader.GetString(1); hash = (byte[])reader[2]; salt = (byte[])reader[3]; iterations = reader.GetInt32(4); active = reader.GetBoolean(5); }
                }
                bool success = hash != null && PasswordHasher.Verify(password, salt, hash, iterations) && active;
                using (var transaction = connection.BeginTransaction())
                {
                    using (var attempt = new SqlCommand("INSERT dbo.LoginAttempts(UserId,Username,Succeeded) VALUES(@id,@username,@success); IF @success=1 UPDATE dbo.AppUsers SET LastLoginAt=SYSUTCDATETIME() WHERE UserId=@id;", connection, transaction))
                    {
                        attempt.Parameters.Add("@id", SqlDbType.Int).Value = (object)userId ?? DBNull.Value;
                        attempt.Parameters.Add("@username", SqlDbType.NVarChar, 50).Value = username;
                        attempt.Parameters.Add("@success", SqlDbType.Bit).Value = success;
                        attempt.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                return new LoginResult { Success = success, FullName = success ? name : null };
            }
        }
    }
}
