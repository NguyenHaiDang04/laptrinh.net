using System;
using System.Data;
using ProjectTask.DTO;
using static ProjectTask.DAL.Db;

namespace ProjectTask.DAL
{
    internal sealed class Repository
    {
        public DataTable DuAn() => Read(@"SELECT MaDuAn,TenDuAn,NgayBatDau,NgayKetThuc,TrangThai
            FROM dbo.DuAn ORDER BY MaDuAn");

        public DataTable ThanhVien() => Read("SELECT MaThanhVien,HoTen FROM dbo.ThanhVien ORDER BY MaThanhVien");

        public int LuuDuAn(DuAn a)
        {
            if (a.MaDuAn == 0)
                return Run(@"INSERT dbo.DuAn(TenDuAn,NgayBatDau,NgayKetThuc,TrangThai)
                    VALUES(@ten,@batdau,@ketthuc,@tt)", P("@ten", a.TenDuAn),
                    P("@batdau", a.NgayBatDau), P("@ketthuc", a.NgayKetThuc), P("@tt", a.TrangThai));
            return Run(@"UPDATE dbo.DuAn SET TenDuAn=@ten,NgayBatDau=@batdau,NgayKetThuc=@ketthuc,TrangThai=@tt
                WHERE MaDuAn=@id", P("@ten", a.TenDuAn), P("@batdau", a.NgayBatDau),
                P("@ketthuc", a.NgayKetThuc), P("@tt", a.TrangThai), P("@id", a.MaDuAn));
        }

        public int XoaDuAn(int id) => Run("DELETE dbo.DuAn WHERE MaDuAn=@id", P("@id", id));

        public DataTable CongViec(int duAn, string trangThai) => Read(@"
            SELECT MaTask,TenTask,MoTa,HanNop,DoUuTien,TrangThai FROM dbo.CongViec
            WHERE MaDuAn=@duan AND (@tt=N'' OR TrangThai=@tt) ORDER BY MaTask",
            P("@duan", duAn), P("@tt", trangThai));

        public DataTable PhanCong(int task) => Read(
            "SELECT MaThanhVien FROM dbo.PhanCong WHERE MaTask=@task", P("@task", task));

        public int LuuCongViec(CongViec v)
        {
            return Transaction((connection, transaction) =>
            {
                int id = v.MaTask;
                if (id == 0)
                {
                    using (var command = Command(connection, transaction, @"
                        INSERT dbo.CongViec(MaDuAn,TenTask,MoTa,HanNop,DoUuTien,TrangThai)
                        OUTPUT INSERTED.MaTask VALUES(@duan,@ten,@mota,@han,@uutien,@tt)",
                        P("@duan", v.MaDuAn), P("@ten", v.TenTask), P("@mota", v.MoTa),
                        P("@han", v.HanNop), P("@uutien", v.DoUuTien), P("@tt", v.TrangThai)))
                        id = (int)command.ExecuteScalar();
                }
                else
                {
                    using (var command = Command(connection, transaction, @"
                        UPDATE dbo.CongViec SET TenTask=@ten,MoTa=@mota,HanNop=@han,DoUuTien=@uutien,TrangThai=@tt
                        WHERE MaTask=@id AND MaDuAn=@duan", P("@id", id), P("@duan", v.MaDuAn),
                        P("@ten", v.TenTask), P("@mota", v.MoTa), P("@han", v.HanNop),
                        P("@uutien", v.DoUuTien), P("@tt", v.TrangThai)))
                    {
                        if (command.ExecuteNonQuery() != 1)
                            throw new InvalidOperationException("Công việc đã bị xóa hoặc đổi dự án.");
                    }
                }

                var current = new System.Collections.Generic.List<int>();
                using (var command = Command(connection, transaction,
                    "SELECT MaThanhVien FROM dbo.PhanCong WHERE MaTask=@id", P("@id", id)))
                using (var reader = command.ExecuteReader())
                    while (reader.Read())
                        current.Add(reader.GetInt32(0));

                foreach (int member in current)
                {
                    if (Array.IndexOf(v.ThanhVien, member) >= 0) continue;
                    using (var command = Command(connection, transaction,
                        "DELETE dbo.PhanCong WHERE MaTask=@id AND MaThanhVien=@tv", P("@id", id), P("@tv", member)))
                        command.ExecuteNonQuery();
                }
                foreach (int member in v.ThanhVien)
                {
                    if (current.Contains(member)) continue;
                    using (var command = Command(connection, transaction,
                        "INSERT dbo.PhanCong(MaTask,MaThanhVien) VALUES(@id,@tv)", P("@id", id), P("@tv", member)))
                        command.ExecuteNonQuery();
                }
                return id;
            });
        }

        public int XoaCongViec(int id) => Run("DELETE dbo.CongViec WHERE MaTask=@id", P("@id", id));

        public int ChuyenTrangThai(int id, string trangThai) => Run(
            "UPDATE dbo.CongViec SET TrangThai=@tt WHERE MaTask=@id", P("@id", id), P("@tt", trangThai));

        public DataTable TreHan(int duAn) => ReadWithReader(@"
            SELECT MaTask,TenTask,HanNop,DoUuTien,TrangThai FROM dbo.CongViec
            WHERE MaDuAn=@duan AND HanNop < GETDATE() AND TrangThai<>N'Done' ORDER BY HanNop",
            P("@duan", duAn));

        public DataTable BaoCao(int duAn) => Read(@"
            SELECT tv.HoTen, COUNT(*) AS TongTask,
                SUM(CASE WHEN v.TrangThai=N'Done' THEN 1 ELSE 0 END) AS DaXong,
                CAST(100.0 * SUM(CASE WHEN v.TrangThai=N'Done' THEN 1 ELSE 0 END)/COUNT(*) AS decimal(5,2)) AS TyLe
            FROM dbo.PhanCong pc JOIN dbo.ThanhVien tv ON pc.MaThanhVien=tv.MaThanhVien
            JOIN dbo.CongViec v ON v.MaTask=pc.MaTask
            WHERE v.MaDuAn=@duan GROUP BY tv.MaThanhVien,tv.HoTen ORDER BY tv.MaThanhVien", P("@duan", duAn));
    }
}
