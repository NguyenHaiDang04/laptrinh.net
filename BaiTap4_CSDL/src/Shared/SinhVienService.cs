using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BaiTap4.Shared
{
    public sealed class SinhVienService
    {
        private readonly DatabaseOptions options;
        public SinhVienService(DatabaseOptions options) { this.options = options; }

        public Task<List<SinhVien>> GetAllAsync() => Task.Run(() =>
        {
            var result = new List<SinhVien>();
            using (var connection = new SqlConnection(options.ConnectionString()))
            using (var command = new SqlCommand("SELECT MaSV, HoTen, NgaySinh, GioiTinh, Lop, DiemTB FROM dbo.SinhVien ORDER BY MaSV", connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                    while (reader.Read()) result.Add(new SinhVien {
                        MaSV = reader.GetString(0), HoTen = reader.GetString(1),
                        NgaySinh = reader.IsDBNull(2) ? (System.DateTime?)null : reader.GetDateTime(2),
                        GioiTinh = reader.IsDBNull(3) ? null : reader.GetString(3),
                        Lop = reader.IsDBNull(4) ? null : reader.GetString(4),
                        DiemTB = reader.IsDBNull(5) ? (decimal?)null : reader.GetDecimal(5)
                    });
            }
            return result;
        });
    }
}
