using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Karaoke.DTO;
using static Karaoke.DAL.Db;

namespace Karaoke.DAL
{
    internal sealed class Repository
    {
        public DataTable Phong() => Read(@"SELECT MaPhong,TenPhong,LoaiPhong,GiaTheoGio,TrangThai
            FROM dbo.PhongHat ORDER BY MaPhong");

        public int LuuPhong(PhongHat p) => Run(p.MaPhong == 0
            ? "INSERT dbo.PhongHat(TenPhong,LoaiPhong,GiaTheoGio) VALUES(@ten,@loai,@gia)"
            : "UPDATE dbo.PhongHat SET TenPhong=@ten,LoaiPhong=@loai,GiaTheoGio=@gia WHERE MaPhong=@id AND TrangThai=N'Trống'",
            P("@id",p.MaPhong), P("@ten",p.TenPhong), P("@loai",p.LoaiPhong), Money("@gia",p.GiaTheoGio));

        public int XoaPhong(int id) => Run(
            "DELETE dbo.PhongHat WHERE MaPhong=@id AND TrangThai=N'Trống'", P("@id",id));

        public DataTable DichVu() => Read("SELECT MaDV,TenDV,DonGia,TonKho FROM dbo.DichVu ORDER BY MaDV");

        public int LuuDichVu(DichVu d) => Run(d.MaDV == 0
            ? "INSERT dbo.DichVu(TenDV,DonGia,TonKho) VALUES(@ten,@gia,@ton)"
            : "UPDATE dbo.DichVu SET TenDV=@ten,DonGia=@gia,TonKho=@ton WHERE MaDV=@id",
            P("@id",d.MaDV), P("@ten",d.TenDV), Money("@gia",d.DonGia), P("@ton",d.TonKho));

        public int XoaDichVu(int id) => Run("DELETE dbo.DichVu WHERE MaDV=@id", P("@id",id));

        public int MoPhong(int maPhong)
        {
            return Transaction((connection, transaction) =>
            {
                using (var command = Command(connection, transaction, @"
                    UPDATE dbo.PhongHat SET TrangThai=N'Đang hát'
                    WHERE MaPhong=@phong AND TrangThai=N'Trống'", P("@phong",maPhong)))
                {
                    if (command.ExecuteNonQuery() != 1)
                        throw new InvalidOperationException("Phòng không còn trống. Hãy tải lại sơ đồ.");
                }
                using (var command = Command(connection, transaction, @"
                    INSERT dbo.HoaDon(MaPhong,GioVao) OUTPUT INSERTED.MaHD VALUES(@phong,GETDATE())",
                    P("@phong",maPhong)))
                    return (int)command.ExecuteScalar();
            });
        }

        public DataTable PhienMo(int maPhong) => Read(@"
            SELECT p.MaPhong,p.TenPhong,p.GiaTheoGio,p.TrangThai,h.MaHD,h.GioVao,GETDATE() AS HienTai
            FROM dbo.PhongHat p LEFT JOIN dbo.HoaDon h ON p.MaPhong=h.MaPhong AND h.TrangThai=0
            WHERE p.MaPhong=@phong", P("@phong",maPhong));

        public DataTable ChiTiet(int maHD) => Read(@"
            SELECT c.MaDV,d.TenDV,c.SoLuong,c.DonGia,c.SoLuong*c.DonGia AS ThanhTien
            FROM dbo.ChiTietDichVu c JOIN dbo.DichVu d ON c.MaDV=d.MaDV WHERE MaHD=@hd ORDER BY c.MaDV",
            P("@hd",maHD));

        public void GoiDichVu(int maPhong, int maHD, int maDV, int soLuong)
        {
            Transaction((connection, transaction) =>
            {
                KhoaPhong(connection, transaction, maPhong);
                PhienDangMo(connection, transaction, maPhong, maHD);
                using (var command = Command(connection, transaction, @"
                    DECLARE @ton int, @gia decimal(18,2);
                    SELECT @ton=TonKho,@gia=DonGia FROM dbo.DichVu WITH (UPDLOCK,HOLDLOCK) WHERE MaDV=@dv;
                    IF @ton IS NULL SELECT -1;
                    ELSE
                    BEGIN
                        DECLARE @daGoi int = ISNULL((SELECT SoLuong FROM dbo.ChiTietDichVu WHERE MaHD=@hd AND MaDV=@dv),0);
                        IF @ton-@daGoi < @sl SELECT 0;
                        ELSE
                        BEGIN
                            IF @daGoi=0
                                INSERT dbo.ChiTietDichVu(MaHD,MaDV,SoLuong,DonGia) VALUES(@hd,@dv,@sl,@gia);
                            ELSE
                                UPDATE dbo.ChiTietDichVu SET SoLuong=SoLuong+@sl WHERE MaHD=@hd AND MaDV=@dv;
                            SELECT 1;
                        END
                    END", P("@hd",maHD), P("@dv",maDV), P("@sl",soLuong)))
                {
                    int result = (int)command.ExecuteScalar();
                    if (result < 0) throw new InvalidOperationException("Dịch vụ không còn tồn tại.");
                    if (result == 0) throw new InvalidOperationException("Số lượng gọi vượt tồn kho hiện tại.");
                    return result;
                }
            });
        }

        public void BoDichVu(int maPhong, int maHD, int maDV)
        {
            Transaction((connection, transaction) =>
            {
                KhoaPhong(connection, transaction, maPhong);
                PhienDangMo(connection, transaction, maPhong, maHD);
                using (var command = Command(connection, transaction, @"
                    DELETE dbo.ChiTietDichVu WHERE MaHD=@hd AND MaDV=@dv", P("@hd",maHD), P("@dv",maDV)))
                {
                    if (command.ExecuteNonQuery() != 1)
                        throw new InvalidOperationException("Dòng dịch vụ đã bị xóa.");
                }
                return true;
            });
        }

        public HoaDon ThanhToan(int maPhong, int maHD, Func<DateTime, DateTime, decimal, decimal> tinhTienPhong)
        {
            return Transaction((connection, transaction) =>
            {
                decimal gia = KhoaPhong(connection, transaction, maPhong);
                DateTime gioVao = PhienDangMo(connection, transaction, maPhong, maHD);
                DateTime gioRa;
                using (var command = Command(connection, transaction, "SELECT GETDATE()"))
                    gioRa = (DateTime)command.ExecuteScalar();
                var bill = new HoaDon
                {
                    MaHD = maHD, GioVao = gioVao, GioRa = gioRa,
                    TongTienPhong = tinhTienPhong(gioVao, gioRa, gia)
                };

                var items = new List<KeyValuePair<int,int>>();
                using (var command = Command(connection, transaction, @"
                    SELECT MaDV,SoLuong,DonGia FROM dbo.ChiTietDichVu WHERE MaHD=@hd ORDER BY MaDV", P("@hd",maHD)))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add(new KeyValuePair<int,int>(reader.GetInt32(0),reader.GetInt32(1)));
                        bill.TongTienDichVu += reader.GetInt32(1) * reader.GetDecimal(2);
                    }
                }

                using (var command = Command(connection, transaction, @"
                    UPDATE dbo.HoaDon SET GioRa=@ra,TongTienPhong=@tp,TongTienDichVu=@tdv,TrangThai=1
                    WHERE MaHD=@hd AND TrangThai=0", P("@hd",maHD), P("@ra",gioRa),
                    Money("@tp",bill.TongTienPhong), Money("@tdv",bill.TongTienDichVu)))
                    command.ExecuteNonQuery();

                foreach (var item in items)
                {
                    using (var command = Command(connection, transaction, @"
                        UPDATE dbo.DichVu SET TonKho=TonKho-@sl WHERE MaDV=@dv AND TonKho>=@sl",
                        P("@dv",item.Key), P("@sl",item.Value)))
                    {
                        if (command.ExecuteNonQuery() != 1)
                            throw new InvalidOperationException("Không đủ tồn kho dịch vụ mã " + item.Key
                                + ". Thanh toán đã được hoàn tác. Hãy kiểm tra hoặc bỏ dòng dịch vụ.");
                    }
                }
                using (var command = Command(connection, transaction,
                    "UPDATE dbo.PhongHat SET TrangThai=N'Trống' WHERE MaPhong=@phong", P("@phong",maPhong)))
                    command.ExecuteNonQuery();
                return bill;
            });
        }

        private static decimal KhoaPhong(SqlConnection connection, SqlTransaction transaction, int maPhong)
        {
            using (var command = Command(connection, transaction, @"
                SELECT GiaTheoGio FROM dbo.PhongHat WITH (UPDLOCK,HOLDLOCK)
                WHERE MaPhong=@phong AND TrangThai=N'Đang hát'", P("@phong",maPhong)))
            {
                object gia = command.ExecuteScalar();
                if (gia == null) throw new InvalidOperationException("Phòng không còn phiên đang hát.");
                return (decimal)gia;
            }
        }

        private static DateTime PhienDangMo(SqlConnection connection, SqlTransaction transaction, int maPhong, int maHD)
        {
            using (var command = Command(connection, transaction, @"
                SELECT GioVao FROM dbo.HoaDon WHERE MaHD=@hd AND MaPhong=@phong AND TrangThai=0",
                P("@hd",maHD), P("@phong",maPhong)))
            {
                object gioVao = command.ExecuteScalar();
                if (gioVao == null) throw new InvalidOperationException("Phiên đã thay đổi. Hãy tải lại sơ đồ.");
                return (DateTime)gioVao;
            }
        }

        public DataTable HoaDon() => Read(@"
            SELECT h.MaHD,p.TenPhong,h.GioVao,h.GioRa,h.TongTienPhong,h.TongTienDichVu,
                h.TongTienPhong+h.TongTienDichVu AS TongTien
            FROM dbo.HoaDon h JOIN dbo.PhongHat p ON h.MaPhong=p.MaPhong
            WHERE h.TrangThai=1 ORDER BY h.MaHD DESC");
    }
}
