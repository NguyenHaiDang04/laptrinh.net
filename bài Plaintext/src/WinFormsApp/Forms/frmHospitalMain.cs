using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp.Models;

namespace WinFormsApp.Forms
{
    internal sealed class HospitalData
    {
        public BindingList<PatientModel> Patients { get; } = new BindingList<PatientModel>();
        public BindingList<DoctorModel> Doctors { get; } = new BindingList<DoctorModel>();
        public BindingList<DepartmentModel> Departments { get; } = new BindingList<DepartmentModel>();
        public HospitalData() { Reload(); }
        public void Reload()
        {
            Patients.Clear(); Doctors.Clear(); Departments.Clear();
            Patients.Add(new PatientModel { PatientId = "BN001", FullName = "Nguyễn Văn Nam", BirthYear = 1998, Diagnosis = "Khám sức khỏe" });
            Patients.Add(new PatientModel { PatientId = "BN002", FullName = "Trần Thị Mai", BirthYear = 2002, Diagnosis = "Viêm họng" });
            Doctors.Add(new DoctorModel { DoctorId = "BS001", FullName = "Nguyễn Minh An", Department = "Nội tổng hợp", Shift = "07:00 - 15:00" });
            Doctors.Add(new DoctorModel { DoctorId = "BS002", FullName = "Lê Thanh Hà", Department = "Tai Mũi Họng", Shift = "15:00 - 23:00" });
            Departments.Add(new DepartmentModel { DepartmentId = "K001", Name = "Nội tổng hợp", Room = "P101" });
            Departments.Add(new DepartmentModel { DepartmentId = "K002", Name = "Tai Mũi Họng", Room = "P102" });
        }
    }
    public sealed class frmHospitalMain : Form
    {
        private readonly HospitalData data = new HospitalData();
        private readonly ToolStripProgressBar progress = new ToolStripProgressBar { Minimum = 0, Maximum = 100, Width = 150 };
        private readonly ToolStripStatusLabel status = new ToolStripStatusLabel("Bác sĩ trực: Nguyễn Minh An (dữ liệu mẫu)");
        private bool reloading;
        public frmHospitalMain()
        {
            Ui.Setup(this, "Bài 3 - Hệ thống MDI quản lý bệnh viện"); IsMdiContainer = true;
            var menu = new MenuStrip(); var system = new ToolStripMenuItem("Hệ thống"); var manage = new ToolStripMenuItem("Quản lý"); var windows = new ToolStripMenuItem("Cửa sổ");
            system.DropDownItems.Add("Đăng xuất", null, (s, e) => Close());
            system.DropDownItems.Add("Nạp lại dữ liệu", null, async (s, e) => await ReloadDataAsync());
            system.DropDownItems.Add("Thoát", null, (s, e) => Close());
            manage.DropDownItems.Add("Hồ sơ Bệnh nhân", null, (s, e) => OpenPatients());
            manage.DropDownItems.Add("Danh mục Bác sĩ / Lịch trực", null, (s, e) => OpenDoctors());
            manage.DropDownItems.Add("Danh mục Khoa/Phòng", null, (s, e) => OpenDepartments());
            windows.DropDownItems.Add("Xếp tầng (Cascade)", null, (s, e) => LayoutMdi(MdiLayout.Cascade));
            windows.DropDownItems.Add("Ngang (Tile Horizontal)", null, (s, e) => LayoutMdi(MdiLayout.TileHorizontal));
            windows.DropDownItems.Add("Dọc (Tile Vertical)", null, (s, e) => LayoutMdi(MdiLayout.TileVertical));
            windows.DropDownItems.Add("Đóng tất cả", null, (s, e) => { foreach (Form child in MdiChildren) child.Close(); });
            menu.Items.AddRange(new ToolStripItem[] { system, manage, windows }); menu.MdiWindowListItem = windows; MainMenuStrip = menu; Controls.Add(menu);
            var strip = new StatusStrip(); strip.Items.Add(status); strip.Items.Add(new ToolStripStatusLabel { Spring = true }); strip.Items.Add(progress); Controls.Add(strip);
        }
        internal Form OpenPatients() => OpenChild(() => new frmPatientProfiles(data.Patients));
        internal Form OpenDoctors() => OpenChild(() => new frmDoctorSchedule(data.Doctors));
        internal Form OpenDepartments() => OpenChild(() => new frmDepartments(data.Departments));
        private T OpenChild<T>(Func<T> create) where T : Form
        {
            foreach (var child in MdiChildren)
                if (child is T existing)
                {
                    if (existing.WindowState == FormWindowState.Minimized) existing.WindowState = FormWindowState.Normal;
                    existing.Activate(); return existing;
                }
            var form = create(); form.MdiParent = this; form.Show(); return form;
        }
        private async Task ReloadDataAsync()
        {
            if (reloading) return;
            if (MessageBox.Show(this, "Khôi phục dữ liệu mẫu? Thay đổi hiện tại sẽ bị thay thế.", "Nạp lại dữ liệu", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            reloading = true; progress.Style = ProgressBarStyle.Marquee;
            try { await Task.Delay(500); if (!IsDisposed) { data.Reload(); status.Text = "Bác sĩ trực: Nguyễn Minh An | Đã nạp lại dữ liệu mẫu"; } }
            finally { if (!IsDisposed) { progress.Style = ProgressBarStyle.Blocks; progress.Value = 100; } reloading = false; }
        }
    }

    // Các danh mục dùng chung thao tác CRUD cơ bản; form cụ thể có tên đúng theo đề.
    public class HospitalListForm<T> : Form where T : class, new()
    {
        private readonly BindingList<T> records;
        private readonly BindingSource source = new BindingSource();
        private readonly string[] properties;
        private readonly TextBox[] fields;
        private readonly DataGridView grid = Ui.Grid("dgvRecords");
        private readonly Label status = new Label { Dock = DockStyle.Fill };
        private readonly ErrorProvider errors;
        private bool adding;
        internal HospitalListForm(string title, BindingList<T> records, params string[] columns)
        {
            this.records = records; Ui.Setup(this, title); MinimumSize = new System.Drawing.Size(650, 420); Size = new System.Drawing.Size(820, 500);
            properties = Enumerable.Range(0, columns.Length / 2).Select(i => columns[i * 2]).ToArray();
            fields = properties.Select(p => Ui.Text("txt" + p)).ToArray(); errors = new ErrorProvider { ContainerControl = this, BlinkStyle = ErrorBlinkStyle.NeverBlink };
            var page = Ui.Page(this, 42, 92, 46, 25, -1); page.Controls.Add(Ui.Heading(title), 0, 0);
            var pairs = new object[columns.Length];
            for (int i = 0; i < fields.Length; i++) { pairs[i * 2] = columns[i * 2 + 1]; pairs[i * 2 + 1] = fields[i]; Ui.Column(grid, properties[i], columns[i * 2 + 1]); }
            page.Controls.Add(Ui.Fields(pairs), 0, 1);
            page.Controls.Add(Ui.Buttons(Ui.Button("btnAdd", "Thêm", (s, e) => Save(false)), Ui.Button("btnEdit", "Sửa", (s, e) => Save(true)), Ui.Button("btnDelete", "Xóa", (s, e) => Delete()), Ui.Button("btnReset", "Làm mới", (s, e) => ResetEditor())), 0, 2);
            page.Controls.Add(status, 0, 3); page.Controls.Add(grid, 0, 4); source.DataSource = records; grid.DataSource = source;
            source.CurrentChanged += (s, e) => Fill(); grid.CellClick += (s, e) => { if (e.RowIndex >= 0) Fill(); }; Shown += (s, e) => Fill();
        }
        private void Fill()
        {
            var item = source.Current as T; if (item == null) { ResetEditor(); return; }
            adding = false; errors.Clear();
            for (int i = 0; i < fields.Length; i++) fields[i].Text = Convert.ToString(typeof(T).GetProperty(properties[i]).GetValue(item));
            status.Text = "Số bản ghi: " + records.Count;
        }
        private void ResetEditor() { adding = true; errors.Clear(); foreach (var box in fields) box.Clear(); grid.ClearSelection(); status.Text = "Nhập thông tin mới."; }
        private void Save(bool edit)
        {
            if (edit && (adding || source.Current == null)) { status.Text = "Chọn bản ghi cần sửa."; return; }
            errors.Clear(); bool valid = true; var values = new object[fields.Length]; var current = source.Current as T;
            for (int i = 0; i < fields.Length; i++)
            {
                values[i] = fields[i].Text.Trim();
                if (string.IsNullOrWhiteSpace(fields[i].Text)) { errors.SetError(fields[i], "Không được để trống."); valid = false; }
                if (typeof(T).GetProperty(properties[i]).PropertyType == typeof(int))
                {
                    int year;
                    if (!int.TryParse(fields[i].Text, out year) || year < 1900 || year > DateTime.Today.Year) { errors.SetError(fields[i], "Năm sinh từ 1900 đến năm hiện tại."); valid = false; }
                    values[i] = year;
                }
            }
            if (records.Any(x => (!edit || x != current) && string.Equals(Convert.ToString(typeof(T).GetProperty(properties[0]).GetValue(x)), fields[0].Text.Trim(), StringComparison.OrdinalIgnoreCase)))
            { errors.SetError(fields[0], "Mã đã tồn tại."); valid = false; }
            if (!valid) return;
            var item = edit ? current : new T(); for (int i = 0; i < properties.Length; i++) typeof(T).GetProperty(properties[i]).SetValue(item, values[i]);
            if (!edit) records.Add(item); else records.ResetItem(records.IndexOf(item)); source.Position = records.IndexOf(item); Fill(); status.Text = "Đã lưu. Số bản ghi: " + records.Count;
        }
        private void Delete()
        {
            if (adding || source.Current == null) return;
            if (MessageBox.Show(this, "Xóa bản ghi đang chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) { source.RemoveCurrent(); Fill(); }
        }
        protected override void Dispose(bool disposing) { if (disposing) { source.Dispose(); errors.Dispose(); } base.Dispose(disposing); }
    }
    public sealed class frmPatientProfiles : HospitalListForm<PatientModel>
    {
        internal frmPatientProfiles(BindingList<PatientModel> data) : base("Hồ sơ bệnh nhân", data, "PatientId", "Mã bệnh nhân", "FullName", "Họ và tên", "BirthYear", "Năm sinh", "Diagnosis", "Chẩn đoán") { }
    }
    public sealed class frmDoctorSchedule : HospitalListForm<DoctorModel>
    {
        internal frmDoctorSchedule(BindingList<DoctorModel> data) : base("Danh mục bác sĩ và lịch trực", data, "DoctorId", "Mã bác sĩ", "FullName", "Họ và tên", "Department", "Khoa", "Shift", "Ca trực") { }
    }
    public sealed class frmDepartments : HospitalListForm<DepartmentModel>
    {
        internal frmDepartments(BindingList<DepartmentModel> data) : base("Danh mục khoa / phòng", data, "DepartmentId", "Mã khoa", "Name", "Tên khoa", "Room", "Phòng") { }
    }
}
