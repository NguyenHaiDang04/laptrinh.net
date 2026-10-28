using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bai4_1_Login;
using Bai4_2_Registration;
using BaiTap4.Shared;

namespace Verification
{
    internal sealed class RecordedMessages : IUserMessages
    {
        public string Text { get; private set; } = "";
        public string Title { get; private set; } = "";
        public MessageBoxIcon Icon { get; private set; }
        public void Show(IWin32Window owner, string text, string title, MessageBoxIcon icon) { Text = text; Title = title; Icon = icon; }
    }
    internal sealed class LoginProbe : FrmLogin
    {
        public LoginProbe(DatabaseOptions options, IUserMessages messages) : base(options, messages) { }
        public bool PressKey(Keys key) => base.ProcessDialogKey(key);
    }
    internal sealed class VerificationForm : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr handle, StringBuilder text, int maxCount);
        private readonly string output;
        private readonly List<string> checks = new List<string>();
        private readonly DatabaseOptions options = DatabaseOptions.Default();
        public VerificationForm(string output)
        {
            this.output = Path.GetFullPath(output); Opacity = 0; ShowInTaskbar = false;
            Shown += async (s, e) =>
            {
                Directory.CreateDirectory(this.output);
                checks.Add("BÀI 4.1 + 4.2 - KIỂM TRA CSDL THẬT VÀ WINFORMS");
                checks.Add("Thời gian (Asia/Bangkok): " + TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "SE Asia Standard Time").ToString("yyyy-MM-dd HH:mm:ss"));
                checks.Add("SQL Server: " + options.Server + " | Database: " + options.DatabaseName);
                try { await RunAsync(); checks.Add("RESULT: PASS"); }
                catch (Exception ex) { checks.Add("RESULT: FAIL\n" + ex); Environment.ExitCode = 1; }
                finally { File.WriteAllLines(Path.Combine(this.output, "verification.txt"), checks, new UTF8Encoding(true)); Close(); }
            };
        }
        private void Check(bool value, string message) { if (!value) throw new InvalidOperationException(message); checks.Add("PASS: " + message); }
        private T Find<T>(Control root, string name) where T : Control => (T)root.Controls.Find(name, true).Single();
        private void ShowForm(Form form) { form.StartPosition = FormStartPosition.Manual; form.Location = new Point(-18000, -18000); form.ShowInTaskbar = false; form.Show(); }
        private void Snapshot(Form form, string file)
        {
            form.Refresh();
            using (var bitmap = new Bitmap(form.Width, form.Height)) { form.DrawToBitmap(bitmap, new Rectangle(Point.Empty, form.Size)); bitmap.Save(Path.Combine(output, file), ImageFormat.Png); }
        }
        private object Scalar(string sql)
        {
            using (var connection = new SqlConnection(options.ConnectionString()))
            using (var command = new SqlCommand(sql, connection)) { connection.Open(); return command.ExecuteScalar(); }
        }
        private async Task RunAsync()
        {
            Check(options.DatabaseName.StartsWith("LTNET_Bai4_Verification_", StringComparison.Ordinal), "Chỉ kiểm tra trên CSDL riêng, không sử dụng CSDL bài làm");
            var initializer = new DatabaseInitializer(options); await initializer.EnsureCreatedAsync(); await initializer.EnsureCreatedAsync();
            Check(Convert.ToInt32(Scalar("SELECT COUNT(*) FROM dbo.AppUsers WHERE Username=N'admin'")) == 1, "Khởi tạo lặp không nhân đôi tài khoản mẫu");
            Check(Convert.ToInt32(Scalar("SELECT COUNT(*) FROM dbo.Courses")) == 3, "CSDL có đủ 3 khóa học mẫu, không nhân đôi khi khởi tạo lại");
            Check(Convert.ToInt32(Scalar("SELECT COUNT(*) FROM dbo.AppUsers WHERE DATALENGTH(PasswordHash)=32 AND DATALENGTH(PasswordSalt)=16 AND PasswordIterations>=100000")) == 1,
                "Mật khẩu lưu hash PBKDF2-SHA256 và salt, không có cột mật khẩu dạng rõ");
            var saltA = PasswordHasher.NewSalt(); var saltB = PasswordHasher.NewSalt();
            Check(!PasswordHasher.Hash("Same@123", saltA, PasswordHasher.Iterations).SequenceEqual(PasswordHasher.Hash("Same@123", saltB, PasswordHasher.Iterations)), "Cùng mật khẩu có salt khác sẽ tạo hash khác");
            var loginMessages = new RecordedMessages();
            using (var login = new LoginProbe(options, loginMessages))
            {
                ShowForm(login); await login.ReadyTask;
                var username = Find<TextBox>(login, "txtUsername"); var password = Find<TextBox>(login, "txtPassword"); var show = Find<CheckBox>(login, "chkShowPassword");
                var button = Find<Button>(login, "btnLogin");
                Check(button.Enabled, "Bài 4.1: kết nối CSDL thành công, bật nút Đăng nhập");
                Check(login.AcceptButton == button && login.CancelButton == Find<Button>(login, "btnExit"), "Bài 4.1: gán đúng AcceptButton và CancelButton");
                Check(password.UseSystemPasswordChar, "Bài 4.1: mặc định ẩn mật khẩu");
                show.Checked = true; Check(!password.UseSystemPasswordChar, "Bài 4.1: tick hiện mật khẩu");
                show.Checked = false; Check(password.UseSystemPasswordChar, "Bài 4.1: bỏ tick ẩn lại mật khẩu");
                username.Text = "  "; password.Text = " "; login.PressKey(Keys.Enter); await login.CurrentTask;
                var errors = (ErrorProvider)typeof(FrmLogin).GetField("errorProvider", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(login);
                Check(errors.GetError(username) != "" && errors.GetError(password) != "", "Bài 4.1: Enter gọi đăng nhập, ErrorProvider chặn cả hai ô trống");
                Check(Convert.ToInt32(Scalar("SELECT COUNT(*) FROM dbo.LoginAttempts")) == 0, "Bài 4.1: dữ liệu trống không gửi truy vấn xác thực");
                Snapshot(login, "bai4_1_validation.png");
                username.Text = "admin"; password.Text = "SaiMatKhau"; button.PerformClick(); await login.CurrentTask;
                Check(loginMessages.Icon == MessageBoxIcon.Warning, "Bài 4.1: sai mật khẩu bị từ chối");
                username.Text = "' OR 1=1--"; password.Text = "Admin@123"; button.PerformClick(); await login.CurrentTask;
                Check(loginMessages.Icon == MessageBoxIcon.Warning, "Bài 4.1: chuỗi SQL injection không vượt qua đăng nhập");
                username.Text = "admin"; password.Text = "Admin@123"; login.PressKey(Keys.Enter); await login.CurrentTask;
                Check(loginMessages.Icon == MessageBoxIcon.Information && loginMessages.Text.Contains("thành công"), "Bài 4.1: Enter xác thực đúng tài khoản trong SQL Server");
                Check(password.Text == "" && button.Enabled, "Bài 4.1: xóa mật khẩu sau xác thực và khôi phục nút");
                Check(Convert.ToInt32(Scalar("SELECT COUNT(*) FROM dbo.LoginAttempts WHERE Succeeded=1")) == 1 && Convert.ToInt32(Scalar("SELECT COUNT(*) FROM dbo.LoginAttempts WHERE Succeeded=0")) == 2,
                    "Bài 4.1: ghi nhật ký cả đăng nhập đúng và sai");
                Check(Scalar("SELECT LastLoginAt FROM dbo.AppUsers WHERE Username=N'admin'") != DBNull.Value, "Bài 4.1: lưu thời điểm đăng nhập cuối");
                Snapshot(login, "bai4_1_login.png");
                username.Clear(); password.Clear(); login.PressKey(Keys.Escape);
                Check(login.IsDisposed || !login.Visible, "Bài 4.1: Esc thoát được ngay cả khi các ô trống");
            }
            var service = new RegistrationService(options); var messages = new RecordedMessages();
            using (var form = new FrmRegistration(options, messages))
            {
                ShowForm(form); await form.ReadyTask;
                var name = Find<TextBox>(form, "txtFullName"); var phone = Find<MaskedTextBox>(form, "mtxtPhone"); var birth = Find<DateTimePicker>(form, "dtpBirthDate"); var course = Find<ComboBox>(form, "cboCourse"); var register = Find<Button>(form, "btnRegister");
                Check(register.Enabled, "Bài 4.2: khởi tạo CSDL và bật nút Đăng ký");
                Check(phone.Mask == "(000) 000-0000", "Bài 4.2: Mask điện thoại đúng (000) 000-0000");
                Check(birth.Format == DateTimePickerFormat.Short && System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern == "dd/MM/yyyy", "Bài 4.2: dùng Format Short, văn hóa hiển thị ngày dd/MM/yyyy");
                Check(course.DisplayMember == "CourseName" && course.ValueMember == "CourseId" && course.Items.Count == 3 && course.SelectedValue is int,
                    "Bài 4.2: ComboBox binding khóa học từ CSDL với DisplayMember và ValueMember");
                name.Text = " "; phone.Text = "123"; register.PerformClick(); await form.CurrentTask;
                Check(Convert.ToInt32(Scalar("SELECT COUNT(*) FROM dbo.Registrations")) == 0, "Bài 4.2: tên trống và điện thoại chưa đủ không được lưu");
                name.Text = "Nguyễn Hải Đăng"; phone.Text = "0912345678"; birth.Value = new DateTime(2004, 9, 21);
                var displayedDate = new StringBuilder(100); GetWindowText(birth.Handle, displayedDate, displayedDate.Capacity);
                Check(displayedDate.ToString() == "21/09/2004", "Bài 4.2: control Windows thực sự hiển thị dd/MM/yyyy kể cả khi vùng máy là en-US");
                Find<RadioButton>(form, "rdoOther").Checked = true; Find<CheckBox>(form, "chkReceiveNews").Checked = true;
                Check(!Find<RadioButton>(form, "rdoMale").Checked, "Bài 4.2: RadioButton chỉ cho chọn một giới tính");
                register.PerformClick(); await form.CurrentTask;
                Check(messages.Icon == MessageBoxIcon.Information && messages.Title == "Đăng ký thành công", "Bài 4.2: lưu thành công mới gửi MessageBox");
                Check(messages.Text.Contains("Nguyễn Hải Đăng") && messages.Text.Contains("0912345678") && messages.Text.Contains("21/09/2004") && messages.Text.Contains("Khác") && messages.Text.Contains("Có") && messages.Text.Contains(course.Text),
                    "Bài 4.2: MessageBox tổng hợp đầy đủ tên, SĐT, ngày sinh, giới tính, khóa học, CheckBox");
                File.WriteAllText(Path.Combine(output, "registration-message.txt"), messages.Text, new UTF8Encoding(true));
                var rows = await service.GetRegistrationsAsync();
                Check(rows.Count == 1 && rows[0].FullName == "Nguyễn Hải Đăng" && rows[0].Phone == "0912345678" && rows[0].BirthDate == new DateTime(2004, 9, 21) && rows[0].Gender == "Khác" && rows[0].ReceiveNews,
                    "Bài 4.2: đọc lại từ SQL Server khớp toàn bộ dữ liệu đã nhập");
                Check(Find<DataGridView>(form, "dgvRegistrations").Rows.Count == 1, "Bài 4.2: danh sách cập nhật sau khi lưu");
                Snapshot(form, "bai4_2_registration.png");
                register.PerformClick(); await form.CurrentTask;
                Check(messages.Icon == MessageBoxIcon.Warning && Convert.ToInt32(Scalar("SELECT COUNT(*) FROM dbo.Registrations")) == 1, "Bài 4.2: chặn đăng ký trùng SĐT và khóa học");
                course.SelectedIndex = 1; Find<CheckBox>(form, "chkReceiveNews").Checked = false; Find<RadioButton>(form, "rdoFemale").Checked = true;
                register.PerformClick(); await form.CurrentTask;
                Check(Convert.ToInt32(Scalar("SELECT COUNT(*) FROM dbo.Registrations")) == 2, "Bài 4.2: cho phép cùng SĐT đăng ký khóa học khác");
                rows = await service.GetRegistrationsAsync(); Check(!rows[0].ReceiveNews && rows[0].Gender == "Nữ", "Bài 4.2: lưu được cả trạng thái bỏ chọn CheckBox và RadioButton khác");
                Snapshot(form, "bai4_2_saved.png"); form.Close();
            }
            using (var reopened = new FrmRegistration(options, new RecordedMessages()))
            {
                ShowForm(reopened); await reopened.ReadyTask;
                Check(Find<DataGridView>(reopened, "dgvRegistrations").Rows.Count == 2, "Bài 4.2: đóng rồi mở form vẫn đọc được dữ liệu đã lưu"); reopened.Close();
            }
            bool invalidRejected = false;
            try { await service.RegisterAsync(new RegistrationInput { FullName = "Test", Phone = "0901234567", BirthDate = DateTime.Today.AddDays(1), Gender = "Nam", CourseId = 1 }); }
            catch (ArgumentException) { invalidRejected = true; }
            Check(invalidRejected, "Bài 4.2: tầng dữ liệu cũng chặn ngày sinh tương lai");
            int probeExit = await Task.Run(() =>
            {
                using (var process = Process.Start(new ProcessStartInfo(Application.ExecutablePath, "--probe") { UseShellExecute = false, CreateNoWindow = true, WindowStyle = ProcessWindowStyle.Hidden }))
                { if (!process.WaitForExit(30000)) throw new TimeoutException("Database probe timed out"); return process.ExitCode; }
            });
            Check(probeExit == 0, "CSDL lưu bền: một tiến trình mới độc lập đọc lại được ít nhất 2 đăng ký");
            var table = new StringBuilder("Table,Rows\n");
            foreach (var name in new[] { "AppUsers", "LoginAttempts", "Courses", "Registrations" }) table.AppendLine(name + "," + Scalar("SELECT COUNT(*) FROM dbo." + name));
            File.WriteAllText(Path.Combine(output, "database-proof.csv"), table.ToString(), new UTF8Encoding(true));
        }
    }
}
