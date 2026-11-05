using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using BaiTap6.Shared.BUS;
using BaiTap6.Shared.DTO;
using RetailSales.DAL;
using RetailSales.DTO;
namespace RetailSales.BUS
{
 public sealed class SalesBus
 {
  private readonly SalesDal dal=new SalesDal();
  public ICatalogService ProductEditor,CustomerEditor,EmployeeEditor;
  public SalesBus()
  {
   ProductEditor=new ValidatedCatalog(dal.Database,new CatalogDefinition("Sản phẩm","SanPham","MaSP",false,new Field("MaSP","Mã vạch",typeof(string),true,20),new Field("TenSP","Tên sản phẩm",typeof(string),true,150),new Field("GiaBan","Giá bán",typeof(decimal)),new Field("SoLuongTon","Tồn kho",typeof(decimal))),v=>{if(Convert.ToDecimal(v[2])<=0||Convert.ToDecimal(v[3])<0||decimal.Truncate(Convert.ToDecimal(v[3]))!=Convert.ToDecimal(v[3]))throw new ArgumentException("Giá phải dương, tồn phải là số nguyên không âm.");});
   CustomerEditor=new ValidatedCatalog(dal.Database,new CatalogDefinition("Khách hàng","KhachHang","MaKH",true,new Field("TenKH","Tên khách",typeof(string)),new Field("DienThoai","Điện thoại",typeof(string),true,15)),v=>Phone(v[1]));
   EmployeeEditor=new ValidatedCatalog(dal.Database,new CatalogDefinition("Nhân viên","NhanVien","MaNV",true,new Field("TenNV","Tên nhân viên",typeof(string)),new Field("SoDienThoai","Điện thoại",typeof(string),true,15),new Field("ChucVu","Chức vụ",typeof(string),true,50)),v=>Phone(v[1]));
  }
  private static void Phone(object v){if(!Regex.IsMatch(Convert.ToString(v),@"^\d{9,15}$"))throw new ArgumentException("Điện thoại phải có 9–15 chữ số.");}
  public DataTable Products(string q){return dal.Products(q);} public DataTable Customers(){return dal.Customers();} public DataTable Employees(){return dal.Employees();} public DataTable Top5(){return dal.Top5();} public DataTable Bills(){return dal.Bills();}
  public decimal Revenue(DateTime from,DateTime to){return dal.Revenue(from,to);}
  public ReceiptDto Pay(SaleDto dto)
  {
   if(dto==null||dto.MaNV<=0||dto.Lines==null||dto.Lines.Count==0)throw new ArgumentException("Chọn nhân viên và thêm sản phẩm vào giỏ.");
   if(dto.Lines.Any(l=>l==null||string.IsNullOrWhiteSpace(l.MaSP)||l.SoLuong<=0))throw new ArgumentException("Mã hàng và số lượng không hợp lệ.");
   if(dto.Lines.Select(l=>l.MaSP).Distinct(StringComparer.OrdinalIgnoreCase).Count()!=dto.Lines.Count)throw new ArgumentException("Gộp các dòng cùng mã sản phẩm trước khi thanh toán.");
   return dal.Pay(dto);
  }
 }
}
