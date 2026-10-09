using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bai5_QuanLyDonHang
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Cài đặt ComboBox Vận chuyển
            cboVanChuyen.Items.AddRange(new string[] { "Giao hàng tiêu chuẩn", "Giao hàng nhanh", "Hỏa tốc" });
            cboVanChuyen.SelectedIndex = 0;

            // Kích hoạt Timer để chạy đồng hồ realtime
            timer1.Start();

            // Cấu hình DataGridView
            dgvHangHoa.AllowUserToAddRows = false; // Tắt tự động thêm dòng để dùng phím F2
            dgvHangHoa.Columns["colThanhTien"].ReadOnly = true; // Cột Thành tiền chỉ đọc (tự tính)
        }

        // 1. Cập nhật thời gian thực trên StatusStrip
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        // 2. Tính toán Thành tiền khi đổi Số lượng hoặc Đơn giá
        private void dgvHangHoa_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvHangHoa.Rows[e.RowIndex];

                double soLuong = 0, donGia = 0;

                if (row.Cells["colSoLuong"].Value != null)
                    double.TryParse(row.Cells["colSoLuong"].Value.ToString(), out soLuong);

                if (row.Cells["colDonGia"].Value != null)
                    double.TryParse(row.Cells["colDonGia"].Value.ToString(), out donGia);

                // Tính và gán Thành tiền
                row.Cells["colThanhTien"].Value = (soLuong * donGia).ToString("N0");

                // Cập nhật lại tổng số dưới thanh StatusStrip
                CapNhatTong();
            }
        }

        // 3. Hàm tính toán và cập nhật Tổng số lượng, Tổng trọng lượng, Tổng tiền
        private void CapNhatTong()
        {
            double tongSoLuong = 0;
            double tongTrongLuong = 0;
            double tongTien = 0;

            foreach (DataGridViewRow row in dgvHangHoa.Rows)
            {
                if (row.Cells["colSoLuong"].Value != null)
                {
                    double.TryParse(row.Cells["colSoLuong"].Value.ToString(), out double sl);
                    tongSoLuong += sl;
                }

                if (row.Cells["colTrongLuong"].Value != null)
                {
                    double.TryParse(row.Cells["colTrongLuong"].Value.ToString(), out double tl);
                    tongTrongLuong += tl;
                }

                if (row.Cells["colThanhTien"].Value != null)
                {
                    // Loại bỏ dấu phẩy/chấm trước khi tính tổng tiền
                    string tienStr = row.Cells["colThanhTien"].Value.ToString().Replace(",", "").Replace(".", "");
                    double.TryParse(tienStr, out double tt);
                    tongTien += tt;
                }
            }

            lblTongSL.Text = "Tổng SL: " + tongSoLuong;
            lblTongTL.Text = "Tổng Trọng lượng: " + tongTrongLuong + " kg";
            lblTongTien.Text = "Tổng Tiền: " + tongTien.ToString("N0") + " VNĐ";
        }

        // 4. Bật biểu tượng lỗi đỏ (ErrorProvider/ErrorText) nếu <= 0
        private void dgvHangHoa_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string colName = dgvHangHoa.Columns[e.ColumnIndex].Name;

            if (colName == "colSoLuong" || colName == "colTrongLuong")
            {
                if (double.TryParse(e.FormattedValue.ToString(), out double val))
                {
                    if (val <= 0)
                    {
                        dgvHangHoa.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Giá trị phải lớn hơn 0!";
                    }
                    else
                    {
                        dgvHangHoa.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = ""; // Xóa lỗi đỏ
                    }
                }
            }
        }

        // 5. Phím tắt: F2 thêm dòng, Delete xóa dòng
        private void dgvHangHoa_KeyDown(object sender, KeyEventArgs e)
        {
            // F2: Thêm dòng mới
            if (e.KeyCode == Keys.F2)
            {
                dgvHangHoa.Rows.Add();
                e.Handled = true;
            }

            // Delete: Xóa dòng đang chọn
            if (e.KeyCode == Keys.Delete)
            {
                if (dgvHangHoa.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgvHangHoa.SelectedRows)
                    {
                        if (!row.IsNewRow) dgvHangHoa.Rows.Remove(row);
                    }
                    CapNhatTong();
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}