namespace Bai4_11_QuanLySuaChua
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExportCSV = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelTop = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboDeviceType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numCost = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpReceiveDate = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtError = new System.Windows.Forms.TextBox();
            this.panelImage = new System.Windows.Forms.Panel();
            this.picDevice = new System.Windows.Forms.PictureBox();
            this.btnChooseImage = new System.Windows.Forms.Button();
            this.panelActions = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.cboFilterStatus = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dgvOrders = new System.Windows.Forms.DataGridView();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);

            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCost)).BeginInit();
            this.panelImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDevice)).BeginInit();
            this.panelActions.SuspendLayout();
            this.panelFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();

            // menuStrip1
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.mnuFile });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";

            // mnuFile
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.mnuExportCSV, this.mnuExit });
            this.mnuFile.Text = "Hệ thống";

            // mnuExportCSV
            this.mnuExportCSV.Text = "Xuất danh sách (CSV)";
            this.mnuExportCSV.Click += new System.EventHandler(this.mnuExportCSV_Click);

            // mnuExit
            this.mnuExit.Text = "Thoát ứng dụng";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);

            // statusStrip1
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.lblStatus });
            this.statusStrip1.Location = new System.Drawing.Point(0, 620);
            this.statusStrip1.Name = "statusStrip1";

            // lblStatus
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(55, 17);
            this.lblStatus.Text = "Sẵn sàng";

            // panelTop
            this.panelTop.Controls.Add(this.tableLayoutPanel1);
            this.panelTop.Controls.Add(this.panelImage);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 24);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(950, 150);

            // panelImage
            this.panelImage.Controls.Add(this.picDevice);
            this.panelImage.Controls.Add(this.btnChooseImage);
            this.panelImage.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelImage.Location = new System.Drawing.Point(750, 0);
            this.panelImage.Name = "panelImage";
            this.panelImage.Padding = new System.Windows.Forms.Padding(10);
            this.panelImage.Size = new System.Drawing.Size(200, 150);

            // picDevice
            this.picDevice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picDevice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picDevice.Location = new System.Drawing.Point(10, 10);
            this.picDevice.Name = "picDevice";
            this.picDevice.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picDevice.Size = new System.Drawing.Size(180, 100);

            // btnChooseImage
            this.btnChooseImage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnChooseImage.Location = new System.Drawing.Point(10, 115);
            this.btnChooseImage.Name = "btnChooseImage";
            this.btnChooseImage.Size = new System.Drawing.Size(180, 25);
            this.btnChooseImage.Text = "Chọn ảnh thiết bị...";
            this.btnChooseImage.Click += new System.EventHandler(this.btnChooseImage_Click);

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
            this.tableLayoutPanel1.Controls.Add(this.cboDeviceType, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.numCost, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label6, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.dtpReceiveDate, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.cboStatus, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label8, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtError, 3, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(750, 150);

            // Labels
            this.label1.AutoSize = true; this.label1.Dock = System.Windows.Forms.DockStyle.Right; this.label1.Text = "Mã Đơn:";
            this.label2.AutoSize = true; this.label2.Dock = System.Windows.Forms.DockStyle.Right; this.label2.Text = "Khách hàng:";
            this.label3.AutoSize = true; this.label3.Dock = System.Windows.Forms.DockStyle.Right; this.label3.Text = "Số ĐT:";
            this.label4.AutoSize = true; this.label4.Dock = System.Windows.Forms.DockStyle.Right; this.label4.Text = "Loại TB:";
            this.label5.AutoSize = true; this.label5.Dock = System.Windows.Forms.DockStyle.Right; this.label5.Text = "Phí dự kiến:";
            this.label6.AutoSize = true; this.label6.Dock = System.Windows.Forms.DockStyle.Right; this.label6.Text = "Ngày nhận:";
            this.label7.AutoSize = true; this.label7.Dock = System.Windows.Forms.DockStyle.Right; this.label7.Text = "Trạng thái:";
            this.label8.AutoSize = true; this.label8.Dock = System.Windows.Forms.DockStyle.Right; this.label8.Text = "Mô tả lỗi:";

            this.txtId.Dock = System.Windows.Forms.DockStyle.Fill; this.txtId.Margin = new System.Windows.Forms.Padding(3, 8, 20, 3);
            this.txtName.Dock = System.Windows.Forms.DockStyle.Fill; this.txtName.Margin = new System.Windows.Forms.Padding(3, 8, 20, 3);
            this.txtPhone.Dock = System.Windows.Forms.DockStyle.Fill; this.txtPhone.Margin = new System.Windows.Forms.Padding(3, 8, 20, 3);
            this.cboDeviceType.Dock = System.Windows.Forms.DockStyle.Fill; this.cboDeviceType.Margin = new System.Windows.Forms.Padding(3, 8, 20, 3);

            // Thuộc tính quan trọng: Set Minimum âm để cố tình cho phép nhập số âm nhằm kích hoạt lỗi ErrorProvider
            this.numCost.Dock = System.Windows.Forms.DockStyle.Fill; this.numCost.Margin = new System.Windows.Forms.Padding(3, 8, 20, 3);
            this.numCost.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            this.numCost.Minimum = new decimal(new int[] { 1000000000, 0, 0, -2147483648 });

            this.dtpReceiveDate.Dock = System.Windows.Forms.DockStyle.Fill; this.dtpReceiveDate.Margin = new System.Windows.Forms.Padding(3, 8, 20, 3); this.dtpReceiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.cboStatus.Dock = System.Windows.Forms.DockStyle.Fill; this.cboStatus.Margin = new System.Windows.Forms.Padding(3, 8, 20, 3); this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtError.Dock = System.Windows.Forms.DockStyle.Fill; this.txtError.Margin = new System.Windows.Forms.Padding(3, 8, 20, 3);

            // panelActions
            this.panelActions.Controls.Add(this.btnClear);
            this.panelActions.Controls.Add(this.btnDelete);
            this.panelActions.Controls.Add(this.btnUpdate);
            this.panelActions.Controls.Add(this.btnAdd);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelActions.Location = new System.Drawing.Point(0, 174);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(950, 45);

            // Buttons
            this.btnAdd.Location = new System.Drawing.Point(20, 10); this.btnAdd.Size = new System.Drawing.Size(90, 25); this.btnAdd.Text = "Thêm mới"; this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnUpdate.Location = new System.Drawing.Point(120, 10); this.btnUpdate.Size = new System.Drawing.Size(90, 25); this.btnUpdate.Text = "Cập nhật"; this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            this.btnDelete.Location = new System.Drawing.Point(220, 10); this.btnDelete.Size = new System.Drawing.Size(90, 25); this.btnDelete.Text = "Xóa đơn"; this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.btnClear.Location = new System.Drawing.Point(320, 10); this.btnClear.Size = new System.Drawing.Size(90, 25); this.btnClear.Text = "Làm mới"; this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            // panelFilter
            this.panelFilter.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panelFilter.Controls.Add(this.cboFilterStatus);
            this.panelFilter.Controls.Add(this.label11);
            this.panelFilter.Controls.Add(this.txtSearch);
            this.panelFilter.Controls.Add(this.label10);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(0, 219);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(950, 45);

            // Filter Controls
            this.label10.AutoSize = true; this.label10.Location = new System.Drawing.Point(20, 15); this.label10.Text = "Tìm kiếm (Mã / Tên KH / SĐT):";
            this.txtSearch.Location = new System.Drawing.Point(190, 12); this.txtSearch.Size = new System.Drawing.Size(200, 22); this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.label11.AutoSize = true; this.label11.Location = new System.Drawing.Point(420, 15); this.label11.Text = "Lọc Trạng thái:";
            this.cboFilterStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.cboFilterStatus.Location = new System.Drawing.Point(510, 12); this.cboFilterStatus.Size = new System.Drawing.Size(150, 24); this.cboFilterStatus.SelectedIndexChanged += new System.EventHandler(this.cboFilterStatus_SelectedIndexChanged);

            // dgvOrders
            this.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrders.Location = new System.Drawing.Point(0, 264);
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.Size = new System.Drawing.Size(950, 356);
            this.dgvOrders.SelectionChanged += new System.EventHandler(this.dgvOrders_SelectionChanged);

            // errorProvider1
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 642);
            this.Controls.Add(this.dgvOrders);
            this.Controls.Add(this.panelFilter);
            this.Controls.Add(this.panelActions);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Repair Service Tracker - Quản lý sửa chữa";
            this.Load += new System.EventHandler(this.Form1_Load);

            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCost)).EndInit();
            this.panelImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picDevice)).EndInit();
            this.panelActions.ResumeLayout(false);
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFile, mnuExportCSV, mnuExit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1, label2, label3, label4, label5, label6, label7, label8;
        private System.Windows.Forms.TextBox txtId, txtName, txtPhone, txtError;
        private System.Windows.Forms.ComboBox cboDeviceType, cboStatus;
        private System.Windows.Forms.NumericUpDown numCost;
        private System.Windows.Forms.DateTimePicker dtpReceiveDate;
        private System.Windows.Forms.Panel panelImage;
        private System.Windows.Forms.PictureBox picDevice;
        private System.Windows.Forms.Button btnChooseImage;
        private System.Windows.Forms.Panel panelActions, panelFilter;
        private System.Windows.Forms.Button btnClear, btnDelete, btnUpdate, btnAdd;
        private System.Windows.Forms.Label label11, label10;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cboFilterStatus;
        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}