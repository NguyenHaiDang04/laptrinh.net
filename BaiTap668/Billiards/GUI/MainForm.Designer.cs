namespace Billiards.GUI
{
 partial class MainForm
 {
  private System.ComponentModel.IContainer components = null;
  protected override void Dispose(bool disposing) { if(disposing && components != null) components.Dispose(); base.Dispose(disposing); }
  #region Windows Form Designer generated code
  private void InitializeComponent()
  {
   this.components = new System.ComponentModel.Container();
   this.Pages = new System.Windows.Forms.TabControl();
   this.tablesPage = new System.Windows.Forms.TabPage();
   this.services = new System.Windows.Forms.DataGridView();
   this.tableButtons = new System.Windows.Forms.FlowLayoutPanel();
   this.actions = new System.Windows.Forms.FlowLayoutPanel();
   this.openButton = new System.Windows.Forms.Button();
   this.saveButton = new System.Windows.Forms.Button();
   this.removeButton = new System.Windows.Forms.Button();
   this.checkoutButton = new System.Windows.Forms.Button();
   this.refreshButton = new System.Windows.Forms.Button();
   this.summary = new System.Windows.Forms.Label();
   this.serviceRow = new System.Windows.Forms.FlowLayoutPanel();
   this.serviceLabel = new System.Windows.Forms.Label();
   this.service = new System.Windows.Forms.ComboBox();
   this.quantityLabel = new System.Windows.Forms.Label();
   this.quantity = new System.Windows.Forms.NumericUpDown();
   this.addButton = new System.Windows.Forms.Button();
   this.menuPage = new System.Windows.Forms.TabPage();
   this.reportPage = new System.Windows.Forms.TabPage();
   this.report = new System.Windows.Forms.DataGridView();
   this.Pages.SuspendLayout();
   this.tablesPage.SuspendLayout();
   ((System.ComponentModel.ISupportInitialize)this.services).BeginInit();
   this.tableButtons.SuspendLayout();
   this.actions.SuspendLayout();
   this.serviceRow.SuspendLayout();
   ((System.ComponentModel.ISupportInitialize)this.quantity).BeginInit();
   this.menuPage.SuspendLayout();
   this.reportPage.SuspendLayout();
   ((System.ComponentModel.ISupportInitialize)this.report).BeginInit();
   this.SuspendLayout();
   // Pages
   this.Pages.Name = "Pages";
   // tablesPage
   this.tablesPage.Name = "tablesPage";
   // services
   this.services.Name = "services";
   // tableButtons
   this.tableButtons.Name = "tableButtons";
   // actions
   this.actions.Name = "actions";
   // openButton
   this.openButton.Name = "openButton";
   // saveButton
   this.saveButton.Name = "saveButton";
   // removeButton
   this.removeButton.Name = "removeButton";
   // checkoutButton
   this.checkoutButton.Name = "checkoutButton";
   // refreshButton
   this.refreshButton.Name = "refreshButton";
   // summary
   this.summary.Name = "summary";
   // serviceRow
   this.serviceRow.Name = "serviceRow";
   // serviceLabel
   this.serviceLabel.Name = "serviceLabel";
   // service
   this.service.Name = "service";
   // quantityLabel
   this.quantityLabel.Name = "quantityLabel";
   // quantity
   this.quantity.Name = "quantity";
   // addButton
   this.addButton.Name = "addButton";
   // menuPage
   this.menuPage.Name = "menuPage";
   // reportPage
   this.reportPage.Name = "reportPage";
   // report
   this.report.Name = "report";
            this.Pages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablesPage.Text = "Sơ đồ bàn / Dịch vụ";
            this.tablesPage.Padding = new System.Windows.Forms.Padding(8);
            this.services.Dock = System.Windows.Forms.DockStyle.Fill;
            this.services.ReadOnly = true;
            this.services.AllowUserToAddRows = false;
            this.services.AllowUserToDeleteRows = false;
            this.services.RowHeadersVisible = false;
            this.services.MultiSelect = false;
            this.services.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.services.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.services.BackgroundColor = System.Drawing.Color.White;
            this.tableButtons.Dock = System.Windows.Forms.DockStyle.Left;
            this.tableButtons.Width = 300;
            this.tableButtons.AutoScroll = true;
            this.tableButtons.Padding = new System.Windows.Forms.Padding(8);
            this.actions.Dock = System.Windows.Forms.DockStyle.Top;
            this.actions.AutoSize = true;
            this.actions.WrapContents = true;
            this.actions.Padding = new System.Windows.Forms.Padding(8);
            this.openButton.Text = "Mở bàn";
            this.openButton.AutoSize = true;
            this.openButton.Padding = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.saveButton.Text = "Lưu dịch vụ";
            this.saveButton.AutoSize = true;
            this.saveButton.Padding = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.removeButton.Text = "Xóa dòng";
            this.removeButton.AutoSize = true;
            this.removeButton.Padding = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.checkoutButton.Text = "Thanh toán";
            this.checkoutButton.AutoSize = true;
            this.checkoutButton.Padding = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.refreshButton.Text = "Tải lại";
            this.refreshButton.AutoSize = true;
            this.refreshButton.Padding = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.summary.Text = "Chọn bàn";
            this.summary.AutoSize = true;
            this.summary.Margin = new System.Windows.Forms.Padding(5, 8, 5, 0);
            this.serviceRow.Dock = System.Windows.Forms.DockStyle.Top;
            this.serviceRow.AutoSize = true;
            this.serviceRow.WrapContents = true;
            this.serviceRow.Padding = new System.Windows.Forms.Padding(8);
            this.serviceLabel.Text = "Dịch vụ";
            this.serviceLabel.AutoSize = true;
            this.serviceLabel.Margin = new System.Windows.Forms.Padding(5, 8, 5, 0);
            this.service.Width = 160;
            this.service.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.quantityLabel.Text = "Số lượng";
            this.quantityLabel.AutoSize = true;
            this.quantityLabel.Margin = new System.Windows.Forms.Padding(5, 8, 5, 0);
            this.quantity.Width = 110;
            this.quantity.Maximum = 1000000000m;
            this.addButton.Text = "Thêm / Tăng món";
            this.addButton.AutoSize = true;
            this.addButton.Padding = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.menuPage.Text = "Quản lý dịch vụ";
            this.menuPage.Padding = new System.Windows.Forms.Padding(8);
            this.reportPage.Text = "Báo cáo hóa đơn";
            this.reportPage.Padding = new System.Windows.Forms.Padding(8);
            this.report.Dock = System.Windows.Forms.DockStyle.Fill;
            this.report.ReadOnly = true;
            this.report.AllowUserToAddRows = false;
            this.report.AllowUserToDeleteRows = false;
            this.report.RowHeadersVisible = false;
            this.report.MultiSelect = false;
            this.report.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.report.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.report.BackgroundColor = System.Drawing.Color.White;
   this.Controls.AddRange(new System.Windows.Forms.Control[] { this.Pages });
   this.Pages.Controls.AddRange(new System.Windows.Forms.Control[] { this.tablesPage, this.menuPage, this.reportPage });
   this.tablesPage.Controls.AddRange(new System.Windows.Forms.Control[] { this.services, this.tableButtons, this.actions, this.serviceRow });
   this.actions.Controls.AddRange(new System.Windows.Forms.Control[] { this.openButton, this.saveButton, this.removeButton, this.checkoutButton, this.refreshButton, this.summary });
   this.serviceRow.Controls.AddRange(new System.Windows.Forms.Control[] { this.serviceLabel, this.service, this.quantityLabel, this.quantity, this.addButton });
   this.reportPage.Controls.AddRange(new System.Windows.Forms.Control[] { this.report });
   this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
   this.AutoScaleDimensions = new System.Drawing.SizeF(7F,17F);
   this.Font = new System.Drawing.Font("Segoe UI",10F);
   this.ClientSize = new System.Drawing.Size(1164,741);
   this.Name = "MainForm";
   this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
   this.Text = "Bài 6.8 - Bàn bida | Nguyễn Hải Đăng - 24810320310";
   ((System.ComponentModel.ISupportInitialize)this.report).EndInit();
   this.reportPage.ResumeLayout(false);
   this.reportPage.PerformLayout();
   this.menuPage.ResumeLayout(false);
   this.menuPage.PerformLayout();
   ((System.ComponentModel.ISupportInitialize)this.quantity).EndInit();
   this.serviceRow.ResumeLayout(false);
   this.serviceRow.PerformLayout();
   this.actions.ResumeLayout(false);
   this.actions.PerformLayout();
   this.tableButtons.ResumeLayout(false);
   this.tableButtons.PerformLayout();
   ((System.ComponentModel.ISupportInitialize)this.services).EndInit();
   this.tablesPage.ResumeLayout(false);
   this.tablesPage.PerformLayout();
   this.Pages.ResumeLayout(false);
   this.Pages.PerformLayout();
   this.ResumeLayout(false);
   this.PerformLayout();
  }
  #endregion
  public System.Windows.Forms.TabControl Pages;
  private System.Windows.Forms.TabPage tablesPage;
  private System.Windows.Forms.DataGridView services;
  private System.Windows.Forms.FlowLayoutPanel tableButtons;
  private System.Windows.Forms.FlowLayoutPanel actions;
  private System.Windows.Forms.Button openButton;
  private System.Windows.Forms.Button saveButton;
  private System.Windows.Forms.Button removeButton;
  private System.Windows.Forms.Button checkoutButton;
  private System.Windows.Forms.Button refreshButton;
  private System.Windows.Forms.Label summary;
  private System.Windows.Forms.FlowLayoutPanel serviceRow;
  private System.Windows.Forms.Label serviceLabel;
  private System.Windows.Forms.ComboBox service;
  private System.Windows.Forms.Label quantityLabel;
  private System.Windows.Forms.NumericUpDown quantity;
  private System.Windows.Forms.Button addButton;
  private System.Windows.Forms.TabPage menuPage;
  private System.Windows.Forms.TabPage reportPage;
  private System.Windows.Forms.DataGridView report;
 }
}
