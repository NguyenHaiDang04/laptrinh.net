using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using BaiTap668.Shared.DAL;
using Billiards.DTO;
namespace Billiards.DAL
{
 public sealed class BilliardsDal
 {
  public readonly Db Database=new Db("BilliardsDB");
  public DataTable Tables(){return Database.Table("SELECT * FROM BanBida ORDER BY MaBan");}public DataTable Menu(){return Database.Table("SELECT * FROM MenuDichVu ORDER BY MaDV");}
  public DataTable Report(){return Database.Table("SELECT *,TienGioChoi+TienDichVu AS TongTien FROM HoaDon WHERE TrangThai=1 ORDER BY MaHD DESC");}
  public DataTable Services(int table){return Database.Table("SELECT c.MaDV,m.TenDV,c.SoLuong,c.DonGia,c.SoLuong*c.DonGia AS ThanhTien FROM HoaDon h JOIN ChiTietHoaDon c ON c.MaHD=h.MaHD JOIN MenuDichVu m ON m.MaDV=c.MaDV WHERE h.MaBan=@b AND h.TrangThai=0",Db.P("@b",table));}
  private static int Active(SqlConnection cn,SqlTransaction tx,int table)
  {
   Db.One(cn,tx,"SELECT MaBan FROM BanBida WITH(UPDLOCK,HOLDLOCK) WHERE MaBan=@b",Db.P("@b",table));
   object id=Db.One(cn,tx,"SELECT MaHD FROM HoaDon WITH(UPDLOCK,HOLDLOCK) WHERE MaBan=@b AND TrangThai=0",Db.P("@b",table));if(id==null)throw new InvalidOperationException("Bàn chưa mở hoặc đã thanh toán.");return Convert.ToInt32(id);
  }
  public int Open(int table)
  {
   return Database.Transaction((cn,tx)=>
   {
    object state=Db.One(cn,tx,"SELECT TrangThai FROM BanBida WITH(UPDLOCK,HOLDLOCK) WHERE MaBan=@b",Db.P("@b",table));if(Convert.ToString(state)!="Trống")throw new InvalidOperationException("Bàn không trống.");
    int id=Convert.ToInt32(Db.One(cn,tx,"INSERT HoaDon(MaBan,GioBatDau,GiaGio) OUTPUT INSERTED.MaHD SELECT MaBan,GETDATE(),GiaTheoGio FROM BanBida WHERE MaBan=@b",Db.P("@b",table)));
    Db.Exec(cn,tx,"UPDATE BanBida SET TrangThai=@tt WHERE MaBan=@b",Db.P("@tt","Đang chơi"),Db.P("@b",table));return id;
   });
  }
  public void Save(int table,DataTable data)
  {
   Database.Transaction((cn,tx)=>
   {
    int id=Active(cn,tx,table);var prices=new Dictionary<int,decimal>();
    foreach(DataRow r in data.Rows)
    {
     if(r.RowState==DataRowState.Deleted)continue;int service=Convert.ToInt32(r["MaDV"]);
     object value=Db.One(cn,tx,"SELECT DonGia FROM ChiTietHoaDon WHERE MaHD=@h AND MaDV=@dv",Db.P("@h",id),Db.P("@dv",service));
     if(value==null)value=Db.One(cn,tx,"SELECT DonGia FROM MenuDichVu WHERE MaDV=@dv",Db.P("@dv",service));if(value==null)throw new ArgumentException("Dịch vụ không tồn tại.");prices.Add(service,Convert.ToDecimal(value));
    }
    Db.Exec(cn,tx,"DELETE ChiTietHoaDon WHERE MaHD=@h",Db.P("@h",id));
    foreach(DataRow r in data.Rows){if(r.RowState==DataRowState.Deleted)continue;int dv=Convert.ToInt32(r["MaDV"]);Db.Exec(cn,tx,"INSERT ChiTietHoaDon VALUES(@h,@dv,@sl,@gia)",Db.P("@h",id),Db.P("@dv",dv),Db.P("@sl",Convert.ToInt32(r["SoLuong"])),Db.P("@gia",prices[dv]));}return true;
   });
  }
  private static SessionDto Calculate(SqlConnection cn,SqlTransaction tx,int id)
  {
   var result=new SessionDto{MaHD=id};decimal rate;
   using(var cmd=Db.Command(cn,tx,"SELECT GioBatDau,GETDATE(),GiaGio,DATEDIFF_BIG(second,GioBatDau,GETDATE()) FROM HoaDon WHERE MaHD=@h",Db.P("@h",id)))using(var r=cmd.ExecuteReader())
   {r.Read();result.BatDau=r.GetDateTime(0);result.KetThuc=r.GetDateTime(1);rate=r.GetDecimal(2);result.SoBlock=Math.Max(1m,decimal.Ceiling(r.GetInt64(3)/900m));}
   result.TienGio=decimal.Round(result.SoBlock*rate/4m,2,MidpointRounding.AwayFromZero);
   result.TienDichVu=Convert.ToDecimal(Db.One(cn,tx,"SELECT COALESCE(SUM(SoLuong*DonGia),0) FROM ChiTietHoaDon WHERE MaHD=@h",Db.P("@h",id)));return result;
  }
  public SessionDto Preview(int table){return Database.Transaction((cn,tx)=>Calculate(cn,tx,Active(cn,tx,table)));}
  public SessionDto Checkout(int table)
  {
   return Database.Transaction((cn,tx)=>
   {
    int id=Active(cn,tx,table);var invoice=Calculate(cn,tx,id);
    var items=new List<Tuple<int,int>>();using(var cmd=Db.Command(cn,tx,"SELECT MaDV,SoLuong FROM ChiTietHoaDon WHERE MaHD=@h ORDER BY MaDV",Db.P("@h",id)))using(var r=cmd.ExecuteReader())while(r.Read())items.Add(Tuple.Create(r.GetInt32(0),r.GetInt32(1)));
    foreach(var i in items)
    {
     object managed=Db.One(cn,tx,"SELECT QuanLyTon FROM MenuDichVu WITH(UPDLOCK,HOLDLOCK) WHERE MaDV=@dv",Db.P("@dv",i.Item1));
     if(Convert.ToBoolean(managed)&&Db.Exec(cn,tx,"UPDATE MenuDichVu SET TonKho=TonKho-@sl WHERE MaDV=@dv AND TonKho>=@sl",Db.P("@sl",i.Item2),Db.P("@dv",i.Item1))!=1)throw new InvalidOperationException("Không đủ tồn dịch vụ: "+i.Item1);
    }
    Db.Exec(cn,tx,"UPDATE HoaDon SET GioKetThuc=@kt,TienGioChoi=@g,TienDichVu=@d,TrangThai=1 WHERE MaHD=@h",Db.P("@kt",invoice.KetThuc),Db.P("@g",invoice.TienGio),Db.P("@d",invoice.TienDichVu),Db.P("@h",id));
    Db.Exec(cn,tx,"UPDATE BanBida SET TrangThai=@tt WHERE MaBan=@b",Db.P("@tt","Trống"),Db.P("@b",table));return invoice;
   });
  }
 }
}
