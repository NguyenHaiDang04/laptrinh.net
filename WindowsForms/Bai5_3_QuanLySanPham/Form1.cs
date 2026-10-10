using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Bai5_3_QuanLySanPham 
{
    // 1. Định nghĩa Lớp Product
    public class Product
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
    }

    public partial class Form1 : Form
    {
        // Khởi tạo danh sách gốc lưu trữ trong RAM
        private List<Product> danhSachSP = new List<Product>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cboDanhMuc.Items.AddRange(new string[] { "Điện thoại", "Laptop", "Phụ kiện", "Gia dụng" });
            cboDanhMuc.SelectedIndex = 0;

            // Cấu hình hiển thị đẹp cho DataGridView
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.ReadOnly = true;
        }

        // Hàm phụ: Reset DataSource để DataGridView cập nhật lại dữ liệu từ List
        private void CapNhatGridView(List<Product> list)
        {
            dgvProducts.DataSource = null; // Cắt kết nối cũ
            dgvProducts.DataSource = list; // Bơm dữ liệu mới

            // Đổi tên cột cho tiếng Việt dễ đọc
            if (dgvProducts.Columns.Count > 0)
            {
                dgvProducts.Columns["ProductId"].HeaderText = "Mã SP";
                dgvProducts.Columns["ProductName"].HeaderText = "Tên SP";
                dgvProducts.Columns["UnitPrice"].HeaderText = "Đơn giá";
                dgvProducts.Columns["Quantity"].HeaderText = "Số lượng";
                dgvProducts.Columns["Category"].HeaderText = "Danh mục";
            }
        }

        // 2. Chức năng: THÊM
        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSP.Text) || string.IsNullOrWhiteSpace(txtTenSP.Text))
            {
                MessageBox.Show("Vui lòng nhập ít nhất Mã và Tên sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (danhSachSP.Any(p => p.ProductId == txtMaSP.Text.Trim()))
            {
                MessageBox.Show("Mã sản phẩm đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaSP.Focus();
                return;
            }

            // Khởi tạo đối tượng Product từ giao diện
            Product spMoi = new Product
            {
                ProductId = txtMaSP.Text.Trim(),
                ProductName = txtTenSP.Text.Trim(),
                UnitPrice = double.TryParse(txtDonGia.Text, out double dg) ? dg : 0,
                Quantity = int.TryParse(txtSoLuong.Text, out int sl) ? sl : 0,
                Category = cboDanhMuc.Text
            };

            // Thêm vào List và làm mới lưới
            danhSachSP.Add(spMoi);
            CapNhatGridView(danhSachSP);
            LamMoiO();
        }

        // 3. Sự kiện: Click vào dòng đẩy dữ liệu lên
        private void DgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvProducts.Rows.Count)
            {
                txtMaSP.Text = dgvProducts.Rows[e.RowIndex].Cells["ProductId"].Value?.ToString();
                txtTenSP.Text = dgvProducts.Rows[e.RowIndex].Cells["ProductName"].Value?.ToString();
                txtDonGia.Text = dgvProducts.Rows[e.RowIndex].Cells["UnitPrice"].Value?.ToString();
                txtSoLuong.Text = dgvProducts.Rows[e.RowIndex].Cells["Quantity"].Value?.ToString();
                cboDanhMuc.Text = dgvProducts.Rows[e.RowIndex].Cells["Category"].Value?.ToString();

                txtMaSP.ReadOnly = true; // Khóa Mã SP không cho sửa
            }
        }

        // Chức năng: SỬA
        private void BtnSua_Click(object sender, EventArgs e)
        {
            // Tìm đối tượng Product trong List dựa vào Mã
            Product sp = danhSachSP.FirstOrDefault(p => p.ProductId == txtMaSP.Text.Trim());

            if (sp != null)
            {
                // Cập nhật thuộc tính
                sp.ProductName = txtTenSP.Text.Trim();
                sp.UnitPrice = double.TryParse(txtDonGia.Text, out double dg) ? dg : 0;
                sp.Quantity = int.TryParse(txtSoLuong.Text, out int sl) ? sl : 0;
                sp.Category = cboDanhMuc.Text;

                CapNhatGridView(danhSachSP);
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoiO();
            }
        }

        // 4. Chức năng: XÓA
        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSP.Text)) return;

            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                Product sp = danhSachSP.FirstOrDefault(p => p.ProductId == txtMaSP.Text.Trim());
                if (sp != null)
                {
                    danhSachSP.Remove(sp);
                    CapNhatGridView(danhSachSP);
                    LamMoiO();
                }
            }
        }

        // Chức năng: TÌM KIẾM
        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTenSP.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(tuKhoa))
            {
                // Nếu để trống ô Tên SP, hiển thị lại toàn bộ
                CapNhatGridView(danhSachSP);
            }
            else
            {
                // Lọc danh sách chứa từ khóa
                List<Product> ketQua = danhSachSP.Where(p => p.ProductName.ToLower().Contains(tuKhoa)).ToList();
                CapNhatGridView(ketQua);
            }
        }

        private void LamMoiO()
        {
            txtMaSP.ReadOnly = false;
            txtMaSP.Clear();
            txtTenSP.Clear();
            txtDonGia.Clear();
            txtSoLuong.Clear();
            txtMaSP.Focus();
        }
    }
}