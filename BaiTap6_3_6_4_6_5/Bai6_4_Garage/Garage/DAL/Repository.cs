using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Garage.DTO;
using static Garage.DAL.Db;

namespace Garage.DAL
{
    internal sealed class Repository
    {
        public DataTable KhachHang() => Read("SELECT MaKH,TenKH,DienThoai,DiaChi FROM dbo.KhachHang ORDER BY MaKH");

        public DataTable Xe(string bienSo) => Read(@"
            SELECT x.BienSo,x.MaKH,k.TenKH,x.HieuXe,x.NamSanXuat
            FROM dbo.Xe x JOIN dbo.KhachHang k ON x.MaKH=k.MaKH
            WHERE @bien='' OR x.BienSo=@bien ORDER BY x.BienSo", Varchar("@bien",20,bienSo));

        public int LuuKhach(KhachHang k) => Run(k.MaKH == 0
            ? "INSERT dbo.KhachHang(TenKH,DienThoai,DiaChi) VALUES(@ten,@dt,@dc)"
            : "UPDATE dbo.KhachHang SET TenKH=@ten,DienThoai=@dt,DiaChi=@dc WHERE MaKH=@id",
            P("@id",k.MaKH), P("@ten",k.TenKH), Varchar("@dt",15,k.DienThoai), P("@dc",k.DiaChi));

        public int XoaKhach(int id) => Run("DELETE dbo.KhachHang WHERE MaKH=@id", P("@id",id));

        public int LuuXe(Xe x, bool them) => Run(them
            ? "INSERT dbo.Xe(BienSo,MaKH,HieuXe,NamSanXuat) VALUES(@bien,@kh,@hieu,@nam)"
            : "UPDATE dbo.Xe SET MaKH=@kh,HieuXe=@hieu,NamSanXuat=@nam WHERE BienSo=@bien",
            Varchar("@bien",20,x.BienSo), P("@kh",x.MaKH), P("@hieu",x.HieuXe), P("@nam",x.NamSanXuat));

        public int XoaXe(string bien) => Run("DELETE dbo.Xe WHERE BienSo=@bien", Varchar("@bien",20,bien));

        public void TiepNhanMoi(KhachHang k, Xe x)
        {
            Transaction((connection, transaction) =>
            {
                int id;
                using (var command = Command(connection, transaction, @"
                    INSERT dbo.KhachHang(TenKH,DienThoai,DiaChi) OUTPUT INSERTED.MaKH VALUES(@ten,@dt,@dc)",
                    P("@ten",k.TenKH), Varchar("@dt",15,k.DienThoai), P("@dc",k.DiaChi)))
                    id = (int)command.ExecuteScalar();
                using (var command = Command(connection, transaction, @"
                    INSERT dbo.Xe(BienSo,MaKH,HieuXe,NamSanXuat) VALUES(@bien,@kh,@hieu,@nam)",
                    Varchar("@bien",20,x.BienSo), P("@kh",id), P("@hieu",x.HieuXe), P("@nam",x.NamSanXuat)))
                    return command.ExecuteNonQuery();
            });
        }

        public DataTable PhuTung() => Read(
            "SELECT MaPhuTung,TenPhuTung,DonGia,SoLuongTon FROM dbo.PhuTung ORDER BY MaPhuTung");

        public int LuuPhuTung(PhuTung p) => Run(p.MaPhuTung == 0
            ? "INSERT dbo.PhuTung(TenPhuTung,DonGia,SoLuongTon) VALUES(@ten,@gia,@ton)"
            : "UPDATE dbo.PhuTung SET TenPhuTung=@ten,DonGia=@gia,SoLuongTon=@ton WHERE MaPhuTung=@id",
            P("@id",p.MaPhuTung), P("@ten",p.TenPhuTung), Money("@gia",p.DonGia), P("@ton",p.SoLuongTon));

        public int XoaPhuTung(int id) => Run("DELETE dbo.PhuTung WHERE MaPhuTung=@id", P("@id",id));

        public int LapPhieu(string bienSo, string noiDung, IEnumerable<DongSuaChua> chiTiet)
        {
            return Transaction((connection, transaction) =>
            {
                int maPhieu;
                using (var command = Command(connection, transaction, @"
                    INSERT dbo.PhieuSuaChua(BienSo,NoiDungSua,TrangThai)
                    OUTPUT INSERTED.MaPhieu VALUES(@bien,@nd,N'Đang sửa')",
                    Varchar("@bien",20,bienSo), P("@nd",noiDung)))
                    maPhieu = (int)command.ExecuteScalar();

                decimal tong = 0;
                foreach (var dong in chiTiet)
                {
                    decimal gia;
                    string ten;
                    using (var command = Command(connection, transaction, @"
                        SELECT TenPhuTung,DonGia FROM dbo.PhuTung WITH (UPDLOCK,HOLDLOCK) WHERE MaPhuTung=@id",
                        P("@id",dong.MaPhuTung)))
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read()) throw new InvalidOperationException("Phụ tùng đã bị xóa.");
                        ten = reader.GetString(0);
                        gia = reader.GetDecimal(1);
                    }
                    using (var command = Command(connection, transaction, @"
                        INSERT dbo.ChiTietSuaChua(MaPhieu,MaPhuTung,SoLuong,TienCong,DonGia)
                        VALUES(@phieu,@pt,@sl,@cong,@gia)", P("@phieu",maPhieu), P("@pt",dong.MaPhuTung),
                        P("@sl",dong.SoLuong), Money("@cong",dong.TienCong), Money("@gia",gia)))
                        command.ExecuteNonQuery();

                    using (var command = Command(connection, transaction, @"
                        UPDATE dbo.PhuTung SET SoLuongTon=SoLuongTon-@sl
                        WHERE MaPhuTung=@pt AND SoLuongTon>=@sl", P("@sl",dong.SoLuong), P("@pt",dong.MaPhuTung)))
                    {
                        if (command.ExecuteNonQuery() != 1)
                            throw new InvalidOperationException("Không đủ tồn kho: " + ten + ". Phiếu đã được hoàn tác.");
                    }
                    tong += gia * dong.SoLuong + dong.TienCong;
                }

                using (var command = Command(connection, transaction, @"
                    UPDATE dbo.PhieuSuaChua SET TongTien=@tong,TrangThai=N'Đã hoàn thành' WHERE MaPhieu=@id",
                    Money("@tong",tong), P("@id",maPhieu)))
                    command.ExecuteNonQuery();
                return maPhieu;
            });
        }

        public DataTable Phieu() => Read(@"SELECT p.MaPhieu,p.BienSo,k.TenKH,p.NgayNhan,
            p.NoiDungSua,p.TongTien,p.TrangThai FROM dbo.PhieuSuaChua p
            JOIN dbo.Xe x ON p.BienSo=x.BienSo JOIN dbo.KhachHang k ON x.MaKH=k.MaKH ORDER BY p.MaPhieu DESC");

        public DataTable ChiTiet(int maPhieu) => Read(@"
            SELECT p.TenPhuTung,c.SoLuong,c.DonGia,c.TienCong,c.SoLuong*c.DonGia+c.TienCong AS ThanhTien
            FROM dbo.ChiTietSuaChua c JOIN dbo.PhuTung p ON c.MaPhuTung=p.MaPhuTung
            WHERE c.MaPhieu=@id ORDER BY c.MaPhuTung", P("@id",maPhieu));

        public int ThanhToan(int maPhieu) => Run(@"
            UPDATE dbo.PhieuSuaChua SET TrangThai=N'Đã thanh toán'
            WHERE MaPhieu=@id AND TrangThai=N'Đã hoàn thành'", P("@id",maPhieu));
    }
}
