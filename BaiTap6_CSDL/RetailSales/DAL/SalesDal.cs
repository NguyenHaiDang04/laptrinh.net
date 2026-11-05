using System;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using BaiTap6.Shared.DAL;
using RetailSales.DTO;
namespace RetailSales.DAL
{
 public sealed class SalesDal
 {
  public readonly Db Database=new Db("SalesDB");
  public DataTable Products(string q){return Database.Table("SELECT * FROM SanPham WHERE MaSP LIKE @q ESCAPE '~' OR TenSP LIKE @q ESCAPE '~' ORDER BY MaSP",Db.P("@q","%"+(q??"").Replace("~","~~").Replace("%","~%").Replace("_","~_").Replace("[","~[")+"%"));}
  public DataTable Employees(){return Database.Table("SELECT * FROM NhanVien ORDER BY MaNV");}
  public DataTable Customers(){return Database.Table("SELECT * FROM KhachHang ORDER BY MaKH");}
  public DataTable Bills(){return Database.Table("SELECT * FROM HoaDon ORDER BY MaHD DESC");}
  public decimal Revenue(DateTime from,DateTime to){return Convert.ToDecimal(Database.Scalar("SELECT COALESCE(SUM(TongTien),0) FROM HoaDon WHERE NgayLap>=@from AND NgayLap<@to",Db.P("@from",from),Db.P("@to",to)));}
  public DataTable Top5()
  {
   using(var cn=Database.Open())using(var cmd=Db.Command(cn,null,"SELECT TOP 5 s.MaSP,s.TenSP,SUM(c.SoLuong) AS SoLuongBan FROM ChiTietHoaDon c JOIN SanPham s ON s.MaSP=c.MaSP GROUP BY s.MaSP,s.TenSP ORDER BY SUM(c.SoLuong) DESC,s.MaSP"))
   using(var reader=cmd.ExecuteReader()){var t=new DataTable();t.Load(reader);return t;}
  }
  public ReceiptDto Pay(SaleDto dto)
  {
   return Database.Transaction((cn,tx)=>
   {
    var receipt=new ReceiptDto();
    receipt.MaHD=Convert.ToInt32(Db.One(cn,tx,"INSERT HoaDon(MaNV,MaKH,TongTien) OUTPUT INSERTED.MaHD VALUES(@nv,@kh,0)",Db.P("@nv",dto.MaNV),new SqlParameter("@kh",SqlDbType.Int){Value=(object)dto.MaKH??DBNull.Value}));
    foreach(var line in dto.Lines.OrderBy(x=>x.MaSP,StringComparer.Ordinal))
    {
     object price=Db.One(cn,tx,"SELECT GiaBan FROM SanPham WITH(UPDLOCK,HOLDLOCK) WHERE MaSP=@sp",Db.P("@sp",line.MaSP));
     if(price==null)throw new InvalidOperationException("Sản phẩm không tồn tại: "+line.MaSP);
     if(Db.Exec(cn,tx,"UPDATE SanPham SET SoLuongTon=SoLuongTon-@sl WHERE MaSP=@sp AND SoLuongTon>=@sl",Db.P("@sl",line.SoLuong),Db.P("@sp",line.MaSP))!=1)throw new InvalidOperationException("Không đủ hàng: "+line.MaSP);
     decimal value=Convert.ToDecimal(price);receipt.TongTien+=value*line.SoLuong;
     Db.Exec(cn,tx,"INSERT ChiTietHoaDon VALUES(@hd,@sp,@sl,@gia)",Db.P("@hd",receipt.MaHD),Db.P("@sp",line.MaSP),Db.P("@sl",line.SoLuong),Db.P("@gia",value));
    }
    receipt.Diem=dto.MaKH.HasValue?(int)decimal.Floor(receipt.TongTien/100000m):0;
    Db.Exec(cn,tx,"UPDATE HoaDon SET TongTien=@t WHERE MaHD=@hd",Db.P("@t",receipt.TongTien),Db.P("@hd",receipt.MaHD));
    if(dto.MaKH.HasValue)Db.Exec(cn,tx,"UPDATE KhachHang SET DiemTichLuy=DiemTichLuy+@d WHERE MaKH=@kh",Db.P("@d",receipt.Diem),Db.P("@kh",dto.MaKH.Value));
    return receipt;
   });
  }
 }
}
