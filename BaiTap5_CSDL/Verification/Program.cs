using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BaiTap5.Shared.DAL;
using BaiTap5.Shared.GUI;
using CafePOS.BUS;
using CafePOS.DTO;
using HotelManagement.BUS;
using HotelManagement.DTO;
using InventoryManagement.BUS;
using InventoryManagement.DTO;

namespace Verification
{
    internal static class Program
    {
        private static int checks;
        private static string output;
        private static readonly System.Text.StringBuilder log=new System.Text.StringBuilder();
        [STAThread]
        private static int Main(string[] args)
        {
            output=args.Length>0?Path.GetFullPath(args[0]):Path.GetFullPath("images");Directory.CreateDirectory(output);
            Application.EnableVisualStyles();Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Cafe();Hotel();Inventory();
                log.AppendLine("PASS: "+checks+" checks; real SQL Server LocalDB + rendered WinForms.");
                Console.WriteLine(log);return 0;
            }
            catch(Exception ex){log.AppendLine("FAIL: "+ex);Console.Error.WriteLine(log);return 1;}
            finally{File.WriteAllText(Path.Combine(output,"verification.txt"),DateTime.UtcNow.ToString("u")+Environment.NewLine+log);}
        }
        private static void Check(bool condition,string name)
        { if(!condition)throw new Exception(name);checks++;log.AppendLine("PASS "+name); }
        private static string Fails(Action action,string name)
        {
            try{action();}catch(Exception ex){checks++;log.AppendLine("PASS "+name+": "+ex.Message);return ex.Message;}
            throw new Exception("Expected failure: "+name);
        }
        // Ảnh từ control WinForms đang chạy, nạp DB thật; không vẽ mô phỏng giao diện.
        private static void Capture(Form form,string relative)
        {
            form.Show();form.BringToFront();Application.DoEvents();form.Refresh();Application.DoEvents();
            string path=Path.Combine(output,relative);Directory.CreateDirectory(Path.GetDirectoryName(path));
            using(var bmp=new Bitmap(form.Width,form.Height))
            {form.DrawToBitmap(bmp,new Rectangle(Point.Empty,form.Size));bmp.Save(path,ImageFormat.Png);}
        }
        private static void Error(string message,string relative)
        {using(var dialog=new ErrorDialog(message)){Capture(dialog,relative);dialog.Close();}}
        private static void Cafe()
        {
            var bus=new CafeBus();var db=new Db("CafePOSDB");
            string validation=Fails(()=>bus.MenuEditor.Save(null,new object[]{"",1,-1m}),"Cafe: reject empty name / negative price");
            bus.CategoriesEditor.Save(null,new object[]{"Danh mục kiểm thử"});
            int cat=Convert.ToInt32(bus.CategoriesEditor.Search("Danh mục kiểm thử").Rows[0]["MaDanhMuc"]);
            bus.CategoriesEditor.Save(cat,new object[]{"Danh mục đã sửa"});
            Check(bus.CategoriesEditor.Search("Danh mục đã sửa").Rows.Count==1,"Cafe: category create/update/search");
            bus.CategoriesEditor.Delete(cat);Check(bus.CategoriesEditor.Search("Danh mục đã sửa").Rows.Count==0,"Cafe: category delete");
            bus.MenuEditor.Save(null,new object[]{"Món kiểm thử",1,12345m});
            int dish=Convert.ToInt32(bus.MenuEditor.Search("Món kiểm thử").Rows[0]["MaMon"]);
            bus.MenuEditor.Save(dish,new object[]{"Món đã sửa",1,15000m});
            Check(bus.MenuEditor.Search("Món đã sửa").Rows.Count==1,"Cafe: menu CRUD update");bus.MenuEditor.Delete(dish);
            bus.Order(new OrderDto{MaBan=1,MaMon=1,SoLuong=2});bus.Order(new OrderDto{MaBan=1,MaMon=1,SoLuong=1});
            Check(Convert.ToInt32(bus.Details(1).Rows[0]["SoLuong"])==3,"Cafe: merge same dish");
            Check(Convert.ToString(db.Scalar("SELECT TrangThai FROM BanAn WHERE MaBan=1"))=="Có khách","Cafe: occupied table");
            bus.Order(new OrderDto{MaBan=3,MaMon=2,SoLuong=1});Fails(()=>bus.Move(1,3),"Cafe: reject occupied destination");
            bus.Move(1,2);Check(bus.Details(1).Rows.Count==0&&bus.Details(2).Rows.Count==1,"Cafe: atomic table move");
            using(var form=new CafePOS.GUI.MainForm())
            {
                form.Show();Application.DoEvents();form.SelectTable(2);Capture(form,"cafe/01_main_screen.png");
                form.Pages.SelectedIndex=2;Capture(form,"cafe/02_crud_screen.png");form.Pages.SelectedIndex=0;Capture(form,"cafe/03_transaction_screen.png");
                Check(bus.Checkout(2)==75000m,"Cafe: exact checkout total");
                Check(Convert.ToString(db.Scalar("SELECT TrangThai FROM BanAn WHERE MaBan=2"))=="Trống","Cafe: checkout frees table");
                Fails(()=>bus.Checkout(2),"Cafe: prevent duplicate checkout");bus.Checkout(3);
                form.RefreshData();form.Pages.SelectedIndex=3;Capture(form,"cafe/04_report_screen.png");form.Close();
            }
            Error(validation,"cafe/05_error_handling.png");
        }
        private static void Hotel()
        {
            var bus=new HotelBus();var db=new Db("HotelDB");
            Check(bus.Customer("' OR 1=1--").Rows.Count==0,"Hotel: CCCD SQL injection treated as literal");
            string error=Fails(()=>bus.CheckIn(new CheckInDto{SoPhong="P101",CCCD="abc",HoTen="A"}),"Hotel: CCCD validation");
            int bill=bus.CheckIn(new CheckInDto{SoPhong="P101",CCCD="012345678901",HoTen="Khách kiểm thử",SoDienThoai="0912345678"});
            Check(bus.Customer("012345678901").Rows.Count==1,"Hotel: create new customer");
            Fails(()=>bus.CheckIn(new CheckInDto{SoPhong="P101",CCCD="000000000001",HoTen="Khách mẫu"}),"Hotel: reject occupied room");
            Check(Convert.ToInt32(db.Scalar("SELECT COUNT(*) FROM PhieuDatPhong WHERE SoPhong='P101' AND NgayTraPhong IS NULL"))==1,"Hotel: single active booking");
            bus.AddService(new ServiceDto{SoPhong="P101",MaDV=1,SoLuong=2});
            // 61 phút => 2 giờ, tránh kiểm thử phụ thuộc mili-giây sát biên.
            db.Execute("UPDATE PhieuDatPhong SET NgayNhanPhong=DATEADD(minute,-61,SYSDATETIME()) WHERE MaPhieu=@id",Db.P("@id",bill));
            var preview=bus.Preview("P101");Check(preview.SoGio==2&&preview.TongTien==230000m,"Hotel: hourly rounding + services");
            Check(HotelBus.Filter(bus.Rooms(),"Đơn","Đang ở").Count==1,"Hotel: disconnected room filter");
            using(var form=new HotelManagement.GUI.MainForm())
            {
                form.Show();Application.DoEvents();Capture(form,"hotel/01_main_screen.png");form.Pages.SelectedIndex=2;Capture(form,"hotel/02_crud_screen.png");
                form.Pages.SelectedIndex=1;Capture(form,"hotel/03_transaction_screen.png");
                var invoice=bus.CheckOut("P101");Check(invoice.TongTien==230000m,"Hotel: checkout persisted total");
                Check(Convert.ToString(db.Scalar("SELECT TrangThai FROM Phong WHERE SoPhong='P101'"))=="Đang dọn dẹp","Hotel: cleaning state after transaction");
                Fails(()=>bus.CheckOut("P101"),"Hotel: prevent duplicate payment");form.ShowInvoice(invoice);Capture(form,"hotel/04_report_screen.png");form.Close();
            }
            bus.Clean("P101");Check(Convert.ToString(db.Scalar("SELECT TrangThai FROM Phong WHERE SoPhong='P101'"))=="Trống","Hotel: finish cleaning");
            bus.CheckIn(new CheckInDto{SoPhong="P102",CCCD="012345678901",HoTen="Khách kiểm thử"});
            Check(bus.Customer("012345678901").Rows.Count==1,"Hotel: reuse existing customer");bus.CheckOut("P102");
            Error(error,"hotel/05_error_handling.png");
        }
        private static void Inventory()
        {
            var bus=new InventoryBus();var db=new Db("InventoryDB");
            bus.MaterialEditor.Save(null,new object[]{"TEST01","Tên vật tư kiểm thử","Cái"});
            bus.MaterialEditor.Save("TEST01",new object[]{"TEST01","Tên đã sửa","Cái"});
            Check(bus.Materials("Tên đã sửa").Rows.Count==1,"Inventory: material CRUD / parameterized search");
            bus.MaterialEditor.Delete("TEST01");Check(bus.Materials("TEST01").Rows.Count==0,"Inventory: material delete");
            bus.SupplierEditor.Save(null,new object[]{"NCC thử","0999999999","Hà Nội"});
            int supplier=Convert.ToInt32(bus.SupplierEditor.Search("NCC thử").Rows[0]["MaNCC"]);
            bus.SupplierEditor.Save(supplier,new object[]{"NCC sửa","0999999999","Hà Nội"});
            Check(bus.SupplierEditor.Search("NCC sửa").Rows.Count==1,"Inventory: supplier CRUD update");bus.SupplierEditor.Delete(supplier);
            Check(bus.Materials("' OR 1=1; DROP TABLE VatTu;--").Rows.Count==0,"Inventory: SQL injection literal");
            Check(bus.Materials("").Rows.Count==3,"Inventory: table survives injection");
            Fails(()=>bus.Save(new StockVoucherDto{LoaiPhieu="NHAP",Lines={new StockLineDto{MaVatTu="VT001",SoLuong=1,DonGia=1}}}),"Inventory: supplier required");
            bus.Save(new StockVoucherDto{LoaiPhieu="NHAP",MaNCC=1,Lines={new StockLineDto{MaVatTu="VT001",SoLuong=10,DonGia=85000}}});
            bus.Save(new StockVoucherDto{LoaiPhieu="XUAT",Lines={new StockLineDto{MaVatTu="VT001",SoLuong=3,DonGia=90000}}});
            Check(Convert.ToDecimal(db.Scalar("SELECT SoLuongTon FROM VatTu WHERE MaVatTu='VT001'"))==107m,"Inventory: import/export quantity");
            Check(bus.Value()==19845000m,"Inventory: ExecuteScalar stock valuation");
            decimal before=Convert.ToDecimal(db.Scalar("SELECT SoLuongTon FROM VatTu WHERE MaVatTu='VT001'"));int count=bus.Vouchers().Rows.Count;
            string rollback=Fails(()=>bus.Save(new StockVoucherDto{LoaiPhieu="XUAT",Lines={
                new StockLineDto{MaVatTu="VT001",SoLuong=2,DonGia=90000},new StockLineDto{MaVatTu="VT003",SoLuong=999,DonGia=350000}}}),"Inventory: insufficient stock rollback");
            Check(bus.Vouchers().Rows.Count==count,"Inventory: rollback removes voucher header");
            Check(Convert.ToDecimal(db.Scalar("SELECT SoLuongTon FROM VatTu WHERE MaVatTu='VT001'"))==before,"Inventory: rollback restores earlier line quantity");
            Check(Convert.ToInt32(db.Scalar("SELECT COUNT(*) FROM ChiTietPhieuKho"))==2,"Inventory: rollback removes detail rows");
            Check(bus.LowStock(10).Rows.Count==1,"Inventory: low stock alert");
            Fails(()=>bus.Save(new StockVoucherDto{LoaiPhieu="XUAT",Lines={new StockLineDto{MaVatTu="VT001",SoLuong=-1}}}),"Inventory: negative quantity rejected");
            using(var form=new InventoryManagement.GUI.MainForm())
            {
                form.Show();Application.DoEvents();Capture(form,"inventory/01_main_screen.png");form.Pages.SelectedIndex=1;Capture(form,"inventory/02_crud_screen.png");
                form.Pages.SelectedIndex=3;Capture(form,"inventory/03_transaction_screen.png");form.Pages.SelectedIndex=4;Capture(form,"inventory/04_report_screen.png");form.Close();
            }
            Error(rollback,"inventory/05_error_handling.png");
        }
    }
}
