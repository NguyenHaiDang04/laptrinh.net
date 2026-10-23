using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp.Forms;
using WinFormsApp.Models;
using WinFormsApp.Services;

namespace WinFormsApp.Verification
{
    // Chạy bằng --verify <thư mục docs>: kiểm tra chức năng và chụp chính các form đang chạy.
    internal sealed class AppVerification : Form
    {
        private readonly string output;
        private readonly List<string> results = new List<string>();
        public AppVerification(string output)
        {
            this.output = Path.GetFullPath(output); ShowInTaskbar = false; Opacity = 0;
            Shown += async (s, e) =>
            {
                try { Directory.CreateDirectory(this.output); await RunAsync(); results.Add("RESULT: PASS"); }
                catch (Exception ex) { results.Add("RESULT: FAIL\n" + ex); Environment.ExitCode = 1; }
                finally { File.WriteAllLines(Path.Combine(this.output, "verification.txt"), results, new UTF8Encoding(true)); Close(); }
            };
        }
        private void Check(bool condition, string message)
        {
            if (!condition) throw new InvalidOperationException(message);
            results.Add("PASS: " + message);
        }
        private T Find<T>(Control form, string name) where T : Control => (T)form.Controls.Find(name, true).Single();
        private void ShowForm(Form form)
        {
            form.StartPosition = FormStartPosition.Manual; form.Location = new Point(-20000, -20000); form.ShowInTaskbar = false; form.Show();
        }
        private void CaptureForm(Form form, string name)
        {
            form.Refresh();
            using (var bitmap = new Bitmap(form.Width, form.Height))
            { form.DrawToBitmap(bitmap, new Rectangle(Point.Empty, form.Size)); bitmap.Save(Path.Combine(output, name), ImageFormat.Png); }
        }
        private async Task RunAsync()
        {
            results.Add("WinForms TEST 02 - kiểm tra chạy thực tế (.NET Framework 4.8)");
            results.Add("Ngày kiểm tra: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            results.Add("CheckForIllegalCrossThreadCalls = true");
            using (var menu = new frmMain()) { ShowForm(menu); CaptureForm(menu, "main.png"); menu.Close(); }
            using (var gym = new frmGymMemberManager())
            {
                ShowForm(gym); await Task.Delay(100);
                var name = Find<TextBox>(gym, "txtFullName"); var phone = Find<TextBox>(gym, "txtPhone"); var fee = Find<TextBox>(gym, "txtFee");
                name.Text = "An123"; phone.Text = "123"; fee.Text = "-1";
                Check(!gym.ValidateInput(), "Bài 1: chặn tên, điện thoại và phí không hợp lệ");
                await Task.Delay(100); CaptureForm(gym, "bai1_error.png");
                name.Text = "Nguyễn Hải Đăng"; phone.Text = "0911222333"; fee.Text = "450000"; gym.AddMember();
                Check(gym.Members.Count == 3 && gym.Members[2].MemberId == "HV003", "Bài 1: thêm hội viên và tự sinh mã");
                name.Text = "Nguyễn Hải Nam"; phone.Text = "0988111222"; fee.Text = "600000";
                Check(gym.Members[2].FullName == "Nguyễn Hải Đăng", "Bài 1: chưa ghi dữ liệu khi chưa bấm Sửa");
                gym.EditMember();
                Check(gym.Members[2].FullName == "Nguyễn Hải Nam" && gym.Members[2].Phone == "0988111222" && gym.Members[2].Fee == 600000,
                    "Bài 1: Sửa đồng bộ toàn bộ các ô về BindingList");
                var grid = Find<DataGridView>(gym, "dgvMembers");
                Check((string)grid.Rows[2].Cells["FullName"].Value == "Nguyễn Hải Nam", "Bài 1: lưới cập nhật ngay sau Sửa");
                gym.Members[2].FullName = "Nguyễn Hải Đăng";
                Check(name.Text == "Nguyễn Hải Đăng", "Bài 1: model thay đổi thông báo ngược lên control");
                CaptureForm(gym, "bai1_main.png");
                gym.Members.RemoveAt(2); gym.ResetEditor(); name.Text = "Lê Minh An"; phone.Text = "0901234567"; fee.Text = "300000"; gym.AddMember();
                Check(gym.Members.Last().MemberId == "HV004", "Bài 1: mã không tái sử dụng sau khi xóa"); gym.Close();
            }
            using (var books = new frmBookManager())
            {
                ShowForm(books); Check(books.Books.Count == 3, "Bài 2: nạp dữ liệu CSV mẫu"); CaptureForm(books, "bai2_main.png");
                books.ResetEditor(); Find<TextBox>(books, "txtBookId").Text = "S004"; Find<TextBox>(books, "txtTitle").Text = "C#, \"thực hành\""; Find<TextBox>(books, "txtAuthor").Text = "Nguyễn Văn An";
                Find<NumericUpDown>(books, "nudPrice").Value = 120000; Find<NumericUpDown>(books, "nudQuantity").Value = 3;
                Check(books.SaveBook(false) && books.Books.Count == 4, "Bài 2: thêm sách và tổng số đầu sách");
                Find<TextBox>(books, "txtTitle").Text = "C#, \"thực hành\" nâng cao"; Check(books.SaveBook(true), "Bài 2: sửa sách");
                string csv = Path.Combine(output, "books_export.csv"); BookCsvService.Write(csv, books.Books); var loaded = BookCsvService.Read(csv);
                Check(loaded.Count == 4 && loaded[3].Title == "C#, \"thực hành\" nâng cao" && loaded[0].CoverPath == books.Books[0].CoverPath,
                    "Bài 2: CSV round-trip tiếng Việt, dấu phẩy, ngoặc kép và đường dẫn ảnh");
                BookCsvService.Write(csv, loaded); Check(BookCsvService.Read(csv).Count == 4, "Bài 2: lưu đè CSV an toàn");
                // Bản CSV trong báo cáo tham chiếu ảnh nguồn, không phụ thuộc bin/Debug.
                string sourceCover = Path.GetFullPath(Path.Combine(output, "..", "src", "WinFormsApp", "Data", "Images", "book-cover.png"));
                if (File.Exists(sourceCover))
                {
                    foreach (var book in loaded.Where(b => b.CoverFile == "book-cover.png")) book.CoverPath = sourceCover;
                    BookCsvService.Write(csv, loaded);
                }
                Find<TextBox>(books, "txtSearch").Text = "nâng cao";
                Check(Find<DataGridView>(books, "dgvBooks").Rows.Count == 1, "Bài 2: tìm kiếm sách");
                books.ImportCsv(csv); CaptureForm(books, "bai2_csv.png");
                string bad = Path.Combine(output, "invalid.tmp");
                try
                {
                    File.WriteAllText(bad, "BookId,Title,Author,UnitPrice,Quantity,CoverPath\nS001,Test,Author,abc,1,", Encoding.UTF8);
                    bool rejected = false; try { books.ImportCsv(bad); } catch (FormatException) { rejected = true; }
                    Check(rejected && books.Books.Count == 4, "Bài 2: CSV lỗi không làm mất danh sách đang có");
                }
                finally { if (File.Exists(bad)) File.Delete(bad); }
                books.Close();
            }
            using (var hospital = new frmHospitalMain())
            {
                ShowForm(hospital); var patient = hospital.OpenPatients(); var same = hospital.OpenPatients();
                await Task.Delay(100); // Cho Shown và binding hoàn tất trước khi kiểm tra control.
                Check(ReferenceEquals(patient, same) && hospital.MdiChildren.Length == 1, "Bài 3: không mở trùng form bệnh nhân");
                Check(Find<TextBox>(patient, "txtFullName").Text == "Nguyễn Văn Nam", "Bài 3: chọn bệnh nhân đồng bộ lên ô nhập");
                Find<TextBox>(patient, "txtDiagnosis").Text = "Theo dõi sức khỏe";
                Find<Button>(patient, "btnEdit").PerformClick();
                Check((string)Find<DataGridView>(patient, "dgvRecords").Rows[0].Cells["Diagnosis"].Value == "Theo dõi sức khỏe", "Bài 3: sửa hồ sơ bệnh nhân");
                patient.WindowState = FormWindowState.Minimized; hospital.OpenPatients();
                Check(patient.WindowState == FormWindowState.Normal, "Bài 3: khôi phục form đang thu nhỏ");
                hospital.OpenDoctors(); hospital.OpenDepartments();
                await Task.Delay(100);
                Check(hospital.MdiChildren.Length == 3, "Bài 3: mở đủ bệnh nhân, bác sĩ/lịch trực, khoa/phòng");
                hospital.LayoutMdi(MdiLayout.TileHorizontal); hospital.LayoutMdi(MdiLayout.TileVertical); hospital.LayoutMdi(MdiLayout.Cascade);
                CaptureForm(hospital, "bai3_mdi.png");
                patient.Close(); Check(!ReferenceEquals(patient, hospital.OpenPatients()), "Bài 3: mở lại form đã đóng bằng instance mới");
                foreach (var child in hospital.MdiChildren) child.Close();
                Check(hospital.MdiChildren.Length == 0, "Bài 3: đóng tất cả form con"); hospital.Close();
            }
            using (var revenue = new frmRevenueAnalytics())
            {
                ShowForm(revenue); int ticks = 0;
                using (var timer = new Timer { Interval = 50 })
                {
                    timer.Tick += (s, e) => ticks++; timer.Start();
                    Find<Button>(revenue, "btnAnalyze").PerformClick();
                    var task = revenue.CurrentTask; await Task.Delay(1200);
                    Find<TextBox>(revenue, "txtNote").Text = "Vẫn nhập được khi thống kê đang chạy";
                    CaptureForm(revenue, "bai4_async.png"); CaptureForm(revenue, "bai3_async.png");
                    Check(ticks >= 5 && !task.IsCompleted, "Bài 4: UI tiếp tục xử lý sự kiện trong lúc chạy nền");
                    var progress = Find<ProgressBar>(revenue, "pgbLoading");
                    Check(progress.Value > 0 && progress.Value < 100, "Bài 4: tiến độ tăng khi công việc đang chạy");
                    Check(!Find<Button>(revenue, "btnAnalyze").Enabled && Find<Button>(revenue, "btnCancel").Enabled, "Bài 4: khóa nút chạy trùng và bật nút Hủy");
                    Find<Button>(revenue, "btnCancel").PerformClick(); await task;
                    Check(revenue.ProcessingStatus == "Đã hủy thao tác!", "Bài 4: hủy tác vụ an toàn qua nút Hủy");
                    Check(progress.Value == 0 && Find<Button>(revenue, "btnAnalyze").Enabled && !Find<Button>(revenue, "btnCancel").Enabled,
                        "Bài 4: sau hủy dừng tiến độ, khôi phục trạng thái nút");
                    CaptureForm(revenue, "bai4_cancel.png");
                    Find<DateTimePicker>(revenue, "dtpFromDate").Value = DateTime.Today;
                    Find<DateTimePicker>(revenue, "dtpToDate").Value = DateTime.Today;
                    Find<ComboBox>(revenue, "cboBranch").SelectedItem = "Đà Nẵng";
                    ticks = 0; var elapsed = Stopwatch.StartNew();
                    Find<Button>(revenue, "btnAnalyze").PerformClick(); await revenue.CurrentTask; elapsed.Stop();
                    Check(elapsed.Elapsed.TotalSeconds >= 4 && elapsed.Elapsed.TotalSeconds <= 6,
                        "Bài 4: thời gian thực tế trong 4–6 giây (" + elapsed.Elapsed.TotalSeconds.ToString("F2") + " giây)");
                    Check(ticks >= 40, "Bài 4: UI xử lý liên tục sự kiện Timer trong toàn bộ lượt thống kê");
                    Check(revenue.ResultCount == 30 && revenue.ProcessingStatus.StartsWith("Hoàn thành"), "Bài 4: chạy lại sau hủy, trả về 30 giao dịch");
                    var revenueGrid = Find<DataGridView>(revenue, "dgvRevenueData");
                    Check(revenueGrid.Rows.Cast<DataGridViewRow>().All(row => (string)row.Cells["Branch"].Value == "Đà Nẵng" && (DateTime)row.Cells["Date"].Value == DateTime.Today),
                        "Bài 4: kết quả đúng khoảng ngày và chi nhánh đã chọn");
                    Check(progress.Value == 100 && Find<Button>(revenue, "btnAnalyze").Enabled && !Find<Button>(revenue, "btnCancel").Enabled,
                        "Bài 4: hoàn thành đạt 100% và khôi phục trạng thái nút");
                    CaptureForm(revenue, "bai4_main.png");
                    Find<DateTimePicker>(revenue, "dtpFromDate").Value = DateTime.Today.AddDays(1); await revenue.AnalyzeAsync();
                    Check(revenue.ProcessingStatus.StartsWith("Từ ngày"), "Bài 4: chặn khoảng ngày đảo ngược"); timer.Stop();
                }
                revenue.Close();
            }
            using (var ticker = new frmCryptoStockTicker())
            {
                ShowForm(ticker); ticker.StartMonitoring(); var firstTask = ticker.CurrentTask; ticker.StartMonitoring();
                Check(ReferenceEquals(firstTask, ticker.CurrentTask), "Bài 5: chống khởi chạy nhiều vòng lặp");
                await Task.Delay(4300); Check(ticker.History.Count == 2, "Bài 5: cập nhật mỗi 2 giây, không có lỗi cross-thread");
                CaptureForm(ticker, "bai5_main.png"); ticker.PauseMonitoring(); await ticker.CurrentTask; int n = ticker.History.Count;
                await Task.Delay(300); Check(ticker.History.Count == n, "Bài 5: tạm dừng không phát sinh cập nhật");
                ticker.StartMonitoring(); await Task.Delay(2150); ticker.PauseMonitoring(); await ticker.CurrentTask;
                Check(ticker.History.Count == n + 1, "Bài 5: tiếp tục và giữ lịch sử");
                ticker.StartMonitoring(); var closingTask = ticker.CurrentTask; ticker.Close(); await closingTask;
                Check(closingTask.IsCompleted, "Bài 5: đóng form dừng tác vụ nền");
            }
            using (var closingRevenue = new frmRevenueAnalytics())
            {
                ShowForm(closingRevenue); var task = closingRevenue.AnalyzeAsync(); closingRevenue.Close(); await task;
                Check(task.IsCompleted, "Bài 4: đóng form hủy tác vụ nền");
            }
        }
    }
}
