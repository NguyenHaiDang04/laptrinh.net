namespace Bai4_1_Login
{
    partial class FrmLogin
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox chkShowPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label lblStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.chkShowPassword = new System.Windows.Forms.CheckBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // lblTitle
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(24, 20);
            this.lblTitle.Size = new System.Drawing.Size(490, 40);
            this.lblTitle.Text = "ĐĂNG NHẬP";
            // lblUsername
            this.lblUsername.Location = new System.Drawing.Point(26, 88);
            this.lblUsername.Size = new System.Drawing.Size(130, 24);
            this.lblUsername.Text = "Tên đăng nhập";
            // txtUsername
            this.txtUsername.Location = new System.Drawing.Point(164, 84);
            this.txtUsername.Size = new System.Drawing.Size(330, 25);
            this.txtUsername.MaxLength = 50;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.TabIndex = 0;
            // lblPassword
            this.lblPassword.Location = new System.Drawing.Point(26, 138);
            this.lblPassword.Size = new System.Drawing.Size(130, 24);
            this.lblPassword.Text = "Mật khẩu";
            // txtPassword
            this.txtPassword.Location = new System.Drawing.Point(164, 134);
            this.txtPassword.Size = new System.Drawing.Size(330, 25);
            this.txtPassword.MaxLength = 128;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.TabIndex = 1;
            // chkShowPassword
            this.chkShowPassword.Location = new System.Drawing.Point(164, 172);
            this.chkShowPassword.Size = new System.Drawing.Size(220, 28);
            this.chkShowPassword.Text = "Hiện mật khẩu";
            this.chkShowPassword.Name = "chkShowPassword";
            this.chkShowPassword.TabIndex = 2;
            this.chkShowPassword.CheckedChanged += new System.EventHandler(this.chkShowPassword_CheckedChanged);
            // btnLogin
            this.btnLogin.Location = new System.Drawing.Point(164, 218);
            this.btnLogin.Size = new System.Drawing.Size(160, 38);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Enabled = false;
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // btnExit
            this.btnExit.Location = new System.Drawing.Point(334, 218);
            this.btnExit.Size = new System.Drawing.Size(160, 38);
            this.btnExit.Name = "btnExit";
            this.btnExit.Text = "Thoát";
            this.btnExit.TabIndex = 4;
            this.btnExit.CausesValidation = false;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // lblStatus
            this.lblStatus.Location = new System.Drawing.Point(26, 282);
            this.lblStatus.Size = new System.Drawing.Size(490, 65);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Text = "Đang chuẩn bị CSDL...";
            // errorProvider
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // FrmLogin
            this.AcceptButton = this.btnLogin;
            this.CancelButton = this.btnExit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 365);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.chkShowPassword);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblStatus);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Name = "FrmLogin";
            this.Text = "Bài 4.1 - Đăng nhập với CSDL";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
