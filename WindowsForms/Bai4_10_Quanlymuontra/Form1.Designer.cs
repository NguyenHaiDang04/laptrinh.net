namespace Bai4_10_Quanlymuontra
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatusCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStudentId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStudentName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboEquipment = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numQuantity = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpBorrowDate = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpDueDate = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpReturnDate = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.panelActions = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnMarkReturned = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.cboFilterStatus = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dgvRecords = new System.Windows.Forms.DataGridView();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
            this.panelActions.SuspendLayout();
            this.panelFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusCount});
            this.statusStrip1.Location = new System.Drawing.Point(0, 578);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(900, 22);
            this.statusStrip1.TabIndex = 0;
            // 
            // lblStatusCount
            // 
            this.lblStatusCount.Name = "lblStatusCount";
            this.lblStatusCount.Size = new System.Drawing.Size(55, 17);
            this.lblStatusCount.Text = "Sẵn sàng";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtId, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtStudentId, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtStudentName, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cboEquipment, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.numQuantity, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.dtpBorrowDate, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.dtpDueDate, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label8, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.dtpReturnDate, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.label9, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.cboStatus, 5, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(900, 110);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label1 to label9 (Tự động gán text)
            // 
            this.label1.AutoSize = true; this.label1.Dock = System.Windows.Forms.DockStyle.Right; this.label1.Text = "Mã Phiếu:";
            this.label2.AutoSize = true; this.label2.Dock = System.Windows.Forms.DockStyle.Right; this.label2.Text = "Mã SV:";
            this.label3.AutoSize = true; this.label3.Dock = System.Windows.Forms.DockStyle.Right; this.label3.Text = "Tên SV:";
            this.label4.AutoSize = true; this.label4.Dock = System.Windows.Forms.DockStyle.Right; this.label4.Text = "Thiết bị:";
            this.label5.AutoSize = true; this.label5.Dock = System.Windows.Forms.DockStyle.Right; this.label5.Text = "Số lượng:";
            this.label6.AutoSize = true; this.label6.Dock = System.Windows.Forms.DockStyle.Right; this.label6.Text = "Ngày mượn:";
            this.label7.AutoSize = true; this.label7.Dock = System.Windows.Forms.DockStyle.Right; this.label7.Text = "Hạn trả:";
            this.label8.AutoSize = true; this.label8.Dock = System.Windows.Forms.DockStyle.Right; this.label8.Text = "Ngày trả TT:";
            this.label9.AutoSize = true; this.label9.Dock = System.Windows.Forms.DockStyle.Right; this.label9.Text = "Trạng thái:";

            // Inputs
            this.txtId.Dock = System.Windows.Forms.DockStyle.Fill; this.txtId.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.txtStudentId.Dock = System.Windows.Forms.DockStyle.Fill; this.txtStudentId.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.txtStudentName.Dock = System.Windows.Forms.DockStyle.Fill; this.txtStudentName.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.cboEquipment.Dock = System.Windows.Forms.DockStyle.Fill; this.cboEquipment.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.numQuantity.Dock = System.Windows.Forms.DockStyle.Fill; this.numQuantity.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3); this.numQuantity.Minimum = 1;
            this.dtpBorrowDate.Dock = System.Windows.Forms.DockStyle.Fill; this.dtpBorrowDate.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3); this.dtpBorrowDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDueDate.Dock = System.Windows.Forms.DockStyle.Fill; this.dtpDueDate.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3); this.dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpReturnDate.Dock = System.Windows.Forms.DockStyle.Fill; this.dtpReturnDate.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3); this.dtpReturnDate.Format = System.Windows.Forms.DateTimePickerFormat.Short; this.dtpReturnDate.Enabled = false;
            this.cboStatus.Dock = System.Windows.Forms.DockStyle.Fill; this.cboStatus.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3); this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.cboStatus.SelectedIndexChanged += new System.EventHandler(this.cboStatus_SelectedIndexChanged);
            // 
            // panelActions
            // 
            this.panelActions.Controls.Add(this.btnExport);
            this.panelActions.Controls.Add(this.btnMarkReturned);
            this.panelActions.Controls.Add(this.btnClear);
            this.panelActions.Controls.Add(this.btnDelete);
            this.panelActions.Controls.Add(this.btnUpdate);
            this.panelActions.Controls.Add(this.btnAdd);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelActions.Location = new System.Drawing.Point(0, 110);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(900, 50);
            this.panelActions.TabIndex = 2;
            // 
            // Buttons
            // 
            this.btnAdd.Location = new System.Drawing.Point(20, 10); this.btnAdd.Size = new System.Drawing.Size(80, 30); this.btnAdd.Text = "Thêm"; this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnUpdate.Location = new System.Drawing.Point(110, 10); this.btnUpdate.Size = new System.Drawing.Size(80, 30); this.btnUpdate.Text = "Cập nhật"; this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            this.btnDelete.Location = new System.Drawing.Point(200, 10); this.btnDelete.Size = new System.Drawing.Size(80, 30); this.btnDelete.Text = "Xóa"; this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.btnClear.Location = new System.Drawing.Point(290, 10); this.btnClear.Size = new System.Drawing.Size(80, 30); this.btnClear.Text = "Làm mới"; this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            this.btnMarkReturned.Location = new System.Drawing.Point(380, 10); this.btnMarkReturned.Size = new System.Drawing.Size(120, 30); this.btnMarkReturned.Text = "Đánh dấu Đã trả"; this.btnMarkReturned.BackColor = System.Drawing.Color.LightGreen; this.btnMarkReturned.Click += new System.EventHandler(this.btnMarkReturned_Click);
            this.btnExport.Location = new System.Drawing.Point(510, 10); this.btnExport.Size = new System.Drawing.Size(120, 30); this.btnExport.Text = "Xuất file CSV"; this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panelFilter.Controls.Add(this.cboFilterStatus);
            this.panelFilter.Controls.Add(this.label11);
            this.panelFilter.Controls.Add(this.txtSearch);
            this.panelFilter.Controls.Add(this.label10);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(0, 160);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(900, 45);
            this.panelFilter.TabIndex = 3;
            // 
            // Filter Controls
            // 
            this.label10.AutoSize = true; this.label10.Location = new System.Drawing.Point(20, 15); this.label10.Text = "Tìm kiếm (Mã SV / Thiết bị):";
            this.txtSearch.Location = new System.Drawing.Point(180, 12); this.txtSearch.Size = new System.Drawing.Size(200, 22); this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.label11.AutoSize = true; this.label11.Location = new System.Drawing.Point(420, 15); this.label11.Text = "Lọc Trạng thái:";
            this.cboFilterStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.cboFilterStatus.Location = new System.Drawing.Point(510, 12); this.cboFilterStatus.Size = new System.Drawing.Size(150, 24); this.cboFilterStatus.SelectedIndexChanged += new System.EventHandler(this.cboFilterStatus_SelectedIndexChanged);
            // 
            // dgvRecords
            // 
            this.dgvRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRecords.Location = new System.Drawing.Point(0, 205);
            this.dgvRecords.Name = "dgvRecords";
            this.dgvRecords.Size = new System.Drawing.Size(900, 373);
            this.dgvRecords.TabIndex = 4;
            this.dgvRecords.SelectionChanged += new System.EventHandler(this.dgvRecords_SelectionChanged);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.dgvRecords);
            this.Controls.Add(this.panelFilter);
            this.Controls.Add(this.panelActions);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lab Equipment Checkout Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            this.panelActions.ResumeLayout(false);
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusCount;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1, label2, label3, label4, label5, label6, label7, label8, label9;
        private System.Windows.Forms.TextBox txtId, txtStudentId, txtStudentName;
        private System.Windows.Forms.ComboBox cboEquipment, cboStatus;
        private System.Windows.Forms.NumericUpDown numQuantity;
        private System.Windows.Forms.DateTimePicker dtpBorrowDate, dtpDueDate, dtpReturnDate;
        private System.Windows.Forms.Panel panelActions, panelFilter;
        private System.Windows.Forms.Button btnExport, btnMarkReturned, btnClear, btnDelete, btnUpdate, btnAdd;
        private System.Windows.Forms.Label label11, label10;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cboFilterStatus;
        private System.Windows.Forms.DataGridView dgvRecords;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}