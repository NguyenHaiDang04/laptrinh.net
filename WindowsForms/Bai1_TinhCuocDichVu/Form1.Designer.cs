namespace Bai1_TinhCuocDichVu
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
            txtDonGia = new System.Windows.Forms.TextBox();
            txtSoLuong = new System.Windows.Forms.TextBox();
            txtGiamGia = new System.Windows.Forms.TextBox();
            lblTongTien = new System.Windows.Forms.Label();
            btnTinhTien = new System.Windows.Forms.Button();
            btnLamMoi = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // txtDonGia
            // 
            txtDonGia.Location = new System.Drawing.Point(284, 78);
            txtDonGia.Name = "txtDonGia";
            txtDonGia.Size = new System.Drawing.Size(155, 27);
            txtDonGia.TabIndex = 0;
            // 
            // txtSoLuong
            // 
            txtSoLuong.Location = new System.Drawing.Point(284, 149);
            txtSoLuong.Name = "txtSoLuong";
            txtSoLuong.Size = new System.Drawing.Size(155, 27);
            txtSoLuong.TabIndex = 1;
            // 
            // txtGiamGia
            // 
            txtGiamGia.Location = new System.Drawing.Point(284, 227);
            txtGiamGia.Name = "txtGiamGia";
            txtGiamGia.Size = new System.Drawing.Size(155, 27);
            txtGiamGia.TabIndex = 2;
            // 
            // lblTongTien
            // 
            lblTongTien.AutoSize = true;
            lblTongTien.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            lblTongTien.Location = new System.Drawing.Point(103, 312);
            lblTongTien.Name = "lblTongTien";
            lblTongTien.Size = new System.Drawing.Size(158, 20);
            lblTongTien.TabIndex = 3;
            lblTongTien.Text = " Tổng tiền thanh toán :";
            // 
            // btnTinhTien
            // 
            btnTinhTien.Location = new System.Drawing.Point(167, 365);
            btnTinhTien.Name = "btnTinhTien";
            btnTinhTien.Size = new System.Drawing.Size(94, 29);
            btnTinhTien.TabIndex = 3;
            btnTinhTien.Text = "Tính tiền  ";
            btnTinhTien.UseVisualStyleBackColor = true;
            btnTinhTien.Click += new System.EventHandler(this.btnTinhTien_Click);
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new System.Drawing.Point(333, 365);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new System.Drawing.Size(94, 29);
            btnLamMoi.TabIndex = 4;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(125, 78);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(120, 20);
            label1.TabIndex = 6;
            label1.Text = "Đơn giá dịch vụ :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(125, 152);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(118, 20);
            label2.TabIndex = 7;
            label2.Text = "Số lượng khách :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(125, 230);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(122, 20);
            label3.TabIndex = 8;
            label3.Text = "Mã giảm giá (%):";
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnLamMoi);
            Controls.Add(btnTinhTien);
            Controls.Add(lblTongTien);
            Controls.Add(txtGiamGia);
            Controls.Add(txtSoLuong);
            Controls.Add(txtDonGia);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtDonGia;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.TextBox txtGiamGia;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Button btnTinhTien;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}