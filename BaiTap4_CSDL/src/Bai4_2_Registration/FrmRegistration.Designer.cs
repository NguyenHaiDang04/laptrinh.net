namespace Bai4_2_Registration
{
    partial class FrmRegistration
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle, lblFullName, lblPhone, lblBirthDate, lblCourse, lblStatus, lblCount;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.MaskedTextBox mtxtPhone;
        private System.Windows.Forms.DateTimePicker dtpBirthDate;
        private System.Windows.Forms.ComboBox cboCourse;
        private System.Windows.Forms.GroupBox grpGender;
        private System.Windows.Forms.RadioButton rdoMale, rdoFemale, rdoOther;
        private System.Windows.Forms.CheckBox chkReceiveNews;
        private System.Windows.Forms.Button btnRegister, btnRefresh, btnExit;
        private System.Windows.Forms.Panel pnlInputs;
        private System.Windows.Forms.DataGridView dgvRegistrations;
        private System.Windows.Forms.TabControl tabData;
        private System.Windows.Forms.TabPage tabRegistrations, tabSinhVien;
        private System.Windows.Forms.DataGridView dgvSinhVien;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaSV, colHoTen, colNgaySinh, colGioiTinh, colLop, colDiemTB;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId, colName, colPhone, colBirth, colGender, colCourse;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colNews;
        private System.Windows.Forms.ErrorProvider errorProvider;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblBirthDate = new System.Windows.Forms.Label();
            this.lblCourse = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.mtxtPhone = new System.Windows.Forms.MaskedTextBox();
            this.dtpBirthDate = new System.Windows.Forms.DateTimePicker();
            this.cboCourse = new System.Windows.Forms.ComboBox();
            this.grpGender = new System.Windows.Forms.GroupBox();
            this.rdoMale = new System.Windows.Forms.RadioButton();
            this.rdoFemale = new System.Windows.Forms.RadioButton();
            this.rdoOther = new System.Windows.Forms.RadioButton();
            this.chkReceiveNews = new System.Windows.Forms.CheckBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.pnlInputs = new System.Windows.Forms.Panel();
            this.dgvRegistrations = new System.Windows.Forms.DataGridView();
            this.tabData = new System.Windows.Forms.TabControl();
            this.tabRegistrations = new System.Windows.Forms.TabPage();
            this.tabSinhVien = new System.Windows.Forms.TabPage();
            this.dgvSinhVien = new System.Windows.Forms.DataGridView();
            this.colMaSV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHoTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgaySinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGioiTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDiemTB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBirth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCourse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNews = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistrations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSinhVien)).BeginInit();
            this.tabData.SuspendLayout(); this.tabRegistrations.SuspendLayout(); this.tabSinhVien.SuspendLayout();
            this.grpGender.SuspendLayout(); this.pnlInputs.SuspendLayout(); this.SuspendLayout();
            // Tiêu đề
            this.lblTitle.Location = new System.Drawing.Point(22, 16);
            this.lblTitle.Size = new System.Drawing.Size(890, 40);
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Text = "ĐĂNG KÝ HỌC VIÊN";
            // Panel nhập liệu
            this.pnlInputs.Location = new System.Drawing.Point(20, 62);
            this.pnlInputs.Size = new System.Drawing.Size(900, 188);
            this.pnlInputs.Name = "pnlInputs";
            this.pnlInputs.TabIndex = 0;
            this.lblFullName.Location = new System.Drawing.Point(0, 8); this.lblFullName.Size = new System.Drawing.Size(110, 25); this.lblFullName.Text = "Họ và tên";
            this.txtFullName.Location = new System.Drawing.Point(115, 5); this.txtFullName.Size = new System.Drawing.Size(300, 25); this.txtFullName.Name = "txtFullName"; this.txtFullName.MaxLength = 100; this.txtFullName.TabIndex = 0;
            this.lblPhone.Location = new System.Drawing.Point(450, 8); this.lblPhone.Size = new System.Drawing.Size(105, 25); this.lblPhone.Text = "Điện thoại";
            this.mtxtPhone.Location = new System.Drawing.Point(560, 5); this.mtxtPhone.Size = new System.Drawing.Size(300, 25); this.mtxtPhone.Name = "mtxtPhone";
            this.mtxtPhone.Mask = "(000) 000-0000";
            this.mtxtPhone.AsciiOnly = true;
            this.mtxtPhone.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.mtxtPhone.TabIndex = 1;
            this.lblBirthDate.Location = new System.Drawing.Point(0, 57); this.lblBirthDate.Size = new System.Drawing.Size(110, 25); this.lblBirthDate.Text = "Ngày sinh";
            this.dtpBirthDate.Location = new System.Drawing.Point(115, 53); this.dtpBirthDate.Size = new System.Drawing.Size(300, 25); this.dtpBirthDate.Name = "dtpBirthDate";
            this.dtpBirthDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBirthDate.MinDate = new System.DateTime(1900, 1, 1);
            this.dtpBirthDate.TabIndex = 2;
            this.lblCourse.Location = new System.Drawing.Point(450, 57); this.lblCourse.Size = new System.Drawing.Size(105, 25); this.lblCourse.Text = "Khóa học";
            this.cboCourse.Location = new System.Drawing.Point(560, 53); this.cboCourse.Size = new System.Drawing.Size(300, 25); this.cboCourse.Name = "cboCourse";
            this.cboCourse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCourse.DisplayMember = "CourseName"; this.cboCourse.ValueMember = "CourseId"; this.cboCourse.TabIndex = 3;
            // RadioButton đặt cùng GroupBox để chỉ chọn một giới tính
            this.grpGender.Location = new System.Drawing.Point(0, 100); this.grpGender.Size = new System.Drawing.Size(415, 70); this.grpGender.Text = "Giới tính"; this.grpGender.Name = "grpGender"; this.grpGender.TabIndex = 4;
            this.rdoMale.Location = new System.Drawing.Point(20, 28); this.rdoMale.Size = new System.Drawing.Size(100, 27); this.rdoMale.Text = "Nam"; this.rdoMale.Name = "rdoMale"; this.rdoMale.Checked = true; this.rdoMale.TabIndex = 0;
            this.rdoFemale.Location = new System.Drawing.Point(140, 28); this.rdoFemale.Size = new System.Drawing.Size(100, 27); this.rdoFemale.Text = "Nữ"; this.rdoFemale.Name = "rdoFemale"; this.rdoFemale.TabIndex = 1;
            this.rdoOther.Location = new System.Drawing.Point(260, 28); this.rdoOther.Size = new System.Drawing.Size(100, 27); this.rdoOther.Text = "Khác"; this.rdoOther.Name = "rdoOther"; this.rdoOther.TabIndex = 2;
            this.grpGender.Controls.Add(this.rdoMale); this.grpGender.Controls.Add(this.rdoFemale); this.grpGender.Controls.Add(this.rdoOther);
            this.chkReceiveNews.Location = new System.Drawing.Point(450, 127); this.chkReceiveNews.Size = new System.Drawing.Size(420, 28); this.chkReceiveNews.Text = "Đăng ký nhận thông báo khóa học"; this.chkReceiveNews.Name = "chkReceiveNews"; this.chkReceiveNews.TabIndex = 5;
            this.pnlInputs.Controls.Add(this.lblFullName); this.pnlInputs.Controls.Add(this.txtFullName); this.pnlInputs.Controls.Add(this.lblPhone); this.pnlInputs.Controls.Add(this.mtxtPhone);
            this.pnlInputs.Controls.Add(this.lblBirthDate); this.pnlInputs.Controls.Add(this.dtpBirthDate); this.pnlInputs.Controls.Add(this.lblCourse); this.pnlInputs.Controls.Add(this.cboCourse); this.pnlInputs.Controls.Add(this.grpGender); this.pnlInputs.Controls.Add(this.chkReceiveNews);
            // Nút chức năng
            this.btnRegister.Location = new System.Drawing.Point(20, 258); this.btnRegister.Size = new System.Drawing.Size(145, 36); this.btnRegister.Name = "btnRegister"; this.btnRegister.Text = "Đăng ký"; this.btnRegister.Enabled = false; this.btnRegister.TabIndex = 1;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            this.btnRefresh.Location = new System.Drawing.Point(178, 258); this.btnRefresh.Size = new System.Drawing.Size(145, 36); this.btnRefresh.Name = "btnRefresh"; this.btnRefresh.Text = "Tải lại danh sách"; this.btnRefresh.Enabled = false; this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            this.btnExit.Location = new System.Drawing.Point(336, 258); this.btnExit.Size = new System.Drawing.Size(145, 36); this.btnExit.Name = "btnExit"; this.btnExit.Text = "Thoát"; this.btnExit.TabIndex = 3; this.btnExit.CausesValidation = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            this.lblStatus.Location = new System.Drawing.Point(20, 304); this.lblStatus.Size = new System.Drawing.Size(900, 26); this.lblStatus.Name = "lblStatus"; this.lblStatus.Text = "Đang chuẩn bị CSDL...";
            this.lblCount.Location = new System.Drawing.Point(20, 340); this.lblCount.Size = new System.Drawing.Size(900, 26); this.lblCount.Name = "lblCount"; this.lblCount.Text = "Đã lưu: 0 đăng ký";
            // Danh sách đã lưu trong CSDL
            this.dgvRegistrations.Dock = System.Windows.Forms.DockStyle.Fill; this.dgvRegistrations.Name = "dgvRegistrations";
            this.dgvRegistrations.AutoGenerateColumns = false;
            this.dgvRegistrations.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRegistrations.ReadOnly = true; this.dgvRegistrations.AllowUserToAddRows = false; this.dgvRegistrations.AllowUserToDeleteRows = false; this.dgvRegistrations.RowHeadersVisible = false;
            this.dgvRegistrations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect; this.dgvRegistrations.MultiSelect = false; this.dgvRegistrations.BackgroundColor = System.Drawing.Color.White;
            this.colId.DataPropertyName = "RegistrationId"; this.colId.HeaderText = "Mã ĐK"; this.colId.FillWeight = 50;
            this.colName.DataPropertyName = "FullName"; this.colName.HeaderText = "Họ tên"; this.colName.FillWeight = 140;
            this.colPhone.DataPropertyName = "Phone"; this.colPhone.HeaderText = "Điện thoại"; this.colPhone.FillWeight = 100;
            this.colBirth.DataPropertyName = "BirthDate"; this.colBirth.HeaderText = "Ngày sinh"; this.colBirth.DefaultCellStyle.Format = "dd/MM/yyyy"; this.colBirth.FillWeight = 90;
            this.colGender.DataPropertyName = "Gender"; this.colGender.HeaderText = "Giới tính"; this.colGender.FillWeight = 60;
            this.colCourse.DataPropertyName = "CourseName"; this.colCourse.HeaderText = "Khóa học"; this.colCourse.FillWeight = 170;
            this.colNews.DataPropertyName = "ReceiveNews"; this.colNews.HeaderText = "Nhận tin"; this.colNews.FillWeight = 65;
            this.dgvRegistrations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colId, this.colName, this.colPhone, this.colBirth, this.colGender, this.colCourse, this.colNews });
            // Hai tab: giữ danh sách đăng ký và bổ sung dữ liệu SinhVien.
            this.tabData.Location = new System.Drawing.Point(20, 371); this.tabData.Size = new System.Drawing.Size(900, 240); this.tabData.Name = "tabData";
            this.tabData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tabRegistrations.Text = "Đăng ký học viên"; this.tabRegistrations.Name = "tabRegistrations"; this.tabRegistrations.Controls.Add(this.dgvRegistrations);
            this.tabSinhVien.Text = "Sinh viên"; this.tabSinhVien.Name = "tabSinhVien"; this.tabSinhVien.Controls.Add(this.dgvSinhVien);
            this.tabData.Controls.Add(this.tabRegistrations); this.tabData.Controls.Add(this.tabSinhVien);
            this.dgvSinhVien.Name = "dgvSinhVien"; this.dgvSinhVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSinhVien.AutoGenerateColumns = false; this.dgvSinhVien.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSinhVien.ReadOnly = true; this.dgvSinhVien.AllowUserToAddRows = false; this.dgvSinhVien.AllowUserToDeleteRows = false;
            this.dgvSinhVien.RowHeadersVisible = false; this.dgvSinhVien.MultiSelect = false;
            this.dgvSinhVien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect; this.dgvSinhVien.BackgroundColor = System.Drawing.Color.White;
            this.colMaSV.DataPropertyName = "MaSV"; this.colMaSV.HeaderText = "Mã SV"; this.colMaSV.FillWeight = 65;
            this.colHoTen.DataPropertyName = "HoTen"; this.colHoTen.HeaderText = "Họ tên"; this.colHoTen.FillWeight = 150;
            this.colNgaySinh.DataPropertyName = "NgaySinh"; this.colNgaySinh.HeaderText = "Ngày sinh"; this.colNgaySinh.DefaultCellStyle.Format = "dd/MM/yyyy";
            this.colGioiTinh.DataPropertyName = "GioiTinh"; this.colGioiTinh.HeaderText = "Giới tính"; this.colGioiTinh.FillWeight = 65;
            this.colLop.DataPropertyName = "Lop"; this.colLop.HeaderText = "Lớp"; this.colLop.FillWeight = 110;
            this.colDiemTB.DataPropertyName = "DiemTB"; this.colDiemTB.HeaderText = "Điểm TB"; this.colDiemTB.DefaultCellStyle.Format = "F2"; this.colDiemTB.FillWeight = 70;
            this.dgvSinhVien.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colMaSV, this.colHoTen, this.colNgaySinh, this.colGioiTinh, this.colLop, this.colDiemTB });
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink; this.errorProvider.ContainerControl = this;
            // Form
            this.AcceptButton = this.btnRegister; this.CancelButton = this.btnExit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F); this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 630); this.MinimumSize = new System.Drawing.Size(956, 669);
            this.Font = new System.Drawing.Font("Segoe UI", 10F); this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Name = "FrmRegistration"; this.Text = "Bài 4.2 - Đăng ký học viên với CSDL";
            this.Controls.Add(this.lblTitle); this.Controls.Add(this.pnlInputs); this.Controls.Add(this.btnRegister); this.Controls.Add(this.btnRefresh); this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblStatus); this.Controls.Add(this.lblCount); this.Controls.Add(this.tabData);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistrations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSinhVien)).EndInit();
            this.tabData.ResumeLayout(false); this.tabRegistrations.ResumeLayout(false); this.tabSinhVien.ResumeLayout(false);
            this.grpGender.ResumeLayout(false); this.pnlInputs.ResumeLayout(false); this.pnlInputs.PerformLayout(); this.ResumeLayout(false);
        }
    }
}
