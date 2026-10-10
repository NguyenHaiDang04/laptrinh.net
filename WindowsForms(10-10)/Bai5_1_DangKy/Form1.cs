using System;
using System.Windows.Forms;

namespace Bai5_1_DangKy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            // Xóa tất cả các thông báo lỗi cũ trước khi kiểm tra lại
            epCheck.Clear();
            bool hopLe = true;

            // 1. Kiểm tra Tên đăng nhập
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text))
            {
                epCheck.SetError(txtTenDangNhap, "Tên đăng nhập không được để trống!");
                hopLe = false;
            }

            // 2. Kiểm tra Mật khẩu
            if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                epCheck.SetError(txtMatKhau, "Mật khẩu không được để trống!");
                hopLe = false;
            }

            // 3. Kiểm tra Xác nhận mật khẩu
            if (string.IsNullOrWhiteSpace(txtXacNhanMK.Text) || txtMatKhau.Text != txtXacNhanMK.Text)
            {
                epCheck.SetError(txtXacNhanMK, "Mật khẩu nhập lại không khớp!");
                hopLe = false;
            }

            // 4. Kiểm tra Độ tuổi (Phải >= 18 tuổi tính đến ngày hiện tại)
            if (dtpNgaySinh.Value.AddYears(18) > DateTime.Now)
            {
                epCheck.SetError(dtpNgaySinh, "Bạn phải đủ 18 tuổi trở lên để đăng ký!");
                hopLe = false;
            }

            // 5. Kiểm tra Điều khoản dịch vụ
            if (!chkDieuKhoan.Checked)
            {
                epCheck.SetError(chkDieuKhoan, "Bạn phải đồng ý với điều khoản dịch vụ!");
                hopLe = false;
            }

            // Nếu mọi thứ đều hợp lệ (hopLe == true)
            if (hopLe)
            {
                MessageBox.Show("Chúc mừng! Đăng ký tài khoản thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            txtXacNhanMK.Clear();
            dtpNgaySinh.Value = DateTime.Now;
            radNam.Checked = true;
            chkDieuKhoan.Checked = false;

            epCheck.Clear(); // Xóa sạch báo lỗi đỏ
            txtTenDangNhap.Focus();
        }
    }
}