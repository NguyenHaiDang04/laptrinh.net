using System;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using BaiTap668.Shared.DAL;
using BaiTap668.Shared.GUI;
using Library.BUS;
using Library.DTO;
using Clinic.BUS;
using Clinic.DTO;
using Billiards.BUS;
namespace Verification
{
 internal static class Program
 {
  private static string output;private static int count;private static readonly System.Text.StringBuilder log=new System.Text.StringBuilder();
  [STAThread]private static int Main(string[] args)
  {
   output=Path.GetFullPath(args[0]);Directory.CreateDirectory(output);Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);Ui.ErrorHandler=e=>{throw new InvalidOperationException("GUI event failed",e);};Application.EnableVisualStyles();Application.SetCompatibleTextRenderingDefault(false);
   try{Designers();LibraryTests();ClinicTests();BilliardsTests();log.AppendLine("PASS: "+count+" checks; SQL Server LocalDB + real WinForms.");Console.WriteLine(log);return 0;}
   catch(Exception e){log.AppendLine("FAIL: "+e);Console.Error.WriteLine(log);return 1;}
   finally{File.WriteAllText(Path.Combine(output,"verification.txt"),DateTime.UtcNow.ToString("u")+Environment.NewLine+log);}
  }
  private static void Check(bool condition,string title){if(!condition)throw new Exception(title);count++;Console.WriteLine("PASS "+title);log.AppendLine("PASS "+title);}
  private static string Fail(Action action,string title){try{action();}catch(Exception e){count++;log.AppendLine("PASS "+title+": "+e.Message);return e.Message;}throw new Exception("Expected failure: "+title);}
  private static void Designers(){foreach(var type in new[]{typeof(Library.GUI.MainForm),typeof(Clinic.GUI.MainForm),typeof(Billiards.GUI.MainForm)})using(var s=new DesignSurface(type))Check(s.IsLoaded&&s.LoadErrors.Count==0,type.Namespace+" loads in design host without database initialization");}
  private static void Layout(Control parent){foreach(Control c in parent.Controls){if(!c.Visible)continue;if(c is FlowLayoutPanel&&c.Dock==DockStyle.Top){if(c.Height<24||!parent.ClientRectangle.Contains(c.Bounds))throw new Exception("Toolbar outside visible area");foreach(Control s in parent.Controls)if(s is DataGridView&&s.Visible&&s.Bounds.IntersectsWith(c.Bounds))throw new Exception("Toolbar overlaps grid");}Layout(c);}}
  private static void Capture(Form f,string path){Console.WriteLine("CAPTURE "+path);f.Show();Application.DoEvents();f.Refresh();Application.DoEvents();Layout(f);string file=Path.Combine(output,path);Directory.CreateDirectory(Path.GetDirectoryName(file));using(var b=new Bitmap(f.Width,f.Height)){f.DrawToBitmap(b,new Rectangle(Point.Empty,f.Size));b.Save(file,ImageFormat.Png);}}
  private static void Error(string text,string path){using(var f=new ErrorDialog(text)){Capture(f,path);f.Close();}}
  private static void LibraryTests()
  {
   var bus=new LibraryBus();var db=new Db("LibraryDB");
   bus.BookEditor.Save(null,new object[]{"Sách thử","Thử nghiệm",3m});int book=Convert.ToInt32(bus.BookEditor.Search("Sách thử").Rows[0]["MaSach"]);bus.BookEditor.Save(book,new object[]{"Sách sửa","Thử nghiệm",4m});Check(bus.BookEditor.Search("Sách sửa").Rows.Count==1,"Library book create/update/search");bus.BookEditor.Delete(book);Check(bus.BookEditor.Search("Sách sửa").Rows.Count==0,"Library book delete");
   bus.ReaderEditor.Save(null,new object[]{"Độc giả thử","test@example.com",DateTime.Today.AddYears(1).ToString("yyyy-MM-dd")});int reader=Convert.ToInt32(bus.ReaderEditor.Search("test@example.com").Rows[0]["MaDocGia"]);bus.ReaderEditor.Save(reader,new object[]{"Độc giả sửa","test@example.com",DateTime.Today.AddYears(2).ToString("yyyy-MM-dd")});Check(bus.ReaderEditor.Search("Độc giả sửa").Rows.Count==1,"Library reader CRUD");bus.ReaderEditor.Delete(reader);
   Check(bus.BookEditor.Search("' OR 1=1; DROP TABLE DauSach;--").Rows.Count==0&&bus.Books().Rows.Count==3,"Library SQL injection literal");
   string expired=Fail(()=>bus.Borrow(new LoanDto{MaDocGia=2,HanTra=DateTime.Now.AddDays(7),Lines={new BookLine{MaSach=1,SoLuong=1}}}),"Library expired card rejected");
   Check(bus.Loans(0,"Tất cả").Rows.Count==0,"Library expired card leaves no loan");
   int id=bus.Borrow(new LoanDto{MaDocGia=1,HanTra=DateTime.Now.AddDays(7),Lines={new BookLine{MaSach=1,SoLuong=2},new BookLine{MaSach=2,SoLuong=1}}});Check(Convert.ToInt32(db.Scalar("SELECT SoLuongCon FROM DauSach WHERE MaSach=1"))==8,"Library borrow deducts stock");Check(bus.Loans(1,"Đang mượn").Rows.Count==1,"Library reader and status filter");
   Fail(()=>bus.Borrow(new LoanDto{MaDocGia=1,HanTra=DateTime.Now.AddDays(7),Lines={new BookLine{MaSach=1,SoLuong=1},new BookLine{MaSach=3,SoLuong=99}}}),"Library multi-line insufficient stock rollback");Check(bus.Loans(0,"Tất cả").Rows.Count==1&&Convert.ToInt32(db.Scalar("SELECT SoLuongCon FROM DauSach WHERE MaSach=1"))==8,"Library rollback header and earlier stock");
   int overdue=bus.Borrow(new LoanDto{MaDocGia=1,HanTra=DateTime.Now.AddDays(1),Lines={new BookLine{MaSach=3,SoLuong=1}}});db.Execute("UPDATE PhieuMuon SET HanTra=DATEADD(day,-1,GETDATE()) WHERE MaPhieu=@p",Db.P("@p",overdue));Check(bus.Overdue().Rows.Count==1&&bus.Loans(1,"Quá hạn").Rows.Count==1,"Library reader-based overdue query and status");Check(bus.Statistics().Rows.Count==3,"Library GROUP BY genre statistics");
   using(var f=new Library.GUI.MainForm()){Capture(f,"library/01_main_screen.png");f.Pages.SelectedIndex=1;Capture(f,"library/02_crud_screen.png");f.Cart.Add(new BookLine{MaSach=1,SoLuong=1});f.Pages.SelectedIndex=3;Capture(f,"library/03_transaction_screen.png");f.Pages.SelectedIndex=4;Capture(f,"library/04_report_screen.png");f.Close();}
   bus.Return(id);Check(Convert.ToInt32(db.Scalar("SELECT SoLuongCon FROM DauSach WHERE MaSach=1"))==10,"Library return restores stock");Fail(()=>bus.Return(id),"Library duplicate return prevented");bus.Return(overdue);Check(bus.Overdue().Rows.Count==0&&bus.Loans(1,"Đã trả").Rows.Count==2,"Library return overdue loan excludes alert");Error(expired,"library/05_error_handling.png");
  }
  private static void ClinicTests()
  {
   var bus=new ClinicBus();var db=new Db("ClinicDB");Check(bus.Patient("' OR 1=1--").Rows.Count==0,"Clinic phone SQL injection literal");
   bus.DrugEditor.Save(null,new object[]{"Thuốc thử","Viên",2000m,10m});int drug=Convert.ToInt32(bus.DrugEditor.Search("Thuốc thử").Rows[0]["MaThuoc"]);bus.DrugEditor.Save(drug,new object[]{"Thuốc sửa","Viên",3000m,10m});Check(bus.DrugEditor.Search("Thuốc sửa").Rows.Count==1,"Clinic drug CRUD");bus.DrugEditor.Delete(drug);
   int record=bus.Admit(new AdmissionDto{HoTen="Bệnh nhân kiểm thử",SoDienThoai="0912345678",NamSinh=2000,TrieuChung="Triệu chứng minh họa"});int second=bus.Admit(new AdmissionDto{HoTen="Bệnh nhân kiểm thử",SoDienThoai="0912345678",NamSinh=2000,TrieuChung="Tái khám"});Check(bus.Patient("0912345678").Rows.Count==1&&bus.Records().Rows.Count==2,"Clinic new/existing patient admission");
   var dto=new PrescriptionDto{MaHoSo=record,ChanDoan="Chẩn đoán minh họa",TienKham=100000,Lines={new DrugLine{MaThuoc=1,SoLuong=3,LieuDung="Liều dùng minh họa"},new DrugLine{MaThuoc=2,SoLuong=1,LieuDung="Liều dùng minh họa"}}};int id=bus.Prescribe(dto);
   Check(Convert.ToDecimal(db.Scalar("SELECT TongTien FROM DonThuoc WHERE MaDon=@d",Db.P("@d",id)))==128000m,"Clinic exact exam + drug total");Check(Convert.ToInt32(db.Scalar("SELECT SoLuongTon FROM Thuoc WHERE MaThuoc=1"))==97,"Clinic prescription deducts stock");Fail(()=>bus.Prescribe(dto),"Clinic duplicate prescription prevented");
   string rollback=Fail(()=>bus.Prescribe(new PrescriptionDto{MaHoSo=second,ChanDoan="Không lưu nếu lỗi",TienKham=100000,Lines={new DrugLine{MaThuoc=1,SoLuong=2,LieuDung="Mẫu"},new DrugLine{MaThuoc=2,SoLuong=99,LieuDung="Mẫu"}}}),"Clinic insufficient drug transaction rollback");
   Check(bus.Orders().Rows.Count==1&&Convert.ToInt32(db.Scalar("SELECT COUNT(*) FROM ChiTietDonThuoc"))==2,"Clinic rollback order and details");Check(Convert.ToInt32(db.Scalar("SELECT SoLuongTon FROM Thuoc WHERE MaThuoc=1"))==97&&db.Scalar("SELECT ChanDoan FROM HoSoKham WHERE MaHoSo=@h",Db.P("@h",second))==DBNull.Value,"Clinic rollback earlier stock and diagnosis");
   bus.Restock(1,10);Check(Convert.ToInt32(db.Scalar("SELECT SoLuongTon FROM Thuoc WHERE MaThuoc=1"))==107,"Clinic restock");db.Execute("UPDATE Thuoc SET DonGia=9000 WHERE MaThuoc=1");Check(bus.Invoice(id).Contains("128,000")||bus.Invoice(id).Contains("128.000"),"Clinic invoice preserves historical unit price");bus.Pay(id);Fail(()=>bus.Pay(id),"Clinic duplicate payment rejected");Check(bus.Invoice(id).Contains("Đã thanh toán"),"Clinic paid invoice status");
   using(var f=new Clinic.GUI.MainForm()){Capture(f,"clinic/01_main_screen.png");f.Pages.SelectedIndex=1;Capture(f,"clinic/02_crud_screen.png");f.Cart.Add(new DrugLine{MaThuoc=1,SoLuong=1,LieuDung="Minh họa"});f.Pages.SelectedIndex=3;Capture(f,"clinic/03_transaction_screen.png");f.ShowInvoice(id);Capture(f,"clinic/04_report_screen.png");f.Close();}Error(rollback,"clinic/05_error_handling.png");
  }
  private static void BilliardsTests()
  {
   var bus=new BilliardsBus();var db=new Db("BilliardsDB");int id=bus.Open(1);Fail(()=>bus.Open(1),"Billiards duplicate open rejected");
   var data=bus.Services(1);data.Rows.Add(1,"Nước tăng lực",2,1m,2m);data.Rows.Add(3,"Thuê gậy",1,1m,1m);bus.Save(1,data);
   Check(Convert.ToDecimal(bus.Services(1).Select("MaDV=1")[0]["DonGia"])==15000m,"Billiards server price ignores modified client price");Check(Convert.ToInt32(db.Scalar("SELECT TonKho FROM MenuDichVu WHERE MaDV=1"))==20,"Billiards draft does not deduct stock before checkout");
   db.Execute("UPDATE HoaDon SET GioBatDau=DATEADD(minute,-31,GETDATE()) WHERE MaHD=@h",Db.P("@h",id));var preview=bus.Preview(1);Check(preview.SoBlock==3&&preview.TienGio==45000m&&preview.TienDichVu==60000m,"Billiards 15-minute blocks + saved services");
   using(var f=new Billiards.GUI.MainForm()){f.Show();Application.DoEvents();f.SelectTable(1);Capture(f,"billiards/01_main_screen.png");f.Pages.SelectedIndex=1;Capture(f,"billiards/02_crud_screen.png");f.Pages.SelectedIndex=0;f.AddService(1,1);f.SaveDraft();Check(Convert.ToInt32(bus.Services(1).Select("MaDV=1")[0]["SoLuong"])==3,"Billiards real DataTable quantity increase/save");Capture(f,"billiards/03_transaction_screen.png");var bill=bus.Checkout(1);Check(bill.TongTien==120000m,"Billiards exact checkout total");Check(Convert.ToInt32(db.Scalar("SELECT TonKho FROM MenuDichVu WHERE MaDV=1"))==17&&Convert.ToInt32(db.Scalar("SELECT TonKho FROM MenuDichVu WHERE MaDV=3"))==0,"Billiards stock deduction excludes rentals");Check(Convert.ToString(db.Scalar("SELECT TrangThai FROM BanBida WHERE MaBan=1"))=="Trống","Billiards checkout frees table");Fail(()=>bus.Checkout(1),"Billiards duplicate checkout rejected");f.SelectTable(1);f.Pages.SelectedIndex=2;Capture(f,"billiards/04_report_screen.png");f.Close();}
   bus.Open(2);data=bus.Services(2);data.Rows.Add(1,"Nước",1,15000m,15000m);data.Rows.Add(2,"Mì",99,40000m,3960000m);bus.Save(2,data);string error=Fail(()=>bus.Checkout(2),"Billiards insufficient service checkout rollback");Check(Convert.ToInt32(db.Scalar("SELECT TonKho FROM MenuDichVu WHERE MaDV=1"))==17,"Billiards rollback restores earlier service stock");Check(Convert.ToString(db.Scalar("SELECT TrangThai FROM BanBida WHERE MaBan=2"))=="Đang chơi"&&bus.Report().Rows.Count==1,"Billiards rollback leaves table and bill open");Error(error,"billiards/05_error_handling.png");
  }
 }
}
