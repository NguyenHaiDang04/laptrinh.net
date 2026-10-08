namespace Bai2_QuanLySuCoIT
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            contextMenuStrip1 = new ContextMenuStrip(components);
            txtMaPhieu = new TextBox();
            txtNguoiYeuCau = new TextBox();
            dtpNgayGhiNhan = new DateTimePicker();
            groupBox1 = new GroupBox();
            radKhanCap = new RadioButton();
            radTrungBinh = new RadioButton();
            radThap = new RadioButton();
            cboLoaiSuCo = new ComboBox();
            groupBox2 = new GroupBox();
            chkDienThoai = new CheckBox();
            chkLaptop = new CheckBox();
            chkMayIn = new CheckBox();
            chkMayTinhBan = new CheckBox();
            label5 = new Label();
            picAnhLoi = new PictureBox();
            btnTaiAnh = new Button();
            btnGuiYeuCau = new Button();
            btnNhapLai = new Button();
            label4 = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picAnhLoi).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(73, 31);
            label1.Name = "label1";
            label1.Size = new Size(77, 20);
            label1.TabIndex = 0;
            label1.Text = "Mã Phiếu :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(73, 79);
            label2.Name = "label2";
            label2.Size = new Size(112, 20);
            label2.TabIndex = 1;
            label2.Text = "Người yêu cầu :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(73, 132);
            label3.Name = "label3";
            label3.Size = new Size(112, 20);
            label3.TabIndex = 2;
            label3.Text = "Ngày ghi nhận :";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // txtMaPhieu
            // 
            txtMaPhieu.Location = new Point(235, 28);
            txtMaPhieu.Name = "txtMaPhieu";
            txtMaPhieu.Size = new Size(250, 27);
            txtMaPhieu.TabIndex = 4;
            // 
            // txtNguoiYeuCau
            // 
            txtNguoiYeuCau.Location = new Point(235, 79);
            txtNguoiYeuCau.Name = "txtNguoiYeuCau";
            txtNguoiYeuCau.Size = new Size(250, 27);
            txtNguoiYeuCau.TabIndex = 5;
            // 
            // dtpNgayGhiNhan
            // 
            dtpNgayGhiNhan.Location = new Point(235, 132);
            dtpNgayGhiNhan.Name = "dtpNgayGhiNhan";
            dtpNgayGhiNhan.Size = new Size(250, 27);
            dtpNgayGhiNhan.TabIndex = 6;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radKhanCap);
            groupBox1.Controls.Add(radTrungBinh);
            groupBox1.Controls.Add(radThap);
            groupBox1.Location = new Point(73, 178);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(412, 50);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Mức độ ưu tiên :";
            // 
            // radKhanCap
            // 
            radKhanCap.AutoSize = true;
            radKhanCap.Location = new Point(295, 26);
            radKhanCap.Name = "radKhanCap";
            radKhanCap.Size = new Size(91, 24);
            radKhanCap.TabIndex = 2;
            radKhanCap.Text = "Khẩn cấp";
            radKhanCap.UseVisualStyleBackColor = true;
            // 
            // radTrungBinh
            // 
            radTrungBinh.AutoSize = true;
            radTrungBinh.Location = new Point(150, 26);
            radTrungBinh.Name = "radTrungBinh";
            radTrungBinh.Size = new Size(100, 24);
            radTrungBinh.TabIndex = 1;
            radTrungBinh.Text = "Trung bình";
            radTrungBinh.UseVisualStyleBackColor = true;
            // 
            // radThap
            // 
            radThap.AutoSize = true;
            radThap.Checked = true;
            radThap.Location = new Point(14, 26);
            radThap.Name = "radThap";
            radThap.Size = new Size(63, 24);
            radThap.TabIndex = 0;
            radThap.TabStop = true;
            radThap.Text = "Thấp";
            radThap.UseVisualStyleBackColor = true;
            // 
            // cboLoaiSuCo
            // 
            cboLoaiSuCo.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoaiSuCo.FormattingEnabled = true;
            cboLoaiSuCo.Items.AddRange(new object[] { "Phần cứng ", "Phần mềm", "Mạng", "Tài khoản" });
            cboLoaiSuCo.Location = new Point(223, 252);
            cboLoaiSuCo.Name = "cboLoaiSuCo";
            cboLoaiSuCo.Size = new Size(262, 28);
            cboLoaiSuCo.TabIndex = 8;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(chkDienThoai);
            groupBox2.Controls.Add(chkLaptop);
            groupBox2.Controls.Add(chkMayIn);
            groupBox2.Controls.Add(chkMayTinhBan);
            groupBox2.Location = new Point(73, 286);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(412, 69);
            groupBox2.TabIndex = 9;
            groupBox2.TabStop = false;
            groupBox2.Text = "Thiết bị ảnh hưởng";
            // 
            // chkDienThoai
            // 
            chkDienThoai.AutoSize = true;
            chkDienThoai.Location = new Point(213, 45);
            chkDienThoai.Name = "chkDienThoai";
            chkDienThoai.Size = new Size(104, 24);
            chkDienThoai.TabIndex = 3;
            chkDienThoai.Text = "Điện thoại ";
            chkDienThoai.UseVisualStyleBackColor = true;
            // 
            // chkLaptop
            // 
            chkLaptop.AutoSize = true;
            chkLaptop.Location = new Point(61, 45);
            chkLaptop.Name = "chkLaptop";
            chkLaptop.Size = new Size(78, 24);
            chkLaptop.TabIndex = 2;
            chkLaptop.Text = "Laptop";
            chkLaptop.UseVisualStyleBackColor = true;
            // 
            // chkMayIn
            // 
            chkMayIn.AutoSize = true;
            chkMayIn.Location = new Point(213, 19);
            chkMayIn.Name = "chkMayIn";
            chkMayIn.Size = new Size(75, 24);
            chkMayIn.TabIndex = 1;
            chkMayIn.Text = "Máy in";
            chkMayIn.UseVisualStyleBackColor = true;
            // 
            // chkMayTinhBan
            // 
            chkMayTinhBan.AutoSize = true;
            chkMayTinhBan.Location = new Point(61, 19);
            chkMayTinhBan.Name = "chkMayTinhBan";
            chkMayTinhBan.Size = new Size(117, 24);
            chkMayTinhBan.TabIndex = 0;
            chkMayTinhBan.Text = "Máy tính bàn";
            chkMayTinhBan.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(73, 255);
            label5.Name = "label5";
            label5.Size = new Size(83, 20);
            label5.TabIndex = 10;
            label5.Text = "Loại sự cố :";
            // 
            // picAnhLoi
            // 
            picAnhLoi.BorderStyle = BorderStyle.FixedSingle;
            picAnhLoi.ContextMenuStrip = contextMenuStrip1;
            picAnhLoi.Location = new Point(181, 361);
            picAnhLoi.Name = "picAnhLoi";
            picAnhLoi.Size = new Size(171, 77);
            picAnhLoi.SizeMode = PictureBoxSizeMode.StretchImage;
            picAnhLoi.TabIndex = 11;
            picAnhLoi.TabStop = false;
            // 
            // btnTaiAnh
            // 
            btnTaiAnh.Location = new Point(358, 396);
            btnTaiAnh.Name = "btnTaiAnh";
            btnTaiAnh.Size = new Size(101, 29);
            btnTaiAnh.TabIndex = 12;
            btnTaiAnh.Text = "Tải ảnh lỗi";
            btnTaiAnh.UseVisualStyleBackColor = true;
            btnTaiAnh.Click += BtnTaiAnh_Click;
            // 
            // btnGuiYeuCau
            // 
            btnGuiYeuCau.Location = new Point(467, 409);
            btnGuiYeuCau.Name = "btnGuiYeuCau";
            btnGuiYeuCau.Size = new Size(74, 29);
            btnGuiYeuCau.TabIndex = 13;
            btnGuiYeuCau.Text = "Gửi yêu cầu";
            btnGuiYeuCau.UseVisualStyleBackColor = true;
            btnGuiYeuCau.Click += BtnGuiYeuCau_Click;
            // 
            // btnNhapLai
            // 
            btnNhapLai.Location = new Point(467, 376);
            btnNhapLai.Name = "btnNhapLai";
            btnNhapLai.Size = new Size(74, 29);
            btnNhapLai.TabIndex = 14;
            btnNhapLai.Text = "Nhập lại ";
            btnNhapLai.UseVisualStyleBackColor = true;
            btnNhapLai.Click += btnNhapLai_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(73, 358);
            label4.Name = "label4";
            label4.Size = new Size(99, 20);
            label4.TabIndex = 15;
            label4.Text = "Ảnh chụp lỗi :";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label4);
            Controls.Add(btnNhapLai);
            Controls.Add(btnGuiYeuCau);
            Controls.Add(btnTaiAnh);
            Controls.Add(picAnhLoi);
            Controls.Add(label5);
            Controls.Add(groupBox2);
            Controls.Add(cboLoaiSuCo);
            Controls.Add(groupBox1);
            Controls.Add(dtpNgayGhiNhan);
            Controls.Add(txtNguoiYeuCau);
            Controls.Add(txtMaPhieu);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picAnhLoi).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private ContextMenuStrip contextMenuStrip1;
        private TextBox txtMaPhieu;
        private TextBox txtNguoiYeuCau;
        private DateTimePicker dtpNgayGhiNhan;
        private GroupBox groupBox1;
        private RadioButton radKhanCap;
        private RadioButton radTrungBinh;
        private RadioButton radThap;
        private ComboBox cboLoaiSuCo;
        private GroupBox groupBox2;
        private CheckBox chkDienThoai;
        private CheckBox chkLaptop;
        private CheckBox chkMayIn;
        private CheckBox chkMayTinhBan;
        private Label label5;
        private PictureBox picAnhLoi;
        private Button btnTaiAnh;
        private Button btnGuiYeuCau;
        private Button btnNhapLai;
        private Label label4;
    }
}
