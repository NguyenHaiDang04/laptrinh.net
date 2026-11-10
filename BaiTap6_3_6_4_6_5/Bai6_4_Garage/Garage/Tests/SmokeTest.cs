using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Garage.BUS;
using Garage.DAL;
using Garage.GUI;

namespace Garage.Tests
{
    internal sealed class SmokeTest
    {
        private readonly MainForm form;
        private readonly string output;
        private readonly List<string> log = new List<string>();

        private SmokeTest(MainForm form, string output)
        {
            this.form = form;
            this.output = output;
        }

        public static int Run(string output)
        {
            Directory.CreateDirectory(output);
            var settings = new SqlConnectionStringBuilder(
                Environment.GetEnvironmentVariable("LTNET_BAI6_CONNECTION") ?? "");
            if (!settings.InitialCatalog.StartsWith("LTNET_Bai6_Test_", StringComparison.Ordinal)
                || settings.DataSource != @"(localdb)\MSSQLLocalDB")
            {
                File.WriteAllText(Path.Combine(output, "verification.txt"), "FAIL: Cần CSDL LocalDB dùng riêng để kiểm thử.");
                return 1;
            }

            using (var form = new MainForm())
            {
                var test = new SmokeTest(form, output);
                int result = 0;
                try
                {
                    form.StartPosition = FormStartPosition.Manual;
                    form.Location = new Point(-15000, -15000);
                    form.ShowInTaskbar = false;
                    form.Show();
                    Application.DoEvents();
                    test.Scenario();
                    test.log.Add("RESULT: PASS (" + test.log.Count + " kiểm tra)");
                }
                catch (Exception ex)
                {
                    test.log.Add("FAIL: " + ex);
                    test.log.Add("Thông báo form: " + test.Find<Label>("lblMessage").Text);
                    result = 1;
                }
                finally
                {
                    form.Close();
                    File.WriteAllLines(Path.Combine(output, "verification.txt"), test.log, new UTF8Encoding(true));
                }
                return result;
            }
        }

