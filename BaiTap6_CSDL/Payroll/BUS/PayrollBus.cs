using System;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using BaiTap6.Shared.BUS;
using BaiTap6.Shared.DTO;
using Payroll.DAL;
using Payroll.DTO;
namespace Payroll.BUS
{
 public sealed class PayrollBus
 {
  private readonly PayrollDal dal=new PayrollDal();
  public ICatalogService EmployeeEditor,DepartmentEditor;
  public PayrollBus()
  {
   DepartmentEditor=new CatalogBus(dal.Database,new CatalogDefinition("Phòng ban","PhongBan","MaPB",true,new Field("TenPB","Tên phòng",typeof(string)),new Field("Mota","Mô tả",typeof(string),false,255)));
   EmployeeEditor=new ValidatedCatalog(dal.Database,new CatalogDefinition("Nhân viên","NhanVien","MaNV",true,
    new Field("HoTen","Họ tên",typeof(string)),new Field("NgaySinh","Sinh yyyy-MM-dd",typeof(string)),new Field("CCCD","CCCD",typeof(string),true,20),new Field("MaPB","Mã phòng",typeof(int)),new Field("LuongCoBan","Lương cơ bản",typeof(decimal)),new Field("HeSoLuong","Hệ số",typeof(decimal))),v=>
    {
     DateTime date;if(!DateTime.TryParseExact(Convert.ToString(v[1]),"yyyy-MM-dd",CultureInfo.InvariantCulture,DateTimeStyles.None,out date)||date>DateTime.Today)throw new ArgumentException("Ngày sinh phải theo yyyy-MM-dd và không ở tương lai.");
     if(!Regex.IsMatch(Convert.ToString(v[2]),@"^\d{12}$"))throw new ArgumentException("CCCD phải có 12 chữ số.");
     if(Convert.ToDecimal(v[4])<=0||Convert.ToDecimal(v[5])<=0)throw new ArgumentException("Lương và hệ số phải lớn hơn 0.");
    });
  }
  public DataTable Employees(string q,int pb){return dal.Employees(q,pb);} public DataTable Load(PeriodDto p){return dal.Load(p);} public bool Closed(PeriodDto p){return dal.Closed(p);} public DataTable Salaries(PeriodDto p){return dal.Salaries(p);}
  public void Save(PeriodDto p,DataTable table)
  {
   if(table==null)throw new ArgumentException("Nạp chấm công trước khi lưu.");
   foreach(DataRow row in table.Rows)
   {
    if(row.RowState==DataRowState.Deleted||row.RowState==DataRowState.Added)throw new ArgumentException("Không thêm hoặc xóa nhân viên trực tiếp trên lưới.");
    if(Convert.ToString(row["ThangNam"])!=p.Key)throw new ArgumentException("Tháng đã thay đổi; hãy nạp lại.");
    if(row.RowState==DataRowState.Modified&&( !Equals(row["MaCong"],row["MaCong",DataRowVersion.Original])||!Equals(row["MaNV"],row["MaNV",DataRowVersion.Original])))throw new ArgumentException("Không được đổi mã nhân viên/chấm công.");
    double days=Convert.ToDouble(row["SoNgayCong"]),ot=Convert.ToDouble(row["SoGioOT"]);
    if(double.IsNaN(days)||double.IsInfinity(days)||double.IsNaN(ot)||double.IsInfinity(ot)||days<0||days>p.Days||ot<0||ot>p.Days*24||days*8+ot>p.Days*24)throw new ArgumentException("Ngày công hoặc OT vượt giới hạn tháng.");
   }
   dal.Save(p,table);
  }
  public int Close(PeriodDto p){return dal.Close(p);}
 }
}
