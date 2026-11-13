namespace Library.GUI
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
   this.loansPage = new System.Windows.Forms.TabPage();
   this.loans = new System.Windows.Forms.DataGridView();
   this.filters = new System.Windows.Forms.FlowLayoutPanel();
   this.readerFilterLabel = new System.Windows.Forms.Label();
   this.readerFilter = new System.Windows.Forms.ComboBox();
   this.stateFilterLabel = new System.Windows.Forms.Label();
   this.stateFilter = new System.Windows.Forms.ComboBox();
   this.filterButton = new System.Windows.Forms.Button();
   this.returnButton = new System.Windows.Forms.Button();
   this.refreshButton = new System.Windows.Forms.Button();
   this.booksPage = new System.Windows.Forms.TabPage();
   this.readersPage = new System.Windows.Forms.TabPage();
   this.borrowPage = new System.Windows.Forms.TabPage();
   this.cartGrid = new System.Windows.Forms.DataGridView();
   this.borrowActions = new System.Windows.Forms.FlowLayoutPanel();
   this.borrowButton = new System.Windows.Forms.Button();
   this.removeButton = new System.Windows.Forms.Button();
   this.bookRow = new System.Windows.Forms.FlowLayoutPanel();
   this.bookLabel = new System.Windows.Forms.Label();
   this.book = new System.Windows.Forms.ComboBox();
   this.quantityLabel = new System.Windows.Forms.Label();
   this.quantity = new System.Windows.Forms.NumericUpDown();
   this.addButton = new System.Windows.Forms.Button();
   this.readerRow = new System.Windows.Forms.FlowLayoutPanel();
   this.readerLabel = new System.Windows.Forms.Label();
   this.reader = new System.Windows.Forms.ComboBox();
   this.dueLabel = new System.Windows.Forms.Label();
   this.due = new System.Windows.Forms.DateTimePicker();
   this.overduePage = new System.Windows.Forms.TabPage();
   this.overdue = new System.Windows.Forms.DataGridView();
   this.reportPage = new System.Windows.Forms.TabPage();
   this.statistics = new System.Windows.Forms.DataGridView();
   this.Pages.SuspendLayout();
   this.loansPage.SuspendLayout();
   ((System.ComponentModel.ISupportInitialize)this.loans).BeginInit();
   this.filters.SuspendLayout();
   this.booksPage.SuspendLayout();
   this.readersPage.SuspendLayout();
   this.borrowPage.SuspendLayout();
   ((System.ComponentModel.ISupportInitialize)this.cartGrid).BeginInit();
   this.borrowActions.SuspendLayout();
   this.bookRow.SuspendLayout();
   ((System.ComponentModel.ISupportInitialize)this.quantity).BeginInit();
   this.readerRow.SuspendLayout();
   this.overduePage.SuspendLayout();
   ((System.ComponentModel.ISupportInitialize)this.overdue).BeginInit();
   this.reportPage.SuspendLayout();
   ((System.ComponentModel.ISupportInitialize)this.statistics).BeginInit();
   this.SuspendLayout();
   // Pages
   this.Pages.Name = "Pages";
   // loansPage
   this.loansPage.Name = "loansPage";
   // loans
   this.loans.Name = "loans";
   // filters
   this.filters.Name = "filters";
   // readerFilterLabel
   this.readerFilterLabel.Name = "readerFilterLabel";
   // readerFilter
   this.readerFilter.Name = "readerFilter";
   // stateFilterLabel
   this.stateFilterLabel.Name = "stateFilterLabel";
   // stateFilter
   this.stateFilter.Name = "stateFilter";
   // filterButton
   this.filterButton.Name = "filterButton";
   // returnButton
   this.returnButton.Name = "returnButton";
   // refreshButton
   this.refreshButton.Name = "refreshButton";
   // booksPage
   this.booksPage.Name = "booksPage";
   // readersPage
   this.readersPage.Name = "readersPage";
   // borrowPage
   this.borrowPage.Name = "borrowPage";
   // cartGrid
   this.cartGrid.Name = "cartGrid";
   // borrowActions
   this.borrowActions.Name = "borrowActions";
   // borrowButton
   this.borrowButton.Name = "borrowButton";
   // removeButton
   this.removeButton.Name = "removeButton";
   // bookRow
   this.bookRow.Name = "bookRow";
   // bookLabel
   this.bookLabel.Name = "bookLabel";
   // book
   this.book.Name = "book";
   // quantityLabel
   this.quantityLabel.Name = "quantityLabel";
   // quantity
   this.quantity.Name = "quantity";
   // addButton
   this.addButton.Name = "addButton";
   // readerRow
   this.readerRow.Name = "readerRow";
   // readerLabel
   this.readerLabel.Name = "readerLabel";
   // reader
   this.reader.Name = "reader";
   // dueLabel
   this.dueLabel.Name = "dueLabel";
   // due
   this.due.Name = "due";
   // overduePage
   this.overduePage.Name = "overduePage";
   // overdue
   this.overdue.Name = "overdue";
   // reportPage
   this.reportPage.Name = "reportPage";
   // statistics
   this.statistics.Name = "statistics";
            this.Pages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loansPage.Text = "Phiếu mượn";
            this.loansPage.Padding = new System.Windows.Forms.Padding(8);
            this.loans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loans.ReadOnly = true;
            this.loans.AllowUserToAddRows = false;
            this.loans.AllowUserToDeleteRows = false;
            this.loans.RowHeadersVisible = false;
            this.loans.MultiSelect = false;
            this.loans.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.loans.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.loans.BackgroundColor = System.Drawing.Color.White;
            this.filters.Dock = System.Windows.Forms.DockStyle.Top;
            this.filters.AutoSize = true;
            this.filters.WrapContents = true;
            this.filters.Padding = new System.Windows.Forms.Padding(8);
            this.readerFilterLabel.Text = "Độc giả";
            this.readerFilterLabel.AutoSize = true;
            this.readerFilterLabel.Margin = new System.Windows.Forms.Padding(5, 8, 5, 0);
            this.readerFilter.Width = 160;
            this.readerFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stateFilterLabel.Text = "Trạng thái";
            this.stateFilterLabel.AutoSize = true;
            this.stateFilterLabel.Margin = new System.Windows.Forms.Padding(5, 8, 5, 0);
            this.stateFilter.Width = 160;
            this.stateFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterButton.Text = "Lọc phiếu";
            this.filterButton.AutoSize = true;
            this.filterButton.Padding = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.returnButton.Text = "Trả sách";
            this.returnButton.AutoSize = true;
            this.returnButton.Padding = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.refreshButton.Text = "Tải lại";
            this.refreshButton.AutoSize = true;
            this.refreshButton.Padding = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.booksPage.Text = "Quản lý đầu sách";
            this.booksPage.Padding = new System.Windows.Forms.Padding(8);
            this.readersPage.Text = "Quản lý độc giả";
            this.readersPage.Padding = new System.Windows.Forms.Padding(8);
            this.borrowPage.Text = "Lập phiếu mượn";
            this.borrowPage.Padding = new System.Windows.Forms.Padding(8);
            this.cartGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cartGrid.ReadOnly = true;
            this.cartGrid.AllowUserToAddRows = false;
            this.cartGrid.AllowUserToDeleteRows = false;
            this.cartGrid.RowHeadersVisible = false;
            this.cartGrid.MultiSelect = false;
            this.cartGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.cartGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.cartGrid.BackgroundColor = System.Drawing.Color.White;
            this.borrowActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.borrowActions.AutoSize = true;
            this.borrowActions.WrapContents = true;
            this.borrowActions.Padding = new System.Windows.Forms.Padding(8);
            this.borrowButton.Text = "Lưu phiếu mượn";
            this.borrowButton.AutoSize = true;
            this.borrowButton.Padding = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.removeButton.Text = "Xóa dòng";
            this.removeButton.AutoSize = true;
            this.removeButton.Padding = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.bookRow.Dock = System.Windows.Forms.DockStyle.Top;
            this.bookRow.AutoSize = true;
            this.bookRow.WrapContents = true;
            this.bookRow.Padding = new System.Windows.Forms.Padding(8);
            this.bookLabel.Text = "Đầu sách";
            this.bookLabel.AutoSize = true;
            this.bookLabel.Margin = new System.Windows.Forms.Padding(5, 8, 5, 0);
            this.book.Width = 160;
            this.book.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.quantityLabel.Text = "Số lượng";
            this.quantityLabel.AutoSize = true;
            this.quantityLabel.Margin = new System.Windows.Forms.Padding(5, 8, 5, 0);
            this.quantity.Width = 110;
            this.quantity.Maximum = 1000000000m;
            this.addButton.Text = "Thêm sách";
            this.addButton.AutoSize = true;
            this.addButton.Padding = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.readerRow.Dock = System.Windows.Forms.DockStyle.Top;
            this.readerRow.AutoSize = true;
            this.readerRow.WrapContents = true;
            this.readerRow.Padding = new System.Windows.Forms.Padding(8);
            this.readerLabel.Text = "Độc giả";
            this.readerLabel.AutoSize = true;
            this.readerLabel.Margin = new System.Windows.Forms.Padding(5, 8, 5, 0);
            this.reader.Width = 160;
            this.reader.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dueLabel.Text = "Hạn trả";
            this.dueLabel.AutoSize = true;
            this.dueLabel.Margin = new System.Windows.Forms.Padding(5, 8, 5, 0);
            this.due.Width = 180;
            this.due.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.due.CustomFormat = "dd/MM/yyyy HH:mm";
            this.overduePage.Text = "Cảnh báo quá hạn";
            this.overduePage.Padding = new System.Windows.Forms.Padding(8);
            this.overdue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.overdue.ReadOnly = true;
            this.overdue.AllowUserToAddRows = false;
            this.overdue.AllowUserToDeleteRows = false;
            this.overdue.RowHeadersVisible = false;
            this.overdue.MultiSelect = false;
            this.overdue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.overdue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.overdue.BackgroundColor = System.Drawing.Color.White;
            this.reportPage.Text = "Thống kê theo thể loại";
            this.reportPage.Padding = new System.Windows.Forms.Padding(8);
            this.statistics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statistics.ReadOnly = true;
            this.statistics.AllowUserToAddRows = false;
            this.statistics.AllowUserToDeleteRows = false;
            this.statistics.RowHeadersVisible = false;
            this.statistics.MultiSelect = false;
            this.statistics.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.statistics.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.statistics.BackgroundColor = System.Drawing.Color.White;
   this.Controls.AddRange(new System.Windows.Forms.Control[] { this.Pages });
   this.Pages.Controls.AddRange(new System.Windows.Forms.Control[] { this.loansPage, this.booksPage, this.readersPage, this.borrowPage, this.overduePage, this.reportPage });
   this.loansPage.Controls.AddRange(new System.Windows.Forms.Control[] { this.loans, this.filters });
   this.filters.Controls.AddRange(new System.Windows.Forms.Control[] { this.readerFilterLabel, this.readerFilter, this.stateFilterLabel, this.stateFilter, this.filterButton, this.returnButton, this.refreshButton });
   this.borrowPage.Controls.AddRange(new System.Windows.Forms.Control[] { this.cartGrid, this.borrowActions, this.bookRow, this.readerRow });
   this.borrowActions.Controls.AddRange(new System.Windows.Forms.Control[] { this.borrowButton, this.removeButton });
   this.bookRow.Controls.AddRange(new System.Windows.Forms.Control[] { this.bookLabel, this.book, this.quantityLabel, this.quantity, this.addButton });
   this.readerRow.Controls.AddRange(new System.Windows.Forms.Control[] { this.readerLabel, this.reader, this.dueLabel, this.due });
   this.overduePage.Controls.AddRange(new System.Windows.Forms.Control[] { this.overdue });
   this.reportPage.Controls.AddRange(new System.Windows.Forms.Control[] { this.statistics });
   this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
   this.AutoScaleDimensions = new System.Drawing.SizeF(7F,17F);
   this.Font = new System.Drawing.Font("Segoe UI",10F);
   this.ClientSize = new System.Drawing.Size(1164,741);
   this.Name = "MainForm";
   this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
   this.Text = "Bài 6.6 - Thư viện | Nguyễn Hải Đăng - 24810320310";
   ((System.ComponentModel.ISupportInitialize)this.statistics).EndInit();
   this.reportPage.ResumeLayout(false);
   this.reportPage.PerformLayout();
   ((System.ComponentModel.ISupportInitialize)this.overdue).EndInit();
   this.overduePage.ResumeLayout(false);
   this.overduePage.PerformLayout();
   this.readerRow.ResumeLayout(false);
   this.readerRow.PerformLayout();
   ((System.ComponentModel.ISupportInitialize)this.quantity).EndInit();
   this.bookRow.ResumeLayout(false);
   this.bookRow.PerformLayout();
   this.borrowActions.ResumeLayout(false);
   this.borrowActions.PerformLayout();
   ((System.ComponentModel.ISupportInitialize)this.cartGrid).EndInit();
   this.borrowPage.ResumeLayout(false);
   this.borrowPage.PerformLayout();
   this.readersPage.ResumeLayout(false);
   this.readersPage.PerformLayout();
   this.booksPage.ResumeLayout(false);
   this.booksPage.PerformLayout();
   this.filters.ResumeLayout(false);
   this.filters.PerformLayout();
   ((System.ComponentModel.ISupportInitialize)this.loans).EndInit();
   this.loansPage.ResumeLayout(false);
   this.loansPage.PerformLayout();
   this.Pages.ResumeLayout(false);
   this.Pages.PerformLayout();
   this.ResumeLayout(false);
   this.PerformLayout();
  }
  #endregion
  public System.Windows.Forms.TabControl Pages;
  private System.Windows.Forms.TabPage loansPage;
  private System.Windows.Forms.DataGridView loans;
  private System.Windows.Forms.FlowLayoutPanel filters;
  private System.Windows.Forms.Label readerFilterLabel;
  private System.Windows.Forms.ComboBox readerFilter;
  private System.Windows.Forms.Label stateFilterLabel;
  private System.Windows.Forms.ComboBox stateFilter;
  private System.Windows.Forms.Button filterButton;
  private System.Windows.Forms.Button returnButton;
  private System.Windows.Forms.Button refreshButton;
  private System.Windows.Forms.TabPage booksPage;
  private System.Windows.Forms.TabPage readersPage;
  private System.Windows.Forms.TabPage borrowPage;
  private System.Windows.Forms.DataGridView cartGrid;
  private System.Windows.Forms.FlowLayoutPanel borrowActions;
  private System.Windows.Forms.Button borrowButton;
  private System.Windows.Forms.Button removeButton;
  private System.Windows.Forms.FlowLayoutPanel bookRow;
  private System.Windows.Forms.Label bookLabel;
  private System.Windows.Forms.ComboBox book;
  private System.Windows.Forms.Label quantityLabel;
  private System.Windows.Forms.NumericUpDown quantity;
  private System.Windows.Forms.Button addButton;
  private System.Windows.Forms.FlowLayoutPanel readerRow;
  private System.Windows.Forms.Label readerLabel;
  private System.Windows.Forms.ComboBox reader;
  private System.Windows.Forms.Label dueLabel;
  private System.Windows.Forms.DateTimePicker due;
  private System.Windows.Forms.TabPage overduePage;
  private System.Windows.Forms.DataGridView overdue;
  private System.Windows.Forms.TabPage reportPage;
  private System.Windows.Forms.DataGridView statistics;
 }
}
