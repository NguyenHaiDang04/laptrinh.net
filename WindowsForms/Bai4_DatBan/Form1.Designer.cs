namespace Bai4_DatBan // Đổi tên namespace này cho khớp với Form1.cs
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

        private void InitializeComponent()
        {
            this.flpSoDo = new System.Windows.Forms.FlowLayoutPanel();
            this.cboKhungGio = new System.Windows.Forms.ComboBox();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.lblTamTinh = new System.Windows.Forms.Label();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // flpSoDo
            this.flpSoDo.Location = new System.Drawing.Point(20, 20);
            this.flpSoDo.Name = "flpSoDo";
            this.flpSoDo.Size = new System.Drawing.Size(440, 280);
            this.flpSoDo.TabIndex = 0;

            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(480, 20);
            this.label1.Name = "label1";
            this.label1.Text = "Khung giờ:";

            // cboKhungGio
            this.cboKhungGio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhungGio.Location = new System.Drawing.Point(480, 45);
            this.cboKhungGio.Name = "cboKhungGio";
            this.cboKhungGio.Size = new System.Drawing.Size(180, 28);
            this.cboKhungGio.TabIndex = 1;
            this.cboKhungGio.SelectedIndexChanged += new System.EventHandler(this.cboKhungGio_SelectedIndexChanged);

            // lblSoLuong
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Location = new System.Drawing.Point(480, 100);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Text = "Số vị trí đang chọn: 0";
            this.lblSoLuong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);

            // lblTamTinh
            this.lblTamTinh.AutoSize = true;
            this.lblTamTinh.Location = new System.Drawing.Point(480, 140);
            this.lblTamTinh.Name = "lblTamTinh";
            this.lblTamTinh.Text = "Tạm tính tiền: 0 VNĐ";
            this.lblTamTinh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTamTinh.ForeColor = System.Drawing.Color.Red;

            // btnXacNhan
            this.btnXacNhan.Location = new System.Drawing.Point(480, 200);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(180, 40);
            this.btnXacNhan.Text = "Xác nhận đặt";
            this.btnXacNhan.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);

            // btnHuy
            this.btnHuy.Location = new System.Drawing.Point(480, 250);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(180, 40);
            this.btnHuy.Text = "Hủy chọn tất cả";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);

            // Form1
            this.ClientSize = new System.Drawing.Size(700, 330);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.lblTamTinh);
            this.Controls.Add(this.lblSoLuong);
            this.Controls.Add(this.cboKhungGio);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flpSoDo);
            this.Name = "Form1";
            this.Text = "Sơ đồ đặt bàn / Chỗ ngồi";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.FlowLayoutPanel flpSoDo;
        private System.Windows.Forms.ComboBox cboKhungGio;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.Label lblTamTinh;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Label label1;
    }
}