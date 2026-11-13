using System;
using System.Data;
using System.Linq;
using System.Text;
using BaiTap668.Shared.DAL;
using Clinic.DTO;
namespace Clinic.DAL
{
 public sealed class ClinicDal
 {
  public readonly Db Database=new Db("ClinicDB");
  public DataTable Patient(string phone){return Database.Table("SELECT * FROM BenhNhan WHERE SoDienThoai=@sdt",Db.P("@sdt",phone));}
  public DataTable Records(){return Database.Table("SELECT h.*,b.HoTen,b.SoDienThoai FROM HoSoKham h JOIN BenhNhan b ON b.MaBN=h.MaBN ORDER BY h.MaHoSo DESC");}
  public DataTable Drugs(){return Database.Table("SELECT * FROM Thuoc ORDER BY MaThuoc");}
  public DataTable Orders(){return Database.Table("SELECT d.*,b.HoTen FROM DonThuoc d JOIN HoSoKham h ON h.MaHoSo=d.MaHoSo JOIN BenhNhan b ON b.MaBN=h.MaBN ORDER BY d.MaDon DESC");}
  public int Admit(AdmissionDto dto)
  {
   return Database.Transaction((cn,tx)=>
   {
    object patient=Db.One(cn,tx,"SELECT MaBN FROM BenhNhan WITH(UPDLOCK,HOLDLOCK) WHERE SoDienThoai=@sdt",Db.P("@sdt",dto.SoDienThoai));
    int id=patient==null?Convert.ToInt32(Db.One(cn,tx,"INSERT BenhNhan(HoTen,SoDienThoai,NamSinh) OUTPUT INSERTED.MaBN VALUES(@ten,@sdt,@ns)",Db.P("@ten",dto.HoTen),Db.P("@sdt",dto.SoDienThoai),Db.P("@ns",dto.NamSinh))):Convert.ToInt32(patient);
    return Convert.ToInt32(Db.One(cn,tx,"INSERT HoSoKham(MaBN,TrieuChung) OUTPUT INSERTED.MaHoSo VALUES(@bn,@tc)",Db.P("@bn",id),Db.P("@tc",dto.TrieuChung)));
   });
  }
  public int Prescribe(PrescriptionDto dto)
  {
   return Database.Transaction((cn,tx)=>
   {
    object record=Db.One(cn,tx,"SELECT MaHoSo FROM HoSoKham WITH(UPDLOCK,HOLDLOCK) WHERE MaHoSo=@h",Db.P("@h",dto.MaHoSo));
    if(record==null)throw new InvalidOperationException("Hồ sơ không tồn tại.");
    if(Db.One(cn,tx,"SELECT MaDon FROM DonThuoc WHERE MaHoSo=@h",Db.P("@h",dto.MaHoSo))!=null)throw new InvalidOperationException("Hồ sơ đã có đơn thuốc.");
    int id=Convert.ToInt32(Db.One(cn,tx,"INSERT DonThuoc(MaHoSo,TienKham,TongTien) OUTPUT INSERTED.MaDon VALUES(@h,@t,@t)",Db.P("@h",dto.MaHoSo),Db.P("@t",dto.TienKham)));decimal total=dto.TienKham;
    foreach(var l in dto.Lines.OrderBy(l=>l.MaThuoc))
    {
     object price=Db.One(cn,tx,"SELECT DonGia FROM Thuoc WITH(UPDLOCK,HOLDLOCK) WHERE MaThuoc=@id",Db.P("@id",l.MaThuoc));
     if(price==null||Db.Exec(cn,tx,"UPDATE Thuoc SET SoLuongTon=SoLuongTon-@sl WHERE MaThuoc=@id AND SoLuongTon>=@sl",Db.P("@id",l.MaThuoc),Db.P("@sl",l.SoLuong))!=1)throw new InvalidOperationException("Không đủ thuốc: "+l.MaThuoc);
     decimal value=Convert.ToDecimal(price);total+=value*l.SoLuong;
     Db.Exec(cn,tx,"INSERT ChiTietDonThuoc VALUES(@d,@id,@sl,@lieu,@gia)",Db.P("@d",id),Db.P("@id",l.MaThuoc),Db.P("@sl",l.SoLuong),Db.P("@lieu",l.LieuDung),Db.P("@gia",value));
    }
    Db.Exec(cn,tx,"UPDATE DonThuoc SET TongTien=@t WHERE MaDon=@d",Db.P("@t",total),Db.P("@d",id));
    Db.Exec(cn,tx,"UPDATE HoSoKham SET ChanDoan=@cd WHERE MaHoSo=@h",Db.P("@cd",dto.ChanDoan),Db.P("@h",dto.MaHoSo));return id;
   });
  }
  public void Restock(int drug,int quantity){if(Database.Execute("UPDATE Thuoc SET SoLuongTon=SoLuongTon+@sl WHERE MaThuoc=@id",Db.P("@sl",quantity),Db.P("@id",drug))!=1)throw new InvalidOperationException("Thuốc không tồn tại.");}
  public void Pay(int id){if(Database.Execute("UPDATE DonThuoc SET TrangThai=@da WHERE MaDon=@d AND TrangThai=@cho",Db.P("@da","Đã thanh toán"),Db.P("@cho","Chờ lấy thuốc"),Db.P("@d",id))!=1)throw new InvalidOperationException("Đơn đã thanh toán hoặc không tồn tại.");}
  public string Invoice(int id)
  {
   var h=Database.Table("SELECT d.*,b.HoTen,h.ChanDoan FROM DonThuoc d JOIN HoSoKham h ON h.MaHoSo=d.MaHoSo JOIN BenhNhan b ON b.MaBN=h.MaBN WHERE d.MaDon=@d",Db.P("@d",id));if(h.Rows.Count==0)throw new ArgumentException("Không tìm thấy hóa đơn.");var r=h.Rows[0];
   var s=new StringBuilder("HÓA ĐƠN PHÒNG KHÁM #"+id+Environment.NewLine);s.AppendLine("Bệnh nhân: "+r["HoTen"]);s.AppendLine("Chẩn đoán: "+r["ChanDoan"]);s.AppendLine("Trạng thái: "+r["TrangThai"]);s.AppendLine("Tiền khám: "+Convert.ToDecimal(r["TienKham"]).ToString("N0")+" đ");
   foreach(DataRow l in Database.Table("SELECT t.TenThuoc,c.SoLuong,c.DonGia,c.LieuDung FROM ChiTietDonThuoc c JOIN Thuoc t ON t.MaThuoc=c.MaThuoc WHERE c.MaDon=@d",Db.P("@d",id)).Rows)s.AppendLine(l["TenThuoc"]+" x "+l["SoLuong"]+" @ "+l["DonGia"]+" | "+l["LieuDung"]);
   s.AppendLine("TỔNG: "+Convert.ToDecimal(r["TongTien"]).ToString("N0")+" đ");return s.ToString();
  }
 }
}