        private void Scenario()
        {
            var service = new Service();
            Tab("tabReceive");
            Check(service.Xe().Rows.Count == 2 && service.PhuTung().Rows.Count == 4,
                "Script có xe, chủ xe và phụ tùng mẫu.");
            Capture("01_main_screen.png");
            Set("txtLookup", "30a-12345");
            Click("btnLookup");
            Check(Find<DataGridView>("gridCars").Rows.Count == 1 && Find<TextBox>("txtBrand").Text == "Toyota",
                "Tra cứu biển số bằng tham số và điền dữ liệu xe.");
            Set("txtLookup", "' OR 1=1--");
            Click("btnLookup");
            Check(Find<DataGridView>("gridCars").Rows.Count == 0, "Chuỗi giống SQL injection không mở rộng kết quả tra cứu.");
            Click("btnNewCar");
            Click("btnNewOwner");
            Set("txtOwner", "Khách chạy thử");
            Set("txtPhone", "0901000003");
            Set("txtAddress", "Hà Nội");
            Set("txtPlate", "30C-33333");
            Set("txtBrand", "VinFast");
            Click("btnReceive");
            DataRow car = service.TraXe("30C-33333").Rows.Cast<DataRow>().Single();
            int owner = (int)car["MaKH"];
            Check((string)car["TenKH"] == "Khách chạy thử", "Tiếp nhận đồng thời chủ xe mới và xe mới.");
            SelectRow("gridOwners", "MaKH", owner);
            Set("txtAddress", "Hà Đông");
            Click("btnSaveOwner");
            Check(service.KhachHang().Rows.Cast<DataRow>().Any(r => (int)r["MaKH"] == owner
                && (string)r["DiaChi"] == "Hà Đông"), "Sửa thông tin chủ xe từ form.");
            SelectRow("gridCars", "BienSo", "30C-33333");
            Set("txtBrand", "Hyundai");
            Click("btnSaveCar");
            Check((string)service.TraXe("30C-33333").Rows[0]["HieuXe"] == "Hyundai", "Sửa thông tin xe từ form.");
            SelectRow("gridCars", "BienSo", "30C-33333");
            Click("btnDeleteCar");
            SelectRow("gridOwners", "MaKH", owner);
            Click("btnDeleteOwner");
            Check(service.TraXe("30C-33333").Rows.Count == 0 && service.KhachHang().Rows.Count == 2,
                "Xóa xe chưa có phiếu, rồi xóa được chủ xe không còn xe.");

            Tab("tabParts");
            Click("btnNewPart");
            Set("txtPart", "Bugi thử nghiệm");
            Find<NumericUpDown>("numPrice").Value = 42000;
            Find<NumericUpDown>("numStock").Value = 2;
            Click("btnSavePart");
            int newPart = (int)service.PhuTung().Rows.Cast<DataRow>()
                .Single(r => (string)r["TenPhuTung"] == "Bugi thử nghiệm")["MaPhuTung"];
            SelectRow("gridParts", "MaPhuTung", newPart);
            Find<NumericUpDown>("numPrice").Value = 44000;
            Find<NumericUpDown>("numStock").Value = 4;
            Click("btnSavePart");
            Check(Stock(service,newPart) == 4 && (decimal)service.PhuTung().Rows.Cast<DataRow>()
                .Single(r => (int)r[0] == newPart)["DonGia"] == 44000, "CRUD phụ tùng cập nhật đúng giá và tồn kho.");
            Capture("02_crud_screen.png");
            SelectRow("gridParts", "MaPhuTung", newPart);
            Click("btnDeletePart");
            Check(service.PhuTung().Rows.Count == 4, "Xóa được phụ tùng chưa sử dụng.");

            Tab("tabRepair");
            Find<ComboBox>("cboCar").SelectedValue = "30A-12345";
            Set("txtWork", "Thay lọc dầu và kiểm tra động cơ");
            AddPart(1, 2, 50000);
            Check(Stock(service,1) == 12, "Thêm dòng nháp chưa trừ tồn kho.");
            Capture("03_transaction_screen.png");
            Click("btnFinish");
            DataRow bill = service.Phieu().Rows.Cast<DataRow>().Single();
            int billId = (int)bill["MaPhieu"];
            Check((decimal)bill["TongTien"] == 300000 && (string)bill["TrangThai"] == "Đã hoàn thành"
                && service.ChiTiet(billId).Rows.Count == 1 && Stock(service,1) == 10,
                "Lập phiếu, lưu chi tiết và trừ kho cùng giao dịch; tổng đúng 300.000 đ.");
            Tab("tabBills");
            SelectRow("gridBills", "MaPhieu", billId);
            Click("btnPay");
            bill = service.Phieu().Rows[0];
            Check((string)bill["TrangThai"] == "Đã thanh toán", "Thanh toán cập nhật trạng thái phiếu.");
            string receiptPath = Path.Combine(output, "HoaDon_" + billId + ".txt");
            service.XuatHoaDon(bill, receiptPath);
            string receipt = File.ReadAllText(receiptPath);
            Check(receipt.Contains("30A-12345") && receipt.Contains("Lọc dầu") && receipt.Contains("Đã thanh toán"),
                "Tệp hóa đơn TXT có biển số, phụ tùng và trạng thái thanh toán.");
            Check(Find<Button>("btnExport").Enabled && !Find<Button>("btnPay").Enabled,
                "Phiếu đã thanh toán bật xuất hóa đơn và khóa thanh toán lại.");
            Capture("04_report_screen.png");

            Tab("tabRepair");
            Find<ComboBox>("cboCar").SelectedValue = "30A-12345";
            Set("txtWork", "Kiểm tra giao dịch khi thiếu phụ tùng");
            AddPart(1, 1, 10000);
            AddPart(4, 1, 0);
            Click("btnFinish");
            Check(service.Phieu().Rows.Count == 1 && Stock(service,1) == 10 && Stock(service,4) == 0
                && Convert.ToInt32(Db.Read("SELECT COUNT(*) FROM dbo.ChiTietSuaChua").Rows[0][0]) == 1,
                "Rollback thật: phiếu mới, chi tiết mới và phần đã trừ ở dòng trước đều được hoàn tác.");
            Check(Find<Label>("lblMessage").Text.Contains("hoàn tác"), "Form báo lỗi thiếu tồn kho.");
            Capture("05_error_handling.png");
        }

        private void AddPart(int part, int qty, decimal labor)
        {
            Find<ComboBox>("cboPart").SelectedValue = part;
            Find<NumericUpDown>("numQty").Value = qty;
            Find<NumericUpDown>("numLabor").Value = labor;
            Click("btnAddLine");
        }

        private static int Stock(Service service, int id) =>
            (int)service.PhuTung().Rows.Cast<DataRow>().Single(r => (int)r[0] == id)["SoLuongTon"];

        private T Find<T>(string name) where T : Control => (T)form.Controls.Find(name, true).Single();

        private void Tab(string name)
        {
            Find<TabControl>("tabs").SelectedTab = Find<TabPage>(name);
            Application.DoEvents();
        }

        private void Click(string name)
        {
            Find<Button>(name).PerformClick();
            Application.DoEvents();
        }

        private void Set(string name, string value) => Find<TextBox>(name).Text = value;

        private void SelectRow(string grid, string column, object value)
        {
            var view = Find<DataGridView>(grid);
            var row = view.Rows.Cast<DataGridViewRow>().Single(r => Equals(r.Cells[column].Value, value));
            view.CurrentCell = row.Cells.Cast<DataGridViewCell>().First(c => c.Visible);
            Application.DoEvents();
        }

        private void Check(bool condition, string message)
        {
            if (!condition) throw new InvalidOperationException(message);
            log.Add("PASS: " + message);
        }

        private void Capture(string name)
        {
            form.Refresh();
            Application.DoEvents();
            using (var image = new Bitmap(form.Width, form.Height))
            {
                form.DrawToBitmap(image, new Rectangle(Point.Empty, form.Size));
                image.Save(Path.Combine(output, name), ImageFormat.Png);
            }
        }
    }
}
