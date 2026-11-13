using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using BaiTap668.Shared.BUS;
using BaiTap668.Shared.DTO;
using Clinic.DAL;
using Clinic.DTO;
namespace Clinic.BUS
{
 public sealed class ClinicBus
 {
  private readonly ClinicDal dal=new ClinicDal();public ICatalogService DrugEditor;
  public ClinicBus(){DrugEditor=new ValidatedCatalog(dal.Database,new CatalogDefinition("Thuốc","Thuoc","MaThuoc",true,new Field("TenThuoc","Tên thuốc",typeof(string),true,150),new Field("DonViTinh","Đơn vị",typeof(string),true,20),new Field("DonGia","Đơn giá",typeof(decimal)),new Field("SoLuongTon","Tồn kho",typeof(decimal))),v=>{decimal n=Convert.ToDecimal(v[3]);if(n<0||decimal.Truncate(n)!=n)throw new ArgumentException("Tồn kho phải là số nguyên không âm.");});}
  public DataTable Patient(string phone){return dal.Patient(phone);}public DataTable Records(){return dal.Records();}public DataTable Drugs(){return dal.Drugs();}public DataTable Orders(){return dal.Orders();}public string Invoice(int id){return dal.Invoice(id);}
  public int Admit(AdmissionDto dto){if(dto==null||string.IsNullOrWhiteSpace(dto.HoTen)||dto.HoTen.Length>100||!Regex.IsMatch(dto.SoDienThoai??"",@"^\d{9,15}$")||dto.NamSinh<1900||dto.NamSinh>DateTime.Today.Year||string.IsNullOrWhiteSpace(dto.TrieuChung))throw new ArgumentException("Kiểm tra họ tên, điện thoại, năm sinh và triệu chứng.");return dal.Admit(dto);}
  public int Prescribe(PrescriptionDto dto){if(dto==null||dto.MaHoSo<=0||string.IsNullOrWhiteSpace(dto.ChanDoan)||dto.ChanDoan.Length>250||dto.TienKham<0||dto.Lines==null)throw new ArgumentException("Hồ sơ, chẩn đoán và tiền khám không hợp lệ.");if(dto.Lines.Any(l=>l==null||l.MaThuoc<=0||l.SoLuong<=0||string.IsNullOrWhiteSpace(l.LieuDung)||l.LieuDung.Length>100)||dto.Lines.Select(l=>l.MaThuoc).Distinct().Count()!=dto.Lines.Count)throw new ArgumentException("Kiểm tra số lượng, liều dùng; không được trùng thuốc.");return dal.Prescribe(dto);}
  public void Restock(int drug,int quantity){if(drug<=0||quantity<=0)throw new ArgumentException("Chọn thuốc và số lượng nhập dương.");dal.Restock(drug,quantity);}public void Pay(int id){dal.Pay(id);}
 }
}
