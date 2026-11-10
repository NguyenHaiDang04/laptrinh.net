using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Karaoke.BUS;
using Karaoke.DAL;
using Karaoke.GUI;

namespace Karaoke.Tests
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
            Tab("tabRooms");
            Check(Find<FlowLayoutPanel>("roomGrid").Controls.Count == 6
                && Find<Button>("room1").BackColor == Color.LightGreen, "Nạp sáu phòng từ SQL và tô xanh phòng trống.");
            Capture("01_main_screen.png");
            Tab("tabServiceCatalog");
            Click("btnNewService");
            Set("txtService", "Dịch vụ thử nghiệm");
            Find<NumericUpDown>("numPrice").Value = 25000;
            Find<NumericUpDown>("numStock").Value = 3;
            Click("btnSaveService");
            int newService = (int)service.DichVu().Rows.Cast<DataRow>()
                .Single(r => (string)r["TenDV"] == "Dịch vụ thử nghiệm")["MaDV"];
            SelectRow("gridServices", "MaDV", newService);
            Find<NumericUpDown>("numStock").Value = 5;
            Click("btnSaveService");
            Check(Stock(service,newService) == 5, "Thêm và sửa dịch vụ trên form.");
            Capture("02_crud_screen.png");
            SelectRow("gridServices", "MaDV", newService);
            Click("btnDeleteService");
            Check(service.DichVu().Rows.Count == 3, "Xóa dịch vụ chưa sử dụng.");

            Tab("tabRoomCatalog");
            Click("btnNewRoom");
            Set("txtRoom", "Phòng thử nghiệm");
            Find<NumericUpDown>("numRate").Value = 99000;
            Click("btnSaveRoom");
            int newRoom = (int)service.Phong().Rows.Cast<DataRow>()
                .Single(r => (string)r["TenPhong"] == "Phòng thử nghiệm")["MaPhong"];
            SelectRow("gridRooms", "MaPhong", newRoom);
            Find<NumericUpDown>("numRate").Value = 110000;
            Click("btnSaveRoom");
            Check((decimal)service.PhienMo(newRoom).Rows[0]["GiaTheoGio"] == 110000, "Thêm và sửa giá phòng trống.");
            SelectRow("gridRooms", "MaPhong", newRoom);
            Click("btnDeleteRoom");
            Check(service.Phong().Rows.Count == 6, "Xóa phòng chưa có hóa đơn.");

            Tab("tabRooms");
            Click("room1");
            Click("btnOpen");
            DataRow first = service.PhienMo(1).Rows[0];
            int firstId = (int)first["MaHD"];
            Check((string)first["TrangThai"] == "Đang hát" && first["GioVao"] is DateTime
                && Find<Button>("room1").BackColor == Color.IndianRed,
                "Mở phòng tạo hóa đơn với GETDATE() và chuyển màu đỏ.");
            bool duplicateBlocked = false;
            try { service.MoPhong(1); }
            catch (InvalidOperationException) { duplicateBlocked = true; }
            Check(duplicateBlocked && (int)service.PhienMo(1).Rows[0]["MaHD"] == firstId,
                "Mở phòng đang hát bị chặn, không tạo hóa đơn trùng.");

            Db.Run("UPDATE dbo.HoaDon SET GioVao=DATEADD(minute,-90,GETDATE()) WHERE MaHD=@id", Db.P("@id", firstId));
            Order(1, 1);
            Order(1, 1);
            Order(3, 1);
            Check(Stock(service,1) == 30 && Stock(service,3) == 8
                && Find<DataGridView>("gridOrders").DataSource is DataTable
                && service.ChiTiet(firstId).Rows.Count == 2 && (int)service.ChiTiet(firstId).Rows[0]["SoLuong"] == 2,
                "SqlDataAdapter và DataTable hiện hai dòng, gộp dịch vụ trùng và chưa trừ kho.");
            string before = Find<Label>("lblClock").Text;
            var watch = Stopwatch.StartNew();
            while (watch.ElapsedMilliseconds < 3500 && Find<Label>("lblClock").Text == before)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(20);
            }
            Check(Find<Label>("lblClock").Text != before, "Timer cập nhật thời gian hát khi form đang mở.");
            Capture("03_transaction_screen.png");

            Click("room2");
            Click("btnOpen");
            int secondId = (int)service.PhienMo(2).Rows[0]["MaHD"];
            Order(1, 1);
            Order(3, 8);
            Click("room1");
            Click("btnPay");
            DataRow paid = service.HoaDon().Rows.Cast<DataRow>().Single();
            decimal expected = Math.Round((decimal)(((DateTime)paid["GioRa"] - (DateTime)paid["GioVao"]).TotalMinutes)
                / 60 * 120000, 2, MidpointRounding.AwayFromZero);
            Check((int)paid["MaHD"] == firstId && Math.Abs((decimal)paid["TongTienPhong"] - expected) <= 0.01m
                && (decimal)paid["TongTienDichVu"] == 109000 && expected >= 180000,
                "Tiền phòng khớp khoảng 90 phút sử dụng và tiền dịch vụ đúng 109.000 đ.");
            Check(Stock(service,1) == 28 && Stock(service,3) == 7
                && (string)service.PhienMo(1).Rows[0]["TrangThai"] == "Trống"
                && Find<Button>("room1").BackColor == Color.LightGreen,
                "Thanh toán đã trừ kho và trả phòng về màu xanh.");
            Tab("tabReports");
            Capture("04_report_screen.png");

            Tab("tabRooms");
            Click("room2");
            Click("btnPay");
            DataRow open = service.PhienMo(2).Rows[0];
            Check((int)open["MaHD"] == secondId && (string)open["TrangThai"] == "Đang hát"
                && service.HoaDon().Rows.Count == 1 && Stock(service,1) == 28 && Stock(service,3) == 7,
                "Thiếu tồn kho ở dòng sau: hoàn tác hóa đơn và cả phần kho vừa trừ ở dòng trước.");
            Check(Find<Label>("lblMessage").Text.Contains("hoàn tác"), "Form báo lỗi hoàn tác thanh toán.");
            Capture("05_error_handling.png");
            SelectRow("gridOrders", "MaDV", 3);
            Click("btnRemove");
            Click("btnPay");
            Check(service.HoaDon().Rows.Count == 2 && Stock(service,1) == 27
                && (string)service.PhienMo(2).Rows[0]["TrangThai"] == "Trống",
                "Bỏ dòng thiếu kho rồi thanh toán lại thành công.");
        }

        private void Order(int item, int qty)
        {
            Find<ComboBox>("cboService").SelectedValue = item;
            Find<NumericUpDown>("numOrder").Value = qty;
            Click("btnOrder");
        }

        private static int Stock(Service service, int id) =>
            (int)service.DichVu().Rows.Cast<DataRow>().Single(r => (int)r[0] == id)["TonKho"];

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
