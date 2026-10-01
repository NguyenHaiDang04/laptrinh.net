namespace TechMartApp
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
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            lblTotal = new ToolStripStatusLabel();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            label1 = new Label();
            txtSearch = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            cboCategory = new ComboBox();
            label7 = new Label();
            txtProductId = new TextBox();
            txtProductName = new TextBox();
            txtUnitPrice = new TextBox();
            txtQuantity = new TextBox();
            picAvatar = new PictureBox();
            btnChooseImage = new Button();
            btnAdd = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            errorProvider1 = new ErrorProvider(components);
            dgvProducts = new DataGridView();
            exportCSVToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            dataGridView1 = new DataGridView();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picAvatar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exportCSVToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblTotal });
            statusStrip1.Location = new Point(0, 424);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(800, 26);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            
            // 
            // lblTotal
            // 
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(85, 20);
            lblTotal.Text = "StatusLabel";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Controls.Add(dgvProducts, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 28);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(800, 396);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // panel1
            // 
            panel1.Controls.Add(dataGridView1);
            panel1.Controls.Add(btnUpdate);
            panel1.Controls.Add(btnDelete);
            panel1.Controls.Add(btnAdd);
            panel1.Controls.Add(btnChooseImage);
            panel1.Controls.Add(picAvatar);
            panel1.Controls.Add(txtQuantity);
            panel1.Controls.Add(txtUnitPrice);
            panel1.Controls.Add(txtProductName);
            panel1.Controls.Add(txtProductId);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(cboCategory);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(txtSearch);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(274, 390);
            panel1.TabIndex = 0;
            
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(29, 22);
            label1.Name = "label1";
            label1.Size = new Size(77, 20);
            label1.TabIndex = 0;
            label1.Text = "Tìm kiếm :";
            // 
            // txtSearch
            // 
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSearch.Location = new Point(120, 15);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(107, 27);
            txtSearch.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(29, 56);
            label2.Name = "label2";
            label2.Size = new Size(57, 20);
            label2.TabIndex = 2;
            label2.Text = "Mã SP :";
            
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(29, 89);
            label3.Name = "label3";
            label3.Size = new Size(59, 20);
            label3.TabIndex = 3;
            label3.Text = "Tên SP :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(29, 158);
            label4.Name = "label4";
            label4.Size = new Size(70, 20);
            label4.TabIndex = 4;
            label4.Text = "Đơn Giá :";
            
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(29, 193);
            label5.Name = "label5";
            label5.Size = new Size(79, 20);
            label5.TabIndex = 5;
            label5.Text = "Số Lương :";
            // 
            // cboCategory
            // 
            cboCategory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboCategory.FormattingEnabled = true;
            cboCategory.Location = new Point(122, 122);
            cboCategory.Name = "cboCategory";
            cboCategory.Size = new Size(105, 28);
            cboCategory.TabIndex = 6;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(29, 125);
            label7.Name = "label7";
            label7.Size = new Size(83, 20);
            label7.TabIndex = 8;
            label7.Text = "Danh Mục :";
            
            // 
            // txtProductId
            // 
            txtProductId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtProductId.Location = new Point(120, 56);
            txtProductId.Name = "txtProductId";
            txtProductId.Size = new Size(107, 27);
            txtProductId.TabIndex = 9;
            
            // 
            // txtProductName
            // 
            txtProductName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtProductName.Location = new Point(122, 89);
            txtProductName.Name = "txtProductName";
            txtProductName.Size = new Size(105, 27);
            txtProductName.TabIndex = 10;
            // 
            // txtUnitPrice
            // 
            txtUnitPrice.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtUnitPrice.Location = new Point(122, 155);
            txtUnitPrice.Name = "txtUnitPrice";
            txtUnitPrice.Size = new Size(105, 27);
            txtUnitPrice.TabIndex = 11;
            // 
            // txtQuantity
            // 
            txtQuantity.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtQuantity.Location = new Point(122, 193);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(105, 27);
            txtQuantity.TabIndex = 12;
            // 
            // picAvatar
            // 
            picAvatar.Location = new Point(53, 237);
            picAvatar.Name = "picAvatar";
            picAvatar.Size = new Size(174, 69);
            picAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            picAvatar.TabIndex = 13;
            picAvatar.TabStop = false;
            // 
            // btnChooseImage
            // 
            btnChooseImage.Location = new Point(91, 312);
            btnChooseImage.Name = "btnChooseImage";
            btnChooseImage.Size = new Size(84, 33);
            btnChooseImage.TabIndex = 14;
            btnChooseImage.Text = "Chọn ảnh";
            btnChooseImage.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(29, 351);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(51, 29);
            btnAdd.TabIndex = 15;
            btnAdd.Text = "Thêm";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(189, 351);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(51, 29);
            btnDelete.TabIndex = 16;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(106, 351);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(51, 29);
            btnUpdate.TabIndex = 17;
            btnUpdate.Text = "Sửa";
            btnUpdate.UseVisualStyleBackColor = true;
            
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // dgvProducts
            // 
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.Dock = DockStyle.Fill;
            dgvProducts.Location = new Point(283, 3);
            dgvProducts.Name = "dgvProducts";
            dgvProducts.RowHeadersWidth = 51;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.Size = new Size(514, 390);
            dgvProducts.TabIndex = 1;
            // 
            // exportCSVToolStripMenuItem
            // 
            exportCSVToolStripMenuItem.Name = "exportCSVToolStripMenuItem";
            exportCSVToolStripMenuItem.Size = new Size(165, 26);
            exportCSVToolStripMenuItem.Text = "Export CSV";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(165, 26);
            exitToolStripMenuItem.Text = "Exit";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(301, 36);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(440, 318);
            dataGridView1.TabIndex = 18;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picAvatar).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblTotal;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private TextBox txtSearch;
        private Label label1;
        private Label label7;
        private ComboBox cboCategory;
        private TextBox txtProductId;
        private TextBox txtQuantity;
        private TextBox txtUnitPrice;
        private TextBox txtProductName;
        private PictureBox picAvatar;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnAdd;
        private Button btnChooseImage;
        private ErrorProvider errorProvider1;
        private DataGridView dgvProducts;
        private ToolStripMenuItem exportCSVToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private DataGridView dataGridView1;
    }
}
