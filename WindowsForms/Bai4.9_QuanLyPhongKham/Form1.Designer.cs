namespace Bai4_9_QuanLyPhongKham
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuHeThong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuXuatCSV = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatusCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.cboDoctor = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.panelActions = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.chkFilterDate = new System.Windows.Forms.CheckBox();
            this.dtpFilterDate = new System.Windows.Forms.DateTimePicker();
            this.cboFilterStatus = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dgvAppointments = new System.Windows.Forms.DataGridView();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);

            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelActions.SuspendLayout();
            this.panelFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();

            // menuStrip1
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.mnuHeThong });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";

            // mnuHeThong
            this.mnuHeThong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.mnuXuatCSV });
            this.mnuHeThong.Text = "Hệ thống";

            // mnuXuatCSV
            this.mnuXuatCSV.Text = "Xuất danh sách (CSV)";
            this.mnuXuatCSV.Click += new System.EventHandler(this.mnuXuatCSV_Click);

            // statusStrip1
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.lblStatusCount });
            this.statusStrip1.Location = new System.Drawing.Point(0, 578);
            this.statusStrip1.Name = "statusStrip1";

            // lblStatusCount
            this.lblStatusCount.Name = "lblStatusCount";
            this.lblStatusCount.Size = new System.Drawing.Size(155, 17);
            this.lblStatusCount.Text = "Đang hiển thị: 0 lịch hẹn";

            // tableLayoutPanel1
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtId, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtName, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtPhone, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.dtpDate, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.cboDoctor, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label7, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.cboStatus, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtReason, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(884, 140);

            // Labels and Inputs configuration...
            this.label1.AutoSize = true; this.label1.Text = "Mã Lịch Hẹn:"; this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtId.Dock = System.Windows.Forms.DockStyle.Fill; this.txtId.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.label2.AutoSize = true; this.label2.Text = "Họ Tên BN:"; this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtName.Dock = System.Windows.Forms.DockStyle.Fill; this.txtName.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.label3.AutoSize = true; this.label3.Text = "Số ĐT:"; this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtPhone.Dock = System.Windows.Forms.DockStyle.Fill; this.txtPhone.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.label4.AutoSize = true; this.label4.Text = "Ngày/Giờ:"; this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtpDate.CustomFormat = "dd/MM/yyyy HH:mm"; this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom; this.dtpDate.Dock = System.Windows.Forms.DockStyle.Fill; this.dtpDate.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.label5.AutoSize = true; this.label5.Text = "Bác sĩ:"; this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.cboDoctor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.cboDoctor.Dock = System.Windows.Forms.DockStyle.Fill; this.cboDoctor.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.label7.AutoSize = true; this.label7.Text = "Trạng thái:"; this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.cboStatus.Dock = System.Windows.Forms.DockStyle.Fill; this.cboStatus.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.label6.AutoSize = true; this.label6.Text = "Lý do khám:"; this.label6.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel1.SetColumnSpan(this.txtReason, 3);
            this.txtReason.Dock = System.Windows.Forms.DockStyle.Fill; this.txtReason.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);

            // panelActions
            this.panelActions.Controls.Add(this.btnClear);
            this.panelActions.Controls.Add(this.btnDelete);
            this.panelActions.Controls.Add(this.btnUpdate);
            this.panelActions.Controls.Add(this.btnAdd);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelActions.Location = new System.Drawing.Point(0, 164);
            this.panelActions.Size = new System.Drawing.Size(884, 50);

            // Buttons
            this.btnAdd.Location = new System.Drawing.Point(100, 10); this.btnAdd.Size = new System.Drawing.Size(100, 30); this.btnAdd.Text = "Thêm mới"; this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnUpdate.Location = new System.Drawing.Point(210, 10); this.btnUpdate.Size = new System.Drawing.Size(100, 30); this.btnUpdate.Text = "Cập nhật"; this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            this.btnDelete.Location = new System.Drawing.Point(320, 10); this.btnDelete.Size = new System.Drawing.Size(100, 30); this.btnDelete.Text = "Xóa lịch"; this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.btnClear.Location = new System.Drawing.Point(430, 10); this.btnClear.Size = new System.Drawing.Size(100, 30); this.btnClear.Text = "Làm mới"; this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            // panelFilter
            this.panelFilter.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panelFilter.Controls.Add(this.chkFilterDate);
            this.panelFilter.Controls.Add(this.dtpFilterDate);
            this.panelFilter.Controls.Add(this.cboFilterStatus);
            this.panelFilter.Controls.Add(this.txtSearch);
            this.panelFilter.Controls.Add(this.label9);
            this.panelFilter.Controls.Add(this.label8);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(0, 214);
            this.panelFilter.Size = new System.Drawing.Size(884, 50);

            // Filter Controls
            this.label8.AutoSize = true; this.label8.Location = new System.Drawing.Point(20, 17); this.label8.Text = "Tìm Tên/SĐT:";
            this.txtSearch.Location = new System.Drawing.Point(100, 14); this.txtSearch.Size = new System.Drawing.Size(200, 22); this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.label9.AutoSize = true; this.label9.Location = new System.Drawing.Point(320, 17); this.label9.Text = "Lọc Trạng thái:";
            this.cboFilterStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.cboFilterStatus.Location = new System.Drawing.Point(410, 14); this.cboFilterStatus.Size = new System.Drawing.Size(120, 24); this.cboFilterStatus.SelectedIndexChanged += new System.EventHandler(this.cboFilterStatus_SelectedIndexChanged);
            this.chkFilterDate.AutoSize = true; this.chkFilterDate.Location = new System.Drawing.Point(550, 16); this.chkFilterDate.Text = "Lọc Ngày:"; this.chkFilterDate.CheckedChanged += new System.EventHandler(this.chkFilterDate_CheckedChanged);
            this.dtpFilterDate.Format = System.Windows.Forms.DateTimePickerFormat.Short; this.dtpFilterDate.Location = new System.Drawing.Point(640, 14); this.dtpFilterDate.Size = new System.Drawing.Size(120, 22); this.dtpFilterDate.ValueChanged += new System.EventHandler(this.dtpFilterDate_ValueChanged);

            // dgvAppointments
            this.dgvAppointments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAppointments.Location = new System.Drawing.Point(0, 264);
            this.dgvAppointments.Size = new System.Drawing.Size(884, 314);
            this.dgvAppointments.SelectionChanged += new System.EventHandler(this.dgvAppointments_SelectionChanged);

            // errorProvider1
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;

            // Form1
            this.ClientSize = new System.Drawing.Size(884, 600);
            this.Controls.Add(this.dgvAppointments);
            this.Controls.Add(this.panelFilter);
            this.Controls.Add(this.panelActions);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clinic Appointment Manager";
            this.Load += new System.EventHandler(this.Form1_Load);

            this.menuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panelActions.ResumeLayout(false);
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuHeThong;
        private System.Windows.Forms.ToolStripMenuItem mnuXuatCSV;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusCount;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1, label2, label3, label4, label5, label6, label7;
        private System.Windows.Forms.TextBox txtId, txtName, txtPhone, txtReason;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.ComboBox cboDoctor, cboStatus;
        private System.Windows.Forms.Panel panelActions, panelFilter;
        private System.Windows.Forms.Button btnClear, btnDelete, btnUpdate, btnAdd;
        private System.Windows.Forms.CheckBox chkFilterDate;
        private System.Windows.Forms.DateTimePicker dtpFilterDate;
        private System.Windows.Forms.ComboBox cboFilterStatus;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label9, label8;
        private System.Windows.Forms.DataGridView dgvAppointments;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}