using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bai4_10_Quanlymuontra
{
    // Lớp đối tượng Phiếu mượn
    public class CheckoutRecord
    {
        public string Id { get; set; }
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string Equipment { get; set; }
        public int Quantity { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; } // Cho phép null nếu chưa trả
        public string Status { get; set; }
    }

    public partial class Form1 : Form
    {
        private BindingList<CheckoutRecord> danhSachGoc = new BindingList<CheckoutRecord>();
        private BindingSource bs = new BindingSource();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Khởi tạo ComboBox
            cboEquipment.Items.AddRange(new string[] { "Oscilloscope", "Máy hàn thiếc", "Multimeter", "Bo mạch Arduino", "Raspberry Pi", "Bộ nguồn DC" });
            cboStatus.Items.AddRange(new string[] { "Đang mượn", "Đã trả", "Quá hạn" });
            cboFilterStatus.Items.AddRange(new string[] { "Tất cả", "Đang mượn", "Đã trả", "Quá hạn" });
            cboFilterStatus.SelectedIndex = 0;

            // Cấu hình DataGridView
            dgvRecords.AutoGenerateColumns = false;
            dgvRecords.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRecords.ReadOnly = true;
            dgvRecords.AllowUserToAddRows = false;

            dgvRecords.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "Mã Phiếu", Width = 80 });
            dgvRecords.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StudentId", HeaderText = "Mã SV", Width = 100 });
            dgvRecords.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StudentName", HeaderText = "Tên SV", Width = 150 });
            dgvRecords.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Equipment", HeaderText = "Thiết bị", Width = 150 });
            dgvRecords.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "SL", Width = 50 });
            dgvRecords.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "BorrowDate", HeaderText = "Ngày mượn", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } });
            dgvRecords.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DueDate", HeaderText = "Hạn trả", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } });
            dgvRecords.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ReturnDate", HeaderText = "Ngày trả", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } });
            dgvRecords.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Status", HeaderText = "Trạng thái", Width = 100 });

            bs.DataSource = danhSachGoc;
            dgvRecords.DataSource = bs;

            LamMoiO();
            CapNhatTrangThai();
        }

        // Tự động kiểm tra và đánh dấu các phiếu đã quá hạn
        private void KiemTraQuaHan()
        {
            foreach (var r in danhSachGoc)
            {
                if (r.Status == "Đang mượn" && r.DueDate.Date < DateTime.Now.Date)
                {
                    r.Status = "Quá hạn";
                }
            }
            dgvRecords.Refresh();
        }

        private bool KiemTraHopLe(bool isUpdate = false)
        {
            errorProvider1.Clear();
            bool hopLe = true;

            if (string.IsNullOrWhiteSpace(txtId.Text)) { errorProvider1.SetError(txtId, "Nhập Mã phiếu!"); hopLe = false; }
            else if (!isUpdate && danhSachGoc.Any(x => x.Id == txtId.Text.Trim())) { errorProvider1.SetError(txtId, "Mã phiếu trùng!"); hopLe = false; }

            if (string.IsNullOrWhiteSpace(txtStudentId.Text)) { errorProvider1.SetError(txtStudentId, "Nhập Mã SV!"); hopLe = false; }
            if (string.IsNullOrWhiteSpace(txtStudentName.Text)) { errorProvider1.SetError(txtStudentName, "Nhập Tên SV!"); hopLe = false; }
            if (cboEquipment.SelectedIndex == -1 && string.IsNullOrWhiteSpace(cboEquipment.Text)) { errorProvider1.SetError(cboEquipment, "Chọn thiết bị!"); hopLe = false; }
            if (numQuantity.Value <= 0) { errorProvider1.SetError(numQuantity, "Số lượng > 0!"); hopLe = false; }

            if (dtpDueDate.Value.Date < dtpBorrowDate.Value.Date)
            {
                errorProvider1.SetError(dtpDueDate, "Hạn trả không được trước ngày mượn!");
                hopLe = false;
            }

            if (dtpReturnDate.Enabled && dtpReturnDate.Value.Date < dtpBorrowDate.Value.Date)
            {
                errorProvider1.SetError(dtpReturnDate, "Ngày trả không được trước ngày mượn!");
                hopLe = false;
            }

            return hopLe;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!KiemTraHopLe(isUpdate: false)) return;

            var record = new CheckoutRecord
            {
                Id = txtId.Text.Trim(),
                StudentId = txtStudentId.Text.Trim(),
                StudentName = txtStudentName.Text.Trim(),
                Equipment = cboEquipment.Text,
                Quantity = (int)numQuantity.Value,
                BorrowDate = dtpBorrowDate.Value,
                DueDate = dtpDueDate.Value,
                Status = cboStatus.Text,
                ReturnDate = cboStatus.Text == "Đã trả" ? (DateTime?)dtpReturnDate.Value : null
            };

            danhSachGoc.Add(record);
            ThucHienLocVaTimKiem();
            LamMoiO();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!KiemTraHopLe(isUpdate: true)) return;

            var r = danhSachGoc.FirstOrDefault(x => x.Id == txtId.Text.Trim());
            if (r != null)
            {
                r.StudentId = txtStudentId.Text.Trim();
                r.StudentName = txtStudentName.Text.Trim();
                r.Equipment = cboEquipment.Text;
                r.Quantity = (int)numQuantity.Value;
                r.BorrowDate = dtpBorrowDate.Value;
                r.DueDate = dtpDueDate.Value;
                r.Status = cboStatus.Text;
                r.ReturnDate = cboStatus.Text == "Đã trả" ? (DateTime?)dtpReturnDate.Value : null;

                ThucHienLocVaTimKiem();
                LamMoiO();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var r = danhSachGoc.FirstOrDefault(x => x.Id == txtId.Text.Trim());
            if (r != null && MessageBox.Show("Xóa phiếu này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                danhSachGoc.Remove(r);
                ThucHienLocVaTimKiem();
                LamMoiO();
            }
        }

        private void btnMarkReturned_Click(object sender, EventArgs e)
        {
            var r = danhSachGoc.FirstOrDefault(x => x.Id == txtId.Text.Trim());
            if (r != null && r.Status != "Đã trả")
            {
                r.Status = "Đã trả";
                r.ReturnDate = DateTime.Now;
                ThucHienLocVaTimKiem();
                LamMoiO();
                MessageBox.Show("Đã cập nhật trạng thái Đã trả thành công!");
            }
        }

        private void btnClear_Click(object sender, EventArgs e) => LamMoiO();

        private void dgvRecords_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRecords.CurrentRow != null && dgvRecords.CurrentRow.DataBoundItem is CheckoutRecord r)
            {
                txtId.Text = r.Id; txtId.ReadOnly = true;
                txtStudentId.Text = r.StudentId;
                txtStudentName.Text = r.StudentName;
                cboEquipment.Text = r.Equipment;
                numQuantity.Value = r.Quantity;
                dtpBorrowDate.Value = r.BorrowDate;
                dtpDueDate.Value = r.DueDate;
                cboStatus.Text = r.Status;

                if (r.ReturnDate.HasValue)
                {
                    dtpReturnDate.Value = r.ReturnDate.Value;
                    dtpReturnDate.Enabled = true;
                }
                else
                {
                    dtpReturnDate.Value = DateTime.Now;
                    dtpReturnDate.Enabled = false;
                }
                errorProvider1.Clear();
            }
        }

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtpReturnDate.Enabled = (cboStatus.Text == "Đã trả");
        }

        private void ThucHienLocVaTimKiem()
        {
            KiemTraQuaHan();

            string kw = txtSearch.Text.Trim().ToLower();
            string st = cboFilterStatus.Text;

            var filtered = danhSachGoc.Where(r =>
                (r.StudentId.ToLower().Contains(kw) || r.Equipment.ToLower().Contains(kw)) &&
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
            txtId.Clear(); txtStudentId.Clear(); txtStudentName.Clear(); txtSearch.Clear();
            cboEquipment.SelectedIndex = -1;
            numQuantity.Value = 1;
            dtpBorrowDate.Value = DateTime.Now;
            dtpDueDate.Value = DateTime.Now.AddDays(7);
            dtpReturnDate.Value = DateTime.Now;
            cboStatus.Text = "Đang mượn";
            dtpReturnDate.Enabled = false;
            txtId.Focus();
            errorProvider1.Clear();
        }

        private void CapNhatTrangThai()
        {
            int total = dgvRecords.RowCount;
            int overdue = danhSachGoc.Count(x => x.Status == "Quá hạn");
            lblStatusCount.Text = $"Tổng số phiếu hiển thị: {total} | Số phiếu quá hạn toàn hệ thống: {overdue}";
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgvRecords.RowCount == 0) return;
            SaveFileDialog sfd = new SaveFileDialog { Filter = "CSV|*.csv", FileName = "MuonTraThietBi.csv" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
                {
                    sw.WriteLine("Mã Phiếu,Mã SV,Tên SV,Thiết bị,SL,Ngày mượn,Hạn trả,Ngày trả,Trạng thái");
                    var data = bs.DataSource as List<CheckoutRecord> ?? danhSachGoc.ToList();
                    foreach (var r in data)
                    {
                        string retDate = r.ReturnDate.HasValue ? r.ReturnDate.Value.ToString("dd/MM/yyyy") : "";
                        sw.WriteLine($"{r.Id},{r.StudentId},{r.StudentName},{r.Equipment},{r.Quantity},{r.BorrowDate:dd/MM/yyyy},{r.DueDate:dd/MM/yyyy},{retDate},{r.Status}");
                    }
                }
                MessageBox.Show("Xuất file thành công!");
            }
        }
    }
}