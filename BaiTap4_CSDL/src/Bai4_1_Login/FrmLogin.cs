using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using BaiTap4.Shared;

namespace Bai4_1_Login
{
    public partial class FrmLogin : Form
    {
        private readonly DatabaseInitializer initializer;
        private readonly AuthService auth;
        private readonly IUserMessages messages;
        private bool busy;
        public Task ReadyTask { get; private set; } = Task.CompletedTask;
        public Task CurrentTask { get; private set; } = Task.CompletedTask;

        public FrmLogin() : this(DatabaseOptions.Default(), new DialogMessages()) { }
        public FrmLogin(DatabaseOptions options, IUserMessages messages)
        {
            InitializeComponent();
            initializer = new DatabaseInitializer(options);
            auth = new AuthService(options);
            this.messages = messages;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode && LicenseManager.UsageMode != LicenseUsageMode.Designtime) ReadyTask = InitializeAsync();
        }
        private async Task InitializeAsync()
        {
            try
            {
                await initializer.EnsureCreatedAsync();
                if (IsDisposed) return;
                btnLogin.Enabled = true; lblStatus.Text = "Sẵn sàng. Enter để đăng nhập, Esc để thoát."; txtUsername.Focus();
            }
            catch (Exception ex)
            {
                if (IsDisposed) return;
                lblStatus.Text = "Chưa kết nối được CSDL. Xem hướng dẫn LocalDB trong README.";
                messages.Show(this, lblStatus.Text + "\n\n" + ex.Message, "Lỗi kết nối", MessageBoxIcon.Error);
            }
        }
        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }
        private void btnExit_Click(object sender, EventArgs e) { Close(); }
        private void btnLogin_Click(object sender, EventArgs e) { CurrentTask = LoginAsync(); }

        private bool ValidateInput()
        {
            errorProvider.Clear();
            if (string.IsNullOrWhiteSpace(txtUsername.Text)) errorProvider.SetError(txtUsername, "Tên đăng nhập không được để trống.");
            if (string.IsNullOrWhiteSpace(txtPassword.Text)) errorProvider.SetError(txtPassword, "Mật khẩu không được để trống.");
            if (errorProvider.GetError(txtUsername) != "") { txtUsername.Focus(); return false; }
            if (errorProvider.GetError(txtPassword) != "") { txtPassword.Focus(); return false; }
            return true;
        }
        private async Task LoginAsync()
        {
            if (busy) return;
            if (!ValidateInput()) { lblStatus.Text = "Vui lòng nhập đủ tên đăng nhập và mật khẩu."; return; }
            string username = txtUsername.Text.Trim(), password = txtPassword.Text;
            busy = true; btnLogin.Enabled = false; txtUsername.Enabled = false; txtPassword.Enabled = false;
            lblStatus.Text = "Đang kiểm tra tài khoản...";
            try
            {
                var result = await auth.LoginAsync(username, password);
                if (IsDisposed) return;
                txtPassword.Clear();
                if (result.Success)
                {
                    lblStatus.Text = "Đăng nhập thành công. Xin chào " + result.FullName + "!";
                    messages.Show(this, lblStatus.Text, "Đăng nhập", MessageBoxIcon.Information);
                }
                else
                {
                    lblStatus.Text = "Tên đăng nhập hoặc mật khẩu không đúng, hoặc tài khoản đã bị khóa.";
                    errorProvider.SetError(txtPassword, "Kiểm tra lại thông tin đăng nhập.");
                    messages.Show(this, lblStatus.Text, "Đăng nhập không thành công", MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                if (!IsDisposed) { lblStatus.Text = "Không thể đăng nhập do lỗi kết nối CSDL."; messages.Show(this, lblStatus.Text + "\n" + ex.Message, "Lỗi", MessageBoxIcon.Error); }
            }
            finally
            {
                busy = false;
                if (!IsDisposed) { btnLogin.Enabled = true; txtUsername.Enabled = true; txtPassword.Enabled = true; txtPassword.Focus(); }
            }
        }
    }
}
