using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp.Models;
using WinFormsApp.Services;

namespace WinFormsApp.Forms
{
    public sealed class frmBookManager : Form
    {
        internal readonly BindingList<BookModel> Books = new BindingList<BookModel>();
        private readonly BindingSource source = new BindingSource();
        private readonly TextBox txtBookId = Ui.Text("txtBookId"), txtTitle = Ui.Text("txtTitle"), txtAuthor = Ui.Text("txtAuthor"), txtSearch = Ui.Text("txtSearch");
        private readonly NumericUpDown nudPrice = new NumericUpDown { Name = "nudPrice", Dock = DockStyle.Fill, Maximum = 1000000000, Minimum = 1, ThousandsSeparator = true, Value = 100000 };
        private readonly NumericUpDown nudQuantity = new NumericUpDown { Name = "nudQuantity", Dock = DockStyle.Fill, Maximum = 1000000, Minimum = 0, Value = 1 };
        private readonly PictureBox picBookCover = new PictureBox { Name = "picBookCover", Dock = DockStyle.Fill, SizeMode = PictureBoxSizeMode.Zoom, BorderStyle = BorderStyle.FixedSingle };
        private readonly DataGridView dgvBooks = Ui.Grid("dgvBooks");
        private readonly ToolStripStatusLabel totals = new ToolStripStatusLabel(), clock = new ToolStripStatusLabel();
        private readonly Label status = new Label { Name = "lblStatus", Dock = DockStyle.Fill };
        private readonly Timer timer = new Timer { Interval = 1000 };
        private readonly ErrorProvider errors;
        private string coverPath = "";
        private BookModel selected;

