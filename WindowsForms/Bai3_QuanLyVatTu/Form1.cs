using System;
using System.Windows.Forms;

namespace Bai3_QuanLyVatTu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cboDVT.Items.Clear();
            cboDVT.Items.AddRange(new string[] { "Cái", "Bộ", "Kg", "Mét" });
            if (cboDVT.Items.Count > 0) cboDVT.SelectedIndex = 0;

            lvVatTu.View = View.Details;
            lvVatTu.FullRowSelect = true;
            lvVatTu.GridLines = true;
            lvVatTu.Columns.Clear();
            lvVatTu.Columns.Add("Mã VT", 100);
            lvVatTu.Columns.Add("Tên VT", 180);
            lvVatTu.Columns.Add("Đơn vị tính", 90);
            lvVatTu.Columns.Add("Đơn giá", 110);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maVT = txtMaVT.Text.Trim();
            string tenVT = txtTenVT.Text.Trim();
            string dvt = cboDVT.Text;
            string donGiaText = txtDonGia.Text.Trim();

            if (string.IsNullOrEmpty(maVT) || string.IsNullOrEmpty(tenVT) || string.IsNullOrEmpty(donGiaText))
            {
                MessageBox.Show("Vui lòng điền đầy đủ tất cả các trường thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(donGiaText, out double donGia) || donGia < 0)
            {
                MessageBox.Show("Đơn giá phải là số hợp lệ và lớn hơn hoặc bằng 0!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDonGia.Focus();
                return;
            }

            foreach (ListViewItem item in lvVatTu.Items)
            {
                if (item.SubItems[0].Text.Equals(maVT, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Mã vật tư đã tồn tại trên hệ thống! Vui lòng chọn mã khác.", "Trùng lặp dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMaVT.Focus();
                    return;
                }
            }

            ListViewItem lvi = new ListViewItem(maVT);
            lvi.SubItems.Add(tenVT);
            lvi.SubItems.Add(dvt);
            lvi.SubItems.Add(donGia.ToString("N0"));

            lvVatTu.Items.Add(lvi);
            LamMoiForm();
        }

        private void lvVatTu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvVatTu.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvVatTu.SelectedItems[0];
                txtMaVT.Text = selectedItem.SubItems[0].Text;
                txtTenVT.Text = selectedItem.SubItems[1].Text;
                cboDVT.Text = selectedItem.SubItems[2].Text;

                string donGiaStr = selectedItem.SubItems[3].Text.Replace(",", "").Replace(".", "");
                txtDonGia.Text = donGiaStr;

                txtMaVT.ReadOnly = true;
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (lvVatTu.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng vật tư trên danh sách để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string tenVT = txtTenVT.Text.Trim();
            string dvt = cboDVT.Text;
            string donGiaText = txtDonGia.Text.Trim();

            if (string.IsNullOrEmpty(tenVT) || string.IsNullOrEmpty(donGiaText))
            {
                MessageBox.Show("Tên vật tư và đơn giá không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(donGiaText, out double donGia) || donGia < 0)
            {
                MessageBox.Show("Đơn giá phải là số hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ListViewItem selectedItem = lvVatTu.SelectedItems[0];
            selectedItem.SubItems[1].Text = tenVT;
            selectedItem.SubItems[2].Text = dvt;
            selectedItem.SubItems[3].Text = donGia.ToString("N0");

            MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LamMoiForm();
        }

        private void btnXoaDong_Click(object sender, EventArgs e)
        {
            if (lvVatTu.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa trên bảng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa dòng vật tư đang chọn không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                lvVatTu.Items.Remove(lvVatTu.SelectedItems[0]);
                LamMoiForm();
            }
        }

        private void btnXoaToanBo_Click(object sender, EventArgs e)
        {
            if (lvVatTu.Items.Count == 0)
            {
                MessageBox.Show("Danh sách vật tư hiện đang trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có thực sự muốn xóa TOÀN BỘ danh sách vật tư không?", "Cảnh báo xóa tất cả", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                lvVatTu.Items.Clear();
                LamMoiForm();
            }
        }

        private void LamMoiForm()
        {
            txtMaVT.ReadOnly = false;
            txtMaVT.Clear();
            txtTenVT.Clear();
            txtDonGia.Clear();
            if (cboDVT.Items.Count > 0) cboDVT.SelectedIndex = 0;
            txtMaVT.Focus();
        }
    }
}