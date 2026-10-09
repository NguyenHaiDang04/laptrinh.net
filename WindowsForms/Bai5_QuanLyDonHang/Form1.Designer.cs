namespace Bai5_QuanLyDonHang
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
            components = new System.ComponentModel.Container();
            splitContainer1 = new SplitContainer();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            cboVanChuyen = new ComboBox();
            txtDiaChi = new TextBox();
            txtTenKH = new TextBox();
            label4 = new Label();
            dgvHangHoa = new DataGridView();
            colTenHang = new DataGridViewTextBoxColumn();
            colSoLuong = new DataGridViewTextBoxColumn();
            colTrongLuong = new DataGridViewTextBoxColumn();
            colDonGia = new DataGridViewTextBoxColumn();
            colThanhTien = new DataGridViewTextBoxColumn();
            statusStrip1 = new StatusStrip();
            lblTime = new ToolStripStatusLabel();
            lblTongSL = new ToolStripStatusLabel();
            lblTongTL = new ToolStripStatusLabel();
            lblTongTien = new ToolStripStatusLabel();
            timer1 = new System.Windows.Forms.Timer(components);
            errorProvider1 = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHangHoa).BeginInit();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tabControl1);
            splitContainer1.Panel1.Padding = new Padding(10);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(dgvHangHoa);
            splitContainer1.Panel2.Controls.Add(label4);
            splitContainer1.Panel2.Padding = new Padding(10);
            splitContainer1.Size = new Size(1100, 504);
            splitContainer1.SplitterDistance = 320;
            splitContainer1.TabIndex = 0;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(10, 10);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(300, 484);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(cboVanChuyen);
            tabPage1.Controls.Add(txtDiaChi);
            tabPage1.Controls.Add(txtTenKH);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(15);
            tabPage1.Size = new Size(292, 451);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Thông tin Khách hàng";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(18, 205);
            label3.Name = "label3";
            label3.Size = new Size(117, 20);
            label3.TabIndex = 0;
            label3.Text = "Loại vận chuyển:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(18, 100);
            label2.Name = "label2";
            label2.Size = new Size(129, 20);
            label2.TabIndex = 1;
            label2.Text = "Địa chỉ giao hàng:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 30);
            label1.Name = "label1";
            label1.Size = new Size(114, 20);
            label1.TabIndex = 2;
            label1.Text = "Tên khách hàng:";
            // 
            // cboVanChuyen
            // 
            cboVanChuyen.DropDownStyle = ComboBoxStyle.DropDownList;
            cboVanChuyen.Location = new Point(18, 230);
            cboVanChuyen.Name = "cboVanChuyen";
            cboVanChuyen.Size = new Size(250, 28);
            cboVanChuyen.TabIndex = 3;
            // 
            // txtDiaChi
            // 
            txtDiaChi.Location = new Point(18, 125);
            txtDiaChi.Multiline = true;
            txtDiaChi.Name = "txtDiaChi";
            txtDiaChi.Size = new Size(250, 60);
            txtDiaChi.TabIndex = 4;
            // 
            // txtTenKH
            // 
            txtTenKH.Location = new Point(18, 55);
            txtTenKH.Name = "txtTenKH";
            txtTenKH.Size = new Size(250, 27);
            txtTenKH.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Top;
            label4.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label4.ForeColor = Color.Blue;
            label4.Location = new Point(10, 10);
            label4.Name = "label4";
            label4.Padding = new Padding(0, 0, 0, 10);
            label4.Size = new Size(563, 33);
            label4.TabIndex = 0;
            label4.Text = "CHI TIẾT HÀNG HÓA (Nhấn F2: Thêm dòng | Nhấn Delete: Xóa dòng)";
            label4.Click += label4_Click;
            // 
            // dgvHangHoa
            // 
            dgvHangHoa.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHangHoa.Columns.AddRange(new DataGridViewColumn[] { colTenHang, colSoLuong, colTrongLuong, colDonGia, colThanhTien });
            dgvHangHoa.Dock = DockStyle.Fill;
            dgvHangHoa.Location = new Point(10, 43);
            dgvHangHoa.Name = "dgvHangHoa";
            dgvHangHoa.RowHeadersWidth = 51;
            dgvHangHoa.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHangHoa.Size = new Size(756, 451);
            dgvHangHoa.TabIndex = 1;
            dgvHangHoa.CellValidating += dgvHangHoa_CellValidating;
            dgvHangHoa.CellValueChanged += dgvHangHoa_CellValueChanged;
            dgvHangHoa.KeyDown += dgvHangHoa_KeyDown;
            // 
            // colTenHang
            // 
            colTenHang.HeaderText = "Tên Hàng";
            colTenHang.MinimumWidth = 6;
            colTenHang.Name = "colTenHang";
            colTenHang.Width = 200;
            // 
            // colSoLuong
            // 
            colSoLuong.HeaderText = "Số Lượng";
            colSoLuong.MinimumWidth = 6;
            colSoLuong.Name = "colSoLuong";
            colSoLuong.Width = 125;
            // 
            // colTrongLuong
            // 
            colTrongLuong.HeaderText = "Trọng Lượng (kg)";
            colTrongLuong.MinimumWidth = 6;
            colTrongLuong.Name = "colTrongLuong";
            colTrongLuong.Width = 130;
            // 
            // colDonGia
            // 
            colDonGia.HeaderText = "Đơn Giá";
            colDonGia.MinimumWidth = 6;
            colDonGia.Name = "colDonGia";
            colDonGia.Width = 120;
            // 
            // colThanhTien
            // 
            colThanhTien.HeaderText = "Thành Tiền";
            colThanhTien.MinimumWidth = 6;
            colThanhTien.Name = "colThanhTien";
            colThanhTien.Width = 150;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblTime, lblTongSL, lblTongTL, lblTongTien });
            statusStrip1.Location = new Point(0, 504);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1100, 26);
            statusStrip1.TabIndex = 1;
            // 
            // lblTime
            // 
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(71, 20);
            lblTime.Text = "Thời gian";
            // 
            // lblTongSL
            // 
            lblTongSL.Margin = new Padding(50, 3, 0, 2);
            lblTongSL.Name = "lblTongSL";
            lblTongSL.Size = new Size(77, 21);
            lblTongSL.Text = "Tổng SL: 0";
            // 
            // lblTongTL
            // 
            lblTongTL.Margin = new Padding(50, 3, 0, 2);
            lblTongTL.Name = "lblTongTL";
            lblTongTL.Size = new Size(163, 21);
            lblTongTL.Text = "Tổng Trọng lượng: 0 kg";
            // 
            // lblTongTien
            // 
            lblTongTien.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTongTien.ForeColor = Color.Red;
            lblTongTien.Margin = new Padding(50, 3, 0, 2);
            lblTongTien.Name = "lblTongTien";
            lblTongTien.Size = new Size(133, 21);
            lblTongTien.Text = "Tổng Tiền: 0 VNĐ";
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // Form1
            // 
            ClientSize = new Size(1100, 530);
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip1);
            Name = "Form1";
            Text = "Bảng điều khiển Quản lý Đơn giao hàng";
            Load += Form1_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHangHoa).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboVanChuyen;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.TextBox txtTenKH;
        private System.Windows.Forms.DataGridView dgvHangHoa;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblTime;
        private System.Windows.Forms.ToolStripStatusLabel lblTongSL;
        private System.Windows.Forms.ToolStripStatusLabel lblTongTL;
        private System.Windows.Forms.ToolStripStatusLabel lblTongTien;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrongLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThanhTien;
        private System.Windows.Forms.Label label4;
    }
}