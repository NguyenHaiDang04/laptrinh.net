using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bai2_QuanLySuCoIT // Tên phải khớp với tên Project bạn vừa tạo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Sự kiện: Nút Tải ảnh lỗi
        private void BtnTaiAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            // Lọc chỉ cho phép chọn file ảnh
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png";
            ofd.Title = "Chọn ảnh chụp lỗi";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picAnhLoi.Image = Image.FromFile(ofd.FileName);
            }
        }

        // Sự kiện: Nút Gửi yêu cầu
        private void BtnGuiYeuCau_Click(object sender, EventArgs e)
        {
            // Bắt lỗi: Không cho để trống 2 ô đầu
            if (string.IsNullOrWhiteSpace(txtMaPhieu.Text) || string.IsNullOrWhiteSpace(txtNguoiYeuCau.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã phiếu và Người yêu cầu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy mức độ ưu tiên
            string uuTien = "Thấp";
            if (radTrungBinh.Checked) uuTien = "Trung bình";
            if (radKhanCap.Checked) uuTien = "Khẩn cấp";

            // Lấy loại sự cố
            string loaiSuCo = cboLoaiSuCo.Text;
            if (string.IsNullOrEmpty(loaiSuCo)) loaiSuCo = "Chưa chọn";

            // Lấy các thiết bị bị ảnh hưởng (Nối chuỗi)
            string thietBi = "";
            if (chkMayTinhBan.Checked) thietBi += "- Máy tính bàn\n";
            if (chkLaptop.Checked) thietBi += "- Laptop\n";
            if (chkMayIn.Checked) thietBi += "- Máy in\n";
            if (chkDienThoai.Checked) thietBi += "- Điện thoại\n";

            if (thietBi == "") thietBi = "- Không có thiết bị nào\n";

            // Gom lại thành 1 đoạn văn bản thông báo
            string thongBao = "THÔNG TIN TIẾP NHẬN SỰ CỐ\n";
            thongBao += "--------------------------------------\n";
            thongBao += "Mã phiếu: " + txtMaPhieu.Text + "\n";
            thongBao += "Người yêu cầu: " + txtNguoiYeuCau.Text + "\n";
            thongBao += "Ngày ghi nhận: " + dtpNgayGhiNhan.Value.ToString("dd/MM/yyyy") + "\n";
            thongBao += "Mức độ ưu tiên: " + uuTien + "\n";
            thongBao += "Loại sự cố: " + loaiSuCo + "\n";
            thongBao += "Thiết bị ảnh hưởng:\n" + thietBi;

            // Hiển thị ra MessageBox
            MessageBox.Show(thongBao, "Xác nhận gửi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Sự kiện: Nút Nhập lại (Clear dữ liệu)
        private void btnNhapLai_Click(object sender, EventArgs e)
        {
            txtMaPhieu.Clear();
            txtNguoiYeuCau.Clear();
            dtpNgayGhiNhan.Value = DateTime.Now; // Reset về ngày hiện tại

            radThap.Checked = true; // Đưa về mặc định là Thấp

            cboLoaiSuCo.SelectedIndex = -1; // Xóa lựa chọn ComboBox

            chkMayTinhBan.Checked = false;
            chkLaptop.Checked = false;
            chkMayIn.Checked = false;
            chkDienThoai.Checked = false;

            picAnhLoi.Image = null; // Xóa ảnh

            txtMaPhieu.Focus(); // Đưa trỏ chuột về ô đầu tiên
        }
    }
}