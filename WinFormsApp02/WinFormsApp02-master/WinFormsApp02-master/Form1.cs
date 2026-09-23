using System;
using System.Windows.Forms;

namespace WinFormsApp02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Ensure Accept/Cancel buttons are set at runtime if not configured in designer
            this.AcceptButton = btnLogin;
            this.CancelButton = btnExit;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Hide password characters on load
            txtPassword.UseSystemPasswordChar = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Placeholder for any logic when username text changes
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Clear previous errors
            errorProvider.Clear();
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                errorProvider.SetError(txtUsername, "Vui lòng nhập tên đăng nhập!");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                errorProvider.SetError(txtPassword, "Vui lòng nhập mật khẩu!");
                isValid = false;
            }

            if (!isValid)
                return;

            // Simple authentication example
            if (txtUsername.Text == "admin" && txtPassword.Text == "123456")
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
