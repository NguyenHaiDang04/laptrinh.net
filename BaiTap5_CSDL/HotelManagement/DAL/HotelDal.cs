using System;
using System.Data;
using System.Data.SqlClient;
using BaiTap5.Shared.DAL;
using HotelManagement.DTO;

namespace HotelManagement.DAL
{
    public sealed class HotelDal
    {
        public readonly Db Database=new Db("HotelDB");
        public DataTable Rooms() { return Database.Table("SELECT * FROM Phong ORDER BY SoPhong"); }
        public DataTable Services() { return Database.Table("SELECT * FROM DichVu ORDER BY TenDV"); }
        public DataTable Customer(string cccd) { return Database.Table("SELECT * FROM KhachHang WHERE CCCD=@cccd",Db.P("@cccd",cccd)); }
        public DataTable Usage(string room)
        { return Database.Table(@"SELECT s.MaSuDung,d.TenDV,s.SoLuong,s.DonGia,s.ThoiGian,s.SoLuong*s.DonGia AS ThanhTien
            FROM SuDungDichVu s JOIN DichVu d ON d.MaDV=s.MaDV JOIN PhieuDatPhong p ON p.MaPhieu=s.MaPhieu
            WHERE p.SoPhong=@phong AND p.NgayTraPhong IS NULL ORDER BY s.MaSuDung",Db.P("@phong",room)); }
        public DataTable Report()
        { return Database.Table(@"SELECT p.MaPhieu,p.SoPhong,k.HoTen,p.NgayNhanPhong,p.NgayTraPhong,p.TongTienThanhToan
            FROM PhieuDatPhong p JOIN KhachHang k ON k.MaKH=p.MaKH WHERE p.NgayTraPhong IS NOT NULL ORDER BY p.MaPhieu DESC"); }
        public int CheckIn(CheckInDto dto)
        {
            return Database.Transaction((cn,tx)=>
            {
                object state=Db.One(cn,tx,"SELECT TrangThai FROM Phong WITH(UPDLOCK,HOLDLOCK) WHERE SoPhong=@p",Db.P("@p",dto.SoPhong));
                if(state==null||Convert.ToString(state)!="Trống")throw new InvalidOperationException("Phòng không còn trống.");
                object customer=Db.One(cn,tx,"SELECT MaKH FROM KhachHang WITH(UPDLOCK,HOLDLOCK) WHERE CCCD=@c",Db.P("@c",dto.CCCD));
                int customerId=customer==null?Convert.ToInt32(Db.One(cn,tx,"INSERT KhachHang(HoTen,CCCD,SoDienThoai) OUTPUT INSERTED.MaKH VALUES(@ten,@c,@sdt)",Db.P("@ten",dto.HoTen),Db.P("@c",dto.CCCD),Db.P("@sdt",dto.SoDienThoai))):Convert.ToInt32(customer);
                int id=Convert.ToInt32(Db.One(cn,tx,@"INSERT PhieuDatPhong(SoPhong,MaKH,GiaPhong) OUTPUT INSERTED.MaPhieu
                    SELECT SoPhong,@kh,GiaTheoGio FROM Phong WHERE SoPhong=@p",Db.P("@kh",customerId),Db.P("@p",dto.SoPhong)));
                Db.Exec(cn,tx,"UPDATE Phong SET TrangThai=N'Đang ở' WHERE SoPhong=@p",Db.P("@p",dto.SoPhong));return id;
            });
        }
        public void AddService(ServiceDto dto)
        {
            Database.Transaction((cn,tx)=>
            {
                Db.One(cn,tx,"SELECT SoPhong FROM Phong WITH(UPDLOCK,HOLDLOCK) WHERE SoPhong=@p",Db.P("@p",dto.SoPhong));
                object bill=Db.One(cn,tx,"SELECT MaPhieu FROM PhieuDatPhong WITH(UPDLOCK,HOLDLOCK) WHERE SoPhong=@p AND NgayTraPhong IS NULL",Db.P("@p",dto.SoPhong));
                if(bill==null)throw new InvalidOperationException("Phòng chưa nhận khách.");
                int count=Db.Exec(cn,tx,@"INSERT SuDungDichVu(MaPhieu,MaDV,SoLuong,DonGia)
                    SELECT @phieu,MaDV,@sl,DonGia FROM DichVu WHERE MaDV=@dv",Db.P("@phieu",Convert.ToInt32(bill)),Db.P("@sl",dto.SoLuong),Db.P("@dv",dto.MaDV));
                if(count!=1)throw new ArgumentException("Dịch vụ không tồn tại.");return true;
            });
        }
        // Giữ giá phòng và đơn giá dịch vụ tại thời điểm phát sinh, tránh thay đổi hóa đơn cũ.
        private static InvoiceDto Calculate(SqlConnection cn,SqlTransaction tx,string room)
        {
            var invoice=new InvoiceDto();
            using(var cmd=Db.Command(cn,tx,@"SELECT p.MaPhieu,p.SoPhong,k.HoTen,p.NgayNhanPhong,p.GiaPhong,SYSDATETIME() AS NgayTra
                FROM PhieuDatPhong p WITH(UPDLOCK,HOLDLOCK) JOIN KhachHang k ON k.MaKH=p.MaKH
                WHERE p.SoPhong=@p AND p.NgayTraPhong IS NULL",Db.P("@p",room)))
            using(var reader=cmd.ExecuteReader())
            {
                if(!reader.Read())throw new InvalidOperationException("Phòng không có phiếu đang lưu trú.");
                invoice.MaPhieu=reader.GetInt32(0);invoice.SoPhong=reader.GetString(1);invoice.HoTen=reader.GetString(2);
                invoice.NgayNhan=reader.GetDateTime(3);invoice.GiaPhong=reader.GetDecimal(4);invoice.NgayTra=reader.GetDateTime(5);
            }
            // Quy ước bài làm: làm tròn lên từng giờ, tối thiểu một giờ.
            invoice.SoGio=Math.Max(1m,decimal.Ceiling((decimal)(invoice.NgayTra-invoice.NgayNhan).TotalHours));
            invoice.TienPhong=invoice.SoGio*invoice.GiaPhong;
            invoice.TienDichVu=Convert.ToDecimal(Db.One(cn,tx,"SELECT COALESCE(SUM(SoLuong*DonGia),0) FROM SuDungDichVu WHERE MaPhieu=@id",Db.P("@id",invoice.MaPhieu)));
            return invoice;
        }
        public InvoiceDto Preview(string room)
        {
            return Database.Transaction((cn,tx)=>
            { Db.One(cn,tx,"SELECT SoPhong FROM Phong WITH(UPDLOCK,HOLDLOCK) WHERE SoPhong=@p",Db.P("@p",room));return Calculate(cn,tx,room); });
        }
        public InvoiceDto CheckOut(string room)
        {
            return Database.Transaction((cn,tx)=>
            {
                Db.One(cn,tx,"SELECT SoPhong FROM Phong WITH(UPDLOCK,HOLDLOCK) WHERE SoPhong=@p",Db.P("@p",room));
                var invoice=Calculate(cn,tx,room);
                Db.Exec(cn,tx,"UPDATE PhieuDatPhong SET NgayTraPhong=@tra,TongTienThanhToan=@tien WHERE MaPhieu=@id",Db.P("@tra",invoice.NgayTra),Db.P("@tien",invoice.TongTien),Db.P("@id",invoice.MaPhieu));
                Db.Exec(cn,tx,"UPDATE Phong SET TrangThai=N'Đang dọn dẹp' WHERE SoPhong=@p",Db.P("@p",room));return invoice;
            });
        }
        public void Clean(string room)
        { if(Database.Execute("UPDATE Phong SET TrangThai=N'Trống' WHERE SoPhong=@p AND TrangThai=N'Đang dọn dẹp'",Db.P("@p",room))!=1)throw new InvalidOperationException("Chỉ hoàn tất dọn phòng đang dọn dẹp."); }
    }
}
