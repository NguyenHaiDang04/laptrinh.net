using System;
using System.Data;
using BaiTap5.Shared.DAL;
using CafePOS.DTO;

namespace CafePOS.DAL
{
    public sealed class CafeDal
    {
        public readonly Db Database = new Db("CafePOSDB");
        public DataTable Tables() { return Database.Table("SELECT * FROM BanAn ORDER BY MaBan"); }
        public DataTable Categories() { return Database.Table("SELECT * FROM DanhMuc ORDER BY TenDanhMuc"); }
        public DataTable Menu(int category) { return Database.Table("SELECT * FROM MonAn WHERE MaDanhMuc=@id ORDER BY TenMon", Db.P("@id", category)); }
        public DataTable Details(int table)
        {
            return Database.Table(@"SELECT c.MaMon,m.TenMon,c.SoLuong,c.DonGia,c.SoLuong*c.DonGia AS ThanhTien
                FROM HoaDon h JOIN ChiTietHoaDon c ON h.MaHoaDon=c.MaHoaDon JOIN MonAn m ON m.MaMon=c.MaMon
                WHERE h.MaBan=@ban AND h.TrangThai=0", Db.P("@ban", table));
        }
        public DataTable Report()
        { return Database.Table("SELECT MaHoaDon,MaBan,GioVao,GioRa,TongTien FROM HoaDon WHERE TrangThai=1 ORDER BY MaHoaDon DESC"); }
        public void Order(OrderDto order)
        {
            Database.Transaction((cn, tx) =>
            {
                object exists = Db.One(cn, tx, "SELECT MaBan FROM BanAn WITH(UPDLOCK,HOLDLOCK) WHERE MaBan=@ban", Db.P("@ban", order.MaBan));
                if (exists == null) throw new ArgumentException("Bàn không tồn tại.");
                object price = Db.One(cn, tx, "SELECT DonGia FROM MonAn WHERE MaMon=@mon", Db.P("@mon", order.MaMon));
                if (price == null) throw new ArgumentException("Món không tồn tại.");
                object bill = Db.One(cn, tx, "SELECT MaHoaDon FROM HoaDon WITH(UPDLOCK,HOLDLOCK) WHERE MaBan=@ban AND TrangThai=0", Db.P("@ban", order.MaBan));
                int id = bill == null ? Convert.ToInt32(Db.One(cn, tx, "INSERT HoaDon(MaBan) OUTPUT INSERTED.MaHoaDon VALUES(@ban)", Db.P("@ban", order.MaBan))) : Convert.ToInt32(bill);
                int rows = Db.Exec(cn, tx, "UPDATE ChiTietHoaDon SET SoLuong=SoLuong+@sl WHERE MaHoaDon=@hd AND MaMon=@mon", Db.P("@sl", order.SoLuong), Db.P("@hd", id), Db.P("@mon", order.MaMon));
                if (rows == 0) Db.Exec(cn, tx, "INSERT ChiTietHoaDon VALUES(@hd,@mon,@sl,@gia)", Db.P("@hd", id), Db.P("@mon", order.MaMon), Db.P("@sl", order.SoLuong), Db.P("@gia", Convert.ToDecimal(price)));
                Db.Exec(cn, tx, "UPDATE BanAn SET TrangThai=N'Có khách' WHERE MaBan=@ban", Db.P("@ban", order.MaBan));
                return id;
            });
        }
        public void Move(int source, int target)
        {
            Database.Transaction((cn, tx) =>
            {
                // Khóa cả hai bàn theo cùng thứ tự để hạn chế deadlock.
                Db.One(cn, tx, "SELECT MaBan FROM BanAn WITH(UPDLOCK,HOLDLOCK) WHERE MaBan=@id", Db.P("@id", Math.Min(source,target)));
                Db.One(cn, tx, "SELECT MaBan FROM BanAn WITH(UPDLOCK,HOLDLOCK) WHERE MaBan=@id", Db.P("@id", Math.Max(source,target)));
                object state = Db.One(cn, tx, "SELECT TrangThai FROM BanAn WHERE MaBan=@id", Db.P("@id",target));
                if (state == null || Convert.ToString(state) != "Trống") throw new InvalidOperationException("Bàn đích phải là bàn trống.");
                int changed = Db.Exec(cn, tx, "UPDATE HoaDon SET MaBan=@dich WHERE MaBan=@nguon AND TrangThai=0", Db.P("@dich",target),Db.P("@nguon",source));
                if (changed != 1) throw new InvalidOperationException("Bàn nguồn chưa có hóa đơn.");
                Db.Exec(cn, tx, "UPDATE BanAn SET TrangThai=N'Trống' WHERE MaBan=@id",Db.P("@id",source));
                Db.Exec(cn, tx, "UPDATE BanAn SET TrangThai=N'Có khách' WHERE MaBan=@id",Db.P("@id",target)); return true;
            });
        }
        public decimal Checkout(int table)
        {
            return Database.Transaction((cn,tx) =>
            {
                Db.One(cn, tx, "SELECT MaBan FROM BanAn WITH(UPDLOCK,HOLDLOCK) WHERE MaBan=@ban", Db.P("@ban", table));
                object bill = Db.One(cn,tx,"SELECT MaHoaDon FROM HoaDon WITH(UPDLOCK,HOLDLOCK) WHERE MaBan=@ban AND TrangThai=0",Db.P("@ban",table));
                if (bill == null) throw new InvalidOperationException("Bàn chưa có hóa đơn cần thanh toán.");
                int id = Convert.ToInt32(bill);
                decimal total = Convert.ToDecimal(Db.One(cn,tx,"SELECT COALESCE(SUM(SoLuong*DonGia),0) FROM ChiTietHoaDon WHERE MaHoaDon=@hd",Db.P("@hd",id)));
                Db.Exec(cn,tx,"UPDATE HoaDon SET GioRa=SYSDATETIME(),TongTien=@tien,TrangThai=1 WHERE MaHoaDon=@hd",Db.P("@tien",total),Db.P("@hd",id));
                Db.Exec(cn,tx,"UPDATE BanAn SET TrangThai=N'Trống' WHERE MaBan=@ban",Db.P("@ban",table)); return total;
            });
        }
    }
}
