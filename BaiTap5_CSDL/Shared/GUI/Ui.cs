using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BaiTap5.Shared.DTO;

namespace BaiTap5.Shared.GUI
{
    public static class Ui
    {
        public static void Style(Form form, string title)
        {
            form.Text = title + " | Nguyễn Hải Đăng - 24810320310";
            form.Font = new Font("Segoe UI", 10);
            form.Size = new Size(1180, 780); form.StartPosition = FormStartPosition.CenterScreen;
            form.BackColor = Color.FromArgb(245, 247, 250);
        }
        public static FlowLayoutPanel Row(Control parent)
        {
            var row = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, Padding = new Padding(8), WrapContents = true };
            // Dock bố trí theo thứ tự Z ngược: các hàng Top phải đứng sau Fill.
            parent.Controls.Add(row); return row;
        }
        public static Button Button(Control parent, string text, Action action)
        {
            var button = new Button { Text = text, AutoSize = true, Height = 34, Padding = new Padding(7, 2, 7, 2) };
            button.Click += (s, e) => Guard(action); parent.Controls.Add(button); return button;
        }
        public static void Guard(Action action)
        {
            try { action(); }
            catch (Exception ex) { using (var error = new ErrorDialog(ex.Message)) error.ShowDialog(); }
        }
        public static void Label(Control row, string text) { row.Controls.Add(new Label { Text = text, AutoSize = true, Margin = new Padding(5, 8, 5, 0) }); }
        public static TextBox Text(Control row, string label, int width = 150)
        { Label(row, label); var t = new TextBox { Width = width }; row.Controls.Add(t); return t; }
        public static NumericUpDown Number(Control row, string label, decimal minimum = 0, int decimals = 0)
        { Label(row, label); var n = new NumericUpDown { Width = 120, Minimum = minimum, Maximum = 1000000000m, DecimalPlaces = decimals, ThousandsSeparator = true, Value = minimum }; row.Controls.Add(n); return n; }
        public static ComboBox Combo(Control row, string label)
        { Label(row, label); var c = new ComboBox { Width = 170, DropDownStyle = ComboBoxStyle.DropDownList }; row.Controls.Add(c); return c; }
        public static void Bind(ComboBox combo, DataTable dt, string value, string display)
        { combo.DataSource = null; combo.DisplayMember = display; combo.ValueMember = value; combo.DataSource = dt; }
        public static int Id(ComboBox combo)
        { if (combo.SelectedValue == null) throw new ArgumentException("Chưa chọn dữ liệu."); return Convert.ToInt32(combo.SelectedValue); }
        public static DataGridView Grid(Control parent)
        {
            var grid = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White, RowHeadersVisible = false, AutoGenerateColumns = true };
            parent.Controls.Add(grid); return grid;
        }
        public static TabPage Tab(TabControl tabs, string title)
        { var page = new TabPage(title) { Padding = new Padding(8) }; tabs.TabPages.Add(page); return page; }
        public static TabControl Tabs(Control parent)
        { var tabs = new TabControl { Dock = DockStyle.Fill }; parent.Controls.Add(tabs); return tabs; }
        public static bool Confirm(string message)
        { return MessageBox.Show(message, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes; }
    }
    public sealed class ErrorDialog : Form
    {
        public ErrorDialog(string message)
        {
            Text = "Không thể thực hiện"; Size = new Size(650, 230);
            Font = new Font("Segoe UI", 11); StartPosition = FormStartPosition.CenterParent;
            Controls.Add(new Label { Text = message, Dock = DockStyle.Fill, Padding = new Padding(20), ForeColor = Color.DarkRed });
            var close = new Button { Text = "Đóng", Dock = DockStyle.Bottom, Height = 40, DialogResult = DialogResult.OK };
            Controls.Add(close); AcceptButton = close; CancelButton = close;
        }
    }
    public sealed class CatalogPanel : UserControl
    {
        private readonly ICatalogService bus;
        private readonly DataGridView grid;
        private readonly TextBox search;
        private readonly Control[] inputs;
        private object selectedKey;
        public CatalogPanel(ICatalogService service)
        {
            bus = service; Dock = DockStyle.Fill;
            grid = Ui.Grid(this);
            var actions = Ui.Row(this);
            search = Ui.Text(actions, "Tra cứu", 180);
            Ui.Button(actions, "Tìm / Tải lại", Reload);
            Ui.Button(actions, "Tạo mới", Clear);
            Ui.Button(actions, "Lưu", Save);
            Ui.Button(actions, "Xóa", () => { if (Ui.Confirm("Xóa bản ghi đã chọn?")) { bus.Delete(selectedKey); Clear(); Reload(); } });
            var edit = Ui.Row(this);
            inputs = new Control[bus.Definition.Fields.Length];
            for (int i = 0; i < inputs.Length; i++)
            {
                var f = bus.Definition.Fields[i];
                if (f.Type == typeof(string)) inputs[i] = Ui.Text(edit, f.Label, 145);
                else inputs[i] = Ui.Number(edit, f.Label, 0, f.Type == typeof(decimal) ? 2 : 0);
            }
            grid.SelectionChanged += (s, e) =>
            {
                var row = grid.CurrentRow == null ? null : grid.CurrentRow.DataBoundItem as DataRowView;
                if (row == null) return;
                selectedKey = row[bus.Definition.Key];
                for (int i = 0; i < inputs.Length; i++)
                {
                    object val = row[bus.Definition.Fields[i].Name];
                    var number = inputs[i] as NumericUpDown;
                    if (number != null) number.Value = val == DBNull.Value ? 0 : Convert.ToDecimal(val);
                    else inputs[i].Text = Convert.ToString(val);
                    inputs[i].Enabled = bus.Definition.Identity || bus.Definition.Fields[i].Name != bus.Definition.Key;
                }
            };
        }
        public void Reload() { grid.DataSource = bus.Search(search.Text); }
        private void Clear()
        {
            grid.ClearSelection(); selectedKey = null;
            foreach (var input in inputs)
            { input.Enabled = true; if (input is NumericUpDown) ((NumericUpDown)input).Value = 0; else input.Text = ""; }
        }
        private void Save()
        {
            var values = new object[inputs.Length];
            for (int i = 0; i < inputs.Length; i++)
            {
                var n = inputs[i] as NumericUpDown;
                values[i] = n == null ? (object)inputs[i].Text : bus.Definition.Fields[i].Type == typeof(int) ? (object)(int)n.Value : n.Value;
            }
            bus.Save(selectedKey, values); Reload();
        }
    }
}
