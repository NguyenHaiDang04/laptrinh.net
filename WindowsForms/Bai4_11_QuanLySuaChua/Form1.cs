using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Bai4_11_QuanLySuaChua
{
    public class RepairOrder
    {
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string DeviceType { get; set; }
        public string ErrorDescription { get; set; }
        public DateTime ReceiveDate { get; set; }
        public decimal EstimatedCost { get; set; }
        public string Status { get; set; }
        public string ImagePath { get; set; }
    }

    public partial class Form1 : Form
    {
        private BindingList<RepairOrder> danhSachGoc = new BindingList<RepairOrder>();
        private BindingSource bs = new BindingSource();
        private string currentImagePath = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cboDeviceType.Items.AddRange(new string[] { "Điện thoại", "Laptop", "Máy tính bảng", "PC", "Đồng hồ thông minh", "Khác" });
            cboStatus.Items.AddRange(new string[] { "Mới tiếp nhận", "Đang sửa", "Chờ linh kiện", "Hoàn tất", "Đã hủy" });
            cboFilterStatus.Items.AddRange(new string[] { "Tất cả", "Mới tiếp nhận", "Đang sửa", "Chờ linh kiện", "Hoàn tất", "Đã hủy" });
            cboFilterStatus.SelectedIndex = 0;

            dgvOrders.AutoGenerateColumns = false;
            dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrders.ReadOnly = true;
            dgvOrders.AllowUserToAddRows = false;

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "Mã Đơn", Width = 80 });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CustomerName", HeaderText = "Khách hàng", Width = 150 });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Phone", HeaderText = "Số ĐT", Width = 100 });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DeviceType", HeaderText = "Loại TB", Width = 100 });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ErrorDescription", HeaderText = "Mô tả lỗi", Width = 200 });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ReceiveDate", HeaderText = "Ngày nhận", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "EstimatedCost", HeaderText = "Chi phí (VNĐ)", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Status", HeaderText = "Trạng thái", Width = 100 });

            bs.DataSource = danhSachGoc;
            dgvOrders.DataSource = bs;

            LamMoiO();
            CapNhatTrangThai();
        }

        private bool KiemTraHopLe(bool isUpdate = false)
        {
            errorProvider1.Clear();
            bool hopLe = true;

            if (string.IsNullOrWhiteSpace(txtId.Text)) { errorProvider1.SetError(txtId, "Vui lòng nhập Mã đơn!"); hopLe = false; }
            else if (!isUpdate && danhSachGoc.Any(x => x.Id == txtId.Text.Trim())) { errorProvider1.SetError(txtId, "Mã đơn đã tồn tại!"); hopLe = false; }

            if (string.IsNullOrWhiteSpace(txtName.Text)) { errorProvider1.SetError(txtName, "Tên khách hàng không để trống!"); hopLe = false; }
            if (string.IsNullOrWhiteSpace(txtPhone.Text)) { errorProvider1.SetError(txtPhone, "Số ĐT không để trống!"); hopLe = false; }
            if (string.IsNullOrWhiteSpace(cboDeviceType.Text)) { errorProvider1.SetError(cboDeviceType, "Chọn Loại thiết bị!"); hopLe = false; }
            if (string.IsNullOrWhiteSpace(txtError.Text)) { errorProvider1.SetError(txtError, "Bắt buộc nhập Mô tả lỗi!"); hopLe = false; }
            if (cboStatus.SelectedIndex == -1) { errorProvider1.SetError(cboStatus, "Chọn Trạng thái!"); hopLe = false; }

            // Bắt lỗi chi phí âm
            if (numCost.Value < 0)
            {
                errorProvider1.SetError(numCost, "LỖI: Chi phí không được nhập số âm!");
                hopLe = false;
            }

            // Bắt lỗi chưa tải ảnh
            if (string.IsNullOrWhiteSpace(currentImagePath))
            {
                errorProvider1.SetError(btnChooseImage, "LỖI: Bắt buộc phải tải ảnh thiết bị!");
                hopLe = false;
            }

            return hopLe;
        }

        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog { Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp" };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                currentImagePath = ofd.FileName;
                picDevice.ImageLocation = currentImagePath;
                errorProvider1.SetError(btnChooseImage, "");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!KiemTraHopLe(isUpdate: false)) return;

            var order = new RepairOrder
            {
                Id = txtId.Text.Trim(),
                CustomerName = txtName.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                DeviceType = cboDeviceType.Text,
                ErrorDescription = txtError.Text.Trim(),
                ReceiveDate = dtpReceiveDate.Value,
                EstimatedCost = numCost.Value,
                Status = cboStatus.Text,
                ImagePath = currentImagePath
            };

            danhSachGoc.Add(order);
            ThucHienLocVaTimKiem();
            LamMoiO();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!KiemTraHopLe(isUpdate: true)) return;

            var r = danhSachGoc.FirstOrDefault(x => x.Id == txtId.Text.Trim());
            if (r != null)
            {
                r.CustomerName = txtName.Text.Trim();
                r.Phone = txtPhone.Text.Trim();
                r.DeviceType = cboDeviceType.Text;
                r.ErrorDescription = txtError.Text.Trim();
                r.ReceiveDate = dtpReceiveDate.Value;
                r.EstimatedCost = numCost.Value;
                r.Status = cboStatus.Text;
                r.ImagePath = currentImagePath;

                ThucHienLocVaTimKiem();
                LamMoiO();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var r = danhSachGoc.FirstOrDefault(x => x.Id == txtId.Text.Trim());
            if (r != null && MessageBox.Show("Xóa đơn này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                danhSachGoc.Remove(r);
                ThucHienLocVaTimKiem();
                LamMoiO();
            }
        }

        private void btnClear_Click(object sender, EventArgs e) => LamMoiO();

        private void dgvOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOrders.CurrentRow != null && dgvOrders.CurrentRow.DataBoundItem is RepairOrder r)
            {
                txtId.Text = r.Id; txtId.ReadOnly = true;
                txtName.Text = r.CustomerName;
                txtPhone.Text = r.Phone;
                cboDeviceType.Text = r.DeviceType;
                txtError.Text = r.ErrorDescription;
                dtpReceiveDate.Value = r.ReceiveDate;
                numCost.Value = r.EstimatedCost;
                cboStatus.Text = r.Status;
                currentImagePath = r.ImagePath;

                if (!string.IsNullOrEmpty(currentImagePath) && File.Exists(currentImagePath))
                    picDevice.ImageLocation = currentImagePath;
                else
                    picDevice.Image = null;

                errorProvider1.Clear();
            }
        }

        private void ThucHienLocVaTimKiem()
        {
            string kw = txtSearch.Text.Trim().ToLower();
            string st = cboFilterStatus.Text;

            var filtered = danhSachGoc.Where(r =>
                (r.Id.ToLower().Contains(kw) || r.CustomerName.ToLower().Contains(kw) || r.Phone.Contains(kw)) &&
                (st == "Tất cả" || r.Status == st)
            ).ToList();

            bs.DataSource = filtered;
            CapNhatTrangThai();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) => ThucHienLocVaTimKiem();
        private void cboFilterStatus_SelectedIndexChanged(object sender, EventArgs e) => ThucHienLocVaTimKiem();

        private void LamMoiO()
        {
            txtId.ReadOnly = false;
            txtId.Clear(); txtName.Clear(); txtPhone.Clear(); txtError.Clear();
            cboDeviceType.SelectedIndex = -1;
            numCost.Value = 0;
            dtpReceiveDate.Value = DateTime.Now;
            cboStatus.Text = "Mới tiếp nhận";
            picDevice.Image = null;
            picDevice.ImageLocation = null;
            currentImagePath = "";
            txtId.Focus();
            errorProvider1.Clear();
        }

        private void CapNhatTrangThai()
        {
            int totalFiltered = dgvOrders.RowCount;
            decimal totalPendingCost = danhSachGoc
                .Where(x => x.Status != "Hoàn tất" && x.Status != "Đã hủy")
                .Sum(x => x.EstimatedCost);

            lblStatus.Text = $"Đang hiển thị: {totalFiltered} đơn | Tổng doanh thu dự kiến: {totalPendingCost:N0} VNĐ";
        }

        private void mnuExportCSV_Click(object sender, EventArgs e)
        {
            if (dgvOrders.RowCount == 0) return;
            SaveFileDialog sfd = new SaveFileDialog { Filter = "CSV|*.csv", FileName = "DonSuaChua.csv" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
                {
                    sw.WriteLine("Mã Đơn,Khách hàng,Số ĐT,Loại TB,Mô tả lỗi,Ngày nhận,Chi phí,Trạng thái");
                    var data = bs.DataSource as List<RepairOrder> ?? danhSachGoc.ToList();
                    foreach (var r in data)
                    {
                        string safeError = r.ErrorDescription.Replace(",", " ");
                        sw.WriteLine($"{r.Id},{r.CustomerName},{r.Phone},{r.DeviceType},{safeError},{r.ReceiveDate:dd/MM/yyyy},{r.EstimatedCost},{r.Status}");
                    }
                }
                MessageBox.Show("Xuất file CSV thành công!");
            }
        }

        private void mnuExit_Click(object sender, EventArgs e) => Application.Exit();
    }
}