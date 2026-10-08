namespace nguyenhaidangbai4
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
            lstMenu = new ListBox();
            lstSelected = new ListBox();
            btnRight = new Button();
            btnLeft = new Button();
            lblTotal = new Label();
            SuspendLayout();
            // 
            // lstMenu
            // 
            lstMenu.FormattingEnabled = true;
            lstMenu.Location = new Point(25, 42);
            lstMenu.Name = "lstMenu";
            lstMenu.Size = new Size(293, 144);
            lstMenu.TabIndex = 0;
            // 
            // lstSelected
            // 
            lstSelected.FormattingEnabled = true;
            lstSelected.Location = new Point(507, 42);
            lstSelected.Name = "lstSelected";
            lstSelected.Size = new Size(235, 144);
            lstSelected.TabIndex = 1;
            // 
            // btnRight
            // 
            btnRight.Location = new Point(364, 83);
            btnRight.Name = "btnRight";
            btnRight.Size = new Size(94, 29);
            btnRight.TabIndex = 2;
            btnRight.Text = ">";
            btnRight.UseVisualStyleBackColor = true;
            btnRight.Click += btnRight_Click;
            // 
            // btnLeft
            // 
            btnLeft.Location = new Point(364, 133);
            btnLeft.Name = "btnLeft";
            btnLeft.Size = new Size(94, 29);
            btnLeft.TabIndex = 3;
            btnLeft.Text = "<";
            btnLeft.UseVisualStyleBackColor = true;
            btnLeft.Click += btnLeft_Click;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(507, 224);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(94, 20);
            lblTotal.TabIndex = 4;
            lblTotal.Text = "Tổng tiền: 0k";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblTotal);
            Controls.Add(btnLeft);
            Controls.Add(btnRight);
            Controls.Add(lstSelected);
            Controls.Add(lstMenu);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lstMenu;
        private ListBox lstSelected;
        private Button btnRight;
        private Button btnLeft;
        private Label lblTotal;
    }
}
