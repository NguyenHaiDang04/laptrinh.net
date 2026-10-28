using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BaiTap4.Shared
{
    public sealed class RegistrationService
    {
        private readonly DatabaseOptions options;
        public RegistrationService(DatabaseOptions options) { this.options = options; }

        public Task<List<Course>> GetCoursesAsync() => Task.Run(() =>
        {
            var result = new List<Course>();
            using (var connection = new SqlConnection(options.ConnectionString()))
            using (var command = new SqlCommand("SELECT CourseId,CourseName FROM dbo.Courses WHERE IsActive=1 ORDER BY CourseId", connection))
            {
                connection.Open(); using (var reader = command.ExecuteReader())
                    while (reader.Read()) result.Add(new Course { CourseId = reader.GetInt32(0), CourseName = reader.GetString(1) });
            }
            return result;
        });

        public Task<int> RegisterAsync(RegistrationInput input) => Task.Run(() =>
        {
            Validate(input);
            using (var connection = new SqlConnection(options.ConnectionString()))
            using (var command = new SqlCommand(@"
                IF NOT EXISTS (SELECT 1 FROM dbo.Courses WHERE CourseId=@course AND IsActive=1)
                    THROW 50002, N'Khóa học không còn mở đăng ký.', 1;
                INSERT dbo.Registrations(FullName,Phone,BirthDate,Gender,CourseId,ReceiveNews)
                OUTPUT INSERTED.RegistrationId
                VALUES(@name,@phone,@birth,@gender,@course,@news);", connection))
            {
                command.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = input.FullName.Trim();
                command.Parameters.Add("@phone", SqlDbType.VarChar, 10).Value = input.Phone;
                command.Parameters.Add("@birth", SqlDbType.Date).Value = input.BirthDate.Date;
                command.Parameters.Add("@gender", SqlDbType.NVarChar, 10).Value = input.Gender;
                command.Parameters.Add("@course", SqlDbType.Int).Value = input.CourseId;
                command.Parameters.Add("@news", SqlDbType.Bit).Value = input.ReceiveNews;
                connection.Open();
                try { return Convert.ToInt32(command.ExecuteScalar()); }
                catch (SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
                { throw new InvalidOperationException("Số điện thoại này đã đăng ký khóa học đã chọn.", ex); }
                catch (SqlException ex) when (ex.Number == 50002)
                { throw new InvalidOperationException("Khóa học không còn mở đăng ký. Hãy tải lại danh sách.", ex); }
            }
        });

        public Task<List<RegistrationRow>> GetRegistrationsAsync() => Task.Run(() =>
        {
            var result = new List<RegistrationRow>();
            using (var connection = new SqlConnection(options.ConnectionString()))
            using (var command = new SqlCommand(@"SELECT r.RegistrationId,r.FullName,r.Phone,r.BirthDate,r.Gender,c.CourseName,r.ReceiveNews,r.CreatedAt
                FROM dbo.Registrations r JOIN dbo.Courses c ON c.CourseId=r.CourseId ORDER BY r.RegistrationId DESC", connection))
            {
                connection.Open(); using (var reader = command.ExecuteReader())
                    while (reader.Read()) result.Add(new RegistrationRow { RegistrationId = reader.GetInt32(0), FullName = reader.GetString(1), Phone = reader.GetString(2),
                        BirthDate = reader.GetDateTime(3), Gender = reader.GetString(4), CourseName = reader.GetString(5), ReceiveNews = reader.GetBoolean(6), CreatedAt = DateTime.SpecifyKind(reader.GetDateTime(7), DateTimeKind.Utc).ToLocalTime() });
            }
            return result;
        });

        public static void Validate(RegistrationInput input)
        {
            if (input == null || string.IsNullOrWhiteSpace(input.FullName) || input.FullName.Trim().Length > 100) throw new ArgumentException("Họ tên không được trống và tối đa 100 ký tự.");
            if (!Regex.IsMatch(input.Phone ?? "", "^[0-9]{10}$")) throw new ArgumentException("Số điện thoại cần đủ 10 chữ số.");
            if (input.BirthDate.Date > DateTime.Today || input.BirthDate.Year < 1900) throw new ArgumentException("Ngày sinh phải từ năm 1900 đến hôm nay.");
            if (input.Gender != "Nam" && input.Gender != "Nữ" && input.Gender != "Khác") throw new ArgumentException("Chọn giới tính.");
            if (input.CourseId <= 0) throw new ArgumentException("Chọn khóa học.");
        }
    }
}
