using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using BaiTap6.Shared.DAL;
using BaiTap6.Shared.GUI;
using RetailSales.BUS;
using RetailSales.DTO;
using Payroll.BUS;
using Payroll.DTO;
namespace Verification
{
 internal static class Program
 {
  private static int checks;private static string output;private static readonly System.Text.StringBuilder log=new System.Text.StringBuilder();
  [STAThread]private static int Main(string[] args)
  {
   output=Path.GetFullPath(args[0]);Directory.CreateDirectory(output);Application.EnableVisualStyles();Application.SetCompatibleTextRenderingDefault(false);
   try{Sales();HR();log.AppendLine("PASS: "+checks+" checks; SQL Server LocalDB and real WinForms.");Console.WriteLine(log);return 0;}
   catch(Exception ex){log.AppendLine("FAIL: "+ex);Console.Error.WriteLine(log);return 1;}
   finally{File.WriteAllText(Path.Combine(output,"verification.txt"),DateTime.UtcNow.ToString("u")+Environment.NewLine+log);}
  }
  private static void Check(bool ok,string name){if(!ok)throw new Exception(name);checks++;log.AppendLine("PASS "+name);}
  private static string Fail(Action a,string name){try{a();}catch(Exception e){checks++;log.AppendLine("PASS "+name+": "+e.Message);return e.Message;}throw new Exception("Expected failure: "+name);}
  private static void Layout(Control parent){foreach(Control c in parent.Controls){if(!c.Visible)continue;if(c is FlowLayoutPanel&&c.Dock==DockStyle.Top){if(c.Height<24||!parent.ClientRectangle.Contains(c.Bounds))throw new Exception("Toolbar not visible");foreach(Control s in parent.Controls)if(s is DataGridView&&s.Visible&&s.Bounds.IntersectsWith(c.Bounds))throw new Exception("Toolbar overlaps grid");}Layout(c);}}
  private static void Capture(Form f,string path){f.Show();Application.DoEvents();f.Refresh();Application.DoEvents();Layout(f);string file=Path.Combine(output,path);Directory.CreateDirectory(Path.GetDirectoryName(file));using(var b=new Bitmap(f.Width,f.Height)){f.DrawToBitmap(b,new Rectangle(Point.Empty,f.Size));b.Save(file,ImageFormat.Png);}}
  private static void Sales()
  {
   var bus=new SalesBus();var db=new Db("SalesDB");
   bus.ProductEditor.Save(null,new object[]{"TEST","Hàng kiểm thử",1000m,10m});bus.ProductEditor.Save("TEST",new object[]{"TEST","Hàng đã sửa",2000m,10m});Check(bus.Products("Hàng đã sửa").Rows.Count==1,"Sales CRUD product update/search");bus.ProductEditor.Delete("TEST");
   Fail(()=>bus.ProductEditor.Save(null,new object[]{"BAD","Giá sai",0m,1m}),"Sales positive price validation");
   bus.CustomerEditor.Save(null,new object[]{"Khách kiểm thử","0999999999"});int id=Convert.ToInt32(bus.CustomerEditor.Search("0999999999").Rows[0]["MaKH"]);bus.CustomerEditor.Save(id,new object[]{"Khách đã sửa","0999999999"});Check(bus.CustomerEditor.Search("Khách đã sửa").Rows.Count==1,"Sales customer CRUD");bus.CustomerEditor.Delete(id);
   bus.EmployeeEditor.Save(null,new object[]{"Nhân viên thử","0988888888","Thu ngân"});id=Convert.ToInt32(bus.EmployeeEditor.Search("0988888888").Rows[0]["MaNV"]);bus.EmployeeEditor.Save(id,new object[]{"Nhân viên sửa","0988888888","Quản lý"});Check(bus.EmployeeEditor.Search("Nhân viên sửa").Rows.Count==1,"Sales employee CRUD");bus.EmployeeEditor.Delete(id);
   Check(bus.Products("' OR 1=1; DROP TABLE SanPham;--").Rows.Count==0&&bus.Products("").Rows.Count==3,"Sales SQL injection literal");
   var bill=bus.Pay(new SaleDto{MaNV=1,MaKH=1,Lines={new SaleLine{MaSP="SP001",SoLuong=4},new SaleLine{MaSP="SP002",SoLuong=1}}});
   Check(bill.TongTien==270000m&&bill.Diem==2,"Sales exact total and floor reward points");Check(Convert.ToInt32(db.Scalar("SELECT SoLuongTon FROM SanPham WHERE MaSP='SP001'"))==96,"Sales stock deducted");Check(Convert.ToInt32(db.Scalar("SELECT DiemTichLuy FROM KhachHang WHERE MaKH=1"))==2,"Sales reward persisted");
   int count=bus.Bills().Rows.Count;
   string rollback=Fail(()=>bus.Pay(new SaleDto{MaNV=1,MaKH=1,Lines={new SaleLine{MaSP="SP001",SoLuong=2},new SaleLine{MaSP="SP003",SoLuong=999}}}),"Sales insufficient stock rollback");
   Check(bus.Bills().Rows.Count==count,"Sales rollback header");Check(Convert.ToInt32(db.Scalar("SELECT COUNT(*) FROM ChiTietHoaDon"))==2,"Sales rollback detail");Check(Convert.ToInt32(db.Scalar("SELECT SoLuongTon FROM SanPham WHERE MaSP='SP001'"))==96,"Sales rollback earlier line stock");Check(Convert.ToInt32(db.Scalar("SELECT DiemTichLuy FROM KhachHang WHERE MaKH=1"))==2,"Sales rollback points");
   Check(bus.Pay(new SaleDto{MaNV=1,Lines={new SaleLine{MaSP="SP003",SoLuong=1}}}).Diem==0,"Sales walk-in customer");
   Check(bus.Revenue(DateTime.Today,DateTime.Today.AddDays(1))==280000m,"Sales daily ExecuteScalar revenue");Check(bus.Top5().Rows.Count==3&&Convert.ToString(bus.Top5().Rows[0]["MaSP"])=="SP001","Sales Top5 SqlDataReader ranking");
   using(var form=new RetailSales.GUI.MainForm()){Capture(form,"sales/01_main_screen.png");form.Pages.SelectedIndex=1;Capture(form,"sales/02_crud_screen.png");form.Pages.SelectedIndex=4;form.AddProduct("SP001",2);form.AddProduct("SP001",1);Check(form.Cart.Rows.Count==1&&(int)form.Cart.Rows[0]["SoLuong"]==3,"Sales real GUI merges scanned items");Capture(form,"sales/03_transaction_screen.png");form.Pages.SelectedIndex=5;Capture(form,"sales/04_report_screen.png");form.Close();}
   using(var e=new ErrorDialog(rollback)){Capture(e,"sales/05_error_handling.png");e.Close();}
  }
  private static void HR()
  {
   var bus=new PayrollBus();var db=new Db("HRMDB");var p=new PeriodDto(11,2026);
   bus.EmployeeEditor.Save(null,new object[]{"Hồ sơ thử","2001-03-04","123456789012",1,5000000m,1m});int id=Convert.ToInt32(bus.EmployeeEditor.Search("123456789012").Rows[0]["MaNV"]);bus.EmployeeEditor.Save(id,new object[]{"Hồ sơ chuyển phòng","2001-03-04","123456789012",2,5500000m,1.2m});Check(bus.Employees("123456789012",2).Rows.Count==1,"HR employee CRUD and transfer department");bus.EmployeeEditor.Delete(id);
   Check(bus.Employees("' OR 1=1--",0).Rows.Count==0,"HR SQL injection literal");Fail(()=>bus.EmployeeEditor.Save(null,new object[]{"A","bad","123",1,0m,0m}),"HR invalid employee validation");
   var table=bus.Load(p);Check(table.Rows.Count==2,"HR adapter loads all employees");var a=table.Select("MaNV=1")[0];a["SoNgayCong"]=22d;a["SoGioOT"]=2d;var b=table.Select("MaNV=2")[0];b["SoNgayCong"]=11d;b["SoGioOT"]=0d;
   bus.Save(p,table);Check(Convert.ToDouble(bus.Load(p).Select("MaNV=1")[0]["SoNgayCong"])==22d,"HR CommandBuilder adapter.Update saves values");
   var invalid=bus.Load(new PeriodDto(2,2027));invalid.Rows[0]["SoNgayCong"]=31d;Fail(()=>bus.Save(new PeriodDto(2,2027),invalid),"HR reject days beyond calendar month");
   // Writer khác thay đổi dòng thứ hai; adapter.Update phải rollback dòng đầu.
   var stale=bus.Load(p);stale.Select("MaNV=1")[0]["SoNgayCong"]=20d;stale.Select("MaNV=2")[0]["SoNgayCong"]=12d;
   db.Execute("UPDATE BangChamCong SET SoGioOT=1 WHERE MaNV=2 AND ThangNam=@m",Db.P("@m",p.Key));
   Fail(()=>bus.Save(p,stale),"HR optimistic concurrency conflict");Check(Convert.ToDouble(bus.Load(p).Select("MaNV=1")[0]["SoNgayCong"])==22d,"HR adapter batch rollback first update");
   db.Execute("UPDATE BangChamCong SET SoGioOT=0 WHERE MaNV=2 AND ThangNam=@m",Db.P("@m",p.Key));
   using(var form=new Payroll.GUI.MainForm())
   {
    Capture(form,"hr/01_main_screen.png");form.Pages.SelectedIndex=1;Capture(form,"hr/02_crud_screen.png");form.LoadPeriod(p);form.Pages.SelectedIndex=3;Capture(form,"hr/03_transaction_screen.png");
    Check(bus.Close(p)==2,"HR transaction closes whole month");var salaries=bus.Salaries(p);Check(Convert.ToDecimal(salaries.Select("MaNV=1")[0]["ThucLinh"])==11100000m,"HR salary formula base + OT");Check(Convert.ToDecimal(salaries.Select("MaNV=2")[0]["ThucLinh"])==6600000m,"HR salary formula coefficient / partial month");
    Check(bus.Closed(p),"HR month marked locked");Fail(()=>bus.Close(p),"HR prevents duplicate salary close");var locked=bus.Load(p);locked.Rows[0]["SoGioOT"]=100d;string error=Fail(()=>bus.Save(p,locked),"HR BUS/DAL reject editing closed month");
    Fail(()=>db.Execute("UPDATE BangChamCong SET SoGioOT=9 WHERE ThangNam=@m",Db.P("@m",p.Key)),"HR database trigger blocks direct edit");Check(bus.Salaries(p).Rows.Count==2,"HR no duplicated payroll rows");form.LoadPeriod(p);form.Pages.SelectedIndex=4;Capture(form,"hr/04_report_screen.png");using(var e=new ErrorDialog(error)){Capture(e,"hr/05_error_handling.png");e.Close();}form.Close();
   }
  }
 }
}
