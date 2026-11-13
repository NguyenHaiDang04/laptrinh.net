using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using BaiTap668.Shared.BUS;
using BaiTap668.Shared.DTO;
using Library.DAL;
using Library.DTO;
namespace Library.BUS
{
 public sealed class LibraryBus
 {
  private readonly LibraryDal dal=new LibraryDal(); public ICatalogService BookEditor,ReaderEditor;
  public LibraryBus()
  {
   BookEditor=new ValidatedCatalog(dal.Database,new CatalogDefinition("Đầu sách","DauSach","MaSach",true,new Field("TenSach","Tên sách",typeof(string),true,200),new Field("TheLoai","Thể loại",typeof(string),true,50),new Field("SoLuongCon","Số lượng còn",typeof(decimal))),v=>{decimal n=Convert.ToDecimal(v[2]);if(n<0||decimal.Truncate(n)!=n)throw new ArgumentException("Số sách phải là số nguyên không âm.");});
   ReaderEditor=new ValidatedCatalog(dal.Database,new CatalogDefinition("Độc giả","DocGia","MaDocGia",true,new Field("HoTen","Họ tên",typeof(string)),new Field("Email","Email",typeof(string)),new Field("NgayHetHanThe","Hạn thẻ yyyy-MM-dd",typeof(string))),v=>{var email=new MailAddress(Convert.ToString(v[1]));DateTime d;if(!DateTime.TryParseExact(Convert.ToString(v[2]),"yyyy-MM-dd",CultureInfo.InvariantCulture,DateTimeStyles.None,out d))throw new ArgumentException("Ngày hết hạn phải có dạng yyyy-MM-dd.");});
  }
  public DataTable Readers(){return dal.Readers();}public DataTable Books(){return dal.Books();}public DataTable Loans(int reader,string state){return dal.Loans(reader,state);}public DataTable Overdue(){return dal.Overdue();}public DataTable Statistics(){return dal.Statistics();}
  public int Borrow(LoanDto dto){if(dto==null||dto.MaDocGia<=0||dto.Lines==null||dto.Lines.Count==0||dto.Lines.Any(l=>l==null||l.MaSach<=0||l.SoLuong<=0))throw new ArgumentException("Chọn độc giả và sách với số lượng dương.");if(dto.Lines.Select(l=>l.MaSach).Distinct().Count()!=dto.Lines.Count)throw new ArgumentException("Gộp các dòng trùng đầu sách.");return dal.Borrow(dto);}
  public void Return(int id){if(id<=0)throw new ArgumentException("Chọn phiếu cần trả.");dal.Return(id);}
 }
}
