namespace RetailSales.GUI
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Pages = new System.Windows.Forms.TabControl();
            this.overviewPage = new System.Windows.Forms.TabPage();
            this.stock = new System.Windows.Forms.DataGridView();
            this.searchRow = new System.Windows.Forms.FlowLayoutPanel();
            this.searchLabel = new System.Windows.Forms.Label();
            this.search = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.reloadButton = new System.Windows.Forms.Button();
            this.productPage = new System.Windows.Forms.TabPage();
            this.customerPage = new System.Windows.Forms.TabPage();
            this.employeePage = new System.Windows.Forms.TabPage();
            this.salePage = new System.Windows.Forms.TabPage();
            this.grid = new System.Windows.Forms.DataGridView();
            this.actionRow = new System.Windows.Forms.FlowLayoutPanel();
            this.removeButton = new System.Windows.Forms.Button();
            this.payButton = new System.Windows.Forms.Button();
            this.total = new System.Windows.Forms.Label();
            this.addRow = new System.Windows.Forms.FlowLayoutPanel();
            this.codeLabel = new System.Windows.Forms.Label();
            this.code = new System.Windows.Forms.TextBox();
            this.qtyLabel = new System.Windows.Forms.Label();
            this.qty = new System.Windows.Forms.NumericUpDown();
            this.addButton = new System.Windows.Forms.Button();
            this.peopleRow = new System.Windows.Forms.FlowLayoutPanel();
            this.employeeLabel = new System.Windows.Forms.Label();
            this.employee = new System.Windows.Forms.ComboBox();
            this.customerLabel = new System.Windows.Forms.Label();
            this.customer = new System.Windows.Forms.ComboBox();
            this.reportPage = new System.Windows.Forms.TabPage();
            this.top = new System.Windows.Forms.DataGridView();
            this.reportRow = new System.Windows.Forms.FlowLayoutPanel();
            this.date = new System.Windows.Forms.DateTimePicker();
            this.reportButton = new System.Windows.Forms.Button();
            this.revenue = new System.Windows.Forms.Label();
            this.billPage = new System.Windows.Forms.TabPage();
            this.bills = new System.Windows.Forms.DataGridView();
            this.Pages.SuspendLayout();
            this.overviewPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stock)).BeginInit();
            this.searchRow.SuspendLayout();
            this.productPage.SuspendLayout();
            this.customerPage.SuspendLayout();
            this.employeePage.SuspendLayout();
            this.salePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.actionRow.SuspendLayout();
            this.addRow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qty)).BeginInit();
            this.peopleRow.SuspendLayout();
            this.reportPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.top)).BeginInit();
            this.reportRow.SuspendLayout();
            this.billPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bills)).BeginInit();
            this.SuspendLayout();
            //
            // Pages
            //
            this.Pages.Name = "Pages";
            //
            // overviewPage
            //
            this.overviewPage.Name = "overviewPage";
            //
            // stock
            //
            this.stock.Name = "stock";
            //
            // searchRow
            //
            this.searchRow.Name = "searchRow";
            //
            // searchLabel
            //
            this.searchLabel.Name = "searchLabel";
            //
            // search
            //
            this.search.Name = "search";
            //
            // searchButton
            //
            this.searchButton.Name = "searchButton";
            //
            // reloadButton
            //
            this.reloadButton.Name = "reloadButton";
            //
            // productPage
            //
            this.productPage.Name = "productPage";
            //
            // customerPage
            //
            this.customerPage.Name = "customerPage";
            //
            // employeePage
            //
            this.employeePage.Name = "employeePage";
            //
            // salePage
            //
            this.salePage.Name = "salePage";
            //
            // grid
            //
            this.grid.Name = "grid";
            //
            // actionRow
            //
            this.actionRow.Name = "actionRow";
            //
            // removeButton
            //
            this.removeButton.Name = "removeButton";
            //
            // payButton
            //
            this.payButton.Name = "payButton";
            //
            // total
            //
            this.total.Name = "total";
            //
            // addRow
            //
            this.addRow.Name = "addRow";
            //
            // codeLabel
            //
            this.codeLabel.Name = "codeLabel";
            //
            // code
            //
            this.code.Name = "code";
            //
            // qtyLabel
            //
            this.qtyLabel.Name = "qtyLabel";
            //
            // qty
            //
            this.qty.Name = "qty";
            //
            // addButton
            //
            this.addButton.Name = "addButton";
            //
            // peopleRow
            //
            this.peopleRow.Name = "peopleRow";
            //
            // employeeLabel
            //
            this.employeeLabel.Name = "employeeLabel";
            //
            // employee
            //
            this.employee.Name = "employee";
            //
            // customerLabel
            //
            this.customerLabel.Name = "customerLabel";
            //
            // customer
            //
            this.customer.Name = "customer";
            //
            // reportPage
            //
            this.reportPage.Name = "reportPage";
            //
            // top
            //
            this.top.Name = "top";
            //
            // reportRow
            //
            this.reportRow.Name = "reportRow";
            //
            // date
            //
            this.date.Name = "date";
            //
            // reportButton
            //
            this.reportButton.Name = "reportButton";
            //
            // revenue
            //
            this.revenue.Name = "revenue";
            //
            // billPage
            //
            this.billPage.Name = "billPage";
            //
            // bills
            //
            this.bills.Name = "bills";
            this.Pages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pages.SelectedIndex = 0;
            this.overviewPage.Text = "Tổng quan";
            this.overviewPage.Padding = new System.Windows.Forms.Padding(8);
            this.overviewPage.UseVisualStyleBackColor = true;
            this.stock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stock.ReadOnly = true;
            this.stock.AllowUserToAddRows = false;
            this.stock.AllowUserToDeleteRows = false;
            this.stock.RowHeadersVisible = false;
            this.stock.MultiSelect = false;
            this.stock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.stock.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.stock.BackgroundColor = System.Drawing.Color.White;
            this.searchRow.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchRow.AutoSize = true;
            this.searchRow.Padding = new System.Windows.Forms.Padding(8);
            this.searchRow.WrapContents = true;
            this.searchLabel.Text = "Mã / tên hàng";
            this.searchLabel.AutoSize = true;
            this.searchLabel.Margin = new System.Windows.Forms.Padding(5, 8, 5, 0);
            this.search.Width = 250;
            this.searchButton.Text = "Tìm";
            this.searchButton.AutoSize = true;
            this.searchButton.Height = 34;
            this.searchButton.Padding = new System.Windows.Forms.Padding(7, 2, 7, 2);
            this.searchButton.UseVisualStyleBackColor = true;
            this.reloadButton.Text = "Tải lại";
            this.reloadButton.AutoSize = true;
            this.reloadButton.Height = 34;
            this.reloadButton.Padding = new System.Windows.Forms.Padding(7, 2, 7, 2);
            this.reloadButton.UseVisualStyleBackColor = true;
            this.productPage.Text = "Sản phẩm";
            this.productPage.Padding = new System.Windows.Forms.Padding(8);
            this.productPage.UseVisualStyleBackColor = true;
            this.customerPage.Text = "Khách hàng";
            this.customerPage.Padding = new System.Windows.Forms.Padding(8);
            this.customerPage.UseVisualStyleBackColor = true;
            this.employeePage.Text = "Nhân viên";
            this.employeePage.Padding = new System.Windows.Forms.Padding(8);
            this.employeePage.UseVisualStyleBackColor = true;
            this.salePage.Text = "Bán hàng";
            this.salePage.Padding = new System.Windows.Forms.Padding(8);
            this.salePage.UseVisualStyleBackColor = true;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.ReadOnly = true;
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.RowHeadersVisible = false;
            this.grid.MultiSelect = false;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid.BackgroundColor = System.Drawing.Color.White;
            this.actionRow.Dock = System.Windows.Forms.DockStyle.Top;
            this.actionRow.AutoSize = true;
            this.actionRow.Padding = new System.Windows.Forms.Padding(8);
            this.actionRow.WrapContents = true;
            this.removeButton.Text = "Xóa dòng";
            this.removeButton.AutoSize = true;
            this.removeButton.Height = 34;
            this.removeButton.Padding = new System.Windows.Forms.Padding(7, 2, 7, 2);
            this.removeButton.UseVisualStyleBackColor = true;
            this.payButton.Text = "Thanh toán";
            this.payButton.AutoSize = true;
            this.payButton.Height = 34;
            this.payButton.Padding = new System.Windows.Forms.Padding(7, 2, 7, 2);
            this.payButton.UseVisualStyleBackColor = true;
            this.total.Text = "Tạm tính: 0 đ";
            this.total.AutoSize = true;
            this.total.Margin = new System.Windows.Forms.Padding(5, 8, 5, 0);
            this.addRow.Dock = System.Windows.Forms.DockStyle.Top;
            this.addRow.AutoSize = true;
            this.addRow.Padding = new System.Windows.Forms.Padding(8);
            this.addRow.WrapContents = true;
            this.codeLabel.Text = "Quét / nhập mã";
            this.codeLabel.AutoSize = true;
            this.codeLabel.Margin = new System.Windows.Forms.Padding(5, 8, 5, 0);
            this.code.Width = 180;
            this.qtyLabel.Text = "Số lượng";
            this.qtyLabel.AutoSize = true;
            this.qtyLabel.Margin = new System.Windows.Forms.Padding(5, 8, 5, 0);
            this.qty.Width = 120;
            this.qty.Minimum = 1m;
            this.qty.Maximum = 1000000000m;
            this.qty.Value = 1m;
            this.qty.ThousandsSeparator = true;
            this.addButton.Text = "Thêm vào giỏ";
            this.addButton.AutoSize = true;
            this.addButton.Height = 34;
            this.addButton.Padding = new System.Windows.Forms.Padding(7, 2, 7, 2);
            this.addButton.UseVisualStyleBackColor = true;
            this.peopleRow.Dock = System.Windows.Forms.DockStyle.Top;
            this.peopleRow.AutoSize = true;
            this.peopleRow.Padding = new System.Windows.Forms.Padding(8);
            this.peopleRow.WrapContents = true;
            this.employeeLabel.Text = "Thu ngân";
            this.employeeLabel.AutoSize = true;
            this.employeeLabel.Margin = new System.Windows.Forms.Padding(5, 8, 5, 0);
            this.employee.Width = 180;
            this.employee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.employee.FormattingEnabled = true;
            this.customerLabel.Text = "Khách hàng";
            this.customerLabel.AutoSize = true;
            this.customerLabel.Margin = new System.Windows.Forms.Padding(5, 8, 5, 0);
            this.customer.Width = 180;
            this.customer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.customer.FormattingEnabled = true;
            this.reportPage.Text = "Báo cáo";
            this.reportPage.Padding = new System.Windows.Forms.Padding(8);
            this.reportPage.UseVisualStyleBackColor = true;
            this.top.Dock = System.Windows.Forms.DockStyle.Fill;
            this.top.ReadOnly = true;
            this.top.AllowUserToAddRows = false;
            this.top.AllowUserToDeleteRows = false;
            this.top.RowHeadersVisible = false;
            this.top.MultiSelect = false;
            this.top.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.top.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.top.BackgroundColor = System.Drawing.Color.White;
            this.reportRow.Dock = System.Windows.Forms.DockStyle.Top;
            this.reportRow.AutoSize = true;
            this.reportRow.Padding = new System.Windows.Forms.Padding(8);
            this.reportRow.WrapContents = true;
            this.date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.reportButton.Text = "Xem doanh thu / Top 5";
            this.reportButton.AutoSize = true;
            this.reportButton.Height = 34;
            this.reportButton.Padding = new System.Windows.Forms.Padding(7, 2, 7, 2);
            this.reportButton.UseVisualStyleBackColor = true;
            this.revenue.Text = "Doanh thu";
            this.revenue.AutoSize = true;
            this.revenue.Margin = new System.Windows.Forms.Padding(5, 8, 5, 0);
            this.billPage.Text = "Lịch sử hóa đơn";
            this.billPage.Padding = new System.Windows.Forms.Padding(8);
            this.billPage.UseVisualStyleBackColor = true;
            this.bills.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bills.ReadOnly = true;
            this.bills.AllowUserToAddRows = false;
            this.bills.AllowUserToDeleteRows = false;
            this.bills.RowHeadersVisible = false;
            this.bills.MultiSelect = false;
            this.bills.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.bills.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.bills.BackgroundColor = System.Drawing.Color.White;
            this.Controls.AddRange(new System.Windows.Forms.Control[] { this.Pages });
            this.Pages.Controls.AddRange(new System.Windows.Forms.Control[] { this.overviewPage, this.productPage, this.customerPage, this.employeePage, this.salePage, this.reportPage, this.billPage });
            this.overviewPage.Controls.AddRange(new System.Windows.Forms.Control[] { this.stock, this.searchRow });
            this.searchRow.Controls.AddRange(new System.Windows.Forms.Control[] { this.searchLabel, this.search, this.searchButton, this.reloadButton });
            this.salePage.Controls.AddRange(new System.Windows.Forms.Control[] { this.grid, this.actionRow, this.addRow, this.peopleRow });
            this.actionRow.Controls.AddRange(new System.Windows.Forms.Control[] { this.removeButton, this.payButton, this.total });
            this.addRow.Controls.AddRange(new System.Windows.Forms.Control[] { this.codeLabel, this.code, this.qtyLabel, this.qty, this.addButton });
            this.peopleRow.Controls.AddRange(new System.Windows.Forms.Control[] { this.employeeLabel, this.employee, this.customerLabel, this.customer });
            this.reportPage.Controls.AddRange(new System.Windows.Forms.Control[] { this.top, this.reportRow });
            this.reportRow.Controls.AddRange(new System.Windows.Forms.Control[] { this.date, this.reportButton, this.revenue });
            this.billPage.Controls.AddRange(new System.Windows.Forms.Control[] { this.bills });
            this.searchButton.Click += new System.EventHandler(this.SearchButton_Click);
            this.reloadButton.Click += new System.EventHandler(this.ReloadButton_Click);
            this.removeButton.Click += new System.EventHandler(this.RemoveButton_Click);
            this.payButton.Click += new System.EventHandler(this.PayButton_Click);
            this.addButton.Click += new System.EventHandler(this.AddButton_Click);
            this.code.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Code_KeyDown);
            this.customer.SelectedIndexChanged += new System.EventHandler(this.Customer_SelectedIndexChanged);
            this.reportButton.Click += new System.EventHandler(this.ReportButton_Click);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.Pages.SelectedIndexChanged += new System.EventHandler(this.Pages_SelectedIndexChanged);
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1164, 741);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bài 6.1 - Bán hàng siêu thị mini | Nguyễn Hải Đăng - 24810320310";
            ((System.ComponentModel.ISupportInitialize)(this.bills)).EndInit();
            this.billPage.ResumeLayout(false);
            this.billPage.PerformLayout();
            this.reportRow.ResumeLayout(false);
            this.reportRow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.top)).EndInit();
            this.reportPage.ResumeLayout(false);
            this.reportPage.PerformLayout();
            this.peopleRow.ResumeLayout(false);
            this.peopleRow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qty)).EndInit();
            this.addRow.ResumeLayout(false);
            this.addRow.PerformLayout();
            this.actionRow.ResumeLayout(false);
            this.actionRow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.salePage.ResumeLayout(false);
            this.salePage.PerformLayout();
            this.employeePage.ResumeLayout(false);
            this.employeePage.PerformLayout();
            this.customerPage.ResumeLayout(false);
            this.customerPage.PerformLayout();
            this.productPage.ResumeLayout(false);
            this.productPage.PerformLayout();
            this.searchRow.ResumeLayout(false);
            this.searchRow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stock)).EndInit();
            this.overviewPage.ResumeLayout(false);
            this.overviewPage.PerformLayout();
            this.Pages.ResumeLayout(false);
            this.Pages.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        public System.Windows.Forms.TabControl Pages;
        private System.Windows.Forms.TabPage overviewPage;
        private System.Windows.Forms.DataGridView stock;
        private System.Windows.Forms.FlowLayoutPanel searchRow;
        private System.Windows.Forms.Label searchLabel;
        private System.Windows.Forms.TextBox search;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Button reloadButton;
        private System.Windows.Forms.TabPage productPage;
        private System.Windows.Forms.TabPage customerPage;
        private System.Windows.Forms.TabPage employeePage;
        private System.Windows.Forms.TabPage salePage;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.FlowLayoutPanel actionRow;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button payButton;
        private System.Windows.Forms.Label total;
        private System.Windows.Forms.FlowLayoutPanel addRow;
        private System.Windows.Forms.Label codeLabel;
        private System.Windows.Forms.TextBox code;
        private System.Windows.Forms.Label qtyLabel;
        private System.Windows.Forms.NumericUpDown qty;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.FlowLayoutPanel peopleRow;
        private System.Windows.Forms.Label employeeLabel;
        private System.Windows.Forms.ComboBox employee;
        private System.Windows.Forms.Label customerLabel;
        private System.Windows.Forms.ComboBox customer;
        private System.Windows.Forms.TabPage reportPage;
        private System.Windows.Forms.DataGridView top;
        private System.Windows.Forms.FlowLayoutPanel reportRow;
        private System.Windows.Forms.DateTimePicker date;
        private System.Windows.Forms.Button reportButton;
        private System.Windows.Forms.Label revenue;
        private System.Windows.Forms.TabPage billPage;
        private System.Windows.Forms.DataGridView bills;
    }
}
