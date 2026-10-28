using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using BaiTap4.Shared;

namespace Bai4_2_Registration
{
    public partial class FrmRegistration : Form
    {
        private readonly DatabaseInitializer initializer;
        private readonly RegistrationService service;
        private readonly IUserMessages messages;
        private bool busy;
        public Task ReadyTask { get; private set; } = Task.CompletedTask;
        public Task CurrentTask { get; private set; } = Task.CompletedTask;

        public FrmRegistration() : this(DatabaseOptions.Default(), new DialogMessages()) { }
        public FrmRegistration(DatabaseOptions options, IUserMessages messages)
        {
            InitializeComponent();
            initializer = new DatabaseInitializer(options); service = new RegistrationService(options); this.messages = messages;
            dtpBirthDate.MaxDate = DateTime.Today; dtpBirthDate.Value = DateTime.Today.AddYears(-18);
            dtpBirthDate.HandleCreated += (sender, args) => ApplyShortDateFormat();
        }
        // Format.Short mặc định phụ thuộc vùng Windows. Giữ thuộc tính Short theo đề,
        // đồng thời đặt chuỗi hiển thị trên control Win32 để luôn đúng dd/MM/yyyy.
        // https://learn.microsoft.com/en-us/windows/win32/controls/dtm-setformat
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr handle, int message, IntPtr wParam, string lParam);
        private void ApplyShortDateFormat()
        {
            if (dtpBirthDate.IsHandleCreated) SendMessage(dtpBirthDate.Handle, 0x1032, IntPtr.Zero, "dd/MM/yyyy");
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ApplyShortDateFormat();
            if (!DesignMode && LicenseManager.UsageMode != LicenseUsageMode.Designtime) ReadyTask = InitializeAsync();
        }
        private async Task InitializeAsync()
        {
            try
            {
                await initializer.EnsureCreatedAsync(); await ReloadAsync();
                if (!IsDisposed) { btnRegister.Enabled = true; btnRefresh.Enabled = true; lblStatus.Text = "Sẵn sàng đăng ký. Dữ liệu được lưu trong CSDL."; }
            }
            catch (Exception ex)
            {
                if (!IsDisposed) { lblStatus.Text = "Chưa kết nối được CSDL. Xem hướng dẫn LocalDB trong README."; messages.Show(this, lblStatus.Text + "\n\n" + ex.Message, "Lỗi kết nối", MessageBoxIcon.Error); }
            }
        }
        private async Task ReloadAsync()
        {
            var courses = await service.GetCoursesAsync(); var records = await service.GetRegistrationsAsync();
            if (IsDisposed) return;
            object selected = cboCourse.SelectedValue;
            cboCourse.DisplayMember = "CourseName"; cboCourse.ValueMember = "CourseId"; cboCourse.DataSource = courses;
            if (selected != null) cboCourse.SelectedValue = selected;
            if (cboCourse.SelectedIndex < 0 && courses.Count > 0) cboCourse.SelectedIndex = 0;
            dgvRegistrations.DataSource = records;
            lblCount.Text = "Đã lưu: " + records.Count + " đăng ký";
        }
        private void btnRegister_Click(object sender, EventArgs e) { CurrentTask = RegisterAsync(); }
        private void btnRefresh_Click(object sender, EventArgs e) { CurrentTask = RefreshAsync(); }
        private void btnExit_Click(object sender, EventArgs e) { Close(); }
        private async Task RefreshAsync()
        {
            if (busy) return;
            SetBusy(true);
            try { await ReloadAsync(); if (!IsDisposed) lblStatus.Text = "Đã tải lại dữ liệu từ CSDL."; }
            catch (Exception ex) { if (!IsDisposed) messages.Show(this, "Không tải được dữ liệu.\n" + ex.Message, "Lỗi", MessageBoxIcon.Error); }
            finally { SetBusy(false); }
        }
        private bool ValidateInput()
        {
            errorProvider.Clear(); bool valid = true;
            if (string.IsNullOrWhiteSpace(txtFullName.Text)) { errorProvider.SetError(txtFullName, "Nhập họ và tên."); valid = false; }
            if (!mtxtPhone.MaskCompleted) { errorProvider.SetError(mtxtPhone, "Nhập đủ 10 chữ số theo mẫu (000) 000-0000."); valid = false; }
            if (dtpBirthDate.Value.Date > DateTime.Today) { errorProvider.SetError(dtpBirthDate, "Ngày sinh không được ở tương lai."); valid = false; }
            if (cboCourse.SelectedItem == null) { errorProvider.SetError(cboCourse, "Chọn khóa học."); valid = false; }
            if (!rdoMale.Checked && !rdoFemale.Checked && !rdoOther.Checked) { errorProvider.SetError(grpGender, "Chọn giới tính."); valid = false; }
            return valid;
        }
        private async Task RegisterAsync()
        {
            if (busy) return;
            if (!ValidateInput()) { lblStatus.Text = "Vui lòng kiểm tra thông tin được đánh dấu."; return; }
            var course = (Course)cboCourse.SelectedItem;
            var input = new RegistrationInput { FullName = txtFullName.Text.Trim(), Phone = mtxtPhone.Text, BirthDate = dtpBirthDate.Value.Date,
                Gender = rdoMale.Checked ? "Nam" : rdoFemale.Checked ? "Nữ" : "Khác", CourseId = (int)cboCourse.SelectedValue, ReceiveNews = chkReceiveNews.Checked };
            SetBusy(true); lblStatus.Text = "Đang lưu đăng ký...";
            try
            {
                int id;
                try { id = await service.RegisterAsync(input); }
                catch (Exception ex)
                {
                    if (!IsDisposed) { lblStatus.Text = "Chưa lưu được đăng ký."; messages.Show(this, ex.Message, "Đăng ký không thành công", MessageBoxIcon.Warning); }
                    return;
                }
                if (IsDisposed) return;
                string summary = "Mã đăng ký: " + id + "\nHọ tên: " + input.FullName + "\nGiới tính: " + input.Gender +
                    "\nNgày sinh: " + input.BirthDate.ToString("dd/MM/yyyy") + "\nĐiện thoại: " + input.Phone +
                    "\nKhóa học: " + course.CourseName + "\nNhận thông báo: " + (input.ReceiveNews ? "Có" : "Không");
                lblStatus.Text = "Đã lưu đăng ký #" + id + " vào CSDL.";
                messages.Show(this, summary, "Đăng ký thành công", MessageBoxIcon.Information);
                try { await ReloadAsync(); }
                catch (Exception ex) { if (!IsDisposed) { lblStatus.Text = "Đã lưu #" + id + ", nhưng chưa tải lại được danh sách."; messages.Show(this, lblStatus.Text + "\n" + ex.Message, "Thông báo", MessageBoxIcon.Warning); } }
            }
            finally { SetBusy(false); }
        }
        private void SetBusy(bool value)
        {
            busy = value; if (IsDisposed) return;
            btnRegister.Enabled = !value; btnRefresh.Enabled = !value; pnlInputs.Enabled = !value;
        }
    }
}
