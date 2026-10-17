using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Bai4_9_QuanLyPhongKham
{
    // Lớp đối tượng lịch hẹn
    public class Appointment
    {
        public string Id { get; set; }
        public string PatientName { get; set; }
        public string Phone { get; set; }
        public DateTime ApptDate { get; set; }
        public string Doctor { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
    }

    public partial class Form1 : Form
    {
        private BindingList<Appointment> danhSachGoc = new BindingList<Appointment>();
        private BindingSource bs = new BindingSource();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cboDoctor.Items.AddRange(new string[] { "BS. Nguyễn Văn Châu", "BS. Trần Thị Thu", "BS. Lê Hoàng Phúc" });
            cboStatus.Items.AddRange(new string[] { "Chờ khám", "Đang khám", "Hoàn tất", "Đã hủy" });

            cboFilterStatus.Items.AddRange(new string[] { "Tất cả", "Chờ khám", "Đang khám", "Hoàn tất", "Đã hủy" });
            cboFilterStatus.SelectedIndex = 0;

            dgvAppointments.AutoGenerateColumns = false;
            dgvAppointments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAppointments.ReadOnly = true;
            dgvAppointments.AllowUserToAddRows = false;

            dgvAppointments.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "Mã LH", Width = 80 });
            dgvAppointments.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PatientName", HeaderText = "Tên Bệnh nhân", Width = 150 });
            dgvAppointments.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Phone", HeaderText = "Số ĐT", Width = 100 });
            dgvAppointments.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ApptDate", HeaderText = "Ngày hẹn", Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" } });
            dgvAppointments.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Doctor", HeaderText = "Bác sĩ", Width = 150 });
            dgvAppointments.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Reason", HeaderText = "Lý do khám", Width = 200 });
            dgvAppointments.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Status", HeaderText = "Trạng thái", Width = 100 });

            bs.DataSource = danhSachGoc;
            dgvAppointments.DataSource = bs;

            LamMoiO();
            CapNhatTrangThai();
        }

        private bool KiemTraHopLe(bool isUpdate = false)
        {
            errorProvider1.Clear();
            bool hopLe = true;

            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                errorProvider1.SetError(txtId, "Vui lòng nhập Mã lịch hẹn!");
                hopLe = false;
            }
            else if (!isUpdate && danhSachGoc.Any(a => a.Id == txtId.Text.Trim()))
            {
                errorProvider1.SetError(txtId, "Mã lịch hẹn đã tồn tại!");
                hopLe = false;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                errorProvider1.SetError(txtName, "Vui lòng nhập Tên bệnh nhân!");
                hopLe = false;
            }

            if (!Regex.IsMatch(txtPhone.Text.Trim(), @"^[0-9]{9,11}$"))
            {
                errorProvider1.SetError(txtPhone, "Số ĐT không hợp lệ (9-11 số)!");
                hopLe = false;
            }

            if (dtpDate.Value.Date < DateTime.Now.Date)
            {
                errorProvider1.SetError(dtpDate, "Ngày hẹn không được nằm trong quá khứ!");
                hopLe = false;
            }

            if (cboDoctor.SelectedIndex == -1)
            {
                errorProvider1.SetError(cboDoctor, "Vui lòng chọn bác sĩ!");
                hopLe = false;
            }

            if (string.IsNullOrWhiteSpace(txtReason.Text))
            {
                errorProvider1.SetError(txtReason, "Vui lòng nhập lý do khám!");
                hopLe = false;
            }

            if (cboStatus.SelectedIndex == -1)
            {
                errorProvider1.SetError(cboStatus, "Vui lòng chọn trạng thái!");
                hopLe = false;
            }

            return hopLe;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!KiemTraHopLe(isUpdate: false)) return;

            var newAppt = new Appointment
            {
                Id = txtId.Text.Trim(),
                PatientName = txtName.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                ApptDate = dtpDate.Value,
                Doctor = cboDoctor.Text,
                Reason = txtReason.Text.Trim(),
                Status = cboStatus.Text
            };

            danhSachGoc.Add(newAppt);
            ThucHienLocVaTimKiem();
            LamMoiO();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!KiemTraHopLe(isUpdate: true)) return;

            var appt = danhSachGoc.FirstOrDefault(a => a.Id == txtId.Text.Trim());
            if (appt != null)
            {
                appt.PatientName = txtName.Text.Trim();
                appt.Phone = txtPhone.Text.Trim();
                appt.ApptDate = dtpDate.Value;
                appt.Doctor = cboDoctor.Text;
                appt.Reason = txtReason.Text.Trim();
                appt.Status = cboStatus.Text;

                ThucHienLocVaTimKiem();
                LamMoiO();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var appt = danhSachGoc.FirstOrDefault(a => a.Id == txtId.Text.Trim());
            if (appt != null)
            {
                if (MessageBox.Show("Chắc chắn muốn xóa lịch hẹn này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    danhSachGoc.Remove(appt);
                    ThucHienLocVaTimKiem();
                    LamMoiO();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            LamMoiO();
        }

        private void dgvAppointments_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAppointments.CurrentRow != null && dgvAppointments.CurrentRow.DataBoundItem is Appointment appt)
            {
                txtId.Text = appt.Id;
                txtId.ReadOnly = true;
                txtName.Text = appt.PatientName;
                txtPhone.Text = appt.Phone;
                dtpDate.Value = appt.ApptDate;
                cboDoctor.Text = appt.Doctor;
                txtReason.Text = appt.Reason;
                cboStatus.Text = appt.Status;

                errorProvider1.Clear();
            }
        }

        private void ThucHienLocVaTimKiem()
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            string statusFilter = cboFilterStatus.Text;
            bool useDateFilter = chkFilterDate.Checked;
            DateTime filterDate = dtpFilterDate.Value.Date;

            var filteredList = danhSachGoc.Where(a =>
                (a.PatientName.ToLower().Contains(keyword) || a.Phone.Contains(keyword)) &&
                (statusFilter == "Tất cả" || a.Status == statusFilter) &&
                (!useDateFilter || a.ApptDate.Date == filterDate)
            ).ToList();

            bs.DataSource = filteredList;
            CapNhatTrangThai();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) => ThucHienLocVaTimKiem();
        private void cboFilterStatus_SelectedIndexChanged(object sender, EventArgs e) => ThucHienLocVaTimKiem();
        private void dtpFilterDate_ValueChanged(object sender, EventArgs e) => ThucHienLocVaTimKiem();
        private void chkFilterDate_CheckedChanged(object sender, EventArgs e) => ThucHienLocVaTimKiem();

        private void LamMoiO()
        {
            txtId.ReadOnly = false;
            txtId.Clear();
            txtName.Clear();
            txtPhone.Clear();
            txtReason.Clear();
            dtpDate.Value = DateTime.Now;
            cboDoctor.SelectedIndex = -1;
            cboStatus.SelectedIndex = 0;
            txtId.Focus();
            errorProvider1.Clear();
        }

        private void CapNhatTrangThai()
        {
            lblStatusCount.Text = $"Đang hiển thị: {dgvAppointments.RowCount} lịch hẹn";
        }

        private void mnuXuatCSV_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.RowCount == 0) return;

            SaveFileDialog sfd = new SaveFileDialog { Filter = "CSV Files|*.csv", FileName = "LichHen.csv" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
                {
                    sw.WriteLine("Mã LH,Bệnh nhân,Số ĐT,Ngày hẹn,Bác sĩ,Lý do,Trạng thái");
                    var hienThi = bs.DataSource as List<Appointment> ?? danhSachGoc.ToList();

                    foreach (var a in hienThi)
                    {
                        sw.WriteLine($"{a.Id},{a.PatientName},{a.Phone},{a.ApptDate:dd/MM/yyyy HH:mm},{a.Doctor},{a.Reason},{a.Status}");
                    }
                }
                MessageBox.Show("Xuất file thành công!");
            }
        }
    }
}