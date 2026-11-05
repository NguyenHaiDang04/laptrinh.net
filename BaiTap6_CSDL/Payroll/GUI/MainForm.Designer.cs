namespace Payroll.GUI
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Pages = new System.Windows.Forms.TabControl();
            this.overviewPage = new System.Windows.Forms.TabPage();
            this.staff = new System.Windows.Forms.DataGridView();
            this.searchRow = new System.Windows.Forms.FlowLayoutPanel();
            this.searchLabel = new System.Windows.Forms.Label();
            this.search = new System.Windows.Forms.TextBox();
            this.departmentLabel = new System.Windows.Forms.Label();
            this.department = new System.Windows.Forms.NumericUpDown();
            this.searchButton = new System.Windows.Forms.Button();
            this.employeePage = new System.Windows.Forms.TabPage();
            this.departmentPage = new System.Windows.Forms.TabPage();
            this.attendancePage = new System.Windows.Forms.TabPage();
            this.attendance = new System.Windows.Forms.DataGridView();
            this.attendanceRow = new System.Windows.Forms.FlowLayoutPanel();
            this.monthLabel = new System.Windows.Forms.Label();
            this.month = new System.Windows.Forms.NumericUpDown();
            this.yearLabel = new System.Windows.Forms.Label();
            this.year = new System.Windows.Forms.NumericUpDown();
            this.loadButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.state = new System.Windows.Forms.Label();
            this.salaryPage = new System.Windows.Forms.TabPage();
            this.salaries = new System.Windows.Forms.DataGridView();
            this.reportRow = new System.Windows.Forms.FlowLayoutPanel();
            this.reportButton = new System.Windows.Forms.Button();
            this.Pages.SuspendLayout();
            this.overviewPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.staff)).BeginInit();
            this.searchRow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.department)).BeginInit();
            this.employeePage.SuspendLayout();
            this.departmentPage.SuspendLayout();
            this.attendancePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.attendance)).BeginInit();
            this.attendanceRow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.month)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.year)).BeginInit();
            this.salaryPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.salaries)).BeginInit();
            this.reportRow.SuspendLayout();
            this.SuspendLayout();
            //
            // Pages
            //
            this.Pages.Name = "Pages";
            //
            // overviewPage
            //
            this.overviewPage.Name = "overviewPage";
            //
            // staff
            //
            this.staff.Name = "staff";
            //
            // searchRow
            //
            this.searchRow.Name = "searchRow";
            //
            // searchLabel
            //
            this.searchLabel.Name = "searchLabel";
            //
            // search
            //
            this.search.Name = "search";
            //
            // departmentLabel
            //
            this.departmentLabel.Name = "departmentLabel";
            //
            // department
            //
            this.department.Name = "department";
            //
            // searchButton
            //
            this.searchButton.Name = "searchButton";
            //
            // employeePage
            //
            this.employeePage.Name = "employeePage";
            //
            // departmentPage
            //
            this.departmentPage.Name = "departmentPage";
            //
            // attendancePage
            //
            this.attendancePage.Name = "attendancePage";
            //
            // attendance
            //
            this.attendance.Name = "attendance";
            //
            // attendanceRow
            //
            this.attendanceRow.Name = "attendanceRow";
            //
            // monthLabel
            //
            this.monthLabel.Name = "monthLabel";
            //
            // month
            //
            this.month.Name = "month";
            //
            // yearLabel
            //
            this.yearLabel.Name = "yearLabel";
            //
            // year
            //
            this.year.Name = "year";
            //
            // loadButton
            //
            this.loadButton.Name = "loadButton";
            //
            // saveButton
            //
            this.saveButton.Name = "saveButton";
            //
            // closeButton
            //
            this.closeButton.Name = "closeButton";
            //
            // state
            //
            this.state.Name = "state";
            //
            // salaryPage
            //
            this.salaryPage.Name = "salaryPage";
            //
            // salaries
            //
            this.salaries.Name = "salaries";
            //
            // reportRow
            //
            this.reportRow.Name = "reportRow";
            //
            // reportButton
            //
            this.reportButton.Name = "reportButton";
            this.Pages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pages.SelectedIndex = 0;
            this.overviewPage.Text = "Tổng quan nhân sự";
            this.overviewPage.Padding = new System.Windows.Forms.Padding(8);
            this.overviewPage.UseVisualStyleBackColor = true;
            this.staff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.staff.ReadOnly = true;
            this.staff.AllowUserToAddRows = false;
            this.staff.AllowUserToDeleteRows = false;
            this.staff.RowHeadersVisible = false;
            this.staff.MultiSelect = false;
            this.staff.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.staff.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.staff.BackgroundColor = System.Drawing.Color.White;
            this.searchRow.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchRow.AutoSize = true;
            this.searchRow.Padding = new System.Windows.Forms.Padding(8);
            this.searchRow.WrapContents = true;
            this.searchLabel.Text = "Tên / CCCD / phòng";
            this.searchLabel.AutoSize = true;
            this.searchLabel.Margin = new System.Windows.Forms.Padding(5, 8, 5, 0);
            this.search.Width = 220;
            this.departmentLabel.Text = "Mã phòng (0=tất cả)";
            this.departmentLabel.AutoSize = true;
            this.departmentLabel.Margin = new System.Windows.Forms.Padding(5, 8, 5, 0);
            this.department.Width = 120;
            this.department.Minimum = 0m;
            this.department.Maximum = 1000000000m;
            this.department.Value = 0m;
            this.department.ThousandsSeparator = true;
            this.searchButton.Text = "Tìm";
            this.searchButton.AutoSize = true;
            this.searchButton.Height = 34;
            this.searchButton.Padding = new System.Windows.Forms.Padding(7, 2, 7, 2);
            this.searchButton.UseVisualStyleBackColor = true;
            this.employeePage.Text = "Hồ sơ nhân viên";
            this.employeePage.Padding = new System.Windows.Forms.Padding(8);
            this.employeePage.UseVisualStyleBackColor = true;
            this.departmentPage.Text = "Phòng ban";
            this.departmentPage.Padding = new System.Windows.Forms.Padding(8);
            this.departmentPage.UseVisualStyleBackColor = true;
            this.attendancePage.Text = "Chấm công tháng";
            this.attendancePage.Padding = new System.Windows.Forms.Padding(8);
            this.attendancePage.UseVisualStyleBackColor = true;
            this.attendance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.attendance.ReadOnly = true;
            this.attendance.AllowUserToAddRows = false;
            this.attendance.AllowUserToDeleteRows = false;
            this.attendance.RowHeadersVisible = false;
            this.attendance.MultiSelect = false;
            this.attendance.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.attendance.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.attendance.BackgroundColor = System.Drawing.Color.White;
            this.attendanceRow.Dock = System.Windows.Forms.DockStyle.Top;
            this.attendanceRow.AutoSize = true;
            this.attendanceRow.Padding = new System.Windows.Forms.Padding(8);
            this.attendanceRow.WrapContents = true;
            this.monthLabel.Text = "Tháng";
            this.monthLabel.AutoSize = true;
            this.monthLabel.Margin = new System.Windows.Forms.Padding(5, 8, 5, 0);
            this.month.Width = 120;
            this.month.Minimum = 1m;
            this.month.Maximum = 12m;
            this.month.Value = 1m;
            this.month.ThousandsSeparator = true;
            this.yearLabel.Text = "Năm";
            this.yearLabel.AutoSize = true;
            this.yearLabel.Margin = new System.Windows.Forms.Padding(5, 8, 5, 0);
            this.year.Width = 120;
            this.year.Minimum = 2000m;
            this.year.Maximum = 2100m;
            this.year.Value = 2026m;
            this.year.ThousandsSeparator = true;
            this.loadButton.Text = "Nạp chấm công";
            this.loadButton.AutoSize = true;
            this.loadButton.Height = 34;
            this.loadButton.Padding = new System.Windows.Forms.Padding(7, 2, 7, 2);
            this.loadButton.UseVisualStyleBackColor = true;
            this.saveButton.Text = "Lưu chấm công";
            this.saveButton.AutoSize = true;
            this.saveButton.Height = 34;
            this.saveButton.Padding = new System.Windows.Forms.Padding(7, 2, 7, 2);
            this.saveButton.UseVisualStyleBackColor = true;
            this.closeButton.Text = "Chốt lương tháng";
            this.closeButton.AutoSize = true;
            this.closeButton.Height = 34;
            this.closeButton.Padding = new System.Windows.Forms.Padding(7, 2, 7, 2);
            this.closeButton.UseVisualStyleBackColor = true;
            this.state.Text = "Chưa nạp dữ liệu";
            this.state.AutoSize = true;
            this.state.Margin = new System.Windows.Forms.Padding(5, 8, 5, 0);
            this.salaryPage.Text = "Bảng lương đã chốt";
            this.salaryPage.Padding = new System.Windows.Forms.Padding(8);
            this.salaryPage.UseVisualStyleBackColor = true;
            this.salaries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.salaries.ReadOnly = true;
            this.salaries.AllowUserToAddRows = false;
            this.salaries.AllowUserToDeleteRows = false;
            this.salaries.RowHeadersVisible = false;
            this.salaries.MultiSelect = false;
            this.salaries.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.salaries.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.salaries.BackgroundColor = System.Drawing.Color.White;
            this.reportRow.Dock = System.Windows.Forms.DockStyle.Top;
            this.reportRow.AutoSize = true;
            this.reportRow.Padding = new System.Windows.Forms.Padding(8);
            this.reportRow.WrapContents = true;
            this.reportButton.Text = "Tải bảng lương tháng đã chọn";
            this.reportButton.AutoSize = true;
            this.reportButton.Height = 34;
            this.reportButton.Padding = new System.Windows.Forms.Padding(7, 2, 7, 2);
            this.reportButton.UseVisualStyleBackColor = true;
            this.Controls.AddRange(new System.Windows.Forms.Control[] { this.Pages });
            this.Pages.Controls.AddRange(new System.Windows.Forms.Control[] { this.overviewPage, this.employeePage, this.departmentPage, this.attendancePage, this.salaryPage });
            this.overviewPage.Controls.AddRange(new System.Windows.Forms.Control[] { this.staff, this.searchRow });
            this.searchRow.Controls.AddRange(new System.Windows.Forms.Control[] { this.searchLabel, this.search, this.departmentLabel, this.department, this.searchButton });
            this.attendancePage.Controls.AddRange(new System.Windows.Forms.Control[] { this.attendance, this.attendanceRow });
            this.attendanceRow.Controls.AddRange(new System.Windows.Forms.Control[] { this.monthLabel, this.month, this.yearLabel, this.year, this.loadButton, this.saveButton, this.closeButton, this.state });
            this.salaryPage.Controls.AddRange(new System.Windows.Forms.Control[] { this.salaries, this.reportRow });
            this.reportRow.Controls.AddRange(new System.Windows.Forms.Control[] { this.reportButton });
            this.searchButton.Click += new System.EventHandler(this.SearchButton_Click);
            this.attendance.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.Attendance_DataError);
            this.loadButton.Click += new System.EventHandler(this.LoadButton_Click);
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            this.closeButton.Click += new System.EventHandler(this.CloseButton_Click);
            this.reportButton.Click += new System.EventHandler(this.ReportButton_Click);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.Pages.SelectedIndexChanged += new System.EventHandler(this.Pages_SelectedIndexChanged);
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1164, 741);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bài 6.2 - Nhân sự và tính lương | Nguyễn Hải Đăng - 24810320310";
            this.reportRow.ResumeLayout(false);
            this.reportRow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.salaries)).EndInit();
            this.salaryPage.ResumeLayout(false);
            this.salaryPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.year)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.month)).EndInit();
            this.attendanceRow.ResumeLayout(false);
            this.attendanceRow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.attendance)).EndInit();
            this.attendancePage.ResumeLayout(false);
            this.attendancePage.PerformLayout();
            this.departmentPage.ResumeLayout(false);
            this.departmentPage.PerformLayout();
            this.employeePage.ResumeLayout(false);
            this.employeePage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.department)).EndInit();
            this.searchRow.ResumeLayout(false);
            this.searchRow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.staff)).EndInit();
            this.overviewPage.ResumeLayout(false);
            this.overviewPage.PerformLayout();
            this.Pages.ResumeLayout(false);
            this.Pages.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        public System.Windows.Forms.TabControl Pages;
        private System.Windows.Forms.TabPage overviewPage;
        private System.Windows.Forms.DataGridView staff;
        private System.Windows.Forms.FlowLayoutPanel searchRow;
        private System.Windows.Forms.Label searchLabel;
        private System.Windows.Forms.TextBox search;
        private System.Windows.Forms.Label departmentLabel;
        private System.Windows.Forms.NumericUpDown department;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TabPage employeePage;
        private System.Windows.Forms.TabPage departmentPage;
        private System.Windows.Forms.TabPage attendancePage;
        private System.Windows.Forms.DataGridView attendance;
        private System.Windows.Forms.FlowLayoutPanel attendanceRow;
        private System.Windows.Forms.Label monthLabel;
        private System.Windows.Forms.NumericUpDown month;
        private System.Windows.Forms.Label yearLabel;
        private System.Windows.Forms.NumericUpDown year;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label state;
        private System.Windows.Forms.TabPage salaryPage;
        private System.Windows.Forms.DataGridView salaries;
        private System.Windows.Forms.FlowLayoutPanel reportRow;
        private System.Windows.Forms.Button reportButton;
    }
}
