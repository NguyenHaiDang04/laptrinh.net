namespace Bai5_1_DangKy
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
            this.grpCaNhan = new System.Windows.Forms.GroupBox();
            this.txtXacNhanMK = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTenDangNhap = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpBoSung = new System.Windows.Forms.GroupBox();
            this.chkDieuKhoan = new System.Windows.Forms.CheckBox();
            this.radNu = new System.Windows.Forms.RadioButton();
            this.radNam = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDangKy = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.epCheck = new System.Windows.Forms.ErrorProvider(this.components);
            this.grpCaNhan.SuspendLayout();
            this.grpBoSung.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epCheck)).BeginInit();
            this.SuspendLayout();
            // 
            // grpCaNhan
            // 
            this.grpCaNhan.Controls.Add(this.txtXacNhanMK);
            this.grpCaNhan.Controls.Add(this.label3);
            this.grpCaNhan.Controls.Add(this.txtMatKhau);
            this.grpCaNhan.Controls.Add(this.label2);
            this.grpCaNhan.Controls.Add(this.txtTenDangNhap);
            this.grpCaNhan.Controls.Add(this.label1);
            this.grpCaNhan.Location = new System.Drawing.Point(20, 20);
            this.grpCaNhan.Name = "grpCaNhan";
            this.grpCaNhan.Size = new System.Drawing.Size(400, 160);
            this.grpCaNhan.TabIndex = 0;
            this.grpCaNhan.TabStop = false;
            this.grpCaNhan.Text = "Thông tin đăng nhập";
            // 
            // txtXacNhanMK
            // 
            this.txtXacNhanMK.Location = new System.Drawing.Point(140, 112);
            this.txtXacNhanMK.Name = "txtXacNhanMK";
            this.txtXacNhanMK.Size = new System.Drawing.Size(220, 22);
            this.txtXacNhanMK.TabIndex = 2;
            this.txtXacNhanMK.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 115);
            this.label3.Name = "label3";
            this.label3.Text = "Xác nhận MK:";
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.Location = new System.Drawing.Point(140, 72);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.Size = new System.Drawing.Size(220, 22);
            this.txtMatKhau.TabIndex = 1;
            this.txtMatKhau.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 75);
            this.label2.Name = "label2";
            this.label2.Text = "Mật khẩu:";
            // 
            // txtTenDangNhap
            // 
            this.txtTenDangNhap.Location = new System.Drawing.Point(140, 32);
            this.txtTenDangNhap.Name = "txtTenDangNhap";
            this.txtTenDangNhap.Size = new System.Drawing.Size(220, 22);
            this.txtTenDangNhap.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 35);
            this.label1.Name = "label1";
            this.label1.Text = "Tên đăng nhập:";
            // 
            // grpBoSung
            // 
            this.grpBoSung.Controls.Add(this.chkDieuKhoan);
            this.grpBoSung.Controls.Add(this.radNu);
            this.grpBoSung.Controls.Add(this.radNam);
            this.grpBoSung.Controls.Add(this.label5);
            this.grpBoSung.Controls.Add(this.dtpNgaySinh);
            this.grpBoSung.Controls.Add(this.label4);
            this.grpBoSung.Location = new System.Drawing.Point(20, 200);
            this.grpBoSung.Name = "grpBoSung";
            this.grpBoSung.Size = new System.Drawing.Size(400, 150);
            this.grpBoSung.TabIndex = 1;
            this.grpBoSung.TabStop = false;
            this.grpBoSung.Text = "Thông tin bổ sung";
            // 
            // chkDieuKhoan
            // 
            this.chkDieuKhoan.AutoSize = true;
            this.chkDieuKhoan.Location = new System.Drawing.Point(23, 115);
            this.chkDieuKhoan.Name = "chkDieuKhoan";
            this.chkDieuKhoan.Text = "Tôi đồng ý với các Điều khoản dịch vụ";
            this.chkDieuKhoan.TabIndex = 6;
            this.chkDieuKhoan.UseVisualStyleBackColor = true;
            // 
            // radNu
            // 
            this.radNu.AutoSize = true;
            this.radNu.Location = new System.Drawing.Point(220, 78);
            this.radNu.Name = "radNu";
            this.radNu.Text = "Nữ";
            this.radNu.TabIndex = 5;
            this.radNu.UseVisualStyleBackColor = true;
            // 
            // radNam
            // 
            this.radNam.AutoSize = true;
            this.radNam.Checked = true;
            this.radNam.Location = new System.Drawing.Point(140, 78);
            this.radNam.Name = "radNam";
            this.radNam.TabStop = true;
            this.radNam.Text = "Nam";
            this.radNam.TabIndex = 4;
            this.radNam.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 80);
            this.label5.Name = "label5";
            this.label5.Text = "Giới tính:";
            // 
            // dtpNgaySinh
            // 
            this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgaySinh.Location = new System.Drawing.Point(140, 35);
            this.dtpNgaySinh.Name = "dtpNgaySinh";
            this.dtpNgaySinh.Size = new System.Drawing.Size(220, 22);
            this.dtpNgaySinh.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 40);
            this.label4.Name = "label4";
            this.label4.Text = "Ngày sinh:";
            // 
            // btnDangKy
            // 
            this.btnDangKy.Location = new System.Drawing.Point(110, 370);
            this.btnDangKy.Name = "btnDangKy";
            this.btnDangKy.Size = new System.Drawing.Size(100, 35);
            this.btnDangKy.TabIndex = 7;
            this.btnDangKy.Text = "Đăng Ký";
            this.btnDangKy.UseVisualStyleBackColor = true;
            this.btnDangKy.Click += new System.EventHandler(this.btnDangKy_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(230, 370);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(100, 35);
            this.btnLamMoi.TabIndex = 8;
            this.btnLamMoi.Text = "Làm Mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // epCheck
            // 
            this.epCheck.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.epCheck.ContainerControl = this;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(450, 430);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.btnDangKy);
            this.Controls.Add(this.grpBoSung);
            this.Controls.Add(this.grpCaNhan);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form Đăng Ký Tài Khoản";
            this.grpCaNhan.ResumeLayout(false);
            this.grpCaNhan.PerformLayout();
            this.grpBoSung.ResumeLayout(false);
            this.grpBoSung.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epCheck)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpCaNhan;
        private System.Windows.Forms.TextBox txtXacNhanMK;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTenDangNhap;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpBoSung;
        private System.Windows.Forms.CheckBox chkDieuKhoan;
        private System.Windows.Forms.RadioButton radNu;
        private System.Windows.Forms.RadioButton radNam;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDangKy;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.ErrorProvider epCheck;
    }
}