        public frmBookManager()
        {
            Ui.Setup(this, "Bài 2 - Quản lý sách và báo cáo CSV");
            errors = new ErrorProvider { ContainerControl = this, BlinkStyle = ErrorBlinkStyle.NeverBlink };
            var root = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4 };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            foreach (float h in new[] { 28F, 32F, -1F, 28F }) root.RowStyles.Add(new RowStyle(h < 0 ? SizeType.Percent : SizeType.Absolute, h < 0 ? 100 : h));
            Controls.Add(root);
            var menu = new MenuStrip(); var file = new ToolStripMenuItem("File"); var edit = new ToolStripMenuItem("Edit");
            file.DropDownItems.Add("Mở file CSV", null, (s, e) => OpenCsv()); file.DropDownItems.Add("Lưu file CSV", null, (s, e) => SaveCsv()); file.DropDownItems.Add("Thoát", null, (s, e) => Close());
            edit.DropDownItems.Add("Thêm", null, (s, e) => ResetEditor()); edit.DropDownItems.Add("Xóa", null, (s, e) => DeleteBook()); edit.DropDownItems.Add("Tìm kiếm", null, (s, e) => txtSearch.Focus());
            menu.Items.AddRange(new ToolStripItem[] { file, edit }); MainMenuStrip = menu; root.Controls.Add(menu, 0, 0);
            var toolbar = new ToolStrip { GripStyle = ToolStripGripStyle.Hidden };
            toolbar.Items.Add(new ToolStripButton("Mở file", SystemIcons.Application.ToBitmap(), (s, e) => OpenCsv()));
            toolbar.Items.Add(new ToolStripButton("Lưu file", SystemIcons.Shield.ToBitmap(), (s, e) => SaveCsv()));
            toolbar.Items.Add(new ToolStripButton("Thêm mới", SystemIcons.Information.ToBitmap(), (s, e) => ResetEditor())); root.Controls.Add(toolbar, 0, 1);
            var content = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(12), ColumnCount = 1, RowCount = 5 };
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            foreach (float h in new[] { 42F, 160F, 46F, 30F, -1F }) content.RowStyles.Add(new RowStyle(h < 0 ? SizeType.Percent : SizeType.Absolute, h < 0 ? 100 : h));
            root.Controls.Add(content, 0, 2); content.Controls.Add(Ui.Heading("QUẢN LÝ SÁCH"), 0, 0);
            var details = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1 };
            details.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100)); details.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 145));
            details.Controls.Add(Ui.Fields("Mã sách", txtBookId, "Tên sách", txtTitle, "Tác giả", txtAuthor, "Đơn giá", nudPrice, "Số lượng", nudQuantity, "Tìm mã/tên/tác giả", txtSearch), 0, 0);
            details.Controls.Add(picBookCover, 1, 0); content.Controls.Add(details, 0, 1);
            content.Controls.Add(Ui.Buttons(Ui.Button("btnAdd", "Thêm sách", (s, e) => SaveBook(false)), Ui.Button("btnEdit", "Sửa sách", (s, e) => SaveBook(true)),
                Ui.Button("btnDelete", "Xóa sách", (s, e) => DeleteBook()), Ui.Button("btnReset", "Làm mới", (s, e) => ResetEditor()), Ui.Button("btnCover", "Tải ảnh bìa", (s, e) => ChooseCover())), 0, 2);
            content.Controls.Add(status, 0, 3); content.Controls.Add(dgvBooks, 0, 4);
            foreach (var pair in new[] { new[] { "BookId", "Mã sách" }, new[] { "Title", "Tên sách" }, new[] { "Author", "Tác giả" }, new[] { "Quantity", "Số lượng" }, new[] { "CoverFile", "Ảnh bìa" } }) Ui.Column(dgvBooks, pair[0], pair[1]);
            Ui.Column(dgvBooks, "UnitPrice", "Đơn giá", "N0"); Ui.Column(dgvBooks, "TotalValue", "Thành tiền", "N0");
            dgvBooks.Columns["Title"].FillWeight = 170;
            var strip = new StatusStrip(); strip.Items.Add(totals); strip.Items.Add(new ToolStripStatusLabel { Spring = true }); strip.Items.Add(clock); root.Controls.Add(strip, 0, 3);
            dgvBooks.DataSource = source; source.CurrentChanged += (s, e) => FillEditor(); dgvBooks.CellClick += (s, e) => { if (e.RowIndex >= 0) FillEditor(); };
            txtSearch.TextChanged += (s, e) => RefreshView(); Books.ListChanged += (s, e) => UpdateTotals();
            timer.Tick += (s, e) => clock.Text = DateTime.Now.ToString("HH:mm:ss"); timer.Start(); clock.Text = DateTime.Now.ToString("HH:mm:ss");
            string sample = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "books.csv");
            try { if (File.Exists(sample)) ImportCsv(sample); else RefreshView(); } catch (Exception ex) { status.Text = "Không đọc được dữ liệu mẫu: " + ex.Message; }
            Shown += (s, e) => { if (Books.Count > 0) FillEditor(); else ResetEditor(); };
        }
        private void UpdateTotals() => totals.Text = "Đầu sách: " + Books.Count + " | Số cuốn: " + Books.Sum(b => (long)b.Quantity) + " | Giá trị kho: " + Books.Sum(b => b.TotalValue).ToString("N0") + " đ";
        internal void RefreshView()
        {
            string q = txtSearch.Text.Trim();
            source.DataSource = new BindingList<BookModel>(Books.Where(b => (b.BookId + " " + b.Title + " " + b.Author).IndexOf(q, StringComparison.CurrentCultureIgnoreCase) >= 0).ToList());
            UpdateTotals(); if (source.Count == 0) ResetEditor();
        }
        private void FillEditor()
        {
            selected = source.Current as BookModel; if (selected == null) return;
            errors.Clear(); txtBookId.Text = selected.BookId; txtTitle.Text = selected.Title; txtAuthor.Text = selected.Author;
            nudPrice.Value = selected.UnitPrice; nudQuantity.Value = selected.Quantity; coverPath = selected.CoverPath; LoadCover();
        }
        internal void ResetEditor()
        {
            selected = null; errors.Clear(); dgvBooks.ClearSelection(); txtBookId.Clear(); txtTitle.Clear(); txtAuthor.Clear(); nudPrice.Value = 100000; nudQuantity.Value = 1;
            coverPath = ""; LoadCover(); status.Text = "Nhập thông tin sách mới hoặc chọn một dòng để sửa.";
        }
        internal bool SaveBook(bool edit)
        {
            errors.Clear(); bool ok = true;
            if (edit && selected == null) { status.Text = "Chọn sách cần sửa."; return false; }
            foreach (TextBox box in new[] { txtBookId, txtTitle, txtAuthor }) if (string.IsNullOrWhiteSpace(box.Text)) { errors.SetError(box, "Không được để trống."); ok = false; }
            if (Books.Any(b => (!edit || b != selected) && string.Equals(b.BookId, txtBookId.Text.Trim(), StringComparison.OrdinalIgnoreCase))) { errors.SetError(txtBookId, "Mã sách đã tồn tại."); ok = false; }
            if (!ok) { status.Text = "Vui lòng sửa dữ liệu được đánh dấu."; return false; }
            var book = edit ? selected : new BookModel(); book.BookId = txtBookId.Text.Trim(); book.Title = txtTitle.Text.Trim(); book.Author = txtAuthor.Text.Trim();
            book.UnitPrice = nudPrice.Value; book.Quantity = (int)nudQuantity.Value; book.CoverPath = coverPath;
            if (!edit) Books.Add(book); else Books.ResetItem(Books.IndexOf(book));
            RefreshView(); source.Position = ((BindingList<BookModel>)source.DataSource).IndexOf(book); FillEditor(); status.Text = edit ? "Đã sửa sách." : "Đã thêm sách."; return true;
        }
        private void DeleteBook()
        {
            if (selected == null) { status.Text = "Chọn sách cần xóa."; return; }
            if (MessageBox.Show(this, "Xóa sách " + selected.Title + "?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            Books.Remove(selected); RefreshView(); status.Text = "Đã xóa sách.";
        }
        private void LoadCover()
        {
            var old = picBookCover.Image; picBookCover.Image = null; old?.Dispose();
            if (string.IsNullOrEmpty(coverPath)) return;
            try { using (var image = Image.FromFile(coverPath)) picBookCover.Image = new Bitmap(image); }
            catch (Exception ex) when (ex is IOException || ex is ArgumentException || ex is OutOfMemoryException || ex is UnauthorizedAccessException)
            { status.Text = "Không mở được ảnh bìa; dữ liệu sách vẫn được giữ."; }
        }
        private void ChooseCover()
        {
            using (var dialog = new OpenFileDialog { Title = "Chọn ảnh bìa", Filter = "Ảnh PNG/JPG|*.png;*.jpg;*.jpeg" })
                if (dialog.ShowDialog(this) == DialogResult.OK) { coverPath = dialog.FileName; LoadCover(); }
        }
        internal void ImportCsv(string path)
        {
            var loaded = BookCsvService.Read(path); // Đọc hết và kiểm tra trước khi thay danh sách hiện tại.
            Books.RaiseListChangedEvents = false;
            try { Books.Clear(); foreach (var b in loaded) Books.Add(b); }
            finally { Books.RaiseListChangedEvents = true; Books.ResetBindings(); }
            txtSearch.Clear(); RefreshView(); status.Text = "Đã nhập " + Books.Count + " đầu sách từ " + Path.GetFileName(path) + ".";
        }
        private void OpenCsv()
        {
            using (var dialog = new OpenFileDialog { Filter = "CSV (*.csv)|*.csv", Title = "Mở danh sách sách" })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                if (Books.Count > 0 && MessageBox.Show(this, "Thay danh sách hiện tại bằng dữ liệu trong CSV? Hãy lưu CSV trước nếu cần giữ dữ liệu.", "Mở CSV", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
                try { ImportCsv(dialog.FileName); } catch (Exception ex) { Ui.Error(this, "Không thể nhập CSV: " + ex.Message); }
            }
        }
        private void SaveCsv()
        {
            using (var dialog = new SaveFileDialog { Filter = "CSV (*.csv)|*.csv", DefaultExt = "csv", AddExtension = true, FileName = "books.csv" })
                if (dialog.ShowDialog(this) == DialogResult.OK)
                    try { BookCsvService.Write(dialog.FileName, Books); status.Text = "Đã xuất toàn bộ " + Books.Count + " đầu sách."; }
                    catch (Exception ex) { Ui.Error(this, "Không thể lưu CSV: " + ex.Message); }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing) { timer.Dispose(); errors.Dispose(); source.Dispose(); picBookCover.Image?.Dispose(); }
            base.Dispose(disposing);
        }
    }
}
