namespace Bai3_QuanLyVatTu
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
            this.txtMaVT = new System.Windows.Forms.TextBox();
            this.txtTenVT = new System.Windows.Forms.TextBox();
            this.txtDonGia = new System.Windows.Forms.TextBox();
            this.cboDVT = new System.Windows.Forms.ComboBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnCapNhat = new System.Windows.Forms.Button();
            this.btnXoaDong = new System.Windows.Forms.Button();
            this.btnXoaToanBo = new System.Windows.Forms.Button();
            this.lvVatTu = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // txtMaVT
            this.txtMaVT.Location = new System.Drawing.Point(100, 30);
            this.txtMaVT.Name = "txtMaVT";
            this.txtMaVT.Size = new System.Drawing.Size(150, 27);
            this.txtMaVT.TabIndex = 0;

            // txtTenVT
            this.txtTenVT.Location = new System.Drawing.Point(100, 70);
            this.txtTenVT.Name = "txtTenVT";
            this.txtTenVT.Size = new System.Drawing.Size(150, 27);
            this.txtTenVT.TabIndex = 1;

            // cboDVT
            this.cboDVT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDVT.Location = new System.Drawing.Point(100, 110);
            this.cboDVT.Name = "cboDVT";
            this.cboDVT.Size = new System.Drawing.Size(150, 28);
            this.cboDVT.TabIndex = 2;

            // txtDonGia
            this.txtDonGia.Location = new System.Drawing.Point(100, 150);
            this.txtDonGia.Name = "txtDonGia";
            this.txtDonGia.Size = new System.Drawing.Size(150, 27);
            this.txtDonGia.TabIndex = 3;

            // btnThem
            this.btnThem.Location = new System.Drawing.Point(30, 200);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(90, 35);
            this.btnThem.Text = "Thêm";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);

            // btnCapNhat
            this.btnCapNhat.Location = new System.Drawing.Point(140, 200);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(90, 35);
            this.btnCapNhat.Text = "Cập nhật";
            this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);

            // btnXoaDong
            this.btnXoaDong.Location = new System.Drawing.Point(30, 250);
            this.btnXoaDong.Name = "btnXoaDong";
            this.btnXoaDong.Size = new System.Drawing.Size(90, 35);
            this.btnXoaDong.Text = "Xóa dòng";
            this.btnXoaDong.Click += new System.EventHandler(this.btnXoaDong_Click);

            // btnXoaToanBo
            this.btnXoaToanBo.Location = new System.Drawing.Point(140, 250);
            this.btnXoaToanBo.Name = "btnXoaToanBo";
            this.btnXoaToanBo.Size = new System.Drawing.Size(90, 35);
            this.btnXoaToanBo.Text = "Xóa tất cả";
            this.btnXoaToanBo.Click += new System.EventHandler(this.btnXoaToanBo_Click);

            // lvVatTu
            this.lvVatTu.Location = new System.Drawing.Point(280, 30);
            this.lvVatTu.Name = "lvVatTu";
            this.lvVatTu.Size = new System.Drawing.Size(480, 255);
            this.lvVatTu.TabIndex = 8;
            this.lvVatTu.UseCompatibleStateImageBehavior = false;
            this.lvVatTu.SelectedIndexChanged += new System.EventHandler(this.lvVatTu_SelectedIndexChanged);

            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 33);
            this.label1.Name = "label1";
            this.label1.Text = "Mã VT:";

            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 73);
            this.label2.Name = "label2";
            this.label2.Text = "Tên VT:";

            // label3
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 113);
            this.label3.Name = "label3";
            this.label3.Text = "ĐVT:";

            // label4
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 153);
            this.label4.Name = "label4";
            this.label4.Text = "Đơn giá:";

            // Form1
            this.ClientSize = new System.Drawing.Size(790, 320);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lvVatTu);
            this.Controls.Add(this.btnXoaToanBo);
            this.Controls.Add(this.btnXoaDong);
            this.Controls.Add(this.btnCapNhat);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.txtDonGia);
            this.Controls.Add(this.cboDVT);
            this.Controls.Add(this.txtTenVT);
            this.Controls.Add(this.txtMaVT);
            this.Name = "Form1";
            this.Text = "Quản lý Vật tư";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtMaVT;
        private System.Windows.Forms.TextBox txtTenVT;
        private System.Windows.Forms.TextBox txtDonGia;
        private System.Windows.Forms.ComboBox cboDVT;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.Button btnXoaDong;
        private System.Windows.Forms.Button btnXoaToanBo;
        private System.Windows.Forms.ListView lvVatTu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}