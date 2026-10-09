using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bai4_DatBan // Đổi tên namespace này nếu Project của bạn tên khác
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Thiết lập ComboBox Khung giờ
            cboKhungGio.Items.Add("Sáng (100.000đ)");
            cboKhungGio.Items.Add("Tối (150.000đ)");
            cboKhungGio.SelectedIndex = 0;

            // Tự động sinh 20 Button (Ma trận 4x5)
            for (int i = 1; i <= 20; i++)
            {
                Button btn = new Button();
                btn.Text = "Bàn " + i;
                btn.Width = 80;
                btn.Height = 60;
                btn.BackColor = Color.LightGray; // Mặc định Trống (Xám nhạt)
                btn.Cursor = Cursors.Hand;

                // Gán chung 1 sự kiện Click cho cả 20 nút
                btn.Click += BtnBan_Click;

                flpSoDo.Controls.Add(btn);
            }

            TinhTien();
        }

        // Sự kiện Click dùng chung cho 20 nút
        private void BtnBan_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            // Nếu màu Đỏ -> Đã có người đặt -> Bỏ qua
            if (btn.BackColor == Color.Red)
            {
                MessageBox.Show("Bàn này đã có người đặt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Đổi trạng thái: Trống <-> Đang chọn
            if (btn.BackColor == Color.LightGray)
                btn.BackColor = Color.Green;
            else if (btn.BackColor == Color.Green)
                btn.BackColor = Color.LightGray;

            TinhTien();
        }

        // Hàm tính toán số lượng và tiền
        private void TinhTien()
        {
            int soLuong = 0;
            // Đếm số bàn đang có màu Xanh (Đang chọn)
            foreach (Control ctrl in flpSoDo.Controls)
            {
                if (ctrl is Button && ctrl.BackColor == Color.Green)
                {
                    soLuong++;
                }
            }

            int donGia = (cboKhungGio.SelectedIndex == 0) ? 100000 : 150000;
            long tongTien = (long)soLuong * donGia;

            lblSoLuong.Text = "Số vị trí đang chọn: " + soLuong;
            lblTamTinh.Text = "Tạm tính tiền: " + tongTien.ToString("N0") + " VNĐ";
        }

        private void cboKhungGio_SelectedIndexChanged(object sender, EventArgs e)
        {
            TinhTien();
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            bool hasSelection = false;
            foreach (Control ctrl in flpSoDo.Controls)
            {
                if (ctrl is Button && ctrl.BackColor == Color.Green)
                {
                    ctrl.BackColor = Color.Red; // Chuyển sang Đỏ (Đã đặt)
                    hasSelection = true;
                }
            }

            if (hasSelection)
            {
                MessageBox.Show("Xác nhận đặt bàn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TinhTien();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ít nhất 1 bàn trống để đặt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in flpSoDo.Controls)
            {
                // Chỉ hủy các bàn đang chọn (Màu xanh), bỏ qua các bàn đã đặt (Màu đỏ)
                if (ctrl is Button && ctrl.BackColor == Color.Green)
                {
                    ctrl.BackColor = Color.LightGray;
                }
            }
            TinhTien();
        }
    }
}