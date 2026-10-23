using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WinFormsApp.Models;

namespace WinFormsApp.Forms
{
    public sealed class frmGymMemberManager : Form
    {
        internal readonly BindingList<MemberModel> Members = new BindingList<MemberModel>();
        private readonly BindingSource source = new BindingSource();
        private readonly ErrorProvider errors;
        private readonly TextBox txtMemberId = Ui.Text("txtMemberId", true), txtFullName = Ui.Text("txtFullName"), txtPhone = Ui.Text("txtPhone"), txtFee = Ui.Text("txtFee");
        private readonly ComboBox cboPackage = Ui.Combo("cboPackage", "Theo tháng", "6 tháng", "1 năm");
        private readonly DateTimePicker dtpStartDate = Ui.Date("dtpStartDate");
        private readonly DataGridView dgvMembers = Ui.Grid("dgvMembers");
        private readonly Label status = new Label { Dock = DockStyle.Fill, Name = "lblStatus" };
        private int nextId = 3;
        private bool adding;

        public frmGymMemberManager()
        {
            Ui.Setup(this, "Bài 1 - Quản lý hội viên Gym");
            errors = new ErrorProvider { ContainerControl = this, BlinkStyle = ErrorBlinkStyle.NeverBlink };
            var p = Ui.Page(this, 48, 135, 48, 30, -1);
            p.Controls.Add(Ui.Heading("QUẢN LÝ HỘI VIÊN GYM"), 0, 0);
            p.Controls.Add(Ui.Fields("Mã hội viên", txtMemberId, "Họ và tên", txtFullName, "Số điện thoại", txtPhone, "Gói tập", cboPackage, "Ngày đăng ký", dtpStartDate, "Phí dịch vụ", txtFee), 0, 1);
            p.Controls.Add(Ui.Buttons(Ui.Button("btnAdd", "Thêm mới", (s, e) => AddMember()), Ui.Button("btnEdit", "Sửa thông tin", (s, e) => EditMember()),
                Ui.Button("btnDelete", "Xóa hội viên", (s, e) => DeleteMember()), Ui.Button("btnReset", "Làm mới", (s, e) => ResetEditor())), 0, 2);
            p.Controls.Add(status, 0, 3); p.Controls.Add(dgvMembers, 0, 4);
            Ui.Column(dgvMembers, "MemberId", "Mã HV"); Ui.Column(dgvMembers, "FullName", "Tên hội viên");
            Ui.Column(dgvMembers, "Phone", "SĐT"); Ui.Column(dgvMembers, "Package", "Gói tập");
            Ui.Column(dgvMembers, "StartDate", "Ngày ĐK", "dd/MM/yyyy"); Ui.Column(dgvMembers, "Fee", "Phí dịch vụ", "N0");
            Members.Add(new MemberModel { MemberId = "HV001", FullName = "Nguyễn Văn An", Phone = "0912345678", Package = "Theo tháng", Fee = 300000 });
            Members.Add(new MemberModel { MemberId = "HV002", FullName = "Trần Thị Bình", Phone = "0987654321", Package = "6 tháng", Fee = 1500000 });
            source.DataSource = Members; dgvMembers.DataSource = source;
            Bind(txtMemberId, "Text", "MemberId"); Bind(txtFullName, "Text", "FullName"); Bind(txtPhone, "Text", "Phone");
            Bind(cboPackage, "SelectedItem", "Package"); Bind(dtpStartDate, "Value", "StartDate"); Bind(txtFee, "Text", "Fee");
            source.CurrentChanged += (s, e) => { adding = false; errors.Clear(); };
            dgvMembers.CellClick += (s, e) => { if (e.RowIndex >= 0) { adding = false; ReadBindings(); } };
            Shown += (s, e) => ResetEditor();
        }
        private void Bind(Control control, string property, string member)
        {
            // Chỉ ghi vào model khi bấm Sửa và toàn bộ dữ liệu đã hợp lệ.
            control.DataBindings.Add(new Binding(property, source, member, true, DataSourceUpdateMode.Never));
        }
        private void ReadBindings()
        {
            foreach (var c in new Control[] { txtMemberId, txtFullName, txtPhone, cboPackage, dtpStartDate, txtFee })
                foreach (Binding b in c.DataBindings) b.ReadValue();
        }
        internal bool ValidateInput()
        {
            errors.Clear(); bool ok = true;
            if (!Regex.IsMatch(txtFullName.Text.Trim(), @"^[\p{L}\p{M}]+(?:\s+[\p{L}\p{M}]+)*$"))
            { errors.SetError(txtFullName, "Nhập họ tên, chỉ gồm chữ và khoảng trắng."); ok = false; }
            if (!Regex.IsMatch(txtPhone.Text.Trim(), @"^[0-9]{10}$"))
            { errors.SetError(txtPhone, "Số điện thoại phải đúng 10 chữ số."); ok = false; }
            decimal fee;
            if (!decimal.TryParse(txtFee.Text.Trim(), NumberStyles.None, CultureInfo.InvariantCulture, out fee) || fee <= 0)
            { errors.SetError(txtFee, "Phí phải là số nguyên dương, không nhập dấu phân cách."); ok = false; }
            status.Text = ok ? "Dữ liệu hợp lệ." : "Vui lòng sửa các ô có biểu tượng báo lỗi.";
            return ok;
        }
        internal void AddMember()
        {
            if (!ValidateInput()) return;
            var member = new MemberModel { MemberId = "HV" + nextId.ToString("D3"), FullName = txtFullName.Text.Trim(), Phone = txtPhone.Text.Trim(),
                Package = cboPackage.Text, StartDate = dtpStartDate.Value.Date, Fee = decimal.Parse(txtFee.Text, CultureInfo.InvariantCulture) };
            nextId++; Members.Add(member); source.Position = Members.Count - 1; adding = false; ReadBindings();
            status.Text = "Đã thêm " + member.MemberId + ". Tổng hội viên: " + Members.Count;
        }
        internal void EditMember()
        {
            if (adding || source.Current == null) { status.Text = "Chọn một hội viên trên lưới để sửa."; return; }
            if (!ValidateInput()) return;
            txtFullName.Text = txtFullName.Text.Trim(); txtPhone.Text = txtPhone.Text.Trim();
            var bindings = new Control[] { txtFullName, txtPhone, cboPackage, dtpStartDate, txtFee }
                .SelectMany(c => c.DataBindings.Cast<Binding>()).ToList();
            // PropertyChanged của ô đầu có thể khiến BindingSource đọc lại các ô sau.
            // Giữ nội dung đang sửa đến khi toàn bộ binding đã ghi xong.
            foreach (var b in bindings) b.ControlUpdateMode = ControlUpdateMode.Never;
            try { foreach (var b in bindings) b.WriteValue(); source.EndEdit(); }
            finally { foreach (var b in bindings) b.ControlUpdateMode = ControlUpdateMode.OnPropertyChanged; }
            ReadBindings(); status.Text = "Đã cập nhật hội viên, không nạp lại danh sách.";
        }
        private void DeleteMember()
        {
            if (adding || source.Current == null) { status.Text = "Chọn hội viên cần xóa."; return; }
            if (MessageBox.Show(this, "Xóa hội viên " + ((MemberModel)source.Current).FullName + "?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            source.RemoveCurrent(); ResetEditor(); status.Text = "Đã xóa hội viên. Tổng hội viên: " + Members.Count;
        }
        internal void ResetEditor()
        {
            adding = true; errors.Clear(); dgvMembers.ClearSelection(); txtMemberId.Text = "HV" + nextId.ToString("D3");
            txtFullName.Clear(); txtPhone.Clear(); txtFee.Clear(); cboPackage.SelectedIndex = 0; dtpStartDate.Value = DateTime.Today;
            status.Text = "Nhập thông tin để thêm mới, hoặc chọn một dòng để sửa/xóa."; txtFullName.Focus();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing) { errors.Dispose(); source.Dispose(); }
            base.Dispose(disposing);
        }
    }
}
