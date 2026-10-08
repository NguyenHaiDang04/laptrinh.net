using System;
using System.Windows.Forms;

namespace Bai1_TinhCuocDichVu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTinhTien_Click(object sender, EventArgs e)
        {
            double donGia, soLuong, giamGia;

            if (!double.TryParse(txtDonGia.Text, out donGia) ||
                !double.TryParse(txtSoLuong.Text, out soLuong) ||
                !double.TryParse(txtGiamGia.Text, out giamGia))
            {
                MessageBox.Show("Dữ liệu không hợp lệ! Vui lòng không để trống và chỉ nhập số.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Đã sửa lại lỗi rớt dòng và thêm dấu nhân (*)
            double tongTien = (donGia * soLuong) * ((100 - giamGia) / 100);

            lblTongTien.Text = "Tổng tiền thanh toán: " + tongTien.ToString("N0") + " VNĐ";
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtDonGia.Clear();
            txtSoLuong.Clear();
            txtGiamGia.Clear();
            lblTongTien.Text = "Tổng tiền thanh toán: 0 VNĐ";
            txtDonGia.Focus();
        }
    }
}