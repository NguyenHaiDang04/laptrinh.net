using System;
using System.Data;
using System.Data.SqlClient;
using BaiTap6.Shared.DAL;
using Payroll.DTO;
namespace Payroll.DAL
{
 public sealed class PayrollDal
 {
  public readonly Db Database=new Db("HRMDB");
  private static void EnsureOpen(SqlConnection cn,SqlTransaction tx,string key)
  {
   object closed=Db.One(cn,tx,"SELECT DaChot FROM KyLuong WITH(UPDLOCK,HOLDLOCK) WHERE ThangNam=@m",Db.P("@m",key));
   if(closed==null)Db.Exec(cn,tx,"INSERT KyLuong(ThangNam) VALUES(@m)",Db.P("@m",key));
   else if(Convert.ToBoolean(closed))throw new InvalidOperationException("Tháng đã chốt lương, không thể sửa dữ liệu.");
  }
  public bool Closed(PeriodDto period){return Convert.ToInt32(Database.Scalar("SELECT COUNT(*) FROM KyLuong WHERE ThangNam=@m AND DaChot=1",Db.P("@m",period.Key)))>0;}
  public DataTable Load(PeriodDto period)
  {
   Database.Transaction((cn,tx)=>
   {
    object closed=Db.One(cn,tx,"SELECT DaChot FROM KyLuong WITH(UPDLOCK,HOLDLOCK) WHERE ThangNam=@m",Db.P("@m",period.Key));
    if(closed==null)Db.Exec(cn,tx,"INSERT KyLuong(ThangNam) VALUES(@m)",Db.P("@m",period.Key));
    if(closed==null||!Convert.ToBoolean(closed))Db.Exec(cn,tx,"INSERT BangChamCong(MaNV,ThangNam) SELECT MaNV,@m FROM NhanVien n WHERE NOT EXISTS(SELECT 1 FROM BangChamCong c WHERE c.MaNV=n.MaNV AND c.ThangNam=@m)",Db.P("@m",period.Key));
    return true;
   });
   using(var cn=Database.Open())using(var cmd=Db.Command(cn,null,"SELECT MaCong,MaNV,ThangNam,SoNgayCong,SoGioOT FROM BangChamCong WHERE ThangNam=@m",Db.P("@m",period.Key)))
   using(var adapter=new SqlDataAdapter(cmd)){adapter.MissingSchemaAction=MissingSchemaAction.AddWithKey;var ds=new DataSet();adapter.Fill(ds,"ChamCong");return ds.Tables[0];}
  }
  public void Save(PeriodDto period,DataTable data)
  {
   Database.Transaction((cn,tx)=>
   {
    EnsureOpen(cn,tx,period.Key);
    using(var cmd=Db.Command(cn,tx,"SELECT MaCong,MaNV,ThangNam,SoNgayCong,SoGioOT FROM BangChamCong WHERE ThangNam=@m",Db.P("@m",period.Key)))
    using(var adapter=new SqlDataAdapter(cmd))using(var builder=new SqlCommandBuilder(adapter))
    {
     adapter.UpdateCommand=builder.GetUpdateCommand();adapter.UpdateCommand.Transaction=tx;
     adapter.AcceptChangesDuringUpdate=false;adapter.Update(data);
    }
    return true;
   });
   data.AcceptChanges(); // Chỉ AcceptChanges sau Commit; lỗi giữ lại dữ liệu để người dùng sửa.
  }
  public int Close(PeriodDto period)
  {
   return Database.Transaction((cn,tx)=>
   {
    EnsureOpen(cn,tx,period.Key);
    int missing=Convert.ToInt32(Db.One(cn,tx,"SELECT COUNT(*) FROM NhanVien n WHERE NOT EXISTS(SELECT 1 FROM BangChamCong c WHERE c.MaNV=n.MaNV AND c.ThangNam=@m)",Db.P("@m",period.Key)));
    if(missing>0)throw new InvalidOperationException("Có nhân viên chưa được nạp chấm công. Hãy nạp và lưu trước.");
    int count=Db.Exec(cn,tx,@"INSERT BangLuong(MaNV,ThangNam,ThucLinh)
     SELECT n.MaNV,c.ThangNam,ROUND((n.LuongCoBan*CAST(n.HeSoLuong AS decimal(18,6))/22)*CAST(c.SoNgayCong AS decimal(10,2))+CAST(c.SoGioOT AS decimal(10,2))*50000,2)
     FROM NhanVien n JOIN BangChamCong c ON c.MaNV=n.MaNV WHERE c.ThangNam=@m",Db.P("@m",period.Key));
    if(count==0)throw new InvalidOperationException("Không có nhân viên để chốt lương.");
    Db.Exec(cn,tx,"UPDATE KyLuong SET DaChot=1,NgayChot=GETDATE() WHERE ThangNam=@m",Db.P("@m",period.Key));return count;
   });
  }
  public DataTable Employees(string q,int department)
  {return Database.Table(@"SELECT n.*,p.TenPB FROM NhanVien n JOIN PhongBan p ON p.MaPB=n.MaPB WHERE (@pb=0 OR n.MaPB=@pb) AND (n.HoTen LIKE @q OR n.CCCD LIKE @q OR p.TenPB LIKE @q) ORDER BY n.MaNV",Db.P("@pb",department),Db.P("@q","%"+q+"%"));}
  public DataTable Salaries(PeriodDto p){return Database.Table("SELECT l.MaNV,n.HoTen,l.ThangNam,l.ThucLinh FROM BangLuong l JOIN NhanVien n ON n.MaNV=l.MaNV WHERE l.ThangNam=@m",Db.P("@m",p.Key));}
 }
}
