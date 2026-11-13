using System;
using System.Data;
using System.Linq;
using BaiTap668.Shared.DAL;
using Library.DTO;
namespace Library.DAL
{
 public sealed class LibraryDal
 {
  public readonly Db Database=new Db("LibraryDB");
  public DataTable Readers(){return Database.Table("SELECT * FROM DocGia ORDER BY MaDocGia");}
  public DataTable Books(){return Database.Table("SELECT * FROM DauSach ORDER BY MaSach");}
  private void MarkOverdue(){Database.Execute("UPDATE PhieuMuon SET TrangThai=@qua WHERE HanTra<GETDATE() AND TrangThai=@dang",Db.P("@qua","Quá hạn"),Db.P("@dang","Đang mượn"));}
  public DataTable Loans(int reader,string state){MarkOverdue();return Database.Table("SELECT p.*,d.HoTen FROM PhieuMuon p JOIN DocGia d ON d.MaDocGia=p.MaDocGia WHERE (@dg=0 OR p.MaDocGia=@dg) AND (@tt=N'Tất cả' OR p.TrangThai=@tt) ORDER BY p.MaPhieu DESC",Db.P("@dg",reader),Db.P("@tt",state));}
  public DataTable Overdue()
  {
   MarkOverdue();using(var cn=Database.Open())using(var cmd=Db.Command(cn,null,"SELECT p.*,d.HoTen FROM PhieuMuon p JOIN DocGia d ON d.MaDocGia=p.MaDocGia WHERE p.HanTra<GETDATE() AND p.TrangThai<>@tra ORDER BY p.HanTra",Db.P("@tra","Đã trả")))using(var reader=cmd.ExecuteReader()){var t=new DataTable();t.Load(reader);return t;}
  }
  public DataTable Statistics(){return Database.Table("SELECT s.TheLoai,COUNT(DISTINCT c.MaPhieu) AS LuotMuon,SUM(c.SoLuong) AS TongSoSach FROM ChiTietMuon c JOIN DauSach s ON s.MaSach=c.MaSach GROUP BY s.TheLoai");}
  public int Borrow(LoanDto dto)
  {
   return Database.Transaction((cn,tx)=>
   {
    object valid=Db.One(cn,tx,"SELECT MaDocGia FROM DocGia WITH(UPDLOCK,HOLDLOCK) WHERE MaDocGia=@dg AND NgayHetHanThe>=GETDATE()",Db.P("@dg",dto.MaDocGia));
    if(valid==null)throw new InvalidOperationException("Thẻ độc giả đã hết hạn hoặc không tồn tại.");
    if(Convert.ToInt32(Db.One(cn,tx,"SELECT CASE WHEN @han>GETDATE() THEN 1 ELSE 0 END",Db.P("@han",dto.HanTra)))!=1)throw new ArgumentException("Hạn trả phải ở tương lai.");
    int id=Convert.ToInt32(Db.One(cn,tx,"INSERT PhieuMuon(MaDocGia,HanTra) OUTPUT INSERTED.MaPhieu VALUES(@dg,@han)",Db.P("@dg",dto.MaDocGia),Db.P("@han",dto.HanTra)));
    foreach(var line in dto.Lines.OrderBy(l=>l.MaSach))
    {
     if(Db.Exec(cn,tx,"UPDATE DauSach WITH(UPDLOCK) SET SoLuongCon=SoLuongCon-@sl WHERE MaSach=@s AND SoLuongCon>=@sl",Db.P("@sl",line.SoLuong),Db.P("@s",line.MaSach))!=1)throw new InvalidOperationException("Không đủ sách: "+line.MaSach);
     Db.Exec(cn,tx,"INSERT ChiTietMuon VALUES(@p,@s,@sl)",Db.P("@p",id),Db.P("@s",line.MaSach),Db.P("@sl",line.SoLuong));
    }return id;
   });
  }
  public void Return(int id)
  {
   Database.Transaction((cn,tx)=>
   {
    if(Db.Exec(cn,tx,"UPDATE PhieuMuon WITH(UPDLOCK) SET TrangThai=@tra WHERE MaPhieu=@p AND TrangThai<>@tra",Db.P("@tra","Đã trả"),Db.P("@p",id))!=1)throw new InvalidOperationException("Phiếu đã trả hoặc không tồn tại.");
    Db.Exec(cn,tx,"UPDATE s SET SoLuongCon=SoLuongCon+c.SoLuong FROM DauSach s JOIN ChiTietMuon c ON c.MaSach=s.MaSach WHERE c.MaPhieu=@p",Db.P("@p",id));return true;
   });
  }
 }
}